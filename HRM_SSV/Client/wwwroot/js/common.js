function onPickFile(id) {
    $(`#${id}`).click();
}
function onFilePicked(id, data) {
    $(`#${id}`).attr('src', 'data:image/jpeg;base64,' + data);
}