$(function () { /* to make sure the script runs after page load */

    $('.item').each(function (event) { /* select all divs with the item class */

        var max_length = 50; /* set the max content length before a read more link will be added */

        if ($(this).html().length > max_length) { /* check for content length */

            var short_content = $(this).html().substr(0, max_length); /* split the content in two parts */
            var long_content = $(this).html().substr(max_length);

            $(this).html(short_content + '<a href="#" class="read_more">Show More</a>' +
                '<span class="more_text" style="display:none;">' + long_content + '</span>' + '<a href="#" class="read_less" style="display:none;">Show Less</a>'); /* Alter the html to allow the read more functionality */

            $(this).find('a.read_more').click(function (event) { /* find the a.read_more element within the new html and bind the following code to it */

                event.preventDefault(); /* prevent the a from changing the url */
                $(this).hide(); /* hide the read more button */
                $('.read_less').show(); /* show read less button */

                $(this).parents('.item').find('.more_text').show(); /* show the .more_text span */

            });

            $(this).find('a.read_less').click(function (event) {
                event.preventDefault();

                $(this).hide(); /* hide the read more button */
                $('.read_less').hide();
                $('.read_more').show();

                $(this).parents('.item').find('.more_text').hide();

            });

        }

    });


});