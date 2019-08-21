tinymce.PluginManager.add("base64_image", function (a, b) {
    a.addButton("base64_image", {
        icon: "image",
        tooltip: "Insert image",
        onclick: function () {
            a.windowManager.open({
                title: "Insert image",
                url: b + "/dialog.html",
                width: 500,
                height: 150,
                buttons: [{
                    text: "Ok",
                    classes: 'widget btn primary first abs-layout-item',
                    onclick: function () {
                        var b = a.windowManager.getWindows()[0];

                        if (b.getContentWindow().document.getElementById('content').value == '') {
                            alert('Please select a file');
                            return false;
                        }

                        if (b.getContentWindow().document.getElementById('content').files[0].size > 1000 * 1024) {
                            alert('Max image file size is 1MB');
                            return false;
                        }

                        if (b.getContentWindow().document.getElementById('content').files[0].type != "image/jpeg" && b.getContentWindow().document.getElementById('content').files[0].type != "image/jpg" &&
                            b.getContentWindow().document.getElementById('content').files[0].type != "image/png" && b.getContentWindow().document.getElementById('content').files[0].type != "image/gif") {
                            alert('Only image file format can be uploaded');
                            return false;
                        }

                        var data;

                        data = new FormData();
                        data.append('file', b.getContentWindow().document.getElementById('content').files[0]);
                        data.append('rewardTypeId', rewardId);

                        $.ajax({
                            url: twdsimagepathUrl,
                            data: data,
                            processData: false,
                            contentType: false,
                            async: false,
                            type: 'POST',
                        }).done(function (msg) {
                            console.log(msg);
                          //  var imageAlt = b.getContentWindow().document.getElementById('desc').value;
                           // var imageSrc = "data:" + b.getContentWindow().document.getElementById('content').files[0].type + ";base64," + msg;
                           // if (msg == "Ok")
                           // alert("Success");
                            var imageTag = ' <a href="'+msg+'">View</a>';
                          
                           // a.insertContent(imageTag), b.close()

                        }).fail(function (jqXHR, textStatus) {
                            alert("Request failed: " + jqXH.responseText + " --- " +RtextStatus);
                        });
                    }
                }, {
                    text: "Close",
                    onclick: "close"
                }]
            })
        }
    }),

    a.addMenuItem("base64_image", {
        icon: "image",
        text: "Insert image",
        context: "insert",
        prependToContext: !0,
        onclick: function () {
            a.windowManager.open({
                title: "Insert image",
                url: b + twdsimagepathUrl,
                width: 500,
                height: 150,
                buttons: [{
                    text: "Ok",
                    classes: 'widget btn primary first abs-layout-item',
                    onclick: function () {
                        var b = a.windowManager.getWindows()[0];

                        if (b.getContentWindow().document.getElementById('content').value == '') {
                            alert('Please select a file');
                            return false;
                        }

                        if (b.getContentWindow().document.getElementById('content').files[0].size > 1000 * 1024) {
                            alert('Max image file size is 1MB');
                            return false;
                        }

                        if (b.getContentWindow().document.getElementById('content').files[0].type != "image/jpeg" && b.getContentWindow().document.getElementById('content').files[0].type != "image/jpg" &&
                            b.getContentWindow().document.getElementById('content').files[0].type != "image/png" && b.getContentWindow().document.getElementById('content').files[0].type != "image/gif") {
                            alert('Only image file format can be uploaded');
                            return false;
                        }

                        var data;

                        data = new FormData();
                        data.append('file', b.getContentWindow().document.getElementById('content').files[0]);
                        data.append('rewardTypeId', rewardId);
                        $.ajax({
                            url: twdsimagepathUrl,
                            data: data,
                            processData: false,
                            contentType: false,
                            async: false,
                            type: 'POST',
                        }).done(function (msg) {
                            console.log(msg);
                          //  var imageAlt = b.getContentWindow().document.getElementById('desc').value;
                           // var imageSrc = "data:" + b.getContentWindow().document.getElementById('content').files[0].type + ";base64," + msg;
                          //  if (msg == "Ok")
                               // alert("Success");
                          //  var imageTag = '<img src="' + msg + '" alt="' + imageAlt + '" style="max-width: 100%;" />';

                            //  a.insertContent(imageTag), b.close()

                            var imageTag = ' <a href="' + msg + '">View</a>';

                           // a.insertContent(imageTag), b.close()

                        }).fail(function (jqXHR, textStatus) {
                            alert("Request failed: " + jqXH.responseText + " --- " + RtextStatus);
                        });
                    }
                }, {
                    text: "Close",
                    onclick: "close"
                }]
            })
        }
    })

});