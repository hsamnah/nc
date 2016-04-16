(function () {
    if (!window.jsWB) { window['jsWB'] = {} }

    function getDimensions(e) {
        return {
            top: e.offsetTop,
            left: e.offsetLeft,
            width: e.offsetWidth,
            height: e.offsetHeight
        };
    };

    function setNumericStyle(e, dim, updateMessage) {
        updateMessage = updateMessage || false;

        var style = {};
        for (var i in dim) {
            if (!dim.hasOwnProperty(i)) continue;
            style[i] = (dim[i] || '0') + 'px';
        }
        jsWB.setStyle(e, style);

        if (updateMessage) {
            e.elements.cropSizeDisplay.firstChild.nodeValue = dim.width + 'x' + dim.height;
        }
    };

    function isCompatible(other) {
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        } return true;
    }
    window['jsWB']['isCompatible'] = isCompatible;

    function isArray(myArray) {
        return myArray.constructor.toString().indexOf('Array') > 1;
    }
    window['jsWB']['isArray'] = isArray;

    function preventDefault(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        } else {
            eventObject.returnValue = false;
        }
    };
    window['jsWB']['preventDefault'] = preventDefault;

    function stopPropagation(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        } else {
            eventObject.cancelBubble = true;
        }
    };
    window['jsWB']['stopPropagation'] = stopPropagation;

    // Helper function that allows setting get and set accessors
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
    window['jsWB']['Accessor'] = Accessor;

    // Helper function that promotes classical inheritance.
    function extend(subClass, superClass) {
        var F = function () { };
        F.prototype = superClass.prototype;
        subClass.prototype = new F();
        subClass.prototype.constructor = subClass;

        subClass.superclass = superClass.prototype;
        if (superClass.prototype.constructor == Object.prototype.constructor) {
            superClass.prototype.constructor = superClass;
        }
    }
    window['jsWB']['extend'] = extend;

    function camelize(s) {
        return s.replace(/-(\w)/g, function (strMatch, p1) {
            return p1.toUpperCase();
        });
    }
    window['jsWB']['camelize'] = camelize;

    function uncamelize(s, sep) {
        sep = sep || '-';
        return s.replace(/([a-z])([A-Z])/g, function (strMatch, p1, p2) {
            return p1 + sep + p2.toLowerCase();
        });
    }
    window['jsWB']['camelize'] = camelize;

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
    window['jsWB']['$'] = $;

    function getEventObject(w3CEvent) {
        return w3CEvent || window.event;
    }
    window['jsWB']['getEventObject'] = getEventObject;

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
    window['jsWB']['addEvent'] = addEvent;

    function addLoadEvent(loadEvent, waitForImages) {
        if (!isCompatible()) return false;
        if (waitForImages) {
            return addEvent(window, 'load', loadEvent);
        }
        var init = function () {
            if (arguments.callee.done) return;
            arguments.callee.done = true;
            loadEvent.apply(document, arguments);
        };
        if (document.addEventListener) {
            document.addEventListener("DOMContentLoaded", init, false);
        }
        if (/WebKit/i.test(navigator.userAgent)) {
            var _timer = setInterval(function () {
                if (/loaded|complete/.test(document.readyState)) {
                    clearInterval(_timer);
                    init();
                }
            }, 10);
        }
        /*@cc_on @*/
        /*@if(@_win32)*/
        document.write("<script id=__ie_onload defer src=javascript:void(0)><\/script>");
        var script = document.getElementById("__ie_onload");
        script.onreadystatechange = function () {
            if (this.readyState == "complete") {
                init();
            }
        };
        /*@end @*/
        return true;
    };
    window['jsWB']['addLoadEvent'] = addLoadEvent;

    function removeEvent(node, type, listener) {
        if (!isCompatible()) { return false; }
        if (node.removeEventListener) {
            node.removeEventListener(type, listener, false);
            return true;
        }
        else if (node.detachEvent) {
            node.detachEvent('on' + type, node[type + listener]);
            node[type + listener] = null;
            return true;
        }
        return false;
    };
    window['jsWB']['removeEvent'] = removeEvent;

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
    window['jsWB']['getElementsByClassName'] = getElementsByClassName;

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
    window['jsWB']['toggleDisplay'] = toggleDisplay;

    function insertAfter(node, referenceNode) {
        if (!(node = $(node))) { return false; }
        if (!(referenceNode = $(referenceNode))) { return false; }
        return referenceNode.parentNode.insertBefore(
            node, referenceNode.nextSibling);
    };
    window['jsWB']['insertAfter'] = insertAfter;

    function domMgmt() {
        this.removeChildren = function (parent) {
            if (!(parent = $(parent))) { return false; }
            while (parent.firstChild) {
                parent.firstChild.parentNode.removeChild(parent.firstChild);
            }
            return parent;
        };
        this.removeCurentNode = function (elem) {
            var elem2Remove = (!jsWB.$(elem)) ? elem : jsWB.$(elem);
            if (elem2Remove != null
            || elem2Remove != 'undefined') {
                var f = elem2Remove.parentNode;
                if (f == 'undefined' || f == undefined) { return false; }
                else { f.removeChild(elem2Remove); }
            }
            return elem;
        };
    };
    window['jsWB']['domMgmt'] = new domMgmt();

    function prependChild(parent, newChild) {
        if (!(parent = $(parent))) { return false; }
        if (!(newChild = $(newChild))) { return false; }
        if (parent.firstChild) {
            parent.insertBefore(newChild, parent.firstChild);
        }
        else {
            parent.appendChild(newChild);
        }
        return parent;
    };
    window['jsWB']['prependChild'] = prependChild;

    function objPos() {
        this.setObjPos = function (obj, relativeObj) {
            var f = jsWB.DomOopObjects.$(relativeObj);
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
    window['jsWB']['objPos'] = new objPos();

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
    if (!window.jsWB) { window['jsWB'] = {}; }
    window['jsWB']['log'] = new myLogger();

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
    window['jsWB']['getBrowserWindowSize'] = new getBrowserWindowSize();

    function getPointerPositionInDocument(eventObject) {
        eventObject = eventObject || getEventObject(eventObject);

        var x = eventObject.pageX || (eventObject.clientX + (document.documentElement.scrollLeft || document.body.scrollLeft));

        var y = eventObject.pageY || (eventObject.clientY + (document.documentElement.scrollTop || document.body.scrollTop));

        return { 'x': x, 'y': y };
    }
    window['jsWB']['getPointerPositionInDocument'] = getPointerPositionInDocument;

    function getMyBoundingClientRect(objRectangle) {
        var _can = objRectangle.getBoundingClientRect();
        return { 'x': _can.left, 'y': _can.top, 'height': _can.height, 'width': _can.width };
    };
    window['jsWB']['getMyBoundingClientRect'] = getMyBoundingClientRect;

    function getKeyPressed(eventObject) {
        eventObject = eventObject || getEventObject(eventObject);

        var code = eventObject.keyCode;
        var value = String.fromCharCode(code);
        return { 'code': code, 'value': value };
    }
    window['jsWB']['getKeyPressed'] = getKeyPressed;

    function setStyleById(element, styles) {
        if (!(element = $(element))) return false;
        for (property in styles) {
            if (!styles.hasOwnProperty(property)) continue;
            if (element.style.setProperty) {
                element.style.setProperty(uncamelize(property, '-'), styles[property], null);
            }
            else {
                element.style[camelize(property)] = styles[property];
            }
        }
        return true;
    }
    window['jsWB']['setStyle'] = setStyleById;
    window['jsWB']['setStyleById'] = setStyleById;

    function setStylesByClassName(parent, tag, className, styles) {
        if (!(parent = $(parent))) return false;
        var elements = getElementsByClassName(className, tag, parent);
        for (var e = 0; e < elements.length; e++) {
            setStyleById(elements[e], styles);
        }
        return true;
    }
    window['jsWB']['setStylesByClassName'] = setStylesByClassName;

    function setStylesByTagName(tagName, styles, parent) {
        parent = $(parent) || document;
        var elements = parent.getElementsByTagName(tagName);
        for (var e = 0; e < elements.length; e++) {
            setStylesById(elements[e], styles);
        }
    }
    window['jsWB']['setStylesByTagName'] = setStylesByTagName;

    function getWindowSize() {
        if (self.innerHeight) {
            return { 'width': self.innerWidth, 'height': self.innerHeight };
        }
        else if (document.documentElement && document.documentElement.clientHeight) {
            return {
                'width': document.documentElement.clientWidth,
                'height': document.documentElement.clientHeight
            };
        }
        else if (document.body) {
            return {
                'width': document.body.clientWidth,
                'height': document.body.clientHeight
            };
        }
    };
    window['jsWB']['getWindowSize'] = getWindowSize;

    // Canvas functions will now follow:
    function degree2rad(degree) {
        return Math.PI * degree / 180;
    };
    window['jsWB']['degree2rad'] = new degree2rad();

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

        var childElement = function (req, container) {
            var x = null, y = null;
            var elem = null;
            for (var i = 0; i < req.childNodes.length; i++) {
                x = document.createTextNode(req.childNodes[i].nodeName + ' : ');
                y = document.createTextNode(req.childNodes[i].nodeValue);
                elem = document.createElement('br');
                container.appendChild(x);
                container.appendChild(y);
                container.appendChild(elem);
                childElement(req.childNodes[i], container);
            }
        };

        this.listRequest = function (req, container) {
            var xmlDoc = req.childNodes;
            var x = null, y = null;
            var elem = null;
            for (var i = 0; i < xmlDoc.length; i++) {
                x = document.createTextNode(xmlDoc[i].nodeName + ' : ');
                y = document.createTextNode(req.childNodes[i].nodeValue);
                elem = document.createElement('br');
                container.appendChild(x);
                container.appendChild(y);
                container.appendChild(elem);
                childElement(xmlDoc[i], container)
            }
        };
    };
    window['jsWB']['xmlRequest'] = new xmlRequest();

    function colorSwatch(eventSrc) {
        var selectedColor = null;
        var txt = [];
        var esrc = {};
        this.getSel = function () {
            if (window.getSelection) {
                var currentSelection = window.getSelection();
                for (var i = 0; i < currentSelection.rangeCount; i++) {
                    txt.push(currentSelection.getRangeAt(i));
                }
                currentSelection.removeAllRanges();
            }
            return txt;
        }
        var handleSelection = function (clr, selection) {
            if (window.getSelection) {
                var currSelection = window.getSelection();
                currSelection.removeAllRanges();
                for (var i = 0; i < selection.length; i++) {
                    currSelection.addRange(selection[i]);
                }
                document.execCommand('ForeColor', true, clr);
                currSelection.removeAllRanges();
                selection.length = 0;
            }
        };

        this.colorPalette = function (target, selection) {
            var e = event.srcElement;
            var t = ((target == null) || (target == 'undefined')) ? e : target;
            jsWB.Accessor(esrc, 'e', function (x) { return typeof x == 'object'; });
            esrc.sete(t);

            var Spectrum = ['00', '33', '66', '99', 'aa', 'bb', 'cc', 'dd', 'ee', 'ff'];
            var container = document.createElement('DIV');
            container.id = 'clrSwatch';
            var rootTbl = document.createElement('TABLE');
            var rootTbody = document.createElement('TBODY');
            var dtr = document.createElement('TR');
            // Create input display.
            var dcell1 = document.createElement('TD');
            var childtbl = document.createElement('TABLE');
            var childTbdy = document.createElement('TBODY');
            var childTR = document.createElement('TR');
            var td1 = document.createElement('TD');
            var td2 = document.createElement('TD');
            var inputObj = document.createElement('INPUT');

            td1.style.width = '15px';
            td1.style.height = '15px';
            childTR.appendChild(td1);
            inputObj.type = 'TEXT';
            inputObj.id = 'clrIO';
            td2.appendChild(inputObj);
            childTR.appendChild(td2);
            childTbdy.appendChild(childTR);
            childtbl.style.borderCollapse = 'collapse';
            childtbl.style.borderWidth = '0px';
            childtbl.appendChild(childTbdy);
            dcell1.appendChild(childtbl);
            dcell1.colSpan = Spectrum.length;
            dtr.appendChild(dcell1);
            rootTbody.appendChild(dtr);

            // Color palette objects.
            for (var i = 0; i < Spectrum.length; i++) {
                for (var j = 0; j < Spectrum.length; j++) {
                    var crow = document.createElement('TR');
                    for (var k = 0; k < Spectrum.length; k++) {
                        var color = Spectrum[i] + Spectrum[j] + Spectrum[k];
                        var ccell = document.createElement('TD');
                        jsWB.addEvent(ccell, 'mouseover', function (W3CEvent) {
                            var e = event.srcElement;
                            var target = jsWB.$('clrIO');
                            target.value = e.style.backgroundColor;
                            td1.style.backgroundColor = e.style.backgroundColor;
                        })
                        jsWB.addEvent(ccell, 'mouseout', function (W3CEvent) {
                            var target = jsWB.$('clrIO');
                            target.value = '';
                            td1.style.backgroundColor = '';
                        });
                        jsWB.addEvent(ccell, 'click', function (W3CEvent) {
                            var e1 = event.srcElement;
                            jsWB.preventDefault(ccell);
                            jsWB.stopPropagation(ccell);

                            if (esrc.gete().nodeName === 'INPUT') {
                                esrc.gete().value = e1.style.backgroundColor;
                                esrc.gete().style.backgroundColor = e1.style.backgroundColor;

                                jsWB.domMgmt.removeChildren(jsWB.$('clrSwatch'));
                                jsWB.domMgmt.removeCurentNode(jsWB.$('clrSwatch'));
                            }

                            else if (esrc.gete().nodeName === 'DIV') {
                                esrc.gete().style.backgroundColor = e1.style.backgroundColor;

                                handleSelection(e1.style.backgroundColor, selection);

                                jsWB.domMgmt.removeChildren(jsWB.$('clrSwatch'));
                                jsWB.domMgmt.removeCurentNode(jsWB.$('clrSwatch'));
                            }
                            else {
                                jsWB.domMgmt.removeChildren(jsWB.$('clrSwatch'));
                                jsWB.domMgmt.removeCurentNode(jsWB.$('clrSwatch'));
                            }
                        });
                        ccell.style.width = '1px';
                        ccell.style.height = '1px';
                        ccell.style.borderCollapse = 'collapse';
                        ccell.style.borderSpacing = '0px';
                        ccell.style.backgroundColor = '#' + color;
                        ccell.setAttribute('class', 'strColor_cls');
                        crow.appendChild(ccell);
                    }
                    crow.setAttribute('class', 'strColor_cls');
                    rootTbody.appendChild(crow);
                }
            }
            // close off the dialogue.
            rootTbl.appendChild(rootTbody);
            rootTbl.style.borderCollapse = 'collapse';
            rootTbl.setAttribute('class', 'strClrTbl_cls');
            container.appendChild(rootTbl);
            container.style.zIndex = '1000';
            container.style.left = '50px';
            container.style.top = '50px';
            container.style.position = 'absolute';
            container.style.backgroundColor = '#CFCFCF';
            container.style.border = '1px solid black';
            container.style.padding = '5px 5px 5px 5px';
            container.style.top = jsWB.objPos.returnObjTop(esrc.gete()) + jsWB.objPos.returnObjHeight(esrc.gete()) + 'px';
            container.style.left = jsWB.objPos.returnObjLeft(esrc.gete()) + 'px';
            jsWB.addEvent(window, 'resize', function (W3CEvent) {
                container.style.top = jsWB.objPos.returnObjTop(esrc.gete()) + jsWB.objPos.returnObjHeight(esrc.gete()) + 'px';
                container.style.left = jsWB.objPos.returnObjLeft(esrc.gete()) + 'px';
            });
            container.setAttribute('class', 'clrContainer_cls');
            document.body.appendChild(container);
        }
    }
    if (!window.jsWB) { window['jsWB'] = {}; }
    window['jsWB']['colorSwatch'] = new colorSwatch();

    function newWindow() {
        this.openWindow = function (link, postfix, args) {
            var nw = window.open(link + '.' + postfix, link, args);
        }
    }
    if (!window.jsWB) { window['jsWB'] = {}; }
    window['jsWB']['newWindow'] = new newWindow();

    function chatter() {
        var eventSrc = {}, lpi = {}, friendName = {}, friendID = {};
        this.createChat = function (parent, leftPosItem) {
            jsWB.Accessor(eventSrc, 'Src', function (x) { return typeof x == 'object'; });
            eventSrc.setSrc(event.srcElement);

            jsWB.Accessor(lpi, 'lPos', function (x) { return typeof x == 'object'; });
            lpi.setlPos((leftPosItem == null) ? event.srcElement : leftPosItem);

            jsWB.Accessor(friendName, 'FN', function (x) { return typeof x == 'string'; });
            friendName.setFN((eventSrc.getSrc().innerHTML).replace('&nbsp;', ' '));

            jsWB.Accessor(friendID, 'FID', function (x) { return typeof x == 'string'; });
            friendID.setFID(eventSrc.getSrc().id);

            var chatContainer = document.createElement('DIV');
            chatContainer.id = 'container';
            chatContainer.style.top = jsWB.objPos.returnObjTop(eventSrc.getSrc()) + 'px';
            chatContainer.style.left = jsWB.objPos.returnObjLeft(lpi.getlPos()) + jsWB.objPos.returnObjWidth(lpi.getlPos()) + 1 + 'px';

            jsWB.addEvent(window, 'resize', function (W3Event) {
                chatContainer.style.top = jsWB.objPos.returnObjTop(eventSrc.getSrc()) + 'px';
                chatContainer.style.left = jsWB.objPos.returnObjLeft(lpi.getlPos()) + jsWB.objPos.returnObjWidth(lpi.getlPos()) + 1 + 'px';
            });
            var titleContainer = document.createElement('DIV');
            titleContainer.id = 'title';
            var titletxt = document.createTextNode(friendName.getFN());
            titleContainer.appendChild(titletxt);

            var hiddenName = document.createElement('INPUT');
            hiddenName.id = 'hiddenFN';
            hiddenName.type = 'HIDDEN';
            hiddenName.value = friendName.getFN();
            titleContainer.appendChild(hiddenName);

            chatContainer.appendChild(titleContainer);
            var messagesContainer = document.createElement('DIV');
            messagesContainer.id = 'messages';
            var discuss = document.createElement('UL');
            discuss.id = 'discussion';
            messagesContainer.appendChild(discuss);
            chatContainer.appendChild(messagesContainer);
            var commContainer = document.createElement('DIV');
            commContainer.id = 'comm';
            var emoContainer = document.createElement('DIV');
            emoContainer.id = 'emo';
            commContainer.appendChild(emoContainer);
            var comm = document.createElement('DIV');
            comm.id = 'comm1';
            var txtMsg = document.createElement('INPUT');
            txtMsg.id = 'message';
            txtMsg.type = 'TEXT';
            comm.appendChild(txtMsg);
            var sendBtn = document.createElement('INPUT');
            sendBtn.id = 'sendM';
            sendBtn.type = 'BUTTON';
            sendBtn.value = 'Send';
            comm.appendChild(sendBtn);
            commContainer.appendChild(comm);
            chatContainer.appendChild(commContainer);
            parent.appendChild(chatContainer);
        };
    }
    if (!window.jsWB) { window['jsWB'] = {}; }
    window['jsWB']['chatter'] = new chatter();

    function preload() {
        var imgs = [];
        this.preloadImgs = function (collection) {
            for (var i = 0; i < collection.length; i++) {
                imgs[i] = new Image();
                imgs[i].src = collection[i];
            }
        }
    }
    window['jsWB']['preload'] = new preload();

    // Page specific functions
})();
var pageName = {}, pageContainer = {}, xmlData = {}, menuBtn = {}, xUserData = {};
var targetAcc = {}, chatWindow = {}, friendName = {}, friendID = {}, addWindow = {}, uObj = {};
var imgEmoticonArray = {}, txtSelection = {}, imgArray = [];

