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

    uppy.on('file-removed', file => {
        console.log('Removed file', file);
        let antiForgeryToken =
            $("input[name='__RequestVerificationToken']").val();

        uppy.removeFile(file.id);

        return fetch("https://" +
            `${location.hostname}:${location.port}`
            + "/admin/media/delete/" +
            `${file.name}`,
            {
                method: "POST",
                headers: {
                    RequestVerificationToken: antiForgeryToken,
                    'Content-Type': "application/json"
                },
                mode: "same-origin",
                body: JSON.stringify(file.name)
            });
    });

    uppy.on('complete', (result) => {
        result.successful[0].response.body.forEach((item, index) => {
            $(".img-select-hidden").append(`<input name="ImgNames" value=${item}>${item}</input>`);
        });
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