﻿(function () {
    if (!window.jsIM) { window['jsIM'] = {} }

    function getDimensions(e) {
        return {
            top: e.offsetTop,
            left: e.offsetLeft,
            width: e.offsetWidth,
            height: e.offsetHeight
        };
    };
    window['jsIM']['getDimensions'] = getDimensions;

    function setNumericStyle(e, dim, updateMessage) {
        updateMessage = updateMessage || false;

        var style = {};
        for (var i in dim) {
            if (!dim.hasOwnProperty(i)) continue;
            style[i] = (dim[i] || '0') + 'px';
        }
        jsIM.setStyle(e, style);

        if (updateMessage) {
            e.elements.cropSizeDisplay.firstChild.nodeValue = dim.width + 'x' + dim.height;
        }
    };
    window['jsIM']['setNumericStyle'] = setNumericStyle;

    function isCompatible(other) {
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        } return true;
    }
    window['jsIM']['isCompatible'] = isCompatible;

    function isArray(myArray) {
        return myArray.constructor.toString().indexOf('Array') > 1;
    }
    window['jsIM']['isArray'] = isArray;

    function preventDefault(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        } else {
            eventObject.returnValue = false;
        }
    };
    window['jsIM']['preventDefault'] = preventDefault;

    function stopPropagation(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        } else {
            eventObject.cancelBubble = true;
        }
    };
    window['jsIM']['stopPropagation'] = stopPropagation;

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
    window['jsIM']['addLoadEvent'] = addLoadEvent;

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
    window['jsIM']['addEvent'] = addEvent;

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
    window['jsIM']['Accessor'] = Accessor;

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
    window['jsIM']['extend'] = extend;

    function camelize(s) {
        return s.replace(/-(\w)/g, function (strMatch, p1) {
            return p1.toUpperCase();
        });
    }
    window['jsIM']['camelize'] = camelize;

    function uncamelize(s, sep) {
        sep = sep || '-';
        return s.replace(/([a-z])([A-Z])/g, function (strMatch, p1, p2) {
            return p1 + sep + p2.toLowerCase();
        });
    }
    window['jsIM']['camelize'] = camelize;

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
    window['jsIM']['$'] = $;

    function getEventObject(w3CEvent) {
        return w3CEvent || window.event;
    }
    window['jsIM']['getEventObject'] = getEventObject;

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
    window['jsIM']['removeEvent'] = removeEvent;

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
    window['jsIM']['getElementsByClassName'] = getElementsByClassName;

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
    window['jsIM']['toggleDisplay'] = toggleDisplay;

    function insertAfter(node, referenceNode) {
        if (!(node = $(node))) { return false; }
        if (!(referenceNode = $(referenceNode))) { return false; }
        return referenceNode.parentNode.insertBefore(
            node, referenceNode.nextSibling);
    };
    window['jsIM']['insertAfter'] = insertAfter;

    function domMgmt() {
        this.removeChildren = function (parent) {
            if (!(parent = $(parent))) { return false; }
            while (parent.firstChild) {
                parent.firstChild.parentNode.removeChild(parent.firstChild);
            }
            return parent;
        };
        this.removeChildrenException = function (parent, exception) {
            if (!(parent = $(parent))) { return false; }
            if (parent.firstChild.id == exception) {
                while (parent.firstChild.nextSibling) {
                    parent.firstChild.parentNode.removeChild(parent.firstChild.nextSibling);
                }
            }
            else {
                while (parent.firstChild) {
                    parent.firstChild.parentNode.removeChild(parent.firstChild);
                }
            }
            return parent;
        };
        this.removeCurentNode = function (elem) {
            var elem2Remove = (!jsIM.$(elem)) ? elem : jsIM.$(elem);
            if (elem2Remove != null
            || elem2Remove != 'undefined') {
                var f = elem2Remove.parentNode;
                if (f == 'undefined' || f == undefined) { return false; }
                else { f.removeChild(elem2Remove); }
            }
            return elem;
        };
    };
    window['jsIM']['domMgmt'] = new domMgmt();

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
    window['jsIM']['prependChild'] = prependChild;

    function objPos() {
        this.setObjPos = function (obj, relativeObj) {
            var f = jsIM.DomOopObjects.$(relativeObj);
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
    window['jsIM']['objPos'] = new objPos();

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
    if (!window.jsIM) { window['jsIM'] = {}; }
    window['jsIM']['log'] = new myLogger();

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
    window['jsIM']['getBrowserWindowSize'] = new getBrowserWindowSize();

    function getPointerPositionInDocument(eventObject) {
        eventObject = eventObject || getEventObject(eventObject);

        var x = eventObject.pageX || (eventObject.clientX + (document.documentElement.scrollLeft || document.body.scrollLeft));

        var y = eventObject.pageY || (eventObject.clientY + (document.documentElement.scrollTop || document.body.scrollTop));

        return { 'x': x, 'y': y };
    }
    window['jsIM']['getPointerPositionInDocument'] = getPointerPositionInDocument;

    function getMyBoundingClientRect(objRectangle) {
        var _can = objRectangle.getBoundingClientRect();
        return { 'x': _can.left, 'y': _can.top, 'height': _can.height, 'width': _can.width };
    };
    window['jsIM']['getMyBoundingClientRect'] = getMyBoundingClientRect;

    function getKeyPressed(eventObject) {
        eventObject = eventObject || getEventObject(eventObject);

        var code = eventObject.keyCode;
        var value = String.fromCharCode(code);
        return { 'code': code, 'value': value };
    }
    window['jsIM']['getKeyPressed'] = getKeyPressed;

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
    window['jsIM']['setStyle'] = setStyleById;
    window['jsIM']['setStyleById'] = setStyleById;

    function setStylesByClassName(parent, tag, className, styles) {
        if (!(parent = $(parent))) return false;
        var elements = getElementsByClassName(className, tag, parent);
        for (var e = 0; e < elements.length; e++) {
            setStyleById(elements[e], styles);
        }
        return true;
    }
    window['jsIM']['setStylesByClassName'] = setStylesByClassName;

    function setStylesByTagName(tagName, styles, parent) {
        parent = $(parent) || document;
        var elements = parent.getElementsByTagName(tagName);
        for (var e = 0; e < elements.length; e++) {
            setStylesById(elements[e], styles);
        }
    }
    window['jsIM']['setStylesByTagName'] = setStylesByTagName;

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
    window['jsIM']['getWindowSize'] = getWindowSize;

    // Canvas functions will now follow:
    function degree2rad(degree) {
        return Math.PI * degree / 180;
    };
    window['jsIM']['degree2rad'] = new degree2rad();

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
    window['jsIM']['xmlRequest'] = new xmlRequest();

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
            jsIM.Accessor(esrc, 'e', function (x) { return typeof x == 'object'; });
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
                        jsIM.addEvent(ccell, 'mouseover', function (W3CEvent) {
                            var e = event.srcElement;
                            var target = jsIM.$('clrIO');
                            target.value = e.style.backgroundColor;
                            td1.style.backgroundColor = e.style.backgroundColor;
                        })
                        jsIM.addEvent(ccell, 'mouseout', function (W3CEvent) {
                            var target = jsIM.$('clrIO');
                            target.value = '';
                            td1.style.backgroundColor = '';
                        });
                        jsIM.addEvent(ccell, 'click', function (W3CEvent) {
                            var e1 = event.srcElement;
                            jsIM.preventDefault(ccell);
                            jsIM.stopPropagation(ccell);

                            if (esrc.gete().nodeName === 'INPUT') {
                                esrc.gete().value = e1.style.backgroundColor;
                                esrc.gete().style.backgroundColor = e1.style.backgroundColor;

                                jsIM.domMgmt.removeChildren(jsIM.$('clrSwatch'));
                                jsIM.domMgmt.removeCurentNode(jsIM.$('clrSwatch'));
                            }

                            else if (esrc.gete().nodeName === 'DIV') {
                                esrc.gete().style.backgroundColor = e1.style.backgroundColor;

                                handleSelection(e1.style.backgroundColor, selection);

                                jsIM.domMgmt.removeChildren(jsIM.$('clrSwatch'));
                                jsIM.domMgmt.removeCurentNode(jsIM.$('clrSwatch'));
                            }
                            else {
                                jsIM.domMgmt.removeChildren(jsIM.$('clrSwatch'));
                                jsIM.domMgmt.removeCurentNode(jsIM.$('clrSwatch'));
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
            container.style.top = jsIM.objPos.returnObjTop(esrc.gete()) + jsIM.objPos.returnObjHeight(esrc.gete()) + 'px';
            container.style.left = jsIM.objPos.returnObjLeft(esrc.gete()) + 'px';
            jsIM.addEvent(window, 'resize', function (W3CEvent) {
                container.style.top = jsIM.objPos.returnObjTop(esrc.gete()) + jsIM.objPos.returnObjHeight(esrc.gete()) + 'px';
                container.style.left = jsIM.objPos.returnObjLeft(esrc.gete()) + 'px';
            });
            container.setAttribute('class', 'clrContainer_cls');
            document.body.appendChild(container);
        }
    }
    if (!window.jsIM) { window['jsIM'] = {}; }
    window['jsIM']['colorSwatch'] = new colorSwatch();

    function newWindow() {
        this.openWindow = function (link, postfix, args) {
            var nw = window.open(link + '.' + postfix, link, args);
        }
    }
    if (!window.jsIM) { window['jsIM'] = {}; }
    window['jsIM']['newWindow'] = new newWindow();

    function asyncImport() {
        var fileElem = null;
        var x = 0, y = 0;
        var geoaddress = null;
        var scriptKey = null;
        this.init = function (fileName, fileType, sKey) {
            if (sKey != scriptKey) {
                if (fileType == 'js') {
                    fileElem = document.createElement('SCRIPT');
                    fileElem.setAttribute('type', 'text/javascript');
                    fileElem.setAttribute('src', fileName);
                }
                if (fileType == 'css') {
                    fileElem = document.createElement('LINK');
                    fileElem.setAttribute('rel', 'stylesheet');
                    fileElem.setAttribute('type', 'text/css');
                    fileElem.setAttribute('href', fileName);
                }
                if (typeof fileElem != 'undefined') {
                    document.getElementsByTagName('head')[0].appendChild(fileElem);
                }
                scriptKey = sKey;
            }
        };
    }
    window['jsIM']['asyncImport'] = new asyncImport();

    function subdir() {
        var fileCollection = function (iContainer,data) {
            if (data.hasOwnProperty('Files')) {
                var files = data.Files;
                for (var f = 0; f < files.length; f++) {
                    var iCon = document.createElement('DIV');
                    iCon.style.display = 'inline-block';
                    iCon.style.padding = '1px 1px 1px 1px';
                    var fImg = document.createElement('IMG');
                    fImg.src = files[f].FilePath;
                    fImg.style.width = '72px';
                    fImg.style.height = '72px';
                    fImg.style.cursor = 'pointer';
                    jsIM.addEvent(fImg, 'click', function (W3CEvent) {
                        var e = event.srcElement;
                        jsIM.log.write(e.src);
                    });
                    iCon.appendChild(fImg);
                    fCon.appendChild(iCon);
                }
                ftd.appendChild(fCon);
                iContainer.appendChild(ftd);
            }
        }
        this.rootContainer = function(Container,colspan){
            var tRootRow = document.createElement('TR');
            var rootTd = document.createElement('TD');
            rootTd.id = 'rootDir';
            if (colspan.hasOwnProperty('directories')) {
                rootTd.colSpan = colspan.directories.length;
            }
            else {
                rootTd.colSpan = '1';
            }
            rootTd.style.cursor = 'pointer';
            rootTd.style.padding = '5px 5px 5px 5px';
            rootTd.style.backgroundColor = '#24323e';
            rootTd.style.color = '#ffffff';
            rootTd.style.fontFamily = "'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif";
            rootTd.style.fontSize = '9pt';

            var rimg = document.createElement('IMG');
            rimg.style.width = '25px';
            rimg.src = '../imgs/imgFolder.png';
            rootTd.appendChild(rimg);
            var rootTxt = document.createTextNode('...');
            rootTd.appendChild(rootTxt);

            var dLbl = document.createElement('SPAN');
            dLbl.id = 'currDir';
            var dirLbl = document.createTextNode(' Root Folder');
            dLbl.appendChild(dirLbl);
            rootTd.appendChild(dLbl);

            jsIM.addEvent(rootTd, 'click', function (W3CEvent) {
                jsIM.subdir.menuContainer(Container, jDataStore.getData());
            });

            tRootRow.appendChild(rootTd);
            Container.appendChild(tRootRow);
        }
        this.menuContainer = function (container, data) {
            jsIM.domMgmt.removeChildren(container);

            var root = jsIM.$('rootDir');

            jsIM.subdir.rootContainer(container, data);

            if (data.hasOwnProperty('directories')) {
                var dir = data.directories;
                var trowMenu = document.createElement('TR');

                for (var m = 0; m < dir.length; m++) {
                    var tcell = document.createElement('TD');

                    tcell.style.cursor = 'pointer';
                    tcell.style.padding = '5px 5px 5px 5px';
                    tcell.style.backgroundColor = '#24323e';
                    tcell.style.color = '#ffffff';
                    tcell.style.fontFamily = "'Lucida Sans', 'Lucida Sans Regular', 'Lucida Grande', 'Lucida Sans Unicode', Geneva, Verdana, sans-serif";
                    tcell.style.fontSize = '9pt';

                    var imgFolder = document.createElement('IMG');
                    imgFolder.src = '../imgs/imgFolder.png';
                    imgFolder.style.width = '20px';
                    tcell.appendChild(imgFolder);

                    var dirName = document.createTextNode(" " + dir[m].DirectoryName);
                    tcell.appendChild(dirName);

                    tcell.Data = dir[m];
                    tcell.ContainingElem = container;
                    jsIM.addEvent(tcell, 'click', function (W3CEvent) {
                        jsIM.subdir.menuContainer(this.ContainingElem, this.Data);
                    });
                    trowMenu.appendChild(tcell);
                }
                container.appendChild(trowMenu);
            }
            if (data.hasOwnProperty('Files')) {
                var files = data.Files;
                var trowImgs = document.createElement('TR');
                iTdCell = document.createElement('TD');
                if (data.hasOwnProperty('directories')) {
                    iTdCell.colSpan = data.directories.length;
                }
                else {
                    iTdCell.colSpan = '1';
                }
                //iTdCell.colSpan = dir.length;
                iTdCell.style.backgroundColor = "rgba(36, 50, 62,0.75)";
                for (var i = 0; i < files.length; i++) {
                    var iCell = document.createElement('DIV');

                    iCell.style.display = 'inline-block';
                    iCell.style.padding = '0px 2px 0px 2px';

                    var img = document.createElement('IMG');
                    img.src = files[i].FilePath;
                    img.style.width = '74px';
                    img.style.height = '74px';
                    img.style.cursor = 'pointer';

                    jsIM.addEvent(img, 'click', function (W3CEvent) {
                        var e = event.srcElement;
                        _pbo.getpItem().innerHTML += '<img style=\'width:200px;\' src=\'' + e.src + '\'/><br />';
                        jsIM.domMgmt.removeCurentNode(containerObj.getcItem());
                    });

                    iCell.appendChild(img);
                    iTdCell.appendChild(iCell);
                }
                trowImgs.appendChild(iTdCell);
                container.appendChild(trowImgs);
            }
        }
    }
    window['jsIM']['subdir'] = new subdir();
})();
var ic = null, pbox=null, imgs = null, _btn = null, jsonData = null, jDataStore = {}, containerObj = {}, _pbo = {};
jsIM.addEvent(window, 'load', function (W3CEvent) {
    ic = jsIM.getElementsByClassName("imgCol_cls", 'INPUT');
    _btn = jsIM.$('imgBtn');

    pbox = jsIM.getElementsByClassName('InfoBox_cls', 'DIV');

    jsIM.Accessor(_pbo, 'pItem', function (x) { return typeof x == 'object' || typeof x=='null'; });

    for (var infoItem = 0; infoItem < pbox.length; infoItem++) {
        _pbo.setpItem(jsIM.$(pbox[infoItem].id));
    }
    if (ic.length > 0) {
        for (var i = 0; i < ic.length; i++) {
            imgs = jsIM.$(ic[i].id);
        }
        jData = JSON.parse("{" + imgs.value + "}");

        jsIM.Accessor(jDataStore, 'Data', function (x) { return typeof x == 'object'; });
        jDataStore.setData(jData);


        _btn.Data = jData;
        jsIM.addEvent(_btn, 'click', function (W3CEvent) {
            var e = event.srcElement;
            //
            if (!jsIM.$('dirContainerObj')) {
                var container = document.createElement('DIV');
                container.style.width = '400px';
                container.id = 'dirContainerObj';
                container.style.position = 'absolute';
                container.style.left = jsIM.objPos.returnObjLeft(e) + 'px';
                container.style.top = jsIM.objPos.returnObjTop(e) + jsIM.objPos.returnObjHeight(e) + 'px';

                jsIM.Accessor(containerObj, 'cItem', function (x) { return typeof x == 'object'; });
                containerObj.setcItem(container);

                var primtbl = document.createElement('TABLE');
                primtbl.style.width = '100%';

                // This is the root directory.
                jsIM.subdir.rootContainer(primtbl, this.Data);
                // directories
                jsIM.subdir.menuContainer(primtbl, this.Data);

                container.appendChild(primtbl);
                document.body.appendChild(container);
            }
            else {
                jsIM.domMgmt.removeCurentNode(containerObj.getcItem());
            }
        });
    }   
});