var content, arrow, loginWindow, vloginWindow, messageWindow, lbtn, vlbtn, mbtn, home, xmlReq;

var dirCreate = null, dirDelete = null, imgAdd = null, imgDel = null;

var _listContainer = null, _imgBtn = null, _sEvent = null, _eventItems = null, eventItem = null, _eHolder = null, _clrP = null, _cSelector = null, txtSel = null, selectedTxt = [], _ctrlBtns = null, _postBox = null, _postValue = null, _sendPost = null, _chatContainer = null, preImgCollection = [], _dc = null, _dn = null, pIdentifier = 0, _epost = null, _evalue = null, _sendE = null, _ePic = null, ep = null;

var _searchBar = null, _sm = null, addUserBtn = null, finvite = null, finviteName = null, finviteEmail = null, linq = null, footLinks = null, winclose = null, selectedFriend = null, cfriend = null, userElem = null;

var inviteBtn = null, iBack = null, inviteTB = null;

jsWB.addLoadEvent(function (W3CEvent) {
    var imgName = null;
    imgBtn_fn = jsWB.getElementsByClassName('imgbtn_cls', 'img');
    finvite = jsWB.getElementsByClassName('FriendInvite_cls', 'input');

    for (var k = 0; k < imgBtn_fn.length; k++) {
        preImgCollection.push(imgBtn_fn[k].src);
    }

    //var userElem = jsWB.$('currentUser');
    jsWB.Accessor(uObj, 'User', function (x) { return typeof x == 'object'; });
    //uObj.setUser(JSON.parse(userElem.value));


    jsWB.preload.preloadImgs(preImgCollection);
    jsWB.Accessor(imgEmoticonArray, 'iEA', function (x) { return typeof x == 'object'; });
    imgEmoticonArray.setiEA(imgArray);
    var letters = ['a', 'b', 'c', 'd', 'e', 'f'];
    for (var i = 0; i < letters.length; i++) {
        switch (letters[i]) {
            case 'a':
                for (var j = 1; j <= 61; j++) {
                    imgName = letters[i] + j.toString() + '.png';
                    imgEmoticonArray.getiEA().push('/imgs/emoticons/' + imgName);
                }
                break;
            case 'b':
                for (var j = 1; j <= 15; j++) {
                    imgName = letters[i] + j.toString() + '.png';
                    imgEmoticonArray.getiEA().push('/imgs/emoticons/' + imgName);
                }
                break;
            case 'c':
                for (var j = 1; j <= 4; j++) {
                    imgName = letters[i] + j.toString() + '.png';
                    imgEmoticonArray.getiEA().push('/imgs/emoticons/' + imgName);
                }
                break;
            case 'd':
                for (var j = 1; j <= 37; j++) {
                    imgName = letters[i] + j.toString() + '.png';
                    imgEmoticonArray.getiEA().push('/imgs/emoticons/' + imgName);
                }
                break;
            case 'e':
                for (var j = 1; j <= 9; j++) {
                    imgName = letters[i] + j.toString() + '.png';
                    imgEmoticonArray.getiEA().push('/imgs/emoticons/' + imgName);
                }
                break;
            case 'f':
                for (var j = 1; j <= 14; j++) {
                    imgName = letters[i] + j.toString() + '.png';
                    imgEmoticonArray.getiEA().push('/imgs/emoticons/' + imgName);
                }
                break;
        }
    }
    jsWB.preload.preloadImgs(imgEmoticonArray.getiEA());

    //Image pre-loader will go here for the photo gallery.
    jsWB.Accessor(pageName, 'Page', function (x) { return typeof x == 'string' });
    jsWB.Accessor(friendName, 'FN', function (x) { return typeof x == 'string'; });
    jsWB.Accessor(friendID, 'FID', function (x) { return typeof x == 'string'; });
    pageName.setPage(window.location.pathname.substring(window.location.pathname.lastIndexOf('/') + 1));

    // login btn Container.
    lbtn = jsWB.$('lbtnContainer');
    // Venue login btn container
    vlbtn = jsWB.$('vbtnContainer');
    // message btn Container.
    mbtn = jsWB.$('mbtnContainer');
    // login panel
    loginWindow = jsWB.$('logwindow');
    // venue log panel
    vloginWindow = jsWB.$('vlogwindow');
    // message panel
    messageWindow = jsWB.$('mwindow');
    // content container
    content = jsWB.$('contentContainer');
    // arrow button
    arrow = jsWB.$('arrow');
    // home button
    home = jsWB.$('homeBtn');

    addUserBtn = jsWB.getElementsByClassName('addUserbtn_cls', 'img');

    if (loginWindow != null) {
        loginWindow.style.left = jsWB.objPos.returnObjLeft(lbtn) + 'px';
        loginWindow.style.top = jsWB.objPos.returnObjTop(lbtn) + jsWB.objPos.returnObjHeight(lbtn) + 5 + 'px';
    }
    if (vloginWindow != null) {
        vloginWindow.style.left = jsWB.objPos.returnObjLeft(vlbtn) + 'px';
        vloginWindow.style.top = jsWB.objPos.returnObjTop(vlbtn) + jsWB.objPos.returnObjHeight(vlbtn) + 5 + 'px';
    }
    if (messageWindow) {
        messageWindow.style.left = jsWB.objPos.returnObjLeft(mbtn) + 'px';
        messageWindow.style.top = jsWB.objPos.returnObjTop(mbtn) + jsWB.objPos.returnObjHeight(mbtn) + 5 + 'px';
    }

    jsWB.addEvent(lbtn, 'click', function (W3CEvent) {
        jsWB.toggleDisplay(loginWindow, 'inline-block');
        messageWindow.style.display = 'none';
        vloginWindow.style.display = 'none';
    });
    jsWB.addEvent(vlbtn, 'click', function (W3CEvent) {
        jsWB.toggleDisplay(vloginWindow, 'inline-block');
        messageWindow.style.display = 'none';
        loginWindow.style.display = 'none';
    });
    jsWB.addEvent(mbtn, 'click', function (W3CEvent) {
        jsWB.toggleDisplay(messageWindow, 'inline-Block');
        loginWindow.style.display = 'none';
        vloginWindow.style.display = 'none';
    });
    jsWB.addEvent(home, 'click', function (W3CEvent) {
        window.location('/default.aspx')
    });

    switch (pageName.getPage()) {
        case 'photos.aspx':
            jsWB.Accessor(xmlData, 'Data', function (x) { return typeof x == 'object' });
            jsWB.Accessor(pageContainer, 'Container', function (x) { return typeof x == 'object' });

            var root = null;
            var venueList = null;
            var xhr = jsWB.xmlRequest.createRequest("venues");
            xhr.open('GET', '/xmlData/images.xml', false);
            xhr.send(null);

            xmlData.setData(xhr.responseXML);
            pageContainer.setContainer(jsWB.$('imgContainer'));
            var tbl = document.createElement('table');
            for (var i = 0; i < xmlData.getData().documentElement.childNodes.length; i++) {
                var tr = document.createElement('tr');
                var trTitle = document.createElement('tr');
                var tdTitle = document.createElement('td');

                var x = xmlData.getData().documentElement.childNodes[i];
                // x.childNodes[0] is the name of the node.  I will need to place the value of this node in a table cell.  Colspan needs to be set to the number of images in the event image list.
                var a = x.childNodes;
                var b = document.createTextNode(a[0].nodeTypedValue);

                for (var c = 0; c < x.childNodes[2].childNodes.length; c++) {
                    for (var d = 0; d < x.childNodes[2].childNodes[c].childNodes.length; d++) {
                        var f = x.childNodes[2].childNodes[c].childNodes;
                        tdTitle.setAttribute('colSpan', f.length);
                    }
                }
                tdTitle.style.textAlign = 'left';
                tdTitle.appendChild(b);
                trTitle.appendChild(tdTitle);
                tbl.appendChild(trTitle);

                var z = x.childNodes[2];
                var y = z.childNodes;

                for (var k = 0; k < y.length; k++) {
                    var w = y[k].childNodes;

                    for (var index = 0; index < w.length; index++) {
                        if (w[index].nodeName === "imgCollection") {
                            var u = w[index].childNodes;

                            for (var index1 = 0; index1 < u.length; index1++) {
                                var t = u[index1];

                                for (var index2 = 0; index2 < t.childNodes.length; index2++) {
                                    var s = t.childNodes[index2];
                                    if (s.nodeName === 'src') {
                                        var td = document.createElement('td');
                                        var img = document.createElement('img');
                                        img.setAttribute('src', s.nodeTypedValue);
                                        img.style.width = '200px';
                                        td.appendChild(img);
                                        tr.appendChild(td);
                                    }
                                }
                            }
                        }
                    }
                }
                tbl.appendChild(tr);
            }
            pageContainer.getContainer().appendChild(tbl);
            break;
        case 'events.aspx':
            break;
        case 'venues.aspx':
            break;
        case 'yourcity.aspx':
            break;
        case 'blognreview.aspx':
            break;
        case 'default.aspx':
            break;
        case 'vRegistry.aspx':
            jsWB.Accessor(targetAcc, 'Targ', function (x) { return typeof x == 'object' });
            var cp = jsWB.$('colorPlt');
            var tar = null;
            var t = jsWB.getElementsByClassName('clrSelector_cls', 'input');
            for (var i = 0; i < t.length; i++) {
                targetAcc.setTarg(jsWB.$(t[i]));
            }
            jsWB.addEvent(cp, 'click', function (W3CEvent) {
                if (jsWB.$('clrSwatch')) {
                    jsWB.domMgmt.removeChildren(jsWB.$('clrSwatch'));
                    jsWB.domMgmt.removeCurentNode(jsWB.$('clrSwatch'));
                }
                else {
                    jsWB.colorSwatch.colorPalette(targetAcc.getTarg());
                }
            });
            break;
        case 'userPhotos.aspx':
            //var dirCreate = null, dirDelete = null, imgAdd = null, imgDel = null;
            dirCreate = jsWB.$('AddDirectory'), dirDelete = jsWB.$('DeleteDirectory'), imgAdd = jsWB.$('addImg'), imgDel = jsWB.$('deleteFile');
            break;
        case 'userWelcome.aspx':
            for (var j = 0; j < addUserBtn.length; j++) {
                var addUserID = jsWB.$(addUserBtn[j]);

                jsWB.addEvent(addUserID, 'click', function (W3CEvent) {
                    var bs = jsWB.getElementsByClassName('blockscreen2_cls', 'div');
                    for (var z = 0; z < bs.length; z++) {
                        jsWB.Accessor(addWindow, 'Win', function (x) { return typeof x == 'object' || typeof x == null; });
                        addWindow.setWin(bs[z]);
                        addWindow.getWin().style.display = 'block';
                        var closebtn = jsWB.getElementsByClassName('closebtn_cls', 'img');
                        for (var y = 0; y < closebtn.length; y++) {
                            jsWB.addEvent(closebtn[y], 'click', function (W3CEvent) {
                                addWindow.getWin().style.display = 'none';
                            });
                        }
                    }
                });
            }
            break;
        default:
            break;
    }
}, false);
var _fc = null,_fl=null;
jsWB.addEvent(window, 'load', function (W3CEvent) {
    
    /*var fCol = jsWB.$('lUFriends');
    var jSonFCol = JSON.parse('{' + fCol.value + '}');
    for (var i = 0;i<jSonFCol.Friends.length;i++){
        var com = jsWB.$(jSonFCol.Friends[i].Friend);
    }*/

    _fc = jsWB.getElementsByClassName('chatCol', 'input');
    _fl = jsWB.getElementsByClassName('friendList_cls', 'span');
    for (var itemBtn = 0; itemBtn < _fl.length; itemBtn++) {
        _fl[itemBtn].itemIndex = itemBtn;
        jsWB.addEvent(_fl[itemBtn], 'click', function (W3CEvent) {
            var msgBox = jsWB.$('messages');
            msgBox.innerHTML = "";
            msgBox.innerHTML = _fc[this.itemIndex].value;
        });
    }
    var getchatWin = jsWB.getElementsByClassName('friendList_cls', 'span');

    _chatContainer = jsWB.$('container');
    var friend = null;
    for (var i = 0; i < getchatWin.length; i++) {
        friend = jsWB.$(getchatWin[i].id);
        friend.itemIndex = i;
        jsWB.addEvent(friend, 'click', function (W3CEvent) {
            var e = event.srcElement;

            selectedFriend = jsWB.getElementsByClassName('selectedFriend_cls', 'input');
            cfriend = jsWB.$('currFriend');
            cfriend.value = selectedFriend[this.itemIndex].value;

            var hf_Identifier = e.innerHTML.replace('&nbsp;', '_') + "_" + e.id;
            var emot = jsWB.$('emo');
            for (var i = 0; i < imgEmoticonArray.getiEA().length; i++) {
                var emoImg = new Image();
                emoImg.id = 'emo_' + i;
                emoImg.src = imgEmoticonArray.getiEA()[i];
                emoImg.style.height = '21px';
                emoImg.style.width = '21px';
                emot.appendChild(emoImg);
                jsWB.addEvent(emoImg, 'click', function (W3CEvent) {
                    var msgArea = jsWB.$('message');
                    msgArea.innerHTML += '<img id="upImg_' + event.srcElement.id + '" src="' + event.srcElement.src + '" style="height:42px;width:42px;" />';
                });
            }

            friendID.setFID(e.id);
            friendName.setFN(e.innerText);

            if (_chatContainer.style.display === 'none') {
                jsWB.$('NameHolder').innerHTML = friendName.getFN();
                jsWB.$('currentFriend').value = friendID.getFID();

                var userIdentifier = hf_Identifier;
                jsWB.$('messages').innerHTML = jsWB.$(userIdentifier).value;

                _chatContainer.style.top = jsWB.objPos.returnObjTop(e) + 'px';
                _chatContainer.style.left = jsWB.objPos.returnObjLeft(e) + jsWB.objPos.returnObjWidth(e) + 2 + 'px';
                _chatContainer.style.display = 'inline-block';
            }
            else {
                // This has to change
                var userIdentifier = hf_Identifier;
                jsWB.$(userIdentifier).value = jsWB.$('messages').innerHTML;
                jsWB.$('messages').innerHTML = '';
                _chatContainer.style.display = 'none';
            }
        });
    }
});

