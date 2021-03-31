/*-------------------- Mostrar Classificações automáticamente quando carrega a página -------------*/
$(document).ready(function () {

    var tableBody = $('tbody[id="viewClassTable"]');
    var status_uc = "";

    $.ajax({
        url: '/admin/viewClass/showTableViewClass',
        type: "GET",
        dataType: "json",
        success: function (data) {
            jQuery(tableBody).empty();
            if (data.length === 0) {
                jQuery(tableBody).empty();
                $(tableBody).append('<tr>' +
                    '<td id="noneClass" colspan="9">Nenhuma Classificação Encontrada</td>' +
                    '</tr>');

            } else{
                jQuery.each(data, function (key, value) {

                    if(value.nota >= 9.5) {
                        status_uc = 'Aprovado'
                    }
                    else {
                        status_uc = 'Reprovado'
                    }

                    $(tableBody).append('<tr>' +
                        '<td id="ano" scope="row">' + value.ano + '</td>' +
                        '<td id="semestre">' + value.semestre + '</td>' +
                        '<td id="nome_uc">' + value.nome_uc + '</td>' +
                        '<td id="ects">' + value.ects + '</td>' +
                        '<td id="anoLetivo">' + value.descricao_al + '</td>' +
                        '<td id="data">' + value.data + '</td>' +
                        '<td id="epoca_desc">' + value.descricao_ep + '</td>' +
                        '<td id="status_uc">'+ status_uc + '</td>' +
                        '<td id="nota">' + value.nota + '</td>' +
                        '</tr>');
                });
            }
        }
    })
})

/*---------------------------- Pesquisar Classificações--------------------------------------------*/
$(document).on('click', '#btn_procurar', function () {
    var anoLetivoSelect = jQuery('#ano_letivo :selected').val();
    var periodoSelect = jQuery('#periodo :selected').val();
    var anoCurricularSelect = jQuery('#ano_curricular :selected').val();
    var epocaSelect = jQuery('#epoca :selected').val();
    console.log(anoLetivoSelect + periodoSelect + anoCurricularSelect + epocaSelect);

    var tableBody = $('tbody[id="viewClassTable"]');
    var status_uc = "";

    $.ajax({
        url: '/admin/viewClass/showTableViewClass/' + anoLetivoSelect + '/' + periodoSelect + '/' + anoCurricularSelect + '/' + epocaSelect,
        type: "GET",
        dataType: "json",
        success: function (data) {

            jQuery(tableBody).empty();
            $(tableBody).append('<td colspan="8" id="loading"><div class="spinner-border spinner-border-sm"></div></td>');

            setTimeout(
                function()
                {
                    if (data.length === 0) {
                        jQuery(tableBody).empty();
                        $(tableBody).append('<tr>' +
                            '<td id="noneClass1" colspan="9">Nenhuma Classificação Encontrada</td>' +
                            '</tr>');

                    } else {
                        jQuery(tableBody).empty();
                        jQuery.each(data, function (key, value) {

                            if(value.nota >= 9.5) {
                                status_uc = 'Aprovado'
                            }
                            else {
                                status_uc = 'Reprovado'
                            }

                            $(tableBody).append('<tr>' +
                                '<td id="ano1" scope="row">' + value.ano + '</td>' +
                                '<td id="semestre1">' + value.semestre + '</td>' +
                                '<td id="nome_uc1">' + value.nome_uc + '</td>' +
                                '<td id="ects1">' + value.ects + '</td>' +
                                '<td id="anoLetivo1">' + value.descricao_al + '</td>' +
                                '<td id="data1">' + value.data + '</td>' +
                                '<td id="epoca_desc1">' + value.descricao_ep + '</td>' +
                                '<td id="status_uc1">'+ status_uc + '</td>' +
                                '<td id="nota1">' + value.nota + '</td>' +
                                '</tr>');
                        });
                    }
                }, 250);





        }
    })
})
