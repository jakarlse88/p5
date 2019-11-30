(function uppy() {
    const uppy = Uppy.Core({
        restrictions: { allowedFileTypes: ['image/*', '.jpg', '.jpeg', '.png',] }
    })
        .use(Uppy.Dashboard, {
            inline: true,
            hideUploadButton: false,
            target: '#drag-drop-area'
        })
        .use(Uppy.XHRUpload, {
            endpoint: "https://" + `${location.hostname}:${location.port}` + "/admin/media/upload",
            formData: true,
            fieldName: "files",
            bundle: true
        });

    uppy.on("upload-success", (file, response) => {
        response.body.forEach((item, index) => {
            $(".img-select-hidden").append(`<input name="ImgNames" value=${item}>${item}</input>`);
        });
    });

    uppy.on('complete', (result) => {
        console.log('Upload complete! We’ve uploaded these files:', result.successful)
    });
})();

(function priceCalculationHandler() {
    let repairCost = $(".__repair-cost");
    let purchasePrice = $(".__purchase-price");

    function calculateSellingPrice() {
        let i = $(repairCost).val() !== "" ? parseInt($(repairCost).val()) : 0;
        let j = $(purchasePrice).val() !== "" ? parseInt($(purchasePrice).val()) : 0;

        return i + j + 500;
    }

    $(repairCost).on("input", () => {
        $(".__selling-price").attr("value", calculateSellingPrice());
    });

    $(purchasePrice).on("input", () => {
        $(".__selling-price").attr("value", calculateSellingPrice());
    });
})();