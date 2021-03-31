$(document).ready(function () {

    var today = new Date();
    var dd = String(today.getDate()).padStart(2, '0');
    var mm = String(today.getMonth() + 1).padStart(2, '0'); //January is 0!
    var yyyy = today.getFullYear();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();

    today = dd + '/' + mm + '/' + yyyy + ' ' + time;
    console.log(today);
    $('#date').attr('value', today);

    var opt_cat = $('select[id="categoria_editReceita"]');

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

    //$('#categoria_editReceita option[value=10]').attr('selected', 'selected');
    //$('#categoria_editReceita').val();
   /* var x = document.getElementById('categoria_editReceita').selectedIndex;
    console.log(x);*/

    $('input[name=ImageFile]').change(function () {
        var file = $('#img_upload')[0].files[0]
        if (file) {
            var filename = file.name;
            console.log(filename);

        }
    })

});