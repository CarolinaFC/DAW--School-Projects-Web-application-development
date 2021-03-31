/*---------------------------------------------- Atualizar automaticamente a UC qd se seleciona o Curso ----------------------------------------------*/
jQuery(document).ready(function () {

    jQuery('select[name="curso"]').on('change', function () {
        var cursoID = jQuery(this).val();
        if (cursoID) {
            jQuery.ajax({
                url: '/admin/RegisterClass/filter/' + cursoID,
                type: "GET",
                dataType: "json",
                success: function (data) {
                    jQuery('select[name="uc"]').empty();
                    jQuery.each(data, function (key, value) {
                        $('select[name="uc"]').append('<option value="' + key + '">' + value + '</option>');
                    });
                }
            });
        } else {
            jQuery('select[name="uc"]').empty();
        }
    });

    $("#success").fadeTo(2000, 800).slideUp(800, function (){
        $("#success").slideUp(800);
    });
});

/*--------------------------------------------------- Apresentar tabela para inserir as classificações --------------------------------------------------*/
$(document).on('click', '#btn_search_add', function () {
    var cursoSelect = jQuery('#curso :selected').val();
    var ucSelect = jQuery('#uc :selected').val();
    var epocaSelect = jQuery('#epoca :selected').val();
    console.log(cursoSelect + ucSelect + epocaSelect);

    var tableBody = $('tbody[id="registarClass"]');

    var i = 0;

    $.ajax({
        url: '/admin/RegisterClass/showTableRegisterClass/' + cursoSelect + '/' + ucSelect + '/' + epocaSelect,
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
                                '<td colspan="6">Nenhuma Avaliação Encontrada</td>' +
                                '</tr>');

                        } else {
                            jQuery(tableBody).empty();
                            jQuery('td[id="dadosNaoSelecionados"]').empty();
                            jQuery.each(data, function (key, value) {
                                $(tableBody).append('<tr><td scope="row"><input class="tableInput" name="numero_aluno.' + i + '" value="' + value.numero + '" readonly></td>' +
                                    '<td ><input class="tableInput" name="nome_completo_aluno.' + i + '" value="' + value.nome_completo + '"></td>' +
                                    '<td><input class="tableInput" name="data_avaliacao" value="' + value.data + '"></td>' +
                                    '<td><input type="number" min="0" max="20" placeholder="Nota" class="nota" name="nota.' + i + '"></td>' +
                                    '<td>\n' +
                                    '         <select class="form-control" id="select_epoca1" name="select_Statepoca.' + i + '">\n' +
                                    '           <option value="Inscrito">Inscrito</option>\n' +
                                    '           <option value="Faltou">Faltou</option>\n' +
                                    '           <option value="Não Admitido">Não Admitido</option>\n' +
                                    '           <option value="Aprovado">Aprovado</option>\n' +
                                    '           <option value="Reprovado">Reprovado</option>\n' +
                                    '           <option value="Desistiu">Desistiu</option>\n' +
                                    '           <option value="Anulado">Anulado</option>\n' +
                                    '           <option value="Disciplina do mestrado realizada na Licenciatura">Disciplina do mestrado realizada na Licenciatura</option> \n' +
                                    '         </select> \n' +
                                    '</td>\n' +
                                    '<td>\n' +
                                    '        <select class="form-control" name="select_Stateuc.' + i + '">\n' +
                                    '          <option value="Inscrito">Inscrito</option>\n' +
                                    '          <option value="Aprovado">Aprovado</option>\n' +
                                    '          <option value="Reprovado">Reprovado</option>\n' +
                                    '          <option value="«Indefinido»">«Indefinido»</option>\n' +
                                    '        </select>\n' +
                                    '</td>' +
                                    '</tr>');
                                i++;
                            });
                        }
                }, 450);
        }
    });
})
