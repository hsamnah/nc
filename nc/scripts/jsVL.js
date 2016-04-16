/* global fname */
(function () {
    if (!window.jsVL) { window['jsVL'] = {} }

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
    if (!window.jsVL) { window['jsVL'] = {}; }
    window['jsVL']['log'] = new myLogger();

    function isCompatible(other) {
        if (other === false || !Array.prototype.push || !Object.hasOwnProperty || !document.createElement || !document.getElementsByTagName) {
            return false;
        } return true;
    }
    window['jsVL']['isCompatible'] = isCompatible;

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
    window['jsVL']['getBrowserWindowSize'] = new getBrowserWindowSize();

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
    window['jsVL']['Accessor'] = Accessor;

    function preventDefault(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.preventDefault) {
            eventObject.preventDefault();
        } else {
            eventObject.returnValue = false;
        }
    };
    window['jsVL']['preventDefault'] = preventDefault;

    function stopPropagation(eventObject) {
        eventObject = eventObject || window.getEventObject(eventObject);
        if (eventObject.stopPropagation) {
            eventObject.stopPropagation();
        } else {
            eventObject.cancelBubble = true;
        }
    };
    window['jsVL']['stopPropagation'] = stopPropagation;

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
    window['jsVL']['$'] = $;

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
    window['jsVL']['addEvent'] = addEvent;

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
    window['jsVL']['getElementsByClassName'] = getElementsByClassName;

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
    window['jsVL']['toggleDisplay'] = toggleDisplay;

    function objPos() {
        this.setObjPos = function (obj, relativeObj) {
            var f = jsVL.DomOopObjects.$(relativeObj);
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
    window['jsVL']['objPos'] = new objPos();

    function domMgmt() {
        this.removeChildren = function (parent) {
            if (!(parent = $(parent))) { return false; }
            while (parent.firstChild) {
                parent.firstChild.parentNode.removeChild(parent.firstChild);
            }
            return parent;
        };
        this.removeCurrentNode = function (elem) {
            var elem2Remove = (!jsVL.$(elem)) ? elem : jsVL.$(elem);
            if (elem2Remove != null
            || elem2Remove != 'undefined') {
                var f = elem2Remove.parentNode;
                if (f == 'undefined' || f == undefined) { return false; }
                else { f.removeChild(elem2Remove); }
            }
            return elem;
        };
        this.insertAfter = function (node, referenceNode) {
            if (!(node = $(node))) { return false; }
            if (!(referenceNode = $(referenceNode))) { return false; }
            referenceNode.parentNode.insertBefore(node, referenceNode.nextSibling);
        };
        this.insertAfter2 = function (node, referenceNode) {
            if (!(node = $(node))) { return false; }
            if (!(referenceNode = $(referenceNode))) { return false; }
            return referenceNode.parentNode.insertBefore(
                node, referenceNode.nextSibling);
        };
        this.prependChild = function (parent, newChild) {
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
    };
    window['jsVL']['domMgmt'] = new domMgmt();
})();
var _container = null, _pc = null, _vbc = null, _vb = null, _v = null, _vdc = null, _vn = null, _vsd = null, heightCollection = 0, _func = null, _det = null, _foll = null;
var _primaryContainer = {}, pcontainer = {}, fun = {}, venueV = {}, vSetSplit = new Array();
jsVL.addEvent(window, 'load', function (W3CEvent) {
    _pc = jsVL.getElementsByClassName('vc_cls', 'div');
    _vbc = jsVL.getElementsByClassName('vBannerContainer_cls', 'div');
    _vb = jsVL.getElementsByClassName('img_cls', 'img');
    _v = jsVL.getElementsByClassName('v_cls', 'input');
    _vdc = jsVL.getElementsByClassName('vDetails_cls', 'div');
    _vn = jsVL.getElementsByClassName('venueName_cls', 'span');
    _vsd = jsVL.getElementsByClassName('venueShort_cls', 'span');

    _container = jsVL.getElementsByClassName('venueContainer_cls', 'div');
    _func = jsVL.getElementsByClassName('functionContainer_cls', 'div');

    _det = jsVL.getElementsByClassName('details_cls', 'span');
    _foll = jsVL.getElementsByClassName('follow_cls', 'span');

    if (_container.toString().length == 0) {
        return;
    }
    else {
        jsVL.Accessor(_primaryContainer, 'PC', function (x) { return typeof x == 'object' || typeof x == null; });
        jsVL.Accessor(pcontainer, 'C0', function (x) { return typeof x == 'object' || typeof x == null; });
        jsVL.Accessor(pcontainer, 'C1', function (x) { return typeof x == 'object' || typeof x == null; });
        jsVL.Accessor(fun, 'F0', function (x) { return typeof x == 'object' || typeof x == null; });
        jsVL.Accessor(fun, 'F1', function (x) { return typeof x == 'object' || typeof x == null; });

        for (var pIndex = 0; pIndex < _container.length; pIndex++) {
            _primaryContainer.setPC(_container[pIndex]);
        }
        for (var index = 0; index < _pc.length; index++) {
            if (index == 0) {
                _pc[index].style.left = jsVL.objPos.returnObjLeft(_primaryContainer.getPC()) + 'px';
                _pc[index].style.top = jsVL.objPos.returnObjTop(_primaryContainer.getPC()) + 'px';

                _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                pcontainer.setC0(_pc[index]);
                fun.setF0(_func[index]);

                heightCollection = heightCollection + jsVL.objPos.returnObjHeight(fun.getF0()) + jsVL.objPos.returnObjHeight(pcontainer.getC0());
            }
            else if (index == 1) {
                _pc[index].style.left = jsVL.objPos.returnObjLeft(_primaryContainer.getPC()) + jsVL.objPos.returnObjWidth(pcontainer.getC0()) + 10 + 'px';
                _pc[index].style.top = jsVL.objPos.returnObjTop(_primaryContainer.getPC()) + 'px';

                _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                fun.setF1(_func[index]);
                pcontainer.setC1(_pc[index]);
            }
            else if (index % 2 == 0) {
                _pc[index].style.left = jsVL.objPos.returnObjLeft(pcontainer.getC0()) + 'px';
                _pc[index].style.top = jsVL.objPos.returnObjTop(pcontainer.getC0()) + jsVL.objPos.returnObjHeight(fun.getF0()) + jsVL.objPos.returnObjHeight(pcontainer.getC0()) + 10 + 'px';

                _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                pcontainer.setC0(_pc[index]);

                heightCollection = heightCollection + jsVL.objPos.returnObjHeight(fun.getF1()) + jsVL.objPos.returnObjHeight(pcontainer.getC0());
            }
            else {
                _pc[index].style.left = jsVL.objPos.returnObjLeft(pcontainer.getC1()) + 'px';
                _pc[index].style.top = jsVL.objPos.returnObjTop(pcontainer.getC1()) + jsVL.objPos.returnObjHeight(fun.getF0()) + jsVL.objPos.returnObjHeight(pcontainer.getC1()) + 10 + 'px';

                _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                pcontainer.setC1(_pc[index]);
            }
            jsVL.addEvent(window, 'resize', function (W3CEvent) {
                jsVL.Accessor(_primaryContainer, 'PC', function (x) { return typeof x == 'object' || typeof x == null; });
                jsVL.Accessor(pcontainer, 'C0', function (x) { return typeof x == 'object' || typeof x == null; });
                jsVL.Accessor(pcontainer, 'C1', function (x) { return typeof x == 'object' || typeof x == null; });
                jsVL.Accessor(fun, 'F0', function (x) { return typeof x == 'object' || typeof x == null; });
                jsVL.Accessor(fun, 'F1', function (x) { return typeof x == 'object' || typeof x == null; });
                for (var pIndex = 0; pIndex < _container.length; pIndex++) {
                    _primaryContainer.setPC(_container[pIndex]);
                }
                for (var index = 0; index < _pc.length; index++) {
                    if (index == 0) {
                        _pc[index].style.left = jsVL.objPos.returnObjLeft(_primaryContainer.getPC()) + 'px';
                        _pc[index].style.top = jsVL.objPos.returnObjTop(_primaryContainer.getPC()) + 'px';

                        _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                        _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                        pcontainer.setC0(_pc[index]);
                        fun.setF0(_func[index]);

                        heightCollection = heightCollection + jsVL.objPos.returnObjHeight(fun.getF0()) + jsVL.objPos.returnObjHeight(pcontainer.getC0());
                    }
                    else if (index == 1) {
                        _pc[index].style.left = jsVL.objPos.returnObjLeft(_primaryContainer.getPC()) + jsVL.objPos.returnObjWidth(pcontainer.getC0()) + 10 + 'px';
                        _pc[index].style.top = jsVL.objPos.returnObjTop(_primaryContainer.getPC()) + 'px';

                        _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                        _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                        fun.setF1(_func[index]);
                        pcontainer.setC1(_pc[index]);
                    }
                    else if (index % 2 == 0) {
                        _pc[index].style.left = jsVL.objPos.returnObjLeft(pcontainer.getC0()) + 'px';
                        _pc[index].style.top = jsVL.objPos.returnObjTop(pcontainer.getC0()) + jsVL.objPos.returnObjHeight(fun.getF0()) + jsVL.objPos.returnObjHeight(pcontainer.getC0()) + 10 + 'px';

                        _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                        _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                        pcontainer.setC0(_pc[index]);

                        heightCollection = heightCollection + jsVL.objPos.returnObjHeight(fun.getF1()) + jsVL.objPos.returnObjHeight(pcontainer.getC0());
                    }
                    else {
                        _pc[index].style.left = jsVL.objPos.returnObjLeft(pcontainer.getC1()) + 'px';
                        _pc[index].style.top = jsVL.objPos.returnObjTop(pcontainer.getC1()) + jsVL.objPos.returnObjHeight(fun.getF0()) + jsVL.objPos.returnObjHeight(pcontainer.getC1()) + 10 + 'px';

                        _func[index].style.left = jsVL.objPos.returnObjLeft(_pc[index]) + 'px';
                        _func[index].style.top = jsVL.objPos.returnObjTop(_pc[index]) + jsVL.objPos.returnObjHeight(_pc[index]) + 'px';

                        pcontainer.setC1(_pc[index]);
                    }
                }
            });
            _pc[index].banner = _vb[index];
            jsVL.addEvent(_pc[index], 'mouseover', function (W3CEvent) {
                this.banner.style.visibility = 'hidden';
            });
            jsVL.addEvent(_pc[index], 'mousemove', function (W3CEvent) {
                this.banner.style.visibility = 'hidden';
            });
            jsVL.addEvent(_pc[index], 'mouseout', function (W3CEvent) {
                this.banner.style.visibility = 'visible';
            });
            _det[index].itemIndex = _v[index];
            jsVL.addEvent(_det[index], 'click', function (W3CEvent) {
                venueV = jsVL.$('vData_' + this.itemIndex.value);
                var parsedVenue = venueV.value.split('~');
                for (var i = 0; i < parsedVenue.length; i++) {
                    var p = parsedVenue[i].split('<>');
                    vSetSplit.push('"' + p[0] + '": "' + p[1] + '",');
                }
                var twojson = '{';
                for (var i = 0; i < vSetSplit.length; i++) {
                    twojson += vSetSplit[i];
                }
                vSetSplit.length = 0;

                var findlastComma = twojson.lastIndexOf(',');
                var getData = twojson.slice(0, findlastComma);
                getData += '}';
                var gData = {};
                try {
                    gData = JSON.parse(getData);
                }
                catch (err) {
                    jsVL.log.write(err);
                }

                var browserWindowSize = jsVL.getBrowserWindowSize;
                var itop = ((browserWindowSize.height - 900) / 2) || 0;
                var ileft = ((browserWindowSize.width - 800) / 2) || 0;

                var bgContainer = document.createElement('DIV');
                bgContainer.setAttribute('class', 'details_bg_cls');
                bgContainer.setAttribute('id', 'bgc');

                var fbar = document.createElement('TABLE');
                fbar.setAttribute('class', 'function_bar_cls');
                var fbarTR = document.createElement('TR');
                var fbarTD0 = document.createElement('TD');
                var fbarTD1 = document.createElement('TD');
                fbarTD1.setAttribute('class', 'fbar_TD1_cls')
                var closeImg = document.createElement('img');
                closeImg.setAttribute('id', 'closeImgBtn');
                closeImg.src = '/imgs/closeInvert.gif';
                closeImg.setAttribute('class', 'close_btn_cls');

                fbarTR.appendChild(fbarTD0);
                fbarTD1.appendChild(closeImg);
                fbarTR.appendChild(fbarTD1);
                fbar.appendChild(fbarTR);
                bgContainer.appendChild(fbar);
                jsVL.addEvent(closeImg, 'click', function (W3CEvent) {
                    var i = jsVL.$('bgc');
                    jsVL.domMgmt.removeChildren(i);
                    jsVL.domMgmt.removeCurrentNode(i);
                });
                //  this is the element thats display attribute must be manipulated.
                var innerContainer = document.createElement('DIV');
                innerContainer.setAttribute('class', 'inner_container_cls');
                innerContainer.id = 'innerDIv_' + gData.VI;
                if (gData.BackgroundImage == '//' || gData.BackgroundImage == null || gData.BackgroundImage == 'undefined') {
                    if (typeof gData.BackgroundColor == 'string' && gData.BackgroundColor.length == 0) {
                        innerContainer.style.backgroundColor = '#3E566A';
                    } else {
                        innerContainer.style.backgroundColor = gData.BackgroundColor;
                    }
                }
                else {
                    innerContainer.style.backgroundImage = 'url("' + gData.BackgroundImage + '")';
                }
                var tblContainer = document.createElement('TABLE');
                tblContainer.style.width = '100%';
                tblContainer.setAttribute('class', 'tblContainer_cls');
                tblContainer.setAttribute('id', 'tblContainerI');
                var tr0 = document.createElement('TR');
                tr0.id = 'tr0';
                var tr0td0 = document.createElement('TD');
                tr0td0.setAttribute('class', 'title_Details_cls')
                tr0td0.setAttribute('colspan', '2');
                var title = document.createTextNode(gData.Venue_Name);
                tr0td0.appendChild(title);
                tr0.appendChild(tr0td0);
                tblContainer.appendChild(tr0);

                // this is the start of the details page
                var tr1 = document.createElement('TR');
                tr1.id = 'tr1';
                var tr1td0 = document.createElement('TD');
                tr1td0.setAttribute('rowspan', '8');
                tr1td0.setAttribute('class', 'bannerBg_cls')
                tr1td0.style.verticalAlign = 'top';
                var img0 = document.createElement('IMAGE');
                img0.setAttribute('id', 'venueBanner');
                img0.style.maxWidth = '450px';
                if (gData.Venue_Banner == '//') {
                    img0.setAttribute('src', '/imgs/vbna_full.jpg');
                }
                else {
                    img0.setAttribute('src', gData.Venue_Banner);
                }
                tr1td0.appendChild(img0);

                //
                var addInput = document.createElement('INPUT');
                addInput.setAttribute('type', 'hidden');
                addInput.setAttribute('id', 'addy_' + gData.VI);
                addInput.setAttribute('class', 'hfg_cls');
                addInput.setAttribute('value', gData.streetAdd1 + ', ' + gData.City + ', ' + gData.SubRegion + ', ' + gData.Country);
                tr1td0.appendChild(addInput);

                var gm = document.createElement('DIV');
                gm.setAttribute('id', 'gmap_');
                gm.setAttribute('class', 'gmap_cls');
                tr1td0.appendChild(gm);
                //

                tr1td0.style.width = '1px';
                tr1.appendChild(tr1td0);
                var tr1td1 = document.createElement('TD');
                tr1td1.setAttribute('class', 'txt_tbl_style_cls');
                var desc = document.createTextNode(gData.Full_Description);
                tr1td1.appendChild(desc);
                tr1.appendChild(tr1td1);
                tblContainer.appendChild(tr1);

                var trws = document.createElement('TR');
                trws.id = 'tr2';
                var tdws = document.createElement('TD');
                tdws.setAttribute('class', 'txt_tbl_style_cls');
                var txtws = document.createTextNode('Web site: ');
                var aws = document.createElement('A');
                var awsTxt = document.createTextNode(gData.Web_Site);
                aws.setAttribute('href', gData.Web_Site);
                aws.setAttribute('target', '_blank');
                tdws.appendChild(txtws);
                aws.appendChild(awsTxt);
                tdws.appendChild(aws);
                trws.appendChild(tdws);
                tblContainer.appendChild(trws);

                var tr2a = document.createElement('TR');
                tr2a.id = 'tr3';
                var tr2atd = document.createElement('TD');
                tr2atd.setAttribute('class', 'txt_tbl_style_cls');
                var tr2atdTxt = document.createTextNode('Contact Number: ' + gData.Contact_Number);
                tr2atd.appendChild(tr2atdTxt);
                tr2a.appendChild(tr2atd);
                tblContainer.appendChild(tr2a);

                var tr4 = document.createElement('TR');
                tr4.id = 'tr4';
                var tr4td0 = document.createElement('TD');
                tr4td0.setAttribute('class', 'txt_tbl_style_cls');
                var tr4td0title = document.createTextNode('Genre:' + gData.Genre);
                tr4td0.appendChild(tr4td0title)
                tr4.appendChild(tr4td0);
                tblContainer.appendChild(tr4);

                var tr5 = document.createElement('TR');
                tr5.id = 'tr5';
                var tr5td0 = document.createElement('TD');
                tr5td0.setAttribute('class', 'txt_tbl_style_cls');
                tr5td0title = document.createTextNode('Hours of operation: ' + gData.Days_Open + ' @ ' + gData.Hours_of_Operation);
                tr5td0.appendChild(tr5td0title);
                tr5.appendChild(tr5td0);
                tblContainer.appendChild(tr5);

                var tr6 = document.createElement('TR');
                tr6.id = 'tr6';
                var tr6td = document.createElement('TD');
                tr6td.setAttribute('class', 'txt_tbl_style_cls');
                var tr6tdTxt = document.createTextNode('Dress Code: ' + gData.Dress_Code);
                tr6td.appendChild(tr6tdTxt);
                tr6.appendChild(tr6td);
                tblContainer.appendChild(tr6);

                var tr7 = document.createElement('TR');
                tr7.id = 'tr7';
                var tr7td = document.createElement('TD');
                tr7td.setAttribute('class', 'txt_tbl_style_cls');
                var tr7tdTxt = document.createTextNode('Capacity: ' + gData.Capacity);
                tr7td.appendChild(tr7tdTxt);
                tr7.appendChild(tr7td);
                tblContainer.appendChild(tr7);

                var tr8 = document.createElement('TR');
                tr8.id = 'tr8';
                var tr8td = document.createElement('TD');
                tr8td.setAttribute('class', 'txt_tbl_style_cls');
                var tr8tdTxt = document.createTextNode('Age Limit: ' + gData.Age_Limit);
                tr8td.appendChild(tr8tdTxt);
                tr8.appendChild(tr8td);
                tblContainer.appendChild(tr8);
                // this is the end

                var tr9 = document.createElement('TR');
                var tr9td = document.createElement('TD');
                tr9td.setAttribute('colspan', '2');
                tr9td.setAttribute('class', 'txt_tbl_span_style_cls');

                var detBtn = document.createElement('SPAN');
                detBtn.setAttribute('class', 'ttssBtn_cls');
                var detBtnTxt = document.createTextNode('Details');
                jsVL.addEvent(detBtn, 'click', function (W3CEvent) {
                    var item2Clean = jsVL.$('galleryContainer');
                    if ((item2Clean === 'undefined') || (item2Clean === null)) {
                        return;
                    }
                    else {
                        jsVL.domMgmt.removeCurrentNode(item2Clean);
                        var title = jsVL.$('tr0');

                        var tr1 = document.createElement('TR');
                        tr1.id = 'tr1';
                        var tr1td0 = document.createElement('TD');
                        tr1td0.setAttribute('rowspan', '8');
                        tr1td0.setAttribute('class', 'bannerBg_cls')
                        tr1td0.style.verticalAlign = 'top';
                        var img0 = document.createElement('IMAGE');
                        img0.setAttribute('id', 'venueBanner');
                        img0.style.maxWidth = '450px';
                        if (gData.Venue_Banner == '//') {
                            img0.setAttribute('src', '/imgs/vbna_full.jpg');
                        }
                        else {
                            img0.setAttribute('src', gData.Venue_Banner);
                        }
                        tr1td0.appendChild(img0);

                        //
                        var addInput = document.createElement('INPUT');
                        addInput.setAttribute('type', 'hidden');
                        addInput.setAttribute('id', 'addy_' + gData.VI);
                        addInput.setAttribute('class', 'hfg_cls');
                        addInput.setAttribute('value', gData.streetAdd1 + ', ' + gData.City + ', ' + gData.SubRegion + ', ' + gData.Country);
                        tr1td0.appendChild(addInput);

                        var gm = document.createElement('DIV');
                        gm.setAttribute('id', 'gmap_');
                        gm.setAttribute('class', 'gmap_cls');
                        tr1td0.appendChild(gm);
                        //

                        tr1td0.style.width = '1px';
                        tr1.appendChild(tr1td0);
                        var tr1td1 = document.createElement('TD');
                        tr1td1.setAttribute('class', 'txt_tbl_style_cls');
                        var desc = document.createTextNode(gData.Full_Description);
                        tr1td1.appendChild(desc);
                        tr1.appendChild(tr1td1);
                        //tblContainer.appendChild(tr1);
                        jsVL.domMgmt.insertAfter(tr1, title);

                        var trws = document.createElement('TR');
                        trws.id = 'tr2';
                        var tdws = document.createElement('TD');
                        tdws.setAttribute('class', 'txt_tbl_style_cls');
                        var txtws = document.createTextNode('Web site: ');
                        var aws = document.createElement('A');
                        var awsTxt = document.createTextNode(gData.Web_Site);
                        aws.setAttribute('href', gData.Web_Site);
                        aws.setAttribute('target', '_blank');
                        tdws.appendChild(txtws);
                        aws.appendChild(awsTxt);
                        tdws.appendChild(aws);
                        trws.appendChild(tdws);
                        //tblContainer.appendChild(trws);
                        jsVL.domMgmt.insertAfter(trws, tr1);

                        var tr2a = document.createElement('TR');
                        tr2a.id = 'tr3';
                        var tr2atd = document.createElement('TD');
                        tr2atd.setAttribute('class', 'txt_tbl_style_cls');
                        var tr2atdTxt = document.createTextNode('Contact Number: ' + gData.Contact_Number);
                        tr2atd.appendChild(tr2atdTxt);
                        tr2a.appendChild(tr2atd);
                        //tblContainer.appendChild(tr2a);
                        jsVL.domMgmt.insertAfter(tr2a, trws);

                        var tr4 = document.createElement('TR');
                        tr4.id = 'tr4';
                        var tr4td0 = document.createElement('TD');
                        tr4td0.setAttribute('class', 'txt_tbl_style_cls');
                        var tr4td0title = document.createTextNode('Genre:' + gData.Genre);
                        tr4td0.appendChild(tr4td0title)
                        tr4.appendChild(tr4td0);
                        //tblContainer.appendChild(tr4);
                        jsVL.domMgmt.insertAfter(tr4, tr2a);

                        var tr5 = document.createElement('TR');
                        tr5.id = 'tr5';
                        var tr5td0 = document.createElement('TD');
                        tr5td0.setAttribute('class', 'txt_tbl_style_cls');
                        tr5td0title = document.createTextNode('Hours of operation: ' + gData.Days_Open + ' @ ' + gData.Hours_of_Operation);
                        tr5td0.appendChild(tr5td0title);
                        tr5.appendChild(tr5td0);
                        //tblContainer.appendChild(tr5);
                        jsVL.domMgmt.insertAfter(tr5, tr4);

                        var tr6 = document.createElement('TR');
                        tr6.id = 'tr6';
                        var tr6td = document.createElement('TD');
                        tr6td.setAttribute('class', 'txt_tbl_style_cls');
                        var tr6tdTxt = document.createTextNode('Dress Code: ' + gData.Dress_Code);
                        tr6td.appendChild(tr6tdTxt);
                        tr6.appendChild(tr6td);
                        //tblContainer.appendChild(tr6);
                        jsVL.domMgmt.insertAfter(tr6, tr5);

                        var tr7 = document.createElement('TR');
                        tr7.id = 'tr7';
                        var tr7td = document.createElement('TD');
                        tr7td.setAttribute('class', 'txt_tbl_style_cls');
                        var tr7tdTxt = document.createTextNode('Capacity: ' + gData.Capacity);
                        tr7td.appendChild(tr7tdTxt);
                        tr7.appendChild(tr7td);
                        //tblContainer.appendChild(tr7);
                        jsVL.domMgmt.insertAfter(tr7, tr6);

                        var tr8 = document.createElement('TR');
                        tr8.id = 'tr8';
                        var tr8td = document.createElement('TD');
                        tr8td.setAttribute('class', 'txt_tbl_style_cls');
                        var tr8tdTxt = document.createTextNode('Age Limit: ' + gData.Age_Limit);
                        tr8td.appendChild(tr8tdTxt);
                        tr8.appendChild(tr8td);
                        //tblContainer.appendChild(tr8);
                        jsVL.domMgmt.insertAfter(tr8, tr7);
                    }
                })
                detBtn.appendChild(detBtnTxt);
                tr9td.appendChild(detBtn);
                tr9td.appendChild(document.createTextNode(' | '));

                var vGallery = document.createElement('SPAN');
                vGallery.setAttribute('class', 'ttssBtn_cls');
                var vGTxt = document.createTextNode('Photo Gallery');

                vGallery.TargetItem = gData.VI;
                jsVL.addEvent(vGallery, 'click', function (W3CEvent) {
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr1'));
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr2'));
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr3'));
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr4'));
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr5'));
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr6'));
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr7'));
                    jsVL.domMgmt.removeCurrentNode(jsVL.$('tr8'));

                    var title = jsVL.$('tr0');
                    var data = jsVL.$('vImgs_' + this.TargetItem);

                    var directory = null;
                    var dItems = null;
                    var dNames = null, Name = null;
                    var records = data.value;
                    var commaIndex = records.lastIndexOf(',');
                    var col = '{' + records + '}';
                    var jCon = JSON.parse(col);
                    var tr = document.createElement('TR');
                    tr.id = 'galleryContainer';
                    var tcell = document.createElement('TD');
                    tcell.setAttribute('class', 'tcell_cls');
                    var tcontainer = document.createElement('DIV');
                    tcontainer.setAttribute('class', 'tcontainer_cls');
                    for (var i = 0; i < jCon.Directories.length; i++) {
                        var imgColTitle = document.createElement('DIV');
                        imgColTitle.setAttribute('class', 'imgColTitle_cls');

                        var dirTitle = document.createTextNode(jCon.Directories[i].DirectoryName);
                        imgColTitle.appendChild(dirTitle);
                        tcontainer.appendChild(imgColTitle);
                        var imgContainer = document.createElement('DIV');
                        imgContainer.setAttribute('class', 'imgContainer_cls');
                        for (var j = 0; j < jCon.Directories[i].Imgs.length; j++) {
                            var img = document.createElement('IMG');
                            img.src = jCon.Directories[i].Imgs[j].filePath;
                            img.setAttribute('class', 'vimg_cls');
                            imgContainer.appendChild(img);
                        }
                        tcontainer.appendChild(imgContainer);
                    }
                    tcell.appendChild(tcontainer);
                    tr.appendChild(tcell);
                    jsVL.domMgmt.insertAfter(tr, title);
                })

                vGallery.appendChild(vGTxt);
                tr9td.appendChild(vGallery);

                tr9td.appendChild(document.createTextNode(' | '));

                var gsBtn = document.createElement('SPAN');
                gsBtn.setAttribute('class', 'ttssBtn_cls');
                var gsBtnTxt = document.createTextNode('Guest Services');

                gsBtn.venueIndex = gData.VI;
                jsVL.addEvent(gsBtn, 'click', function (W3CEvent) {
                    var gsWindow = window.open('/UserItems/vGServes.aspx?gsv=' + this.venueIndex, 'gsWindow', 'height=755,width=1000,menubar=no,scrollbars=no,status=no,titlebar=no,toolbar=no');
                });

                gsBtn.appendChild(gsBtnTxt);
                tr9td.appendChild(gsBtn);

                tr9.appendChild(tr9td);
                tblContainer.appendChild(tr9);

                innerContainer.style.left = ileft + 'px';
                innerContainer.style.top = itop + 'px';
                innerContainer.style.width = '800px';
                innerContainer.style.height = jsVL.objPos.returnObjHeight(jsVL.$('tblContainerI')) + 'px';
                innerContainer.appendChild(tblContainer);
                bgContainer.appendChild(innerContainer);
                document.body.appendChild(bgContainer);
            });
        }
        _primaryContainer.getPC().style.height = heightCollection + 10 + 'px';
        _primaryContainer.setPC(null);
        pcontainer.setC0(null);
        pcontainer.setC1(null);
        fun.setF0(null);
        fun.setF1(null);
    }
});