jsWB.addLoadEvent(function (W3CEvent) {
    _listContainer = jsWB.$('listContainer');
    _imgBtn = jsWB.$('ddImage');
    _sEvent = jsWB.$('selectedEvent');
    _eHolder = jsWB.$('sEID');

    inviteBtn = jsWB.$('inviteBtn');
    iBack = jsWB.$('inviteBack');

    _eventItems = jsWB.getElementsByClassName('tblDDL_cls', 'TABLE');

    _dc = jsWB.getElementsByClassName('dntContainer_cls', 'div');
    _dn = jsWB.getElementsByClassName('dirbtn_cls', 'a');

    footLinks = jsWB.getElementsByClassName('footLinks_cls', 'SPAN');
    winclose = jsWB.getElementsByClassName('Close_cls', 'SPAN');

    _epost = jsWB.$('eventDescription');
    _evalue = jsWB.$('Div1');
    _sendE = jsWB.$('sendEvent');

    _postBox = jsWB.$('PostBox');
    _postValue = jsWB.$('postValue');
    _sendPost = jsWB.$('sendPost');
    if (inviteBtn) {
        inviteBtn.Target = iBack;
        jsWB.addEvent(inviteBtn, 'click', function (W3CEvent) {
            var closeBtn = jsWB.$('cmdInviteBtn');
            var innerBox = jsWB.$('innerInvite');
            this.Target.style.display = 'inline-block';

            var bws = jsWB.getBrowserWindowSize;
            var top = ((bws.height - 300) / 2) || 0;
            var left = ((bws.width - 500) / 2) || 0;
            innerBox.style.top = top + 'px';
            innerBox.style.left = left + 'px';

            closeBtn.closeTarget = this.Target;
            jsWB.addEvent(closeBtn, 'click', function (W3CEvent) {
                inviteTB = jsWB.getElementsByClassName('userInvite_cls', 'TEXTAREA');
                this.closeTarget.style.display = 'none';
                for (var i = 0; i < inviteTB.length; i++) {
                    inviteTB[i].value = '';
                }
            });
        });
    }
    // user posts buttons
    if (_postBox === 'null' || _postBox === 'undefined') {
        return;
    }
    else {
        jsWB.addEvent(_postBox, 'blur', function (W3CEvent) {
            _postValue.value = _postBox.innerHTML;
        });
        jsWB.addEvent(_sendPost, 'focus', function (W3CEvent) {
            _postValue.value = _postBox.innerHTML;
        });
    }

    if (_epost === 'null' || _epost === 'undefined') {
        return;
    }
    else {
        jsWB.addEvent(_evalue, 'blur', function (W3CEvent) {
            _epost.value = _evalue.innerHTML;
        });
        jsWB.addEvent(_sendE, 'focus', function (W3CEvent) {
            _evalue.value = _epost.innerHTML;
        })
    }

    // friend list postioning
    if (_listContainer === null || _listContainer === 'undefined') {
        return;
    }
    else {
        _listContainer.style.width = jsWB.objPos.returnObjWidth(_sEvent) + jsWB.objPos.returnObjWidth(_imgBtn) + 'px';
        _listContainer.style.top = jsWB.objPos.returnObjTop(_sEvent) + jsWB.objPos.returnObjHeight(_sEvent) + 'px';
        _listContainer.style.left = jsWB.objPos.returnObjLeft(_sEvent) + 'px';

        jsWB.addEvent(window, 'resize', function (W3CEvent) {
            _listContainer.style.width = jsWB.objPos.returnObjWidth(_sEvent) + jsWB.objPos.returnObjWidth(_imgBtn) + 'px';
            _listContainer.style.top = jsWB.objPos.returnObjTop(_sEvent) + jsWB.objPos.returnObjHeight(_sEvent) + 'px';
            _listContainer.style.left = jsWB.objPos.returnObjLeft(_sEvent) + 'px';
        });
        jsWB.addEvent(_imgBtn, 'click', function (W3CEvent) {
            jsWB.toggleDisplay(_listContainer, 'inline-block');
            _listContainer.style.width = jsWB.objPos.returnObjWidth(_sEvent) + jsWB.objPos.returnObjWidth(_imgBtn) + 'px';
            _listContainer.style.top = jsWB.objPos.returnObjTop(_sEvent) + jsWB.objPos.returnObjHeight(_sEvent) + 'px';
            _listContainer.style.left = jsWB.objPos.returnObjLeft(_sEvent) + 'px';

            jsWB.addEvent(window, 'resize', function (W3CEvent) {
                _listContainer.style.width = jsWB.objPos.returnObjWidth(_sEvent) + jsWB.objPos.returnObjWidth(_imgBtn) + 'px';
                _listContainer.style.top = jsWB.objPos.returnObjTop(_sEvent) + jsWB.objPos.returnObjHeight(_sEvent) + 'px';
                _listContainer.style.left = jsWB.objPos.returnObjLeft(_sEvent) + 'px';
            });
        });
        for (var i = 0; i < _eventItems.length; i++) {
            eventItem = jsWB.$(_eventItems[i]);
            jsWB.addEvent(eventItem, 'click', function (W3CEvent) {
                var e = event.srcElement;
                var ddlTbl = e.parentNode.parentNode.parentNode;
                jsWB.addEvent(ddlTbl, 'click', function (W3CEvent) {
                    _sEvent.innerHTML = ddlTbl.parentNode.innerHTML;
                    _eHolder.value = ddlTbl.id;
                    _listContainer.style.display = 'none';
                })
            });
        }
    };

    // gallery images.
    switch (pageName.getPage()) {
        case 'register.aspx':
            var pwds = jsWB.getElementsByClassName('regFormpwd_cls', 'INPUT');
            jsWB.addEvent(pwds[1], 'change', function (W3CEvent) {
                var pwdErr = jsWB.$('err');
                if (pwds[0].value != pwds[1].value) {
                    pwdErr.style.display = 'block';
                    pwdErr.innerText = "Passwords do not match";
                    pwds[0].value = null;
                    pwds[1].value = null;
                    return;
                }
                else {
                    pwdErr.style.display = 'none';
                }
            })
            break;
        case 'userWelcome.aspx':
            break;
    };
    for (var i1 = 0; i1 < footLinks.length; i1++) {
        linq = jsWB.$(footLinks[i1].id);
        jsWB.addEvent(linq, 'click', function (W3CEvent) {
            var e = event.srcElement;
            //jsWB.log.write(e.getAttribute('itemid'));
            jsWB.newWindow.openWindow(e.getAttribute('itemid'), 'aspx', 'width=800,height=800,menubar=no,resiable=yes,status=yes,titlebar=yes,toolbar=no,scrollbars=yes');
        })
    }
    for (var i2 = 0; i2 < winclose.length; i2++) {
        jsWB.addEvent(winclose[i2], 'click', function (W3CEvent) {
            window.close();
        });
    }
}, true);