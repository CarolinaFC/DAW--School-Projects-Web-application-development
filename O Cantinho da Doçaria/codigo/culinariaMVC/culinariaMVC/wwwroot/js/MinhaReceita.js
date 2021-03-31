$(document).ready(function () {

    var id_rec = $('.hidden_id_rec').attr('id');
    console.log(id_rec);

    var id_cat = $('.hidden_id_cat').attr('id');
    console.log(id_cat);

    //Obter o nome da categoria da receita
    var card = $('td[id="cat"]');
    $.ajax({
        url: '/Receitas/GetCategoriasNames/' + id_cat,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);

            $.each(data, function (key, value) {
                $(card).append(value.descriçãoCat)
            });
        }
    });

    //Obter a quantidade de estrelas da receita
    var stars = $('div[id=stars]');
    $.ajax({
        url: '/Receitas/GetAvaliacao/?id_rec=' + id_rec,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);

            var soma_estrelas = 0;

            $.each(data, function (key, value) {
                console.log(value.quantidadeEstrelas);

                soma_estrelas = soma_estrelas + parseInt(value.quantidadeEstrelas);
            });

            var i;

            var media_estrelas = soma_estrelas / data.length;
            for (i = 0; i < media_estrelas; i++) {
                $(stars).append('<span class="fa fa-star checked"></span>')
            }

        }
    });


    //Obter todos os comentarios da receita
    comentPriv(id_rec);

    $('#btn_comentar').click(function () {
        var coment = $('#new_note').val();
        console.log(coment);


        $.ajax({
            url: '/MinhasReceitas/PostComentariosPriv/?coment=' + coment + '&id_receita=' + id_rec,
            type: 'POST',
            data: coment,
            success: function (data) {
                console.log(data);

                comentPriv(id_rec);

                $('#new_note').val('');
            },
            error: function (jqXHR, textStatus, errorThrown) {
                console.log(textStatus, errorThrown);
            }
        })
    })

});

function comentPriv(id_rec) {

    var card_coment = $('div[id="coment_box"]');

    console.log(id_rec);

    $.ajax({
        url: '/MinhasReceitas/GetComentariosPriv/?id_receita=' + id_rec,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            console.log("ENTRA")
            $(card_coment).empty();

            $.each(data, function (key, value) {
                    if (key % 2 == 0) {
                        $(card_coment).append('<div class="container">\n' +
                            '<p id="' + value.id_comentariosPrivados + '">' + value.descricao + '</p>\n' +
                            '</div>')
                    }
                    else {
                        $(card_coment).append('<div class="container darker">\n' +
                            '<p id="' + value.id_comentariosPrivados + '">' + value.descricao + '</p>\n' +
                            '</div>')
                    }
            });
        },
        error: function (jqXHR, textStatus, errorThrown) {
            console.log(textStatus, errorThrown);
        }
    });
}