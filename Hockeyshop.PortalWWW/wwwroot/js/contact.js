$(function () {
    $('#contactForm, #teamContactForm').on('submit', function (e) {
        e.preventDefault();
        var $form = $(this);
        $.ajax({
            url: $form.attr('action'),
            type: 'POST',
            data: $form.serialize(),
            success: function (response) {
                if (response.success) {
                    $form.trigger('reset');
                    if ($form.attr('id') === 'teamContactForm') {
                        var modal = bootstrap.Modal.getInstance(document.getElementById('contactModal'));
                        modal.hide();
                    }
                    var successModal = new bootstrap.Modal(document.getElementById('contactSuccessModal'));
                    successModal.show();
                } else {
                    alert(response.message);
                }
            },
            error: function (xhr) {
                alert('An error occurred. Please try again later.');
            }
        });
    });
});