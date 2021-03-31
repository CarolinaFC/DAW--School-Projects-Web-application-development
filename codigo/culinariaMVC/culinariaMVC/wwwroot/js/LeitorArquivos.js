$(document).ready(function () {

    var name_leitor = $('.hidden_name_leitor').attr('id');
    
    var opt_cat = $('select[id="categoria"]');

    $.ajax({
        url: '/MinhasReceitas/GetCategoria/',
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);

            $(opt_cat).empty();

            if (data.length === 0) {
                $(opt_cat).empty();
                $(card).append('<div class="container" id="aviso_auth" style="background-color:#c83f3f;color:white">\n' +
                    '<a> Não foram encontradas <b>Receitas</b> guardadas por você.</a >\n' +
                    '<br>\n' +
                    '</div >')

            } else {

                $.each(data, function (key, value) {
                    $(opt_cat).append('<option value="' + value.id + '">' + value.descriçãoCat + '</option>\n')
                });

            }
        }
    });

    $('.footer').empty();
    $('.footer').append('<div id="footer" style="margin-top:12.5%">\n' +
        '&copy; 2021 - culinariaMVC\n' +
        '</div>')

});

$('#btn_pesquisar').click(function () {
    var name_leitor = $('.hidden_name_leitor').attr('id');
    console.log(name_leitor);

    var cate = $('#categoria').val();

    console.log(cate);

    var card = $('div[id="card_receita"]');

    if (cate == 0) {
        //Mostrar todas as receitas guardadas do leitor
        $.ajax({
            url: '/Leitors/GetReceitaCategoryFilter_Arquivos/?name_leitor=' + name_leitor,
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
                            '<a href="/MinhasReceitas/Details/' + value.id + '" id="nome_rec" style="color: black"><b>' + value.nomeReceita + '</b></a>\n' +
                            '<div class="row">\n' +
                            '<div class="col-12">\n' +
                            '<a class="btn btn-danger" type="button" id="btn_remove" href="/MinhasReceitas/Delete/' + value.id + '>Remover</a>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div >')
                    });

                    $('.footer').empty();
                    $('.footer').append('<div id="footer" style="margin-top:12.5%">\n' +
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
            url: '/Leitors/GetReceitaCategoriaFilter/?id_cate=' + cate + '&name_leitor=' + name_leitor,
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
                            '<a href="/MinhasReceitas/Details/' + value.id + '" id="nome_rec" style="color: black"><b>' + value.nomeReceita + '</b></a>\n' +
                            '<div class="row">\n' +
                            '<div class="col-12">\n' +
                            '<a class="btn btn-danger" type="button" id="btn_remove" href="/MinhasReceitas/Delete/' + value.id + '>Remover</a>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div>\n' +
                            '</div >')
                    });

                    $('.footer').empty();
                    $('.footer').append('<div id="footer" style="margin-top:12.5%">\n' +
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