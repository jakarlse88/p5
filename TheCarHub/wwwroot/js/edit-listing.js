$(document).ready(() => {
    console.log($('.statusSelect').text());
    if ($('.statusSelect :selected').text().toLowerCase() === "sold") {
        $('#saleDatePicker').show();
    }

    (function uppy() {
        const uppy = Uppy.Core({
            restrictions: {allowedFileTypes: ['image/*', '.jpg', '.jpeg', '.png',]}
        })
            .use(Uppy.Dashboard, {
                inline: true,
                hideUploadButton: false,
                target: '#drag-drop-area',
                height: "400px"
            })
            .use(Uppy.XHRUpload, {
                endpoint: "https://" + `${location.hostname}:${location.port}` + "/admin/media/upload",
                formData: true,
                fieldName: "files",
                bundle: true
            });

        $("#edit-media-list").children('li').each(function (i) {
            uppy.getFiles().forEach(file => {
                uppy.setFileState(file.id, {
                    progress: {uploadComplete: true, uploadStarted: true}
                })
            });

            fetch('/media/' + $(this).text())
                .then(res => res.blob())
                .then(blob => {
                    uppy.addFile({
                        name: $(this).text(),
                        type: "image/jpeg",
                        data: blob,
                        source: "Local",
                        isRemote: false
                    })
                });

            uppy.reset();
        });

        uppy.on('file-removed', async (file) => {
            console.log('Removed file', file);
            let antiForgeryToken =
                $("input[name='__RequestVerificationToken']").val();

            await fetch("https://" +
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

            // uppy.removeFile(file.id);
        });

        uppy.on('complete', (result) => {
            console.log("complete");
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
});

$('.statusSelect').on('change', () => {
    $('#saleDatePicker').toggle();
});
