
$(document).ready(function () {
    $('.delete-form').on('submit', function (e) { // Lyssnar efter submit på form med klass="delete-form"
        e.preventDefault(); // Eventet används sedan mha jQuery till att förhindra att form skickas "som vanligt".
        var form = $(this); // I stället skapas en kopia av form som en variabel i jQuery.
        $.ajax({
            url: form.attr('action'),       // jQuery-form skickas sedan till action-metod.
            type: form.attr('method'),
            data: form.serialize(),
            success: function (result) {    // om status-code 200 => Visa success.
                if (result.success) {
                    // Remove author
                    form.closest('tr').remove(); // Deletar närmsta <tr> relativt sett från form-submit.
                    // Feedback to user if succes
                    $('#message').html('<div class="alert alert-success">Author deleted successfully.</div>');
                }
                else {
                    // Show error message if failure
                    $('#message').html('<div class="alert alert-danger">There was an error deleting the author.</div>');
                }
            },
            error: function () {
                // Show an error message
                $('#message').html('<div class="alert alert-danger">There was an error deleting the author.</div>');
            }
        });
    });
});
