$(document).ready(function () {

    var opt_cat = $('select[id="categoria"]');

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

$('#btn_pesquisar').click(function () {
    var name_admin = $('.hidden_name_admin').attr('id');
    console.log(name_admin);

    var cate = $('#categoria').val();
    console.log(cate);

    var card = $('div[id="card_receita"]');

    if (cate == 0) {
        //Mostrar todas as receitas criadas pelo admin especifico
        $.ajax({
            url: '/Admin_pasteleiro/GetReceitaCategoryFilter_Arquivos/?name_admin=' + name_admin,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);

                $(card).empty();

                if (data.length === 0) {
                    $(card).empty();

                } else {

                    $.each(data, function (key, value) {
                        $(card).append('<div class="column">\n' +
                            '<div class="card">\n' +
                            '<img src="https://localhost:44313/Photos/' + value.imgReceita + '" class= "rounded img_card" >\n' +
                            '<br>\n' +
                            '<a href="/Receitas/Details/' + value.id + '" id="nome_rec" style="color: black"><b>' + value.nomeReceita + '</b></a>\n' +
                            '<div class="row">\n' +
                            '<div class="col-6">\n' +
                            '<a class="btn btn-secondary" type="button" id="btn_edit" href="/Receitas/Edit/' + value.id + '">Editar</a>\n' +
                            '</div>\n' +
                            '<div class="col-6">\n' +
                            '<a class="btn btn-danger" type="button" id="btn_remove" href="/Receitas/Delete/' + value.id + '">Remover</a>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div >')
                    });

                    $('.footer').empty();
                    $('.footer').append('<div id="footer">\n' +
                        '&copy; 2021 - culinariaMVC\n' +
                        '</div>')
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }

        });
    } else {
        //Filtrar só por categoria
        $.ajax({
            url: '/Admin_pasteleiro/GetReceitaCategoriaFilter/?id_cate=' + cate + '&name_admin=' + name_admin,
            type: 'GET',
            dataType: 'json',
            success: function (data) {
                console.log(data);

                $(card).empty();

                if (data.length === 0) {
                    $(card).empty();
                    $(card).append('<div class="container" id="aviso_auth" style="background-color:#c83f3f;color:white">\n' +
                        '<a> Não foram encontradas <b>Receitas</b> criadas por você.</a >\n' +
                        '<br>\n' +
                        '</div >')

                } else {

                    $.each(data, function (key, value) {
                        $(card).append('<div class="column">\n' +
                            '<div class="card">\n' +
                            '<img src="https://localhost:44313/Photos/' + value.imgReceita + '" class= "rounded img_card" >\n' +
                            '<br>\n' +
                            '<a href="/Receitas/Details/' + value.id + '" id="nome_rec" style="color: black"><b>' + value.nomeReceita + '</b></a>\n' +
                            '<div class="row">\n' +
                            '<div class="col-6">\n' +
                            '<a class="btn btn-secondary" type="button" id="btn_edit" href="/Receitas/Edit/' + value.id + '">Editar</a>\n' +
                            '</div>\n' +
                            '<div class="col-6">\n' +
                            '<a class="btn btn-danger" type="button" id="btn_remove" href="/Receitas/Delete/' + value.id + '">Remover</a>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div >')
                    });

                    $('.footer').empty();
                    $('.footer').append('<div id="footer" style="margin-top:11.4%">\n' +
                        '&copy; 2021 - culinariaMVC\n' +
                        '</div>')
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        });
    }
})