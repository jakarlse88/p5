function onSubmit(){
    console.log($('#sender-name').val());
    
    let antiForgeryToken =
        $("input[name='__RequestVerificationToken']").val();
    
    $.ajax({
        url: "https://" + `${location.hostname}:${location.port}` + "contact/sendemail",
        data: {
            'inputModel': {
                'SenderName': $('#sender-name').val(),
                'SenderEmail': $('#sender-email').val(),
                'Message': $('#sender-message').val()
            }
            // + JSON.stringify({
            //     SenderName: ,
            //     SenderEmail: $('#sender-email').val(),
            //     Message: $('#sender-message').val()
            // })
        },
        type: "POST",
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'text/plain',
            RequestVerificationToken: antiForgeryToken,
        },
        // contentType: "application/json",
        success: res => $('#quickContactPartialContainer').html(res)
    })
}