var vList = null, vl = null, jvList = null;
var data = {}, iInput = {};
var _mp = null, _ms = null;
var _pbtn = null, add = {},postBtn=null;
var _va=null,_vs=null,_vn = null;
jsRU.addEvent(window, 'load', function (W3CEvent) {
    vList = jsRU.getElementsByClassName('jsV_cls', 'INPUT');
    _va = jsRU.getElementsByClassName('venueStreet_cls', 'INPUT');
    _vs = jsRU.getElementsByClassName('venueAddress_cls', 'INPUT');
    _vn = jsRU.getElementsByClassName('venueName_cls', 'INPUT');
    postBtn = jsRU.$('pMap');
    _pbtn=jsRU.$('pbtn');
    jsRU.Accessor(data, 'Data', function (x) { return typeof x == 'object'; });
    jsRU.Accessor(iInput, 'Input', function (x) { return typeof x == 'object'; });

    _mp = jsRU.$('mapBtn');
    _ms = jsRU.$('mapSelection');

    // Venue Name is VN, Venue Address is VA, Venue Identifier is VI.
    for (var i = 0; i < vList.length; i++) {
        vl = vList[i].value;
        jvList = JSON.parse('{' + vl + '}');
        data.setData(jvList);
    }
    for (var j = 0; j < _vn.length; j++) {
        iInput.setInput(jsRU.$(_vn[j].id));
    }
    // Venue search input field: iInput.getInput(): Functional, venue list data: data.getData():functional;
    iInput.getInput().Data = data.getData().v;
    jsRU.addEvent(iInput.getInput(), 'keyup', function (W3CEvent) {
        var e = event.srcElement;
        var kp = jsRU.getKeyPressed();
        //jsRU.log.write(kp.code + ' : ' + kp.value);
        // 8 is backspace and 46 is delete
        if (kp.code == 8 || kp.code == 46) {
            jsRU.ddlSelector.autoSuggest(e, this.Data, e.value);
        }
        else if ((
            // keys not representing alphanumeric characters
                  (kp.code == 16 && kp.code < 32) ||
            // Arrow keys and command keys
                  (kp.code >= 33 && kp.code <= 46) ||
            // FKeys
                  (kp.code >= 112 && kp.code <= 123) ||
            // num and scroll lock
                  (kp.code >= 144 && kp.code <= 145) ||
            // ,,<,.,/,?,'.~,[,{,\,|,],},',"
                  (kp.code >= 188 && kp.code <= 192) ||
                  (kp.code >= 219 && kp.code <= 222))) {
            return;
        }
            // all remaining keys represent alphanumeric characters
        else {
            jsRU.ddlSelector.autoSuggest(e, this.Data, e.value);
        }
    });
    _mp.Target = _ms;
    jsRU.addEvent(_mp, 'click', function (W3CEvent) {
        var e = event.srcElement;
        if (this.Target.style.display == 'none') {
            this.Target.style.display = 'inline-block';
            this.Target.style.top = jsRU.objPos.returnObjTop(e) + jsRU.objPos.returnObjHeight(e) + 'px';
            this.Target.style.left = jsRU.objPos.returnObjLeft(e) + 'px';
        }
        else {
            this.Target.style.display = 'none';
            iInput.getInput().value = '';
        }
    });
    jsRU.addEvent(iInput.getInput(), 'blur', function (W3CEvent) {
        if ((document.activeElement.id != 'ddContainer') &&
            (document.activeElement.id.toString().length != 0)) {
            jsRU.domMgmt.removeCurentNode(jsRU.$('ddContainer'));
        }
    });
    for (var vaItem = 0; vaItem < _va.length; vaItem++) {
        jsRU.addEvent(_va[vaItem], 'change', function (W3CEvent) {
            _vn = jsRU.getElementsByClassName('venueName_cls', 'INPUT');
            for (var i = 0; i < _vn.length; i++) {
                _vn[i].value = '';
            }
        });
    }
    for (var vsItem = 0; vsItem < _vs.length; vsItem++) {
        jsRU.addEvent(_vs[vsItem], 'change', function (W3CEvent) {
            _vn = jsRU.getElementsByClassName('venueName_cls', 'INPUT');
            for (var i = 0; i < _vn.length; i++) {
                _vn[i].value = '';
            }
        });
    }
    for (var vnItem = 0; vnItem < _vn.length; vnItem++) {
        jsRU.addEvent(_vn[vnItem], 'change', function (W3CEvent) {
            _va = jsRU.getElementsByClassName('venueStreet_cls', 'INPUT');
            _vs = jsRU.getElementsByClassName('venueAddress_cls', 'INPUT');
            for (var i = 0; i < _va.length; i++) {
                _va[i].value = '';
            }
            for (var j = 0; j < _vs.length; j++) {
                _vs[j].value = '';
            }
        });
    }

    jsRU.addEvent(_pbtn, 'click', function (W3CEvent) {
        var e = event.srcElement;
        jsRU.$('address').value ='';
        _va = jsRU.getElementsByClassName('venueAddress_cls', 'INPUT');
        _vs = jsRU.getElementsByClassName('venueStreet_cls', 'INPUT');
        jsRU.Accessor(add, 'Address', function (x) { return typeof x == 'string'; });
        for (var j = 0; j < _vs.length; j++) {
            add.setAddress(_vs[j].value);
        }
        for (var i = 0; i < _va.length; i++) {
            add.setAddress(add.getAddress() + ',' + _va[i].value);
        }
        jsRU.$('address').value = add.getAddress();
        jsRU.asyncImport.init('http://maps.googleapis.com/maps/api/js?v=3&callback=jsRU.asyncImport.initialize&key=AIzaSyBQd7QvR8I53IiPEz14eVQ_Yx3-Oeztxds&v=3.exp&sensor=false', 'js');
        add.setAddress('');
    });

    jsRU.addEvent(postBtn, 'click', function (W3CEvent) {
        var des = jsRU.getElementsByClassName('destinationBox_cls', 'INPUT');
        var sendTo = null;
        for (var i = 0; i < des.length; i++) {
            sendTo = jsRU.$(des[i].value);
            var googleStatMap = '<img width=\"350\" src=\"http://maps.googleapis.com/maps/api/staticmap?center=' + jsRU.$('address').value + '&zoom=17&scale=false&size=350x300&maptype=roadmap&format=png&visual_refresh=true&markers=size:mid%7Ccolor:0xff0000%7Clabel:1%7C' + jsRU.$('address').value + 'alt="Google Map of ' + jsRU.$('address').value + '">';
            sendTo.innerHTML = googleStatMap;
        }
        var _ms = jsRU.$('mapSelection');
        if (_ms.style.display == 'inline-block') {
            _ms.style.display = 'none';
        }
        jsRU.$('address').value = '';
    });

});