window.readFileAsBase64 = (fileInput) => {
    const readAsDataURL = (fileInput) => {
        return new Promise((resolve, reject) => {
            const reader = new FileReader();
            reader.onerror = () => {
                reader.abort();
                reject(new Error("Error reading file."));
            };
            reader.addEventListener("load", () => {
                resolve(reader.result);
            }, false);
            reader.readAsDataURL(fileInput.files[0]);
        });
    };

    return readAsDataURL(fileInput);
};

function saveAsFile(filename, bytesBase64) {
    var link = document.createElement('a');
    link.download = filename;
    link.href = "data:application/octet-stream;base64," + bytesBase64;
    document.body.appendChild(link); // Needed for Firefox
    link.click();
    document.body.removeChild(link);
}