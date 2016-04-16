using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Security;

namespace nc
{
    public class UnderChat : Hub
    {
        private string conn
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["conString"].ConnectionString;
            }
        }

        private int userID
        {
            get
            {
                System.Security.Principal.IIdentity u = Context.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
            }
        }

        private string un;

        private string userName
        {
            get
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("get_logged_Name_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@uid", userID);

                SqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr.Read())
                {
                    un = dr["firstName"].ToString() + "_" + dr["lastName"].ToString();
                }
                return un;
            }
        }

        private string _u;

        private string uName
        {
            get
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand ucmd = new SqlCommand("get_logged_Name_sp", con);
                ucmd.CommandType = CommandType.StoredProcedure;
                ucmd.Parameters.AddWithValue("@uid", userID);
                SqlDataReader udr = ucmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (udr.Read())
                {
                    _u = udr["UName"].ToString();
                }
                return _u;
            }
        }
        // list of users on the current users friend list. The current users identifier is retrieved from the authentication cookie on sign in.
        private DataTable friends(int uIdentifier)
        {
            DataTable dt = new DataTable();

            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("get_FriendList_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@userID", uIdentifier);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            con.Close();
            da.Fill(dt);
            return dt;
        }
        // Initiate connection mapping to users who connect to the hub.
        private readonly static mapConnection<string> _connections = new mapConnection<string>();

        public void send(string Name, string msg)
        {
            string name = Context.User.Identity.Name;
            foreach (var connectionId in _connections.GetConnections(Name.ToLower()))
            {
                Clients.Client(connectionId).send(name, msg);
                storeMsg sm = new storeMsg(name, Name, msg);
                sm.insertRecord();
            }
        }

        public override Task OnConnected()
        {
            string name = Context.User.Identity.Name;
            _connections.Add(name.ToLower(), Context.ConnectionId);

            // Broadcast the connected user to all friends who are online.
            foreach (DataRow dr in friends(userID).Rows)
            {
                // friends are matched to current connections on the current users contact list
                foreach (var connectionId in _connections.GetConnections(dr["uName"].ToString().ToLower()))
                {
                    string on = "true";
                    Clients.Client(connectionId).connected(on, uName);
                }
            }
            // this iteration represents incoming connection confirmation.
            foreach (DataRow drCon in friends(userID).Rows)
            {
                foreach (var connection in _connections.GetConnections(drCon["uName"].ToString().ToLower()))
                {
                    if (connection != null)
                    {
                        string online = "true";
                        Clients.Client(Context.ConnectionId).friendConnected(online, drCon["uName"].ToString());
                    }
                }
            }
            return base.OnConnected();
        }
        // pretty straight forward disconnection signal letting other users the current user has disconnected.
        public override Task OnDisconnected(bool stopCalled)
        {
            string name = Context.User.Identity.Name;
            _connections.Remove(name, Context.ConnectionId);

            foreach (DataRow drDiscon in friends(userID).Rows)
            {
                foreach (var connectionId in _connections.GetConnections(drDiscon["uName"].ToString().ToLower()))
                {
                    string on = "false";
                    Clients.Client(connectionId).disconnected(on, uName);
                }
            }
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string name = Context.User.Identity.Name;
            if (!_connections.GetConnections(name.ToLower()).Contains(Context.ConnectionId))
            {
                _connections.Add(name.ToLower(), Context.ConnectionId);
            }
            return base.OnReconnected();
        }
    }

    public class mapConnection<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections = new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }

        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }
                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }
        // Enumerator that lists connected users
        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }
            return Enumerable.Empty<string>();
        }

        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }
                lock (connections)
                {
                    connections.Remove(connectionId);
                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }

    public class storeMsg
    {
        public storeMsg(string sender, string reciever, string msg)
        {
            _sender = sender;
            _reciever = reciever;
            _msg = msg;
        }

        public string _sender { get; set; }
        public string _reciever { get; set; }
        public string _msg { get; set; }

        private string conn
        {
            get { return ConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        public void insertRecord()
        {
            SqlConnection con = new SqlConnection(conn);
            con.Open();
            SqlCommand cmd = new SqlCommand("record_ChatMsg_sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@sender", _sender);
            cmd.Parameters.AddWithValue("@reciever", _reciever);
            cmd.Parameters.AddWithValue("@msg", _msg);

            cmd.ExecuteNonQuery();
            con.Close();
        }
    }
}