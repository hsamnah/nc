(function () {
    if (!window.jsEGS) { window['jsEGS'] = {} }

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
                return this.writeRaw('js.log: null message');
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
    if (!window.jsEGS) { window['jsEGS'] = {}; }
    window['jsEGS']['log'] = new myLogger();

    function isCompatible(other) {
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        } return true;
    }
    window['jsEGS']['isCompatible'] = isCompatible;

    function preventDefault(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        } else {
            eventObject.returnValue = false;
        }
    };
    window['jsEGS']['preventDefault'] = preventDefault;

    function stopPropagation(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        } else {
            eventObject.cancelBubble = true;
        }
    };
    window['jsEGS']['stopPropagation'] = stopPropagation;

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
    window['jsEGS']['$'] = $;

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
    window['jsEGS']['Accessor'] = Accessor;

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
    window['jsEGS']['getElementsByClassName'] = getElementsByClassName;

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
    window['jsEGS']['addEvent'] = addEvent;

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
    window['jsEGS']['addLoadEvent'] = addLoadEvent;

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
    window['jsEGS']['removeEvent'] = removeEvent;

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
    window['jsEGS']['toggleDisplay'] = toggleDisplay;

    function objPos() {
        this.setObjPos = function (obj, relativeObj) {
            var f = jsEGS.DomOopObjects.$(relativeObj);
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
    window['jsEGS']['objPos'] = new objPos();

    function insertAfter(node, referenceNode) {
        if (!(node = $(node))) { return false; }
        if (!(referenceNode = $(referenceNode))) { return false; }
        return referenceNode.parentNode.insertBefore(
            node, referenceNode.nextSibling);
    };
    window['jsEGS']['insertAfter'] = insertAfter;

    function domMgmt() {
        this.removeChildren = function (parent) {
            if (!(parent = $(parent))) { return false; }
            while (parent.firstChild) {
                parent.firstChild.parentNode.removeChild(parent.firstChild);
            }
            return parent;
        };
        this.removeCurentNode = function (elem) {
            var elem2Remove = (!jsEGS.$(elem)) ? elem : jsEGS.$(elem);
            if (elem2Remove != null
            || elem2Remove != 'undefined') {
                var f = elem2Remove.parentNode;
                if (f == 'undefined' || f == undefined) { return false; }
                else { f.removeChild(elem2Remove); }
            }
            return elem;
        };
    };
    window['jsEGS']['domMgmt'] = new domMgmt();

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
    window['jsEGS']['prependChild'] = prependChild;

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
    window['jsEGS']['getBrowserWindowSize'] = new getBrowserWindowSize();

    function showEventImg(target, topPos, leftPos) {
        var f = target;
        f.setAttribute('style', 'display:inline-block;left:' + leftPos + 'px;top:' + topPos + 'px;');
        for (var k = 0; k < f.children.length; k++) {
            f.children[k].setAttribute('style', 'display:inline-block;width:435px;');
        }
    }
    window['jsEGS']['showEventImg'] = showEventImg;

    function winOpen(url, arguements, parameters) {
        var wo = window.open(url + '?' + arguements, '_blank', parameters);
    }
    window['jsEGS']['winOpen'] = winOpen;
})();
var eCard = null, eImg = null, itemsHeight = 0, colHeight = 0, defaultContainer = null, omicron = null;
var itemHeight = 0, imgHeight = 0, cellHeight = 0, colHeight = 0, colHeight2 = 0;
var defaultCell = {}, cell = {}, imgs = {}, imgItems = [], cellItems = [], spanItems = [], inputItem = [];

