function onPickFile(id) {
    $(`#${id}`).click();
}
function onFilePicked(id, data) {
    $(`#${id}`).attr('src', 'data:image/jpeg;base64,' + data);
}

function triggerFileDownload(fileName, url) {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    anchorElement.click();
    anchorElement.remove();
}