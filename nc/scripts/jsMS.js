﻿(function () {
    if (!window.jsMS) { window['jsMS'] = {} }

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
    if (!window.jsMS) { window['jsMS'] = {}; }
    window['jsMS']['log'] = new myLogger();

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
    window['jsMS']['getBrowserWindowSize'] = new getBrowserWindowSize();

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
    window['jsMS']['Accessor'] = Accessor;

    function isCompatible(other) {
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        } return true;
    }
    window['jsMS']['isCompatible'] = isCompatible;

    function preventDefault(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        } else {
            eventObject.returnValue = false;
        }
    };
    window['jsMS']['preventDefault'] = preventDefault;

    function stopPropagation(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        } else {
            eventObject.cancelBubble = true;
        }
    };
    window['jsMS']['stopPropagation'] = stopPropagation;

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
    window['jsMS']['$'] = $;

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
    window['jsMS']['addEvent'] = addEvent;

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
    window['jsMS']['getElementsByClassName'] = getElementsByClassName;

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
    window['jsMS']['toggleDisplay'] = toggleDisplay;

    function objPos() {
        this.setObjPos = function (obj, relativeObj) {
            var f = jsMS.DomOopObjects.$(relativeObj);
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
    window['jsMS']['objPos'] = new objPos();

    function domMgmt() {
        this.removeChildren = function (parent) {
            if (!(parent = $(parent))) { return false; }
            while (parent.firstChild) {
                parent.firstChild.parentNode.removeChild(parent.firstChild);
            }
            return parent;
        };
        this.removeCurentNode = function (elem) {
            var elem2Remove = (!jsMS.$(elem)) ? elem : jsMS.$(elem);
            if (elem2Remove != null
            || elem2Remove != 'undefined') {
                var f = elem2Remove.parentNode;
                if (f == 'undefined' || f == undefined) { return false; }
                else { f.removeChild(elem2Remove); }
            }
            return elem;
        };
    };
    window['jsMS']['domMgmt'] = new domMgmt();

    function xmlRequest() {
        var xmlFactories = [
            function () { return new XMLHttpRequest(); },
            function () { return new ActiveXObject('Msxml2.XMLHTTP'); },
            function () { return new ActiveXObject('Msxml3.XMLHTTP'); },
            function () { return new ActiveXObject('Microsoft.XMLHTTP'); }
        ];
        this.createRequest = function () {
            var xmlHttp = false;
            for (var i = 0; i < xmlFactories.length; i++) {
                try {
                    xmlHttp = xmlFactories[i]();
                }
                catch (e) {
                    continue;
                }
            }
            return xmlHttp;
        };
    };
    window['jsMS']['xmlRequest'] = new xmlRequest();

    function strMatch(str2Test, strInput) {
        var userBox = document.createElement('div');
        userBox.style.cursor = 'pointer';
        var k = str2Test.UserName;
        var id = str2Test.uID;
        for(var iLen=0;iLen<=strInput.length;iLen++){
            var matchitem = k.slice(0, iLen);
            if (matchitem == strInput) {
                var userContainer = document.createElement('div');
                var spanUC = document.createElement('span');
                spanUC.className = 'positiveSuggest_cls';
                var spanNUC = document.createElement('span');
                spanNUC.className = "negativeSuggest_cls";
                var userSameName = document.createTextNode(k.slice(0, iLen));
                spanUC.appendChild(userSameName);
                userContainer.appendChild(spanUC);
                var userNotSameName = document.createTextNode(k.slice(iLen, k.length));
                spanNUC.appendChild(userNotSameName);
                userContainer.appendChild(spanNUC);
                userBox.appendChild(userContainer);
                var idContainer = document.createElement('input');
                idContainer.type = 'hidden';
                idContainer.value = id;
                userBox.appendChild(idContainer);

                userBox.selectedUser = k;
                userBox.selectedUid = id;
                jsMS.addEvent(userBox, 'mouseover', function (W3CEvent) {
                    var e = event.srcElement;
                    for (var i = 0; i < e.children.length; i++) {
                        e.children[i].style.backgroundColor = '#CFCFCF';
                    }
                    e.style.backgroundColor = '#CFCFCF';
                });
                jsMS.addEvent(userBox, 'mouseout', function (W3CEvent) {
                    var e = event.srcElement;
                    for (var i = 0; i < e.children.length; i++) {
                        e.children[i].style.backgroundColor = '#ffffff';
                    }
                    e.style.backgroundColor = '#ffffff';
                });
                jsMS.addEvent(userBox, 'click', function (W3CEvent) {
                    var e = event.srcElement;

                    var txtContainer = jsMS.$('autosuggest');
                    var userIContainer = jsMS.$(this.userIdentifier);
                    var iContainer = jsMS.$('uIdentifier');

                    txtContainer.value = this.selectedUser;
                    iContainer.value = this.selectedUid;
                    jsMS.domMgmt.removeChildren(ddC.getContainer());
                    ddC.getContainer().style.visibility = 'hidden';
                });
            }
        }
       return userBox;
    }
    window['jsMS']['strMatch'] = strMatch;
})()
var targetItem = {}, dataDoc = {}, ddC = {};
jsMS.addEvent(window, 'load', function (W3CEvent) {
    jsMS.Accessor(targetItem, 'Target', function (x) { return typeof x == 'object'; });
    jsMS.Accessor(dataDoc, 'Doc', function (x) { return typeof x == 'object'; });
    jsMS.Accessor(ddC, 'Container', function (x) { return typeof x == 'object'; });
    targetItem.setTarget(jsMS.$('autosuggest'));
    ddC.setContainer(jsMS.$('dropDownContainer'));

    var _xhr = jsMS.xmlRequest.createRequest();
    _xhr.open('GET', '/xmlData/UserList.json', false);
    _xhr.send(null);
    dataDoc.setDoc(JSON.parse(_xhr.responseText));

    jsMS.addEvent(targetItem.getTarget(), 'keyup', function (W3CEvent) {
        jsMS.domMgmt.removeChildren(ddC.getContainer());
        ddC.getContainer().style.visibility = 'hidden';

        var e = event.srcElement || event.target;
        var _e = event;

        if (_e.keyCode == 8 || _e.keyCode == 46) {
            jsMS.domMgmt.removeChildren(ddC.getContainer());
            for (var i = 0; i < dataDoc.getDoc().length; i++) {
                if (e.value.length > 0) {
                    ddC.getContainer().appendChild(jsMS.strMatch(dataDoc.getDoc()[i], e.value));
                }
                else {
                    jsMS.domMgmt.removeChildren(ddC.getContainer());
                }
            }
            if (ddC.getContainer().childElementCount <= 0) {
                ddC.getContainer().style.visibility = 'hidden';
            }
            else {
                ddC.getContainer().style.visibility = 'visible';
            }
        }
        else if (((_e.keyCode != 16 && _e.keyCode < 32) ||
            (_e.keyCode >= 33 && _e.keyCode <= 46) ||
            (_e.keyCode >= 112 && _e.keyCode <= 123))) {
        }
        else {
            jsMS.domMgmt.removeChildren(ddC.getContainer());
            for (var i = 0; i < dataDoc.getDoc().length; i++) {
               if (e.value.length > 0) {
                 ddC.getContainer().appendChild(jsMS.strMatch(dataDoc.getDoc()[i], e.value));
                 ddC.getContainer().style.visibility = 'visible';
               }
               else {
                 jsMS.domMgmt.removeChildren(ddC.getContainer());
               }
            }
        }
    });

    jsMS.addEvent(targetItem.getTarget(), 'keydown', function (W3CEvent) {
        var _e = event;
        var e = event.srcElement || event.target;
        switch (_e.keyCode) {
            case 38: // up arrow
                jsMS.log.write('Up arrow');
                break;
            case 40: // down arrow
                jsMS.log.write('down arrow');
                break;
            case 27: // esc
                jsMS.log.write('esc key');
                break;
            case 13: // enter
                jsMS.log.write('enter button');
                break;
            case 8:// backspace
                break;
        }
    });
});