jsEGS.addEvent(window, 'load', function (W3CEvent) {
    defaultContainer = jsEGS.getElementsByClassName('primary_event_container_cls', 'div');
    omicron = jsEGS.getElementsByClassName('primary_cls', 'input');

    jsEGS.Accessor(defaultCell, 'Col', function (x) { return typeof x == 'object' || typeof x == 'null'; });
    jsEGS.Accessor(cell, 'Col0', function (x) { return typeof x == 'object' || typeof x == null; });
    jsEGS.Accessor(cell, 'Col1', function (x) { return typeof x == 'object' || typeof x == null; });
    jsEGS.Accessor(imgs, 'Img0', function (x) { return typeof x == 'object' || typeof x == null; });
    jsEGS.Accessor(imgs, 'Img1', function (x) { return typeof x == 'object' || typeof x == null; });

    if (defaultContainer.toString().length == 0) {
        return;
    }
    else {
        for (var dIndex = 0; dIndex < defaultContainer.length; dIndex++) {
            var item = defaultContainer[dIndex].children;
            for (var index = 0; index < item.length; index++) {
                if (item[index].tagName == 'IMG') {
                    imgItems.push(item[index]);
                }
                else if (item[index].tagName == 'DIV') {
                    cellItems.push(item[index]);
                }
                else if (item[index].tagName == 'SPAN') {
                    spanItems.push(item[index]);
                }
                else if (item[index].tagName == 'INPUT') {
                    inputItem.push(item[index]);
                }
            }
            for (var i = 0; i < imgItems.length; i++) {
                if (i == 0) {
                    imgItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + 'px';
                    imgItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                    cellItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + 'px';
                    cellItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                    spanItems[i].style.left = jsEGS.objPos.returnObjLeft(cellItems[i]) + 'px';
                    spanItems[i].style.top = jsEGS.objPos.returnObjTop(cellItems[i]) + jsEGS.objPos.returnObjHeight(cellItems[i]) + 'px';

                    colHeight = (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                    cell.setCol0(cellItems[i]);
                    imgs.setImg0(imgItems[i]);
                }
                else if (i == 1) {
                    imgItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + jsEGS.objPos.returnObjWidth(imgs.getImg0()) + 'px';
                    imgItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                    cellItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + jsEGS.objPos.returnObjWidth(cell.getCol0()) + 'px';
                    cellItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                    spanItems[i].style.left = jsEGS.objPos.returnObjLeft(cellItems[i]) + 'px';
                    spanItems[i].style.top = jsEGS.objPos.returnObjTop(cellItems[i]) + jsEGS.objPos.returnObjHeight(cellItems[i]) + 'px';

                    colHeight2 = (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                    cell.setCol1(cellItems[i]);
                    imgs.setImg1(imgItems[i]);
                }
                else if (i % 2 == 0) {
                    imgHeight = jsEGS.objPos.returnObjHeight(imgs.getImg0());
                    cellHeight = jsEGS.objPos.returnObjHeight(cell.getCol0());
                    itemHeight = (cellHeight > imgHeight) ? cellHeight : imgHeight;

                    imgItems[i].style.left = jsEGS.objPos.returnObjLeft(imgs.getImg0()) + 'px';
                    imgItems[i].style.top = jsEGS.objPos.returnObjTop(imgs.getImg0()) + itemHeight + 'px';

                    cellItems[i].style.left = jsEGS.objPos.returnObjLeft(cell.getCol0()) + 'px';
                    cellItems[i].style.top = jsEGS.objPos.returnObjTop(cell.getCol0()) + itemHeight + 'px';

                    spanItems[i].style.left = jsEGS.objPos.returnObjLeft(cellItems[i]) + 'px';
                    spanItems[i].style.top = jsEGS.objPos.returnObjTop(cellItems[i]) + jsEGS.objPos.returnObjHeight(cellItems[i]) + 'px';

                    colHeight += (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                    cell.setCol0(cellItems[i]);
                    imgs.setImg0(imgItems[i]);
                }
                else {
                    imgHeight = jsEGS.objPos.returnObjHeight(imgs.getImg1());
                    cellHeight = jsEGS.objPos.returnObjHeight(cell.getCol1());
                    itemHeight = (cellHeight > imgHeight) ? cellHeight : imgHeight;

                    imgItems[i].style.left = jsEGS.objPos.returnObjLeft(imgs.getImg1()) + 'px';
                    imgItems[i].style.top = jsEGS.objPos.returnObjTop(imgs.getImg1()) + itemHeight + 'px';

                    cellItems[i].style.left = jsEGS.objPos.returnObjLeft(cell.getCol1()) + 'px';
                    cellItems[i].style.top = jsEGS.objPos.returnObjTop(cell.getCol1()) + itemHeight + 'px';

                    spanItems[i].style.left = jsEGS.objPos.returnObjLeft(cellItems[i]) + 'px';
                    spanItems[i].style.top = jsEGS.objPos.returnObjTop(cellItems[i]) + jsEGS.objPos.returnObjHeight(cellItems[i]) + 'px';

                    colHeight2 += (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                    cell.setCol1(cellItems[i]);
                    imgs.setImg1(imgItems[i]);
                }
                spanItems[i].eID = inputItem[i].value;
                jsEGS.addEvent(spanItems[i], 'click', function (W3CEvent) {
                    jsEGS.winOpen('/UserItems/guestServices.aspx', 'm=' + this.eID, 'height=755,width=1000,menubar=no,scrollbars=no,status=no,titlebar=no,toolbar=no');
                });
                jsEGS.addEvent(imgItems[i], 'mouseover', function (W3CEvent) {
                    var k = event.srcElement;
                    k.style.visibility = 'hidden';
                });
                cellItems[i].img = imgItems[i];
                spanItems[i].img = imgItems[i];
                jsEGS.addEvent(cellItems[i], 'mousemove', function (W3CEvent) {
                    var k = this.img;
                    k.style.visibility = 'hidden';
                });
                jsEGS.addEvent(spanItems[i], 'mousemove', function (W3CEvent) {
                    var k = this.img;
                    k.style.visibility = 'hidden';
                });
                jsEGS.addEvent(imgItems[i], 'mouseout', function (W3CEvent) {
                    var k = event.srcElement;
                    k.style.visibility = 'visible';
                });
                jsEGS.addEvent(cellItems[i], 'mouseout', function (W3CEvent) {
                    var k = this.img;
                    k.style.visibility = 'visible';
                });
                jsEGS.addEvent(spanItems[i], 'mouseout', function (W3CEvent) {
                    var k = this.img;
                    k.style.visibility = 'visible';
                });
            }
            imgItems = [];
            cellItems = [];
            spanItems = [];
            inputItem = [];
            var containerHeight = (colHeight > colHeight2) ? colHeight : colHeight2;
            defaultContainer[dIndex].style.height = containerHeight + 'px';
            colHeight = 0;
            colHeight2 = 0;
            containerHeight = 0;
            cell.setCol0(null);
            imgs.setImg0(null);
            cell.setCol1(null);
            imgs.setImg1(null);
        }
        jsEGS.addEvent(window, 'resize', function (W3CEvent) {
            for (var dIndex = 0; dIndex < defaultContainer.length; dIndex++) {
                var item = defaultContainer[dIndex].children;
                for (var index = 0; index < item.length; index++) {
                    if (item[index].tagName == 'IMG') {
                        imgItems.push(item[index]);
                    }
                    else if (item[index].tagName == 'DIV') {
                        cellItems.push(item[index]);
                    }
                }
                for (var i = 0; i < imgItems.length; i++) {
                    if (i == 0) {
                        imgItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + 'px';
                        imgItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                        cellItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + 'px';
                        cellItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                        colHeight = (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                        cell.setCol0(cellItems[i]);
                        imgs.setImg0(imgItems[i]);
                    }
                    else if (i == 1) {
                        imgItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + jsEGS.objPos.returnObjWidth(imgs.getImg0()) + 'px';
                        imgItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                        cellItems[i].style.left = jsEGS.objPos.returnObjLeft(defaultContainer[dIndex]) + jsEGS.objPos.returnObjWidth(cell.getCol0()) + 'px';
                        cellItems[i].style.top = jsEGS.objPos.returnObjTop(defaultContainer[dIndex]) + 'px';

                        colHeight2 = (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                        cell.setCol1(cellItems[i]);
                        imgs.setImg1(imgItems[i]);
                    }
                    else if (i % 2 == 0) {
                        imgHeight = jsEGS.objPos.returnObjHeight(imgs.getImg0());
                        cellHeight = jsEGS.objPos.returnObjHeight(cell.getCol0());
                        itemHeight = (cellHeight > imgHeight) ? cellHeight : imgHeight;

                        imgItems[i].style.left = jsEGS.objPos.returnObjLeft(imgs.getImg0()) + 'px';
                        imgItems[i].style.top = jsEGS.objPos.returnObjTop(imgs.getImg0()) + itemHeight + 'px';

                        cellItems[i].style.left = jsEGS.objPos.returnObjLeft(cell.getCol0()) + 'px';
                        cellItems[i].style.top = jsEGS.objPos.returnObjTop(cell.getCol0()) + itemHeight + 'px';

                        colHeight += (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                        cell.setCol0(cellItems[i]);
                        imgs.setImg0(imgItems[i]);
                    }
                    else {
                        imgHeight = jsEGS.objPos.returnObjHeight(imgs.getImg1());
                        cellHeight = jsEGS.objPos.returnObjHeight(cell.getCol1());
                        itemHeight = (cellHeight > imgHeight) ? cellHeight : imgHeight;

                        imgItems[i].style.left = jsEGS.objPos.returnObjLeft(imgs.getImg1()) + 'px';
                        imgItems[i].style.top = jsEGS.objPos.returnObjTop(imgs.getImg1()) + itemHeight + 'px';

                        cellItems[i].style.left = jsEGS.objPos.returnObjLeft(cell.getCol1()) + 'px';
                        cellItems[i].style.top = jsEGS.objPos.returnObjTop(cell.getCol1()) + itemHeight + 'px';

                        colHeight2 += (jsEGS.objPos.returnObjHeight(cellItems[i]) > jsEGS.objPos.returnObjHeight(imgItems[i])) ? jsEGS.objPos.returnObjHeight(cellItems[i]) : jsEGS.objPos.returnObjHeight(imgItems[i]);

                        cell.setCol1(cellItems[i]);
                        imgs.setImg1(imgItems[i]);
                    }
                    cellItems[i].img = imgItems[i];
                    jsEGS.addEvent(cellItems[i], 'mouseover', function (W3CEvent) {
                        var k = this.img;
                        k.style.visibility = 'hidden';
                    });
                    jsEGS.addEvent(cellItems[i], 'mousemove', function (W3CEvent) {
                        var k = this.img;
                        k.style.visibility = 'hidden';
                    });
                    jsEGS.addEvent(cellItems[i], 'mouseout', function (W3CEvent) {
                        var k = this.img;
                        k.style.visibility = 'visible';
                    });
                }
                imgItems.length = [];
                cellItems = [];
                var containerHeight = (colHeight > colHeight2) ? colHeight : colHeight2;
                defaultContainer[dIndex].style.height = containerHeight + 'px';
                colHeight = 0;
                colHeight2 = 0;
                containerHeight = 0;
                cell.setCol0(null);
                imgs.setImg0(null);
                cell.setCol1(null);
                imgs.setImg1(null);
            }
        });
    }
});