﻿var fileManagerController = function (dataService) {
    var callBack;

    function open(openCallback) {
        $('#fileManager').modal();
        load(1);
        callBack = openCallback;
    }

    function close() {
        $('#fileManager').modal('hide');
    }

    function pick(id) {
        var items = $('.bf-filemanager .item-check:checked');
        if (callBack.name === 'insertImageCallback') {
            if (id === 0) {
                for (i = 0; i < items.length; i++) {
                    dataService.get('blogifier/api/assets/asset/' + items[i].id, callBack, fail);
                }
            }
            else {
                dataService.get('blogifier/api/assets/asset/' + id, callBack, fail);
            }
        }
        else {
            if (id === 0) {
                if (items.length === 0) {
                    toastr.error('Please select an item');
                }
                else {
                    id = items[0].id;
                }
            }
            var url = 'blogifier/api/assets/asset/' + id;
            if (callBack.name === 'updateAvatarCallback') {
                url = 'blogifier/api/assets/profileavatar/' + id;
            }
            else if (callBack.name === 'updateCoverCallback') {
                url = 'blogifier/api/assets/profileimage/' + id;
            }
            else if (callBack.name === 'updateLogoCallback') {
                url = 'blogifier/api/assets/profilelogo/' + id;
            }
            else if (callBack.name === 'updatePostCoverCallback') {
                url = 'blogifier/api/assets/postimage/' + id + '/' + $('#hdnPostId').val();
            }
            dataService.get(url, callBack, fail);
        }
        close();
    }

    function uploadClick() {
        $('#files').trigger('click');
        return false;
    }
    function uploadSubmit() {
        var data = new FormData($('#frmUpload')[0]);

        dataService.upload('blogifier/api/assets/multiple', data, submitCallback, fail);
    }
    function submitCallback() {
        load(1);
    }

    function remove() {
        loading();
        var items = $('#fileManagerList input:checked');
        for (i = 0; i < items.length; i++) {
            if (i + 1 < items.length) {
                dataService.remove('blogifier/api/assets/' + items[i].id, emptyCallback, fail);
            }
            else {
                dataService.remove('blogifier/api/assets/' + items[i].id, removeCallback, fail);
            }
        }
    }
    function removeCallback(data) {
        loaded();
        toastr.success('Deleted');
        load(1);
    }

    function load(page) {
        $(firstItemCheckfm).prop('checked', false);
        var filter = $('input[name=filter]:checked').val();
        if (!filter) {
            filter = 'filterAll';
        }
        var search = $('#search').val();
        if (search.length > 0) {
            dataService.get('blogifier/api/assets?page=' + page + '&filter=' + filter + '&search=' + search, loadCallback, fail);
        }
        else {
            dataService.get('blogifier/api/assets?page=' + page + '&filter=' + filter, loadCallback, fail);
        }
        return false;
    }
    function loadCallback(data) {
        $('#fileManagerList').empty();
        var assets = data.assets;
        $.each(assets, function (index) {
            var asset = assets[index];
            var src = asset.assetType === 0 ? asset.url : webRoot + asset.image;
            var tag = '<div class="col-sm-6 col-md-4 col-lg-3">' +
                '	<div class="item">' +
                '	    <a href="" onclick="fileManagerController.pick(' + asset.id + '); return false">' +
                '		    <div class="item-img"><img src="' + src + '" alt="' + asset.title + '" /></div>' +
                '	    </a>' +
                '		<label class="item-name custom-control custom-checkbox">' +
                '			<input type="checkbox" id="' + asset.id + '" class="custom-control-input item-check" onchange="fileManagerController.check(this)">' +
                '			<span class="custom-control-indicator"></span>' +
                '			<span class="custom-control-description">' + asset.title + '</span>' +
                '		</label>' +
                '	</div>' +
                '</div>';
            $("#fileManagerList").append(tag);
        });
        loadPager(data.pager);
    }
    function loadPager(pg) {
        $('#file-pagination').empty();

        var last = pg.currentPage * pg.itemsPerPage;
        var first = pg.currentPage == 1 ? 1 : ((pg.currentPage - 1) * pg.itemsPerPage) + 1;
        if (last > pg.total) { last = pg.total; }

        var pager = "";

        if (pg.showOlder === true) {
            pager += '<button type="button" class="item-btn" onclick="return fileManagerController.load(' + pg.older + ')"><i class="fa fa-chevron-left"></i></button>';
        }
        pager += '<span class="item-pagination">' + first + '-' + last + ' out of ' + pg.total + '</span>';
        if (pg.showNewer === true) {
            pager += '<button type="button" class="item-btn" onclick="return fileManagerController.load(' + pg.newer + ')"><i class="fa fa-chevron-right"></i></button>';
        }


        $('#file-pagination').append(pager);
        showBtns();
    }

    function loading() {
        $('#btnDelete').hide();
        $('.loading').fadeIn();
    }
    function loaded() {
        $('.loading').hide();
    }

    function humanFileSize(size) {
        var i = Math.floor(Math.log(size) / Math.log(1024));
        return (size / Math.pow(1024, i)).toFixed(2) * 1 + ' ' + ['B', 'kB', 'MB', 'GB', 'TB'][i];
    }

    function emptyCallback(data) { }
    function fail() {
        toastr.error('Failed');
    }

    function check(cbx) {
        if (!cbx.checked) {
            $(firstItemCheckfm).prop('checked', false);
        }
        showBtns();
    }
    function showBtns() {
        var items = $('.bf-filemanager .item-check:checked');
        if (items.length > 0) {
            $('#btnDelete').show();
            $('#btnSelect').show();
        }
        else {
            $('#btnDelete').hide();
            $('#btnSelect').hide();
        }
    }

    return {
        open: open,
        load: load,
        close: close,
        pick: pick,
        uploadClick: uploadClick,
        uploadSubmit: uploadSubmit,
        remove: remove,
        check: check,
        showBtns: showBtns
    }
}(DataService);

$('#search').keypress(function (event) {
    var keycode = (event.keyCode ? event.keyCode : event.which);
    if (keycode == '13') {
        fileManagerController.load(1);
        return false;
    }
});

$('.bf-posts-list .item-link-desktop').click(function () {
    $('.bf-posts-list .item-link-desktop').removeClass('active');
    $(this).addClass('active');
});

// check all
var itemCheckfm = $('.bf-filemanager .item-check');
var firstItemCheckfm = itemCheckfm.first();

$(firstItemCheckfm).on('change', function () {
    var itemCheckfm = $('.bf-filemanager .item-check');
    $(itemCheckfm).prop('checked', this.checked);
    fileManagerController.showBtns();
});
