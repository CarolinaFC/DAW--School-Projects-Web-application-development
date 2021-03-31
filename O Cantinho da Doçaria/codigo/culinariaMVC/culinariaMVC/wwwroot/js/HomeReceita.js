$(document).ready(function () {
    var id_rec = $('.hidden_id_rec').attr('id');


    var opt_cat = $('select[id="categoria"]');

    $.ajax({
        url: '/Home/GetCategoria/',
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
    var cate = $('#categoria').val();
    var grau = $('#grau_dific').val();
    var custo = $('#custo_ref').val();
    var tempo_conf = $('#tempo_conf').val();

    var tempo = tempo_conf.replace(/\s/g, '');

    console.log(cate);
    console.log(grau);
    console.log(custo);
    console.log(tempo_conf);

    var card = $('div[id="card_receita"]');

    if (cate == 0) {
        //Mostrar todas as receitas
        $.ajax({
            url: '/Home/GetReceita',
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
                            '</div>\n' +
                            '</div >')
                    });

                    $('.footer').empty();
                    $('.footer').append('<div id="footer">\n' +
                        '&copy; 2021 - culinariaMVC\n' +
                        '</div>')
                }
            }
        });
    } else {
        //Filtrar só por categoria
        $.ajax({
            url: '/Home/GetReceitaCategoriaFilter/?id_cate=' + cate,
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
                            '</div>\n' +
                            '</div >')
                    });

                    $('.footer').empty();
                    $('.footer').append('<div id="footer" style="margin-top:13.8%">\n' +
                        '&copy; 2021 - culinariaMVC\n' +
                        '</div>')
                }
            }
        });
    }

    

    

    
});

