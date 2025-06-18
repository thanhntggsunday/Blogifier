﻿var editorController = function (dataService) {

    function savePost(publish) {
        var obj = {
            Id: $('#hdnPostId').val(),
            Title: $("#txtPostTitle").val(),
            Slug: $('#hdnPostSlug').val(),
            Image: $('#hdnPostImg').val(),
            Content: tinyMCE.activeEditor.getContent(),
            Description: $('#txtDescription').val(),
            Publish: publish ? true : false,
            Categories: []
        }
        if (obj.Title.length > 0 || obj.Content.length > 0) {
            $('.loading').fadeIn('fast');
            $('.admin-editor-toolbar').fadeOut('fast');
        }
        if (obj.Title.length === 0) {
            toastr.error("Title is required");
            return false;
        }
        if (obj.Content.length === 0) {
            toastr.error("Content is required");
            return false;
        }
        $("#edit-categories input[type=checkbox]").each(function () {
            if ($(this).is(':checked')) {
                var cat = { Value: $(this).attr('value') };
                obj.Categories.push(cat);
            }
        });
        dataService.post("blogifier/api/posts", obj, savePostCallback, fail);
    }

    function savePostCallback(data) {
        var callback = JSON.parse(data);
        $('#hdnPostId').val(callback.id);
        $('#hdnPostSlug').val(callback.slug);
        $('#hdnPostImg').val(callback.image);
        $('#hdnPublished').val(callback.published);
        toastr.success('Saved');
        loadActionButtons();
        $('.loading').fadeOut('fast');
        $('.admin-editor-toolbar').fadeIn('fast');
    }

    function deletePost() {
        var postId = $('#hdnPostId').val();
        dataService.remove("blogifier/api/posts/" + postId, deletePostCallback, fail);
    }

    function deletePostCallback(data) {
        toastr.success('Deleted');
        setTimeout(function () {
            window.location.href = webRoot + 'admin';
        }, 1000);
    }

    function unpublishPost() {
        var postId = $('#hdnPostId').val();
        var obj = {};
        dataService.put("blogifier/api/posts/unpublish/" + postId, obj, unpublishPostCallback, fail);
    }

    function unpublishPostCallback() {
        toastr.success('Unpublished');
        $('#hdnPublished').val('1/1/0001 12:00:00 AM');
        loadActionButtons();
    }

    function categoryKeyPress(e) {
        if (e.which === 13) {
            e.preventDefault();
            saveCategory();
        }
        return false;
    }

    function saveCategory() {
        var obj = { Title: $("#txtCategory").val() }
        dataService.post("blogifier/api/categories/addcategory", obj, saveCategoryCallback, fail);
    }

    function saveCategoryCallback(data) {
        var obj = JSON.parse(data);
        var li = '<li id="cat-' + obj.id + '">';
        li += '	<label class="custom-control custom-checkbox">';
        li += '	  <input type="checkbox" id="cbx-' + obj.id + '" value="' + obj.id + '" checked="checked" class="custom-control-input">';
        li += '   <span class="custom-control-indicator"></span>';
        li += '   <span class="custom-control-description">' + obj.title + '</span>';
        li += '	</label>';
        li += '<i class="fa fa-times" onclick="editorController.removeCategory(\'' + obj.id + '\')"></i>';
        li += '</li>';
        $("#edit-categories").prepend(li);
        $("#txtCategory").val('');
        $("#txtCategory").focus();
    }

    function removeCategory(id) {
        dataService.remove("blogifier/api/categories/" + id, removeCategoryCallback(id), fail);
    }

    function removeCategoryCallback(id) {
        $("#cat-" + id).remove();
        $("#txtCategory").focus();
    }

    function resetPostImage() {
        var postId = $('#hdnPostId').val();
        if (postId == "0") {
            imgResetCallback();
        }
        else {
            dataService.remove('blogifier/api/assets/resetpostimage/' + postId, imgResetCallback, fail);
        }
    }

    function imgResetCallback() {
        $('#hdnPostImg').val('');
        loadPostImage();
    }

    function loadPostImage() {
        var postId = $('#hdnPostId').val();
        var postImg = $('#hdnPostImg').val();
        $('#post-image').empty();
        if (!postImg.length > 0) {
            var btn = '<button type="button" title="Add Cover" class="btn btn-secondary btn-block" data-placement="bottom" onclick="return editorController.openFilePicker(' + postId + ');">Upload Post Cover</button >';
        }
        $('#post-image').append(btn);
        if (postImg.length > 0) {
            var dd = '<div class="admin-editor-cover-image"><img src="' + postImg + '" /></div>';
            dd += '<button type="button" class="btn btn-danger btn-block" onclick="return editorController.resetPostImage();">Remove Cover</button>';
        }
        $('#post-image').append(dd);
    }

    function loadActionButtons() {
        var postId = $('#hdnPostId').val();
        var postSlug = $('#hdnPostSlug').val();
        var published = $('#hdnPublished').val();
        $('#action-buttons').empty();
        var btn = '';
        if (postId === '0') {
            // new
            btn += '<div class="btn-group dropup float-left">';
            btn += '<button type="button" onclick="editorController.savePost(true); return false;" class="btn btn-sm btn-primary">Publish</button>';
            btn += '<button type="button" class="btn btn-sm btn-primary dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>';
            btn += '<ul class="dropdown-menu dropdown-menu-right">';
            btn += '<li><a class="dropdown-item" href="#" onclick="editorController.savePost(); return false;">Save</a></li>';
            btn += '</ul></div>';
        }
        else {
            if (published.indexOf("0001") >= 0) {
                // draft
                btn += '<div class="btn-group dropup float-left">';
                btn += '<button type="button" onclick="editorController.savePost(true); return false;" class="btn btn-sm btn-primary">Publish</button>';
                btn += '<button type="button" class="btn btn-sm btn-primary dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>';
                btn += '<ul class="dropdown-menu dropdown-menu-right">';
                btn += '<li><a class="dropdown-item" href="#" onclick="editorController.savePost(); return false;">Save</a></li>';
                btn += '<li><a class="dropdown-item" href="#" onclick="editorController.deletePost(); return false;">Delete</a></li>';
                btn += '</ul></div>';
            }
            else {
                // published
                btn += '<div class="btn-group dropup float-left">';
                btn += '<button type="button" onclick="editorController.savePost(); return false;" class="btn btn-sm btn-primary">Save</button>';
                btn += '<button type="button" class="btn btn-sm btn-primary dropdown-toggle dropdown-toggle-split" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"></button>';
                btn += '<ul class="dropdown-menu dropdown-menu-right">';
                btn += '<li><a class="dropdown-item" href="#" onclick="editorController.unpublishPost(); return false;">Unpublish</a></li>';
                btn += '<li><a class="dropdown-item" href="#" onclick="editorController.deletePost(); return false;">Delete</a></li>';
                btn += '</ul></div>';
                btn += '<a href="' + webRoot + blogRoute + postSlug + '" target="_blank" class="btn btn-outline-primary btn-icon float-left btn-sm ml-2" aria-label="View post" title="View" data-placement="bottom"><i class="fa fa-eye"></i></a>';
            }
        }
        $('#action-buttons').append(btn);
    }

    function fail(jqXHR, exception) {
        var msg = '';
        if (jqXHR.status === 0) { msg = 'Not connect.\n Verify Network.'; }
        else if (jqXHR.status == 404) { msg = 'Requested page not found. [404]'; }
        else if (jqXHR.status == 500) { msg = 'Internal Server Error [500].'; }
        else if (exception === 'parsererror') { msg = 'Requested JSON parse failed.'; }
        else if (exception === 'timeout') { msg = 'Time out error.'; }
        else if (exception === 'abort') { msg = 'Ajax request aborted.'; }
        else { msg = 'Uncaught Error.\n' + jqXHR.responseText; }
        toastr.error(msg);
    }

    return {
        savePost: savePost,
        unpublishPost: unpublishPost,
        deletePost: deletePost,
        saveCategory: saveCategory,
        removeCategory: removeCategory,
        categoryKeyPress: categoryKeyPress,
        loadPostImage: loadPostImage,
        resetPostImage: resetPostImage,
        loadActionButtons: loadActionButtons
    }
}(DataService);

