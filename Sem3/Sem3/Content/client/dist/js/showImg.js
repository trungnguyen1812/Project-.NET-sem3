function showImg(imgUpload, preview) {
    if (imgUpload.files && imgUpload.files[0]) {
        var reader = new FileReader();
        reader.onload = function (e) {
            $(preview).attr('src', e.target.result);
        }
        reader.readAsDataURL(imgUpload.files[0]);

    }
}