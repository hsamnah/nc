(function () {
    if (!window.jsVIG) { window['jsVIG'] = {} }
})();
var _GDC = null, _gc = null, _icoll, _d = 0, imgBtn_fn = null;
var rootImg = [], column0 = [], column1 = [], rowIdentifier = [];
jsVPS.addLoadEvent(function (W3CEvent) {
    _GDC = jsVPS.$('GDC');
    _gc = jsVPS.$('GC');
    _icoll = jsVPS.getElementsByClassName("vIColl_cls", 'div');
    _d = 3;
    imgBtn_fn = jsWB.getElementsByClassName('imgbtn_cls', 'img');

    jsVPS.Accessor(rootImg, 'RI', function (x) { return typeof x == 'number'; });
    jsVPS.Accessor(column0, 'c0', function (x) { return typeof x == 'number'; });
    jsVPS.Accessor(column1, 'c1', function (x) { return typeof x == 'number'; });
    jsVPS.Accessor(rowIdentifier, 'RI', function (x) { return typeof x == 'number'; });

    var height1 = 0, height2 = 0, height3 = 0;
    for (var iIndex = 0; iIndex < _icoll.length; iIndex++) {
        if (iIndex === 0) {
            rootImg.setRI(iIndex);

            _icoll[iIndex].style.top = jsVPS.objPos.returnObjTop(_gc) + 'px';
            _icoll[iIndex].style.left = jsVPS.objPos.returnObjLeft(_gc) - 3 + 'px';

            height1 += jsVPS.objPos.returnObjHeight(_icoll[iIndex]);
        }
        else if (iIndex === 1) {
            column0.setc0(iIndex);

            _icoll[iIndex].style.top = jsVPS.objPos.returnObjTop(_icoll[rootImg.getRI()]) + 'px';
            _icoll[iIndex].style.left = jsVPS.objPos.returnObjLeft(_icoll[rootImg.getRI()]) + jsVPS.objPos.returnObjWidth(_icoll[rootImg.getRI()]) + 'px';

            height2 += jsVPS.objPos.returnObjHeight(_icoll[iIndex]);
        }
        else if (iIndex === 2) {
            column1.setc1(iIndex);

            _icoll[iIndex].style.top = jsVPS.objPos.returnObjTop(_icoll[column0.getc0()]) + 'px';
            _icoll[iIndex].style.left = jsVPS.objPos.returnObjLeft(_icoll[column0.getc0()]) + jsVPS.objPos.returnObjWidth(_icoll[column0.getc0()]) + 'px';

            height3 += jsVPS.objPos.returnObjHeight(_icoll[iIndex]);
        }
        else if (iIndex % 3 == 0) {
            rowIdentifier.setRI(iIndex);

            var rcell = rowIdentifier.getRI() - _d;
            _icoll[iIndex].style.top = jsVPS.objPos.returnObjTop(_icoll[rcell]) + jsVPS.objPos.returnObjHeight(_icoll[rcell]) + 'px';
            _icoll[iIndex].style.left = jsVPS.objPos.returnObjLeft(_icoll[rcell]) + 'px';

            height1 += jsVPS.objPos.returnObjHeight(_icoll[iIndex]);
        }
        else {
            if (iIndex == rowIdentifier.getRI() + 1) {
                refCell = (rowIdentifier.getRI() - _d) + 1;

                _icoll[iIndex].style.top = jsVPS.objPos.returnObjTop(_icoll[refCell]) + jsVPS.objPos.returnObjHeight(_icoll[refCell]) + 'px';
                _icoll[iIndex].style.left = jsVPS.objPos.returnObjLeft(_icoll[column0.getc0()]) + 'px';

                height2 += jsVPS.objPos.returnObjHeight(_icoll[iIndex]);
            }
            else if (iIndex == rowIdentifier.getRI() + 2) {
                refCell2 = (rowIdentifier.getRI() - _d) + 2;

                _icoll[iIndex].style.top = jsVPS.objPos.returnObjTop(_icoll[refCell2]) + jsVPS.objPos.returnObjHeight(_icoll[refCell2]) + 'px';
                _icoll[iIndex].style.left = jsVPS.objPos.returnObjLeft(_icoll[column1.getc1()]) + 'px';

                height3 += jsVPS.objPos.returnObjHeight(_icoll[iIndex]);
            }
        }
    }
    if (_gc) {
        if (height1 > height3 && height1 > height2) {
            _gc.style.height = height1 + 'px';
        }
        else if (height2 > height1 && height2 > height3) {
            _gc.style.height = height2 + 'px';
        }
        else if (height3 > height1 && height3 > height2) {
            _gc.style.height = height3 + 'px';
        }
    }
    for (var j = 0; j < imgBtn_fn.length; j++) {
        jsWB.addEvent(imgBtn_fn[j], 'click', function (x) {
            var e = event.srcElement;
            var bws = jsWB.getBrowserWindowSize;
            var windowMidPointTop = (bws.height * .10);
            var windowMidPointLeft = (bws.width * .10);

            var closeBtn = document.createElement('IMG');
            var closePanel = document.createElement('DIV');

            var db = document.createElement('DIV');
            var imgHolder = document.createElement('DIV');
            var imgie = document.createElement('IMG');

            closeBtn.setAttribute('class', 'closebtn_cls');
            closeBtn.src = 'imgs/close.gif';
            closePanel.appendChild(closeBtn);

            closePanel.style.textAlign = 'right';
            db.appendChild(closePanel);
            imgie.src = e.src;
            imgie.style.width = '50%';
            imgie.style.left = imgHolder.style.left + 2 + 'px';
            imgie.style.top = imgHolder.style.top + 2 + 'px';
            imgHolder.appendChild(imgie);

            imgHolder.setAttribute('class', 'imgHolder_cls');
            imgHolder.style.left = windowMidPointLeft + 'px';
            imgHolder.style.top = windowMidPointTop + 'px';
            imgHolder.style.width = e.style.width + 300 + 'px';
            imgHolder.style.height = e.style.height + 'px';
            db.appendChild(imgHolder);
            db.id = 'ImgViewer';
            db.setAttribute('class', 'blockscreen_cls')
            document.body.appendChild(db);

            jsWB.addEvent(closeBtn, 'click', function (W3CEvent) {
                jsWB.domMgmt.removeChildren(jsWB.$('ImgViewer'));
                jsWB.domMgmt.removeCurentNode(jsWB.$('ImgViewer'));
            });
        })
    }
}, true);