document.addEventListener("DOMContentLoaded", function (event) {
    tinymce.init({
        skin: "blogifier",
        selector: '#txtContent',
        plugins: [
            "autoresize autolink lists link image paste code textcolor imagetools hr media table contextmenu fileupload codesample placeholder"
        ],
        toolbar: "heading bold italic underline strikethrough alignleft aligncenter alignright alignjustify | bullist numlist forecolor backcolor link media fileupload codesample code",
        block_formats: 'H=""; H1=h1;H2=h2;H3=h3;H4=h4;H5=h5;H6=h6',
        autosave_ask_before_unload: false,
        contextmenu_never_use_native: true,
        contextmenu: "link bold italic underline | inserttable hr | subscript superscript | removeformat",
        height: 360,
        autoresize_min_height: 260,
        content_style: "html {overflow:hidden !important; opacity:0;}",
        statusbar: false,
        branding: false,
        menubar: false,
        relative_urls: false,
        browser_spellcheck: true,
        paste_data_images: true,
        images_upload_url: '/blogifier/api/assets/upload',
        placeholder_attrs: {
            class: "tinymce-placeholder",
            style: ""
        },
        setup: function (editor) {
            editor.addButton('heading', {
                type: 'menubutton',
                title: 'Heading',
                text: false,
                icon: 'header',
                classes: 'btn-heading',
                menu: [{
                    text: 'h1',
                    classes: 'heading-1',
                    onclick: function () {
                        editor.focus();
                        editor.execCommand('FormatBlock', false, 'h1');
                    }
                }, {
                    text: 'h2',
                    classes: 'heading-2',
                    onclick: function () {
                        editor.execCommand('FormatBlock', false, 'h2');
                    }
                }, {
                    text: 'h3',
                    classes: 'heading-3',
                    onclick: function () {
                        editor.execCommand('FormatBlock', false, 'h3');
                    }
                }, {
                    text: 'h4',
                    classes: 'heading-4',
                    onclick: function () {
                        editor.execCommand('FormatBlock', false, 'h4');
                    }
                }, {
                    text: 'h5',
                    classes: 'heading-5',
                    onclick: function () {
                        editor.execCommand('FormatBlock', false, 'h5');
                    }
                }, {
                    text: 'h6',
                    classes: 'heading-6',
                    onclick: function () {
                        editor.execCommand('FormatBlock', false, 'h6');
                    }
                }]
            });
        }
    });
});