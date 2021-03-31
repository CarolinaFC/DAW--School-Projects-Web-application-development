$(document).ready(function () {

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

    today = dd + '/' + mm + '/' + yyyy + ' ' + time;
    console.log(today);
    $('#date').attr('value', today);


    var opt_cat = $('select[id="categoria_novaReceita"]');

    $.ajax({
        url: '/Admin_pasteleiro/GetCategoria/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);

            $(opt_cat).empty();

            if (data.length === 0) {
                $(opt_cat).empty();

            } else {

                $.each(data, function (key, value) {
                    $(opt_cat).append('<option value="' + value.id + '">' + value.descriçãoCat + '</option>\n')
                });

            }
        }
    });
});

