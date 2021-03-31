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
    getAvaliacao(id_rec);

    //Obter todos os comentarios da receita
    comentPub(id_rec);

    $('#btn_comentar').click(function () {
        var coment = $('#new_comentario').val();
        console.log(coment);

        var rating = $('input[id=rating]').val();
        console.log(rating);

        if (rating != null && coment != "") {
            $.ajax({
                url: '/Receitas/PostAvaliacao/?id_receita=' + id_rec + '&rating=' + rating,
                type: 'POST',
                data: rating,
                success: function (data) {
                    console.log(data);

                    $('#rating').val('');

                    getAvaliacao(id_rec);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });

            $.ajax({
                url: '/Receitas/PostComentPub/?coment=' + coment + '&id_receita=' + id_rec,
                type: 'POST',
                data: coment,
                success: function (data) {
                    console.log(data);

                    comentPub(id_rec);

                    $('#new_comentario').val('');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });
        } else if (rating != null && coment == "") {
            $.ajax({
                url: '/Receitas/PostAvaliacao/?id_receita=' + id_rec + '&rating=' + rating,
                type: 'POST',
                data: rating,
                success: function (data) {
                    console.log(data);

                    $('#rating').val('');
                    getAvaliacao(id_rec);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });

            coment = "Sem Comentário";
            $.ajax({
                url: '/Receitas/PostComentPub/?coment=' + coment + '&id_receita=' + id_rec,
                type: 'POST',
                data: coment,
                success: function (data) {
                    console.log(data);

                    comentPub(id_rec);

                    $('#new_comentario').val('');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });

        } else {
            $.ajax({
                url: '/Receitas/PostAvaliacao/?id_receita=' + id_rec + '&rating=' + rating,
                type: 'POST',
                data: rating,
                success: function (data) {
                    console.log(data);

                    $('#rating').val('');
                    getAvaliacao(id_rec);
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });

            $.ajax({
                url: '/Receitas/PostComentPub/?coment=' + coment + '&id_receita=' + id_rec,
                type: 'POST',
                data: coment,
                success: function (data) {
                    console.log(data);

                    comentPub(id_rec);

                    $('#new_comentario').val('');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    console.log(textStatus, errorThrown);
                }
            });
        }
    })
       
     
});

function comentPub(id_rec) {

    var card_coment = $('div[id="coment_box"]');

    $.ajax({
        url: '/Receitas/GetComentariosPub/?id=' + id_rec,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);
            $(card_coment).empty();

            $.each(data, function (key, value) {
                if (value.descricaoComentariosPub != "Sem Comentário") {
                    if (key % 2 == 0) {
                        $(card_coment).append('<div class="container">\n' +
                            '<p id="' + value.id + '">' + value.descricaoComentariosPub + '</p>\n' +
                            '</div>')
                    }
                    else {
                        $(card_coment).append('<div class="container darker">\n' +
                            '<p id="' + value.id + '">' + value.descricaoComentariosPub + '</p>\n' +
                            '</div>')
                    }
                }
                



            });
        }
    });
}

function getAvaliacao(id_rec) {
    var stars = $('div[id=stars]');
    $.ajax({
        url: '/Receitas/GetAvaliacao/?id_rec=' + id_rec,
        type: 'GET',
        dataType: 'json',
        success: function (data) {
            console.log(data);

            var soma_estrelas = 0;

            $.each(data, function (key, value) {
                soma_estrelas = soma_estrelas + parseInt(value.quantidadeEstrelas);
            });


            $(stars).empty();
            var i;
            var media_estrelas = soma_estrelas / data.length;
            for (i = 0; i < media_estrelas; i++) {
                $(stars).append('<span class="fa fa-star checked"></span>')
            }
        }
    });
}