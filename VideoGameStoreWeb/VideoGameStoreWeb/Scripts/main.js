
               function ShowMessage(message, messagetype) {            
                   switch (messagetype) {
                       case 'success':
                           toastr.success(message);
                           break;
                       case 'warning':
                           toastr.warning(message);
                           break;
                       case 'error':
                           toastr.error(message);
                           break;
                       default:
                           toastr.success(message);
                   }
               }

function filter(genre, platform) {
    //filter by both genre and platform
    if (genre != 'All' && platform != 'All') {
        $('.gameCategory').each(function (index, value) {
            if ($(this).html() != genre || $(this).parent().next().find('.gamePlatform').html() != platform) {
                $(this).closest('tr').hide();
            }
            else {
                $(this).closest('tr').show();
            }
        });
    }
    //filter by genre only
    else if (genre != 'All' && platform == 'All') {
        $('.gameCategory').each(function (index, value) {
            if ($(this).html() != genre) {
                $(this).closest('tr').hide();
            }
            else {
                $(this).closest('tr').show();
            }
        });
    }
    //filter by platform only
    else if (genre == 'All' && platform != 'All') {
        $('.gamePlatform').each(function (index, value) {
            if ($(this).html() != platform) {
                $(this).closest('tr').hide();
            }
            else {
                $(this).closest('tr').show();
            }
        });
    }
    //not filtered
    else {
        $('.gameCategory').closest('tr').show();
    }     
}
