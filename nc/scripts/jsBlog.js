(function () {
    if (!window.jsBlog) { window['jsBlog'] = {} }

    function myLogger(id) {
        id = id || 'LambdaLogWindow';
        var logWindow = null;
        var createWindow = function () {
            var browserWindowSize = getBrowserWindowSize();
            var top = ((browserWindowSize.height - 200) / 2) || 0;
            var left = ((browserWindowSize.width - 200) / 2) || 0;

            logWindow = document.createElement('UL');
            logWindow.setAttribute('id', id);
            logWindow.style.position = 'absolute';
            logWindow.style.top = top + 'px';
            logWindow.style.left = left + 'px';
            logWindow.style.width = '200px';
            logWindow.style.height = '200px';
            logWindow.style.overflow = 'scroll';
            logWindow.style.padding = '0';
            logWindow.style.margin = '0';
            logWindow.style.border = '1px solid black';
            logWindow.style.backgroundColor = 'White';
            logWindow.style.listStyle = 'none';
            logWindow.style.font = '10px/10px Verdana, Tahoma, Sans';
            logWindow.style.zIndex = '10000';
            document.body.appendChild(logWindow);
        };
        this.writeRaw = function (message) {
            if (!logWindow) {
                createWindow();
            };
            var li = document.createElement('LI');
            li.style.padding = '2px';
            li.style.border = '0';
            li.style.borderbottom = '1px dotted black';
            li.style.margin = '0';
            li.style.color = '#000000';
            li.style.font = '9px/9px Verdana, Tahoma, Sans';
            if (typeof message == 'undefined') {
                li.appendChild(document.createTextNode('Message was undefined'));
            }
            else if (typeof li.innerHTML != 'undefined') {
                li.innerHTML = message;
            }
            else {
                li.appendChild(document.createTextNode(message));
            }
            logWindow.appendChild(li);
            return this;
        };
    }
    myLogger.prototype = {
        write: function (message) {
            if (typeof message == 'string' && message.length == 0) {
                return this.writeRaw('Lambda.log: null message');
            }
            if (typeof message != 'string') {
                if (message.toString()) {
                    return this.writeRaw(message.toString());
                }
                else {
                    return this.writeRaw(typeof message);
                }
            }
            message = message.replace(/</g, "&lt;").replace(/>/g, "&gt;");

            return this.writeRaw(message);
        },
        header: function (message) {
            message = '<span style="color:white;background-color:black;font-weight:bold;padding:0px 5px;">' + message + '</span>';
            return this.writeRaw(message);
        }
    };
    if (!window.jsBlog) { window['jsBlog'] = {}; }
    window['jsBlog']['log'] = new myLogger();

    function isCompatible(other) {
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        } return true;
    }
    window['jsBlog']['isCompatible'] = isCompatible;

    function preventDefault(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        } else {
            eventObject.returnValue = false;
        }
    };
    window['jsBlog']['preventDefault'] = preventDefault;

    function stopPropagation(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        } else {
            eventObject.cancelBubble = true;
        }
    };
    window['jsBlog']['stopPropagation'] = stopPropagation;

    function $() {
        var elements = new Array();
        for (var i = 0; i < arguments.length; i++) {
            var element = arguments[i];
            if (typeof element == 'string') {
                element = document.getElementById(element);
            }
            if (arguments.length == 1) { return element; }
            return elements;
        }
    };
    window['jsBlog']['$'] = $;

    function addEvent(node, type, listener) {
        if (!isCompatible()) { return false; }
        if (!(node = $(node))) { return false; }
        if (node.addEventListener) {
            node.addEventListener(type, listener, false);
        }
        else if (node.attachEvent) {
            node['e' + type + listener] = listener;
            node[type + listener] = function () {
                node['e' + type + listener](window.event);
            }
            node.attachEvent('on' + type, node[type + listener]);
            return true;
        }
        return false;
    };
    window['jsBlog']['addEvent'] = addEvent;

    function getElementsByClassName(className, tag, parent) {
        parent = parent || document;
        if (!(parent = $(parent))) { return false; }
        var allTags = (tag == "*" && parent.all) ? parent.all : parent.getElementsByTagName(tag);
        var matchingElements = new Array();
        className = className.replace(/\-/g, "\\-");
        var regex = new RegExp("(^|\\s)" + className + "(\\s|$)");

        var elements;
        for (var i = 0; i < allTags.length; i++) {
            element = allTags[i];
            if (regex.test(element.className)) {
                matchingElements.push(element);
            }
        }
        return matchingElements;
    };
    window['jsBlog']['getElementsByClassName'] = getElementsByClassName;

    function toggleDisplay(node, value) {
        if (!(node = $(node))) { return false; }
        if (node.style.display != 'none') {
            node.style.display = 'none';
        }
        else {
            node.style.display = value || '';
        }
        return true;
    };
    window['jsBlog']['toggleDisplay'] = toggleDisplay;

    function objPos() {
        this.setObjPos = function (obj, relativeObj) {
            var f = jsBlog.DomOopObjects.$(relativeObj);
            var x = 0;
            var y = 0;
            while (f) {
                x += f.offsetLeft;
                y += f.offsetTop;
                f = f.offsetParent;
            }
            if (navigator.userAgent.indexOf('Mac') != -1 && typeof (document.body.leftMargin) != 'undefined') {
                x += document.body.leftMargin;
                y += document.body.topMargin;
            }
            obj.style.left = x + 'px';
            obj.style.top = y + 'px';
        };
        this.returnObjTop = function (obj) {
            var f = $(obj);
            if (f == null || f == 'undefined') { return false; }
            var y = 0;
            while (f) {
                if (navigator.userAgent.indexOf('Mac') != -1 && typeof (document.body.topMargin) != 'undefined') {
                    y += document.body.topMargin;
                    f = f.offsetParent;
                } else {
                    y += f.offsetTop - f.scrollTop;
                    f = f.offsetParent;
                }
            }
            return y;
        };
        this.returnObjLeft = function (obj) {
            var h = $(obj);
            if (h == null || h == 'undefined') { return false; }
            var x = 0;
            while (h) {
                if (navigator.userAgent.indexOf('Mac') != -1 && typeof (document.body.topMargin) != 'undefined') {
                    x += document.body.topMargin;
                    h = h.offsetParent;
                } else {
                    x += h.offsetLeft - h.scrollLeft;
                    h = h.offsetParent;
                }
            }
            return x;
        };
        this.returnObjHeight = function (obj) {
            var f = $(obj);
            if (f == null || f == 'undefined') { return false; }
            var x = 0;
            x += f.offsetHeight;
            return x;
        };
        this.returnObjWidth = function (obj) {
            var f = $(obj);
            if (f == null || f == 'undefined') { return false; }
            var x = 0;
            x += f.offsetWidth;
            return x;
        };
    };
    window['jsBlog']['objPos'] = new objPos();

    function Accessor(obj, name, predicate) {
        var value;
        obj['get' + name] = function () { return value; };
        obj['set' + name] = function (v) {
            if (predicate && !predicate(v)) {
                throw 'set' + name + ': invalid value ' + v;
            }
            else { value = v; };
        };
    };
    window['jsBlog']['Accessor'] = Accessor;

    function domMgmt() {
        this.removeChildren = function (parent) {
            if (!(parent = $(parent))) { return false; }
            while (parent.firstChild) {
                parent.firstChild.parentNode.removeChild(parent.firstChild);
            }
            return parent;
        };
        this.removeCurentNode = function (elem) {
            var elem2Remove = (!jsBlog.$(elem)) ? elem : jsBlog.$(elem);
            if (elem2Remove != null
            || elem2Remove != 'undefined') {
                var f = elem2Remove.parentNode;
                if (f == 'undefined' || f == undefined) { return false; }
                else { f.removeChild(elem2Remove); }
            }
            return elem;
        };
    };
    window['jsBlog']['domMgmt'] = new domMgmt();

    function getBrowserWindowSize() {
        var de = document.documentElement;
        return {
            'width': (
        window.innerWidth
        || (de && de.clientWidth)
        || (document.body.clientWidth)),
            'height': (
        window.innerHeight
        || (de & de.clientHeight)
        || (document.body.clientHeight))
        }
    };
    window['jsBlog']['getBrowserWindowSize'] = new getBrowserWindowSize();

    function toggleDisplay(node, value) {
        if (!(node = $(node))) { return false; }
        if (node.style.display != 'none') {
            node.style.display = 'none';
        }
        else {
            node.style.display = value || '';
        }
        return true;
    };
    window['jsBlog']['toggleDisplay'] = toggleDisplay;
})();
var hID = null, hTitle = null, hDesc = null, hArticle = null, hAuthor = null, bdate = null;
var sTitle = null, sAuthor = null, sDate = null, sDesc = null, sDetails = null;
var iContainer = null, bd = null;
var browserWindowSize = null, clsbtn=null, bws=null, mp=null;
jsBlog.addEvent(window, 'load', function (W3CEvent) {
    sTitle          =           jsBlog.$('rTitle');
    sAuthor         =           jsBlog.$('rAuthor');
    sDate           =           jsBlog.$('rDate');
    sDesc           =           jsBlog.$('rDesc');
    sDetails        =           jsBlog.$('rDetails');
    bd              =           jsBlog.$('backDrop');
    hID             =           jsBlog.getElementsByClassName('hidIdentifier', 'INPUT');
    hTitle          =           jsBlog.getElementsByClassName('bTitle_cls', 'SPAN');
    hDesc           =           jsBlog.getElementsByClassName('hidDesc', 'INPUT');
    hArticle        =           jsBlog.getElementsByClassName('hidArticle', 'INPUT');
    hAuthor         =           jsBlog.getElementsByClassName('hidAuthor', 'INPUT');
    bDate           =           jsBlog.getElementsByClassName('bDate_cls', 'SPAN');

    iContainer      =           jsBlog.$('innerContainer');
    mp              =           jsBlog.$('mainPanel');

    bws = jsBlog.getBrowserWindowSize;
    var Top = ((bws.height - 500) / 2) || 0;
    var left = ((bws.width - 800) / 2) || 0;
    mp.style.top = Top + 'px';
    mp.style.left = left + 'px';
    jsBlog.addEvent(window, 'resize', function (W3CEvent) {
        bws = jsBlog.getBrowserWindowSize;
        var Top = ((bws.height - 500) / 2) || 0;
        var left = ((bws.width - 800) / 2) || 0;
        mp.style.top = Top + 'px';
        mp.style.left = left + 'px';
    });
    for (var index = 0; index < hID.length; index++) {
        hTitle[index].Title = hTitle[index].innerHTML;
        hTitle[index].Des = hDesc[index].value;
        hTitle[index].Author = hAuthor[index].value;
        hTitle[index].Date = bDate[index].innerHTML;
        hTitle[index].Article = hArticle[index].value;
        hTitle[index].backdrop = bd;
        hTitle[index].container = iContainer;
        jsBlog.addEvent(hTitle[index], 'click', function (W3CEvent) {
            sTitle = jsBlog.$('rTitle');
            sAuthor = jsBlog.$('rAuthor');
            sDate = jsBlog.$('rDate');
            sDesc = jsBlog.$('rDesc');
            sDetails = jsBlog.$('rDetails');
            clsbtn = jsBlog.$('closeBtn');

            browserWindowSize = jsBlog.getBrowserWindowSize;
            var innerTop = ((browserWindowSize.height - 800) / 2) || 0;
            var innerLeft = ((browserWindowSize.width - 700) / 2) || 0;

            this.container.style.top = innerTop + 'px';
            this.container.style.left = innerLeft + 'px';

            this.backdrop.style.display = 'block';
            sTitle.innerHTML = this.Title;
            sDate.innerHTML = this.Date;
            sDesc.innerHTML = this.Des;
            sDetails.innerHTML = this.Article;
            jsBlog.addEvent()
            clsbtn.ElementToClose = this.backdrop;
            jsBlog.addEvent(clsbtn, 'click', function (W3CEvent) {
                this.ElementToClose.style.display = 'none';
            });
        });
    };
});