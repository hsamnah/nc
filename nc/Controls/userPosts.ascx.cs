using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace nc.Controls
{
    public partial class userPosts : System.Web.UI.UserControl
    {
        private string conn
        {
            get { return WebConfigurationManager.ConnectionStrings["conString"].ConnectionString; }
        }

        public int uId { get; set; }
        public string loc { get; set; }
        public string direction { get; set; }
        public int ctrlHeight { get; set; }

        private int UI1
        {
            get
            {
                System.Security.Principal.IIdentity u = Page.User.Identity;
                FormsIdentity userIdentifier = (FormsIdentity)u;
                FormsAuthenticationTicket ticket = userIdentifier.Ticket;
                return Convert.ToInt32(ticket.UserData);
            }
        }

        private SqlConnection con
        {
            get { return new SqlConnection(conn); }
        }

        private int intResult = new int();

        private DataTable primaryPost(int id)
        {
            DataTable _pp = new DataTable();
            con.Open();
            SqlCommand ppCmd = new SqlCommand("get_userPost_sp", con);
            ppCmd.CommandType = CommandType.StoredProcedure;
            ppCmd.Parameters.AddWithValue("@uid", id);
            ppCmd.Parameters.AddWithValue("@Type", direction);
            SqlDataAdapter ppDA = new SqlDataAdapter(ppCmd);
            con.Close();
            ppDA.Fill(_pp);
            return _pp;
        }

        private DataTable childPost(int postID)
        {
            DataTable _cp = new DataTable();
            con.Open();
            SqlCommand cpCmd = new SqlCommand("get_userPost_Com_sp", con);
            cpCmd.CommandType = CommandType.StoredProcedure;
            cpCmd.Parameters.AddWithValue("@parentID", postID);
            SqlDataAdapter cpDA = new SqlDataAdapter(cpCmd);
            con.Close();
            cpDA.Fill(_cp);
            return _cp;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            ClientScriptManager csm = Page.ClientScript;
            if (!csm.IsStartupScriptRegistered("postScript"))
            {
                csm.RegisterClientScriptInclude("postScript", "/scripts/jsPost.js");
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (loc == "profile")
            {
                DataRowCollection drc = primaryPost(UI1).Rows;
                foreach (DataRow drPost in drc)
                {
                    HtmlGenericControl pc = new HtmlGenericControl("DIV");
                    pc.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                    pc.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                    pc.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                    pc.Style.Add(HtmlTextWriterStyle.BorderWidth, "2px");
                    pc.Style.Add(HtmlTextWriterStyle.Padding, "2px 2px 2px 2px");
                    HiddenField pcHF = new HiddenField();
                    pcHF.Value = drPost["postId"].ToString();
                    pc.Controls.Add(pcHF);

                    HtmlTable postTbl = new HtmlTable();
                    postTbl.Style.Add(HtmlTextWriterStyle.Width, "100%");
                    HtmlTableRow ptr = new HtmlTableRow();
                    HtmlTableCell pCell0 = new HtmlTableCell();
                    pCell0.Style.Add(HtmlTextWriterStyle.Width, "60px");
                    pCell0.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");

                    Literal lit = new Literal();
                    lit.Text = drPost["UserName"].ToString() + "<br />";
                    pCell0.Controls.Add(lit);
                    HtmlImage avatar = new HtmlImage();
                    avatar.Src = "/" + drPost["Avatar"].ToString();
                    avatar.Style.Add(HtmlTextWriterStyle.Width, "50px");
                    pCell0.Controls.Add(avatar);
                    ptr.Controls.Add(pCell0);
                    HtmlTableCell pCell1 = new HtmlTableCell();
                    pCell1.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
                    Literal lit1 = new Literal();
                    lit1.Text = drPost["Post"].ToString();
                    pCell1.Controls.Add(lit1);

                    // don't forget the date of the post.
                    HtmlGenericControl br = new HtmlGenericControl("BR");
                    pCell1.Controls.Add(br);
                    HtmlGenericControl dtContainer = new HtmlGenericControl("DIV");
                    dtContainer.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                    Literal dt = new Literal();
                    dt.Text = Convert.ToDateTime(drPost["Post_Date"].ToString()).ToShortDateString();
                    dtContainer.Controls.Add(dt);
                    pCell1.Controls.Add(dtContainer);

                    ptr.Controls.Add(pCell1);
                    postTbl.Controls.Add(ptr);

                    HtmlTableRow ptr2 = new HtmlTableRow();
                    HtmlTableCell cmntCell = new HtmlTableCell();
                    cmntCell.ColSpan = 2;

                    HtmlGenericControl tbCon = new HtmlGenericControl("DIV");
                    tbCon.Attributes.Add("class", "tbCon_cls");
                    tbCon.Style.Add(HtmlTextWriterStyle.BackgroundColor, "white");
                    tbCon.Style.Add(HtmlTextWriterStyle.Width, "100%");
                    tbCon.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                    tbCon.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                    tbCon.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                    tbCon.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                    tbCon.Style.Add(HtmlTextWriterStyle.Height, "35px");

                    HtmlInputHidden hidMessage = new HtmlInputHidden();
                    hidMessage.Attributes.Add("class", "tbhd_cls");
                    hidMessage.ID += ClientID + "_" + drPost["postId"].ToString();
                    hidMessage.ClientIDMode = ClientIDMode.Static;
                    tbCon.Controls.Add(hidMessage);

                    HtmlGenericControl tb = new HtmlGenericControl("DIV");
                    tb.Attributes.Add("class", "tb_cls");
                    tb.Attributes.Add("contenteditable", "true");
                    tb.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
                    tb.Style.Add("word-wrap", "break-word");
                    tb.Style.Add(HtmlTextWriterStyle.Height, "35px");
                    tb.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
                    tb.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");
                    tb.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                    tb.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                    tb.Style.Add(HtmlTextWriterStyle.Position, "static");
                    tb.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                    tbCon.Controls.Add(tb);

                    ImageButton commentBtn = new ImageButton();
                    commentBtn.Style.Add(HtmlTextWriterStyle.Width, "40px");
                    commentBtn.Style.Add(HtmlTextWriterStyle.Height, "35px");
                    commentBtn.Attributes.Add("class", "postBtn_cls");
                    commentBtn.ImageUrl = "/imgs/post.png";
                    commentBtn.Command += CommentBtn_Command;
                    commentBtn.CommandName = "postComment";

                    string elements = hidMessage.ClientID + ":" + drPost["postId"].ToString();
                    commentBtn.CommandArgument = elements;
                    tbCon.Controls.Add(commentBtn);

                    cmntCell.Controls.Add(tbCon);

                    ptr2.Controls.Add(cmntCell);
                    postTbl.Controls.Add(ptr2);
                    pc.Controls.Add(postTbl);
                    PostContainer.Controls.Add(pc);
                    if (Convert.ToInt32(drPost["children"].ToString()) > 0)
                    {
                        intResult = Convert.ToInt32(drPost["postId"].ToString());
                        comments(pc, intResult);
                    }
                }
                drc.Clear();
                intResult = -1;
            }
            else if (loc == "share")
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("get_FriendList_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userID", UI1);
                SqlDataReader da = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (da.Read())
                {
                    DataRowCollection drc = primaryPost(Convert.ToInt32(da["FID"].ToString())).Rows;
                    foreach (DataRow drPost in drc)
                    {
                        HtmlGenericControl pc = new HtmlGenericControl("DIV");
                        pc.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                        pc.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                        pc.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                        pc.Style.Add(HtmlTextWriterStyle.BorderWidth, "2px");
                        pc.Style.Add(HtmlTextWriterStyle.Padding, "2px 2px 2px 2px");
                        HiddenField pcHF = new HiddenField();
                        pcHF.Value = drPost["postId"].ToString();
                        pc.Controls.Add(pcHF);

                        HtmlTable postTbl = new HtmlTable();
                        postTbl.Style.Add(HtmlTextWriterStyle.Width, "100%");
                        HtmlTableRow ptr = new HtmlTableRow();
                        HtmlTableCell pCell0 = new HtmlTableCell();
                        pCell0.Style.Add(HtmlTextWriterStyle.Width, "60px");
                        pCell0.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");

                        Literal lit = new Literal();
                        lit.Text = drPost["UserName"].ToString() + "<br />";
                        pCell0.Controls.Add(lit);
                        HtmlImage avatar = new HtmlImage();
                        avatar.Src = "/" + drPost["Avatar"].ToString();
                        avatar.Style.Add(HtmlTextWriterStyle.Width, "50px");
                        pCell0.Controls.Add(avatar);
                        ptr.Controls.Add(pCell0);
                        HtmlTableCell pCell1 = new HtmlTableCell();
                        pCell1.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
                        Literal lit1 = new Literal();
                        lit1.Text = drPost["Post"].ToString();
                        pCell1.Controls.Add(lit1);

                        // don't forget the date of the post.

                        HtmlGenericControl br = new HtmlGenericControl("BR");
                        pCell1.Controls.Add(br);
                        HtmlGenericControl dtContainer = new HtmlGenericControl("DIV");
                        dtContainer.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                        Literal dt = new Literal();
                        dt.Text = Convert.ToDateTime(drPost["Post_Date"].ToString()).ToShortDateString();
                        dtContainer.Controls.Add(dt);
                        pCell1.Controls.Add(dtContainer);

                        ptr.Controls.Add(pCell1);
                        postTbl.Controls.Add(ptr);

                        HtmlTableRow ptr2 = new HtmlTableRow();
                        HtmlTableCell cmntCell = new HtmlTableCell();
                        cmntCell.ColSpan = 2;

                        HtmlGenericControl tbCon = new HtmlGenericControl("DIV");
                        tbCon.Attributes.Add("class", "tbCon_cls");
                        tbCon.Style.Add(HtmlTextWriterStyle.BackgroundColor, "white");
                        tbCon.Style.Add(HtmlTextWriterStyle.Width, "100%");
                        tbCon.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                        tbCon.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                        tbCon.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                        tbCon.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                        tbCon.Style.Add(HtmlTextWriterStyle.Height, "35px");

                        HtmlInputHidden hidMessage = new HtmlInputHidden();
                        hidMessage.Attributes.Add("class", "tbhd_cls");
                        hidMessage.ID += ClientID + "_" + drPost["postId"].ToString();
                        hidMessage.ClientIDMode = ClientIDMode.Static;
                        tbCon.Controls.Add(hidMessage);

                        HtmlGenericControl tb = new HtmlGenericControl("DIV");
                        tb.Attributes.Add("class", "tb_cls");
                        tb.Attributes.Add("contenteditable", "true");
                        tb.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
                        tb.Style.Add("word-wrap", "break-word");
                        tb.Style.Add(HtmlTextWriterStyle.Height, "35px");
                        tb.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
                        tb.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");
                        tb.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                        tb.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                        tb.Style.Add(HtmlTextWriterStyle.Position, "static");
                        tb.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                        tbCon.Controls.Add(tb);

                        ImageButton commentBtn = new ImageButton();
                        commentBtn.Style.Add(HtmlTextWriterStyle.Width, "40px");
                        commentBtn.Style.Add(HtmlTextWriterStyle.Height, "35px");
                        commentBtn.Attributes.Add("class", "postBtn_cls");
                        commentBtn.ImageUrl = "/imgs/post.png";
                        commentBtn.Command += CommentBtn_Command;
                        commentBtn.CommandName = "postComment";

                        string elements = hidMessage.ClientID + ":" + drPost["postId"].ToString();
                        commentBtn.CommandArgument = elements;
                        tbCon.Controls.Add(commentBtn);

                        cmntCell.Controls.Add(tbCon);

                        ptr2.Controls.Add(cmntCell);
                        postTbl.Controls.Add(ptr2);
                        pc.Controls.Add(postTbl);
                        PostContainer.Controls.Add(pc);
                        if (Convert.ToInt32(drPost["children"].ToString()) > 0)
                        {
                            intResult = Convert.ToInt32(drPost["postId"].ToString());
                            comments(pc, intResult);
                        }
                    }
                    drc.Clear();
                    intResult = -1;
                }
                da.Close();
            }
            PostContainer.Style.Add(HtmlTextWriterStyle.Height, ctrlHeight.ToString() + "px");
        }

        private void comments(HtmlGenericControl parent, int parentID)
        {
            DataRowCollection cDRC = childPost(parentID).Rows;

            foreach (DataRow cp1 in cDRC)
            {
                parent.Controls.Add(helperContainer(cp1));
            }
            cDRC.Clear();
            intResult = -1;
        }

        private HtmlGenericControl helperContainer(DataRow cp)
        {
            HtmlGenericControl cpp = new HtmlGenericControl("DIV");
            cpp.Style.Add(HtmlTextWriterStyle.BackgroundColor, "rgb(157,184,207)");
            cpp.Style.Add(HtmlTextWriterStyle.Color, "rgb(36,50,62)");
            cpp.Style.Add(HtmlTextWriterStyle.PaddingTop, "5px");
            cpp.Style.Add(HtmlTextWriterStyle.PaddingBottom, "5px");
            cpp.Style.Add(HtmlTextWriterStyle.PaddingRight, "1px");
            HiddenField cpphf = new HiddenField();
            cpphf.Value = cp["postId"].ToString();
            cpp.Controls.Add(cpphf);

            HtmlTable comTbl = new HtmlTable();
            comTbl.Style.Add(HtmlTextWriterStyle.Width, "100%");
            HtmlTableRow comTR = new HtmlTableRow();
            HtmlTableCell comTD0 = new HtmlTableCell();
            comTD0.Style.Add(HtmlTextWriterStyle.Width, "60px");
            comTD0.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            Literal lit0 = new Literal();
            lit0.Text = cp["UserName"].ToString() + "<br />";
            comTD0.Controls.Add(lit0);
            HtmlImage Av = new HtmlImage();
            Av.Style.Add(HtmlTextWriterStyle.Width, "50px");
            Av.Src = "/" + cp["Avatar"].ToString();
            comTD0.Controls.Add(Av);
            comTR.Controls.Add(comTD0);
            HtmlTableCell comTD1 = new HtmlTableCell();
            comTD1.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
            Literal lit1 = new Literal();
            lit1.Text = cp["Post"].ToString();
            comTD1.Controls.Add(lit1);

            // date container.
            HtmlGenericControl br = new HtmlGenericControl("BR");
            comTD1.Controls.Add(br);
            HtmlGenericControl dtContainer = new HtmlGenericControl("DIV");
            dtContainer.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            Literal dt = new Literal();
            dt.Text = Convert.ToDateTime(cp["Post_Date"].ToString()).ToShortDateString();
            dtContainer.Controls.Add(dt);
            comTD1.Controls.Add(dtContainer);

            comTR.Controls.Add(comTD1);
            comTbl.Controls.Add(comTR);
            cpp.Controls.Add(comTbl);

            HtmlGenericControl tbCon = new HtmlGenericControl("DIV");
            tbCon.Attributes.Add("class", "tbCon_cls");
            tbCon.Style.Add(HtmlTextWriterStyle.BackgroundColor, "white");
            tbCon.Style.Add(HtmlTextWriterStyle.Width, "100%");
            tbCon.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
            tbCon.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
            tbCon.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
            tbCon.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
            tbCon.Style.Add(HtmlTextWriterStyle.Height, "25px");

            HtmlInputHidden hidMessage = new HtmlInputHidden();
            hidMessage.Attributes.Add("class", "tbhd_cls");
            hidMessage.ID += ClientID + "_" + cp["postId"].ToString();
            hidMessage.ClientIDMode = ClientIDMode.Static;
            tbCon.Controls.Add(hidMessage);

            HtmlGenericControl tb = new HtmlGenericControl("DIV");
            tb.Attributes.Add("class", "tb_cls");
            tb.Attributes.Add("contenteditable", "true");
            tb.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
            tb.Style.Add("word-wrap", "break-word");
            tb.Style.Add(HtmlTextWriterStyle.Height, "25px");
            tb.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
            tb.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");
            tb.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
            tb.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
            tb.Style.Add(HtmlTextWriterStyle.Position, "static");
            tb.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
            tbCon.Controls.Add(tb);

            ImageButton commentBtn = new ImageButton();
            commentBtn.Style.Add(HtmlTextWriterStyle.Width, "30px");
            commentBtn.Style.Add(HtmlTextWriterStyle.Height, "25px");
            commentBtn.Attributes.Add("class", "postBtn_cls");
            commentBtn.ImageUrl = "/imgs/post.png";
            commentBtn.Command += CommentBtn_Command;
            commentBtn.CommandName = "postComment";
            // Add a split string to the command arguement with both the identifiers for the hiddenfield and the identfier for the original comment. cp["postId"].ToString()
            string elements = hidMessage.ClientID + ":" + cp["postId"].ToString();
            commentBtn.CommandArgument = elements;
            tbCon.Controls.Add(commentBtn);

            cpp.Controls.Add(tbCon);

            if (Convert.ToInt32(cp["children"].ToString()) > 0)
            {
                intResult = Convert.ToInt32(cp["postId"].ToString());
                comments(cpp, intResult);
            }
            return cpp;
        }

        private void CommentBtn_Command(object sender, CommandEventArgs e)
        {
            if (e.CommandName == "postComment")
            {
                char[] delimiter = { ':' };
                string[] splitIDs = e.CommandArgument.ToString().Split(delimiter);
                string hfValue = splitIDs[0];
                string originalComID = splitIDs[1];
                HtmlInputHidden hf = (HtmlInputHidden)this.FindControl(hfValue);

                SqlConnection con1 = new SqlConnection(conn);
                con1.Open();
                SqlCommand addCPcmd = new SqlCommand("add_postComment_sp", con1);
                addCPcmd.CommandType = CommandType.StoredProcedure;

                addCPcmd.Parameters.AddWithValue("@uid", UI1);
                addCPcmd.Parameters.AddWithValue("@parentID", Convert.ToInt32(originalComID));
                addCPcmd.Parameters.AddWithValue("@post", hf.Value);

                addCPcmd.ExecuteNonQuery();
                con1.Close();

                reset();
            }
        }

        public void reset()
        {
            PostContainer.Controls.Clear();
            if (loc == "profile")
            {
                DataRowCollection drc = primaryPost(UI1).Rows;
                foreach (DataRow drPost in drc)
                {
                    HtmlGenericControl pc = new HtmlGenericControl("DIV");
                    pc.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                    pc.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                    pc.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                    pc.Style.Add(HtmlTextWriterStyle.BorderWidth, "2px");
                    pc.Style.Add(HtmlTextWriterStyle.Padding, "2px 2px 2px 2px");
                    HiddenField pcHF = new HiddenField();
                    pcHF.Value = drPost["postId"].ToString();
                    pc.Controls.Add(pcHF);

                    HtmlTable postTbl = new HtmlTable();
                    postTbl.Style.Add(HtmlTextWriterStyle.Width, "100%");
                    HtmlTableRow ptr = new HtmlTableRow();
                    HtmlTableCell pCell0 = new HtmlTableCell();
                    pCell0.Style.Add(HtmlTextWriterStyle.Width, "60px");
                    pCell0.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");

                    Literal lit = new Literal();
                    lit.Text = drPost["UserName"].ToString() + "<br />";
                    pCell0.Controls.Add(lit);
                    HtmlImage avatar = new HtmlImage();
                    avatar.Src = "/" + drPost["Avatar"].ToString();
                    avatar.Style.Add(HtmlTextWriterStyle.Width, "50px");
                    pCell0.Controls.Add(avatar);
                    ptr.Controls.Add(pCell0);
                    HtmlTableCell pCell1 = new HtmlTableCell();
                    pCell1.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
                    Literal lit1 = new Literal();
                    lit1.Text = drPost["Post"].ToString();
                    // don't forget the date of the post.

                    HtmlGenericControl br = new HtmlGenericControl("BR");
                    pCell1.Controls.Add(br);
                    HtmlGenericControl dtContainer = new HtmlGenericControl("DIV");
                    dtContainer.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                    Literal dt = new Literal();
                    dt.Text = Convert.ToDateTime(drPost["Post_Date"].ToString()).ToShortDateString();
                    dtContainer.Controls.Add(dt);
                    pCell1.Controls.Add(dtContainer);

                    pCell1.Controls.Add(lit1);
                    ptr.Controls.Add(pCell1);
                    postTbl.Controls.Add(ptr);

                    HtmlTableRow ptr2 = new HtmlTableRow();
                    HtmlTableCell cmntCell = new HtmlTableCell();
                    cmntCell.ColSpan = 2;

                    HtmlGenericControl tbCon = new HtmlGenericControl("DIV");
                    tbCon.Attributes.Add("class", "tbCon_cls");
                    tbCon.Style.Add(HtmlTextWriterStyle.BackgroundColor, "white");
                    tbCon.Style.Add(HtmlTextWriterStyle.Width, "100%");
                    tbCon.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                    tbCon.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                    tbCon.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                    tbCon.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                    tbCon.Style.Add(HtmlTextWriterStyle.Height, "35px");

                    HtmlInputHidden hidMessage = new HtmlInputHidden();
                    hidMessage.Attributes.Add("class", "tbhd_cls");
                    hidMessage.ID += ClientID + "_" + drPost["postId"].ToString();
                    hidMessage.ClientIDMode = ClientIDMode.Static;
                    tbCon.Controls.Add(hidMessage);

                    HtmlGenericControl tb = new HtmlGenericControl("DIV");
                    tb.Attributes.Add("class", "tb_cls");
                    tb.Attributes.Add("contenteditable", "true");
                    tb.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
                    tb.Style.Add("word-wrap", "break-word");
                    tb.Style.Add(HtmlTextWriterStyle.Height, "35px");
                    tb.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
                    tb.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");
                    tb.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                    tb.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                    tb.Style.Add(HtmlTextWriterStyle.Position, "static");
                    tb.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                    tbCon.Controls.Add(tb);

                    ImageButton commentBtn = new ImageButton();
                    commentBtn.Style.Add(HtmlTextWriterStyle.Width, "40px");
                    commentBtn.Style.Add(HtmlTextWriterStyle.Height, "35px");
                    commentBtn.Attributes.Add("class", "postBtn_cls");
                    commentBtn.ImageUrl = "/imgs/post.png";
                    commentBtn.Command += CommentBtn_Command;
                    commentBtn.CommandName = "postComment";
                    // Add a split string to the command arguement with both the identifiers for the hiddenfield and the identfier for the original comment. cp["postId"].ToString()
                    string elements = hidMessage.ClientID + ":" + drPost["postId"].ToString();
                    commentBtn.CommandArgument = elements;
                    tbCon.Controls.Add(commentBtn);

                    cmntCell.Controls.Add(tbCon);

                    ptr2.Controls.Add(cmntCell);
                    postTbl.Controls.Add(ptr2);
                    pc.Controls.Add(postTbl);
                    PostContainer.Controls.Add(pc);
                    if (Convert.ToInt32(drPost["children"].ToString()) > 0)
                    {
                        intResult = Convert.ToInt32(drPost["postId"].ToString());
                        comments(pc, intResult);
                    }
                }
                drc.Clear();
                intResult = -1;
            }
            else if (loc == "share")
            {
                SqlConnection con = new SqlConnection(conn);
                con.Open();
                SqlCommand cmd = new SqlCommand("get_FriendList_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("userID", UI1);
                SqlDataReader da = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                while (da.Read())
                {
                    DataRowCollection drc = primaryPost(Convert.ToInt32(da["FID"].ToString())).Rows;
                    foreach (DataRow drPost in drc)
                    {
                        HtmlGenericControl pc = new HtmlGenericControl("DIV");
                        pc.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                        pc.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                        pc.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                        pc.Style.Add(HtmlTextWriterStyle.BorderWidth, "2px");
                        pc.Style.Add(HtmlTextWriterStyle.Padding, "2px 2px 2px 2px");
                        HiddenField pcHF = new HiddenField();
                        pcHF.Value = drPost["postId"].ToString();
                        pc.Controls.Add(pcHF);

                        HtmlTable postTbl = new HtmlTable();
                        postTbl.Style.Add(HtmlTextWriterStyle.Width, "100%");
                        HtmlTableRow ptr = new HtmlTableRow();
                        HtmlTableCell pCell0 = new HtmlTableCell();
                        pCell0.Style.Add(HtmlTextWriterStyle.Width, "60px");
                        pCell0.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");

                        Literal lit = new Literal();
                        lit.Text = drPost["UserName"].ToString() + "<br />";
                        pCell0.Controls.Add(lit);
                        HtmlImage avatar = new HtmlImage();
                        avatar.Src = "/" + drPost["Avatar"].ToString();
                        avatar.Style.Add(HtmlTextWriterStyle.Width, "50px");
                        pCell0.Controls.Add(avatar);
                        ptr.Controls.Add(pCell0);
                        HtmlTableCell pCell1 = new HtmlTableCell();
                        pCell1.Style.Add(HtmlTextWriterStyle.VerticalAlign, "top");
                        Literal lit1 = new Literal();
                        lit1.Text = drPost["Post"].ToString();
                        pCell1.Controls.Add(lit1);

                        // This should already appear.
                        HtmlGenericControl br = new HtmlGenericControl("BR");
                        pCell1.Controls.Add(br);
                        HtmlGenericControl dtContainer = new HtmlGenericControl("DIV");
                        dtContainer.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                        Literal dt = new Literal();
                        dt.Text = Convert.ToDateTime(drPost["Post_Date"].ToString()).ToShortDateString();
                        dtContainer.Controls.Add(dt);
                        pCell1.Controls.Add(dtContainer);

                        // don't forget the date of the post.
                        ptr.Controls.Add(pCell1);
                        postTbl.Controls.Add(ptr);

                        HtmlTableRow ptr2 = new HtmlTableRow();
                        HtmlTableCell cmntCell = new HtmlTableCell();
                        cmntCell.ColSpan = 2;

                        HtmlGenericControl tbCon = new HtmlGenericControl("DIV");
                        tbCon.Attributes.Add("class", "tbCon_cls");
                        tbCon.Style.Add(HtmlTextWriterStyle.BackgroundColor, "white");
                        tbCon.Style.Add(HtmlTextWriterStyle.Width, "100%");
                        tbCon.Style.Add(HtmlTextWriterStyle.BorderColor, "#24323e");
                        tbCon.Style.Add(HtmlTextWriterStyle.BorderStyle, "solid");
                        tbCon.Style.Add(HtmlTextWriterStyle.BorderWidth, "1px");
                        tbCon.Style.Add(HtmlTextWriterStyle.TextAlign, "right");
                        tbCon.Style.Add(HtmlTextWriterStyle.Height, "35px");

                        HtmlInputHidden hidMessage = new HtmlInputHidden();
                        hidMessage.Attributes.Add("class", "tbhd_cls");
                        hidMessage.ID += ClientID + "_" + drPost["postId"].ToString();
                        hidMessage.ClientIDMode = ClientIDMode.Static;
                        tbCon.Controls.Add(hidMessage);

                        HtmlGenericControl tb = new HtmlGenericControl("DIV");
                        tb.Attributes.Add("class", "tb_cls");
                        tb.Attributes.Add("contenteditable", "true");
                        tb.Style.Add(HtmlTextWriterStyle.OverflowX, "auto");
                        tb.Style.Add("word-wrap", "break-word");
                        tb.Style.Add(HtmlTextWriterStyle.Height, "35px");
                        tb.Style.Add(HtmlTextWriterStyle.BorderStyle, "none");
                        tb.Style.Add(HtmlTextWriterStyle.BorderWidth, "0");
                        tb.Style.Add(HtmlTextWriterStyle.BackgroundColor, "White");
                        tb.Style.Add(HtmlTextWriterStyle.TextAlign, "left");
                        tb.Style.Add(HtmlTextWriterStyle.Position, "static");
                        tb.Style.Add(HtmlTextWriterStyle.Display, "inline-block");
                        tbCon.Controls.Add(tb);

                        ImageButton commentBtn = new ImageButton();
                        commentBtn.Style.Add(HtmlTextWriterStyle.Width, "40px");
                        commentBtn.Style.Add(HtmlTextWriterStyle.Height, "35px");
                        commentBtn.Attributes.Add("class", "postBtn_cls");
                        commentBtn.ImageUrl = "/imgs/post.png";
                        commentBtn.Command += CommentBtn_Command;
                        commentBtn.CommandName = "postComment";

                        string elements = hidMessage.ClientID + ":" + drPost["postId"].ToString();
                        commentBtn.CommandArgument = elements;
                        tbCon.Controls.Add(commentBtn);

                        cmntCell.Controls.Add(tbCon);

                        ptr2.Controls.Add(cmntCell);
                        postTbl.Controls.Add(ptr2);
                        pc.Controls.Add(postTbl);
                        PostContainer.Controls.Add(pc);
                        if (Convert.ToInt32(drPost["children"].ToString()) > 0)
                        {
                            intResult = Convert.ToInt32(drPost["postId"].ToString());
                            comments(pc, intResult);
                        }
                    }
                    drc.Clear();
                    intResult = -1;
                }
                da.Close();
            }
        }
    }
}