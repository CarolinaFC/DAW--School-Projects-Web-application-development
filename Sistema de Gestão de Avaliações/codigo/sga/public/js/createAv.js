function loadtable(){
    var curID = jQuery('#cursos :selected').val();

    if (curID) {

        jQuery.ajax({
            url: '/admin/CreateAv/filter/table/' + curID,
            type: "GET",
            dataType: "json",
            success: function (data) {

                jQuery('tbody[id="avaliacao"]').empty();
                $('tbody[id="avaliacao"]').append('<td colspan="8" id="loading" class="td_h"><div class="spinner-border spinner-border-sm"></div></td>');

                setTimeout(
                    function()
                    {
                        if (data.length === 0) {
                            jQuery('tbody[id="avaliacao"]').empty();
                            $('tbody[id="avaliacao"]').append('<tr>' +
                                '<td colspan="8">Nenhuma Avaliação Encontrada</td>' +
                                '</tr>');

                        } else {
                            jQuery('tbody[id="avaliacao"]').empty();
                            jQuery.each(data, function (key, value) {
                                $('tbody[id="avaliacao"]').append('<tr>' +
                                    '<td scope="row">' + value.descricao_al + '</td>' +
                                    '<td scope="row">' + value.ano + '</td>' +
                                    '<td scope="row">' + value.semestre + '</td>' +
                                    '<td scope="row">' + value.nome_uc + '</td>' +
                                    '<td scope="row">' + value.data + '</td>' +
                                    '<td scope="row">' + value.hora + '</td>' +
                                    '<td scope="row">' + value.sala + '</td>' +
                                    '<td scope="row">' + value.descricao_ep + '</td>');
                            });
                        }
                    }, 450);
            }
        });
    }
}

jQuery(document).ready(function () {

    // Load uc
    jQuery('select[name="curso"]').on('change', function () {
        var cursoID = jQuery(this).val();
        if (cursoID) {
            jQuery.ajax({
                url: '/admin/CreateAv/filter/' + cursoID,
                type: "GET",
                dataType: "json",
                success: function (data) {
                    if (data.length ===  0){
                        jQuery('select[name="uc"]').empty();
                        $('select[name="uc"]').append('<option>Nenhuma Uc Disponivel</option>');
                    } else{
                        jQuery('select[name="uc"]').empty();
                        jQuery.each(data, function (key, value) {
                            $('select[name="uc"]').append('<option value="' + key + '">' + value + '</option>');
                        });
                    }


                }
            });

        } else {
            jQuery.each(data, function () {
                jQuery('select[name="uc"]').empty();
                $('select[name="uc"]').append('<option value="">Nenhuma Uc Disponivel </option>');
            })
        }
    });

    // Load table
    jQuery('select[name="curso"]').on('change', function () {
        loadtable();
    });

    // Load table with filters
    jQuery('#filtrar').on('click', function () {
        var anoLetivoID = jQuery('#AnoLetivo :selected').val();
        var ucID = jQuery('#uc :selected').val();

        if (anoLetivoID && ucID) {
            jQuery.ajax({
                url: '/admin/CreateAv/filter/table/' + anoLetivoID + '/' + ucID,
                type: "GET",
                dataType: "json",
                success: function (data) {

                    jQuery('tbody[id="avaliacao"]').empty();
                    $('tbody[id="avaliacao"]').append('<td colspan="8" id="loading" class="td_h"><div class="spinner-border spinner-border-sm"></div></td>');

                    setTimeout(
                        function() {
                            if (data.length === 0) {
                                jQuery('tbody[id="avaliacao"]').empty();
                                $('tbody[id="avaliacao"]').append('<tr>' +
                                    '<td colspan="8">Nenhuma Avaliação Encontrada</td>' +
                                    '</tr>');

                            } else {
                                jQuery('tbody[id="avaliacao"]').empty();
                                jQuery.each(data, function (key, value) {
                                    $('tbody[id="avaliacao"]').append('<tr>' +
                                        '<td scope="row">' + value.descricao_al + '</td>' +
                                        '<td scope="row">' + value.ano + '</td>' +
                                        '<td scope="row">' + value.semestre + '</td>' +
                                        '<td scope="row">' + value.nome_uc + '</td>' +
                                        '<td scope="row">' + value.data + '</td>' +
                                        '<td scope="row">' + value.hora + '</td>' +
                                        '<td scope="row">' + value.sala + '</td>' +
                                        '<td scope="row">' + value.descricao_ep + '</td>');
                                });
                            }
                        }, 300);
                }
            });
        }
    });

    $("#success").fadeTo(2000, 800).slideUp(800, function (){
        $("#success").slideUp(800);
    });
});
