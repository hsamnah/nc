﻿(function () {
    if (!window.jsDP) { window['jsDP'] = {} }

    function getDimensions(e) {
        return {
            top: e.offsetTop,
            left: e.offsetLeft,
            width: e.offsetWidth,
            height: e.offsetHeight
        };
    };
    window['jsDP']['getDimensions'] = getDimensions;

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
    window['jsDP']['getBrowserWindowSize'] = new getBrowserWindowSize();

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
    if (!window.jsDP) { window['jsDP'] = {}; }
    window['jsDP']['log'] = new myLogger();

    function isCompatible(other) {
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        } return true;
    }
    window['jsDP']['isCompatible'] = isCompatible;

    function preventDefault(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        } else {
            eventObject.returnValue = false;
        }
    };
    window['jsDP']['preventDefault'] = preventDefault;

    function stopPropagation(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        } else {
            eventObject.cancelBubble = true;
        }
    };
    window['jsDP']['stopPropagation'] = stopPropagation;

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
    window['jsDP']['Accessor'] = Accessor;

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
    window['jsDP']['$'] = $;

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
    window['jsDP']['addEvent'] = addEvent;

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
    window['jsDP']['getElementsByClassName'] = getElementsByClassName;

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
    window['jsDP']['toggleDisplay'] = toggleDisplay;

    function objPos() {
        this.setObjPos = function (obj, relativeObj) {
            var f = jsDP.DomOopObjects.$(relativeObj);
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
    window['jsDP']['objPos'] = new objPos();
})();
var tb = null, marker = null, cc = null;
var tbI = null, markerI = null, ccI = null, container = null, cont = {}, cD=null;
jsDP.addEvent(window, 'load', function (W3CEvent) {
    cc = jsDP.getElementsByClassName('calContainer_Cls', 'div');
    marker = jsDP.getElementsByClassName('mainContainer_Cls', 'div');
    tb = jsDP.getElementsByClassName('eventDate_Cls', 'input');
    jsDP.Accessor(cont, 'I', function (x) { return typeof x == 'object'; });
    container = jsDP.getElementsByClassName('cIdentifier_cls', 'INPUT');

    if (container.length > 0) {
        for (var items = 0; items < container.length; items++) {
                var identity = container[items].value;
                cont.setI(jsDP.$(identity));
        }
    }

    if (cc <= 0) {
        return;
    }
    else {
        for (var tIndex = 0; tIndex < tb.length; tIndex++) {
            tbI = jsDP.$(tb[tIndex]);
        }
        for (var mIndex = 0; mIndex < marker.length; mIndex++) {
            markerI = jsDP.$(marker[mIndex]);
        }
        for (var cIndex = 0; cIndex < cc.length; cIndex++) {
            ccI = jsDP.$(cc[cIndex]);
        }
        cD = jsDP.getDimensions(cont.getI());
        ccI.style.top = jsDP.objPos.returnObjTop(markerI) - cD.top + jsDP.objPos.returnObjHeight(markerI) + 2 + 'px';
        ccI.style.left = jsDP.objPos.returnObjLeft(markerI) - cD.left + 'px';
    }
});

jsDP.addEvent(window, 'resize', function (W3CEvent) {
    cc = jsDP.getElementsByClassName('calContainer_Cls', 'div');
    marker = jsDP.getElementsByClassName('mainContainer_Cls', 'div');
    tb = jsDP.getElementsByClassName('eventDate_Cls', 'input');
    cD = jsDP.getDimensions(cont.getI());

    if (cc <= 0) {
        return;
    }
    else {
        for (var tIndex = 0; tIndex < tb.length; tIndex++) {
            tbI = jsDP.$(tb[tIndex]);
        }
        for (var mIndex = 0; mIndex < marker.length; mIndex++) {
            markerI = jsDP.$(marker[mIndex]);
        }
        for (var cIndex = 0; cIndex < cc.length; cIndex++) {
            ccI = jsDP.$(cc[cIndex]);
        }

        ccI.style.top = jsDP.objPos.returnObjTop(markerI) - cD.top + jsDP.objPos.returnObjHeight(markerI) + 2 + 'px';
        ccI.style.left = jsDP.objPos.returnObjLeft(markerI) - cD.left + 'px';
    }
})