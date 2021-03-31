jQuery(document).ready(function () {

    // Load table de Avaliações
    var i = 0

    jQuery.ajax({
        url: '/admin/InscribeAv/avaliacoes',
        type: "GET",
        dataType: "json",
        success: function (data) {
            if (data.length ===  0){
                jQuery('tbody[id="av"]').empty();
                $('tbody[id="av"]').append('<tr>' +
                    '<td colspan="8">Nenhuma Avaliação Encontrada</td>' +
                    '</tr>' );
            } else{
                jQuery('tbody[id="av"]').empty();
                jQuery.each(data, function (key, value) {
                    $('tbody[id="av"]').append('<tr id="tr'+i+'">');

                    if (value.status_uc === "Aprovado" || value.descricao_ep === "Especial"){
                        $('#tr'+i+'').append('<th scope="row">\n' +
                            '                    <div class="form-check">\n' +
                            '                        <input class="Checked" id="ch'+i+'" name="'+value.nome_uc+'.10.'+value.id_avaliacao+'" type="checkbox">\n' +
                            '                    </div>\n' +
                            '                </th>');
                    } else{
                        $('#tr'+i+'').append('<th scope="row">\n' +
                            '                    <div class="form-check">\n' +
                            '                        <input class="Checked" id="ch'+i+'" name="'+value.nome_uc+'.5.'+value.id_avaliacao+'" type="checkbox">\n' +
                            '                    </div>\n' +
                            '                </th>');
                    }

                    $('#tr'+i+'').append('<td scope="row" class="ano">' + value.ano + '</td>' +
                        '<td scope="row" class="uc">' + value.nome_uc + '</td>' +
                        '<td scope="row">' + value.data + '</td>' +
                        '<td scope="row">' + value.hora + '</td>' +
                        '<td scope="row">' + value.sala + '</td>');


                    if (value.status_uc === "Aprovado"){
                        $('#tr'+i+'').append('<td scope="row">Melhoria</td>' +
                            '<td scope="row">10€</td>');
                    } else{
                        $('#tr'+i+'').append('<td scope="row">' + value.descricao_ep + '</td>' +
                            '<td scope="row">5€</td>');
                    }

                    $('#tr'+i+'').append('</tr>');
                    i = i +1;
                });
            }
        }
    });

    var j = 0
    // Load table definidas
    jQuery.ajax({
        url: '/admin/InscribeAv/definidas',
        type: "GET",
        dataType: "json",
        success: function (data) {
            if (data.length ===  0){
                jQuery('tbody[id="definidas"]').empty();
                $('tbody[id="definidas"]').append('<tr id="d'+j+'">' +
                    '<td colspan="8">Nenhuma Avaliação Definida</td>' +
                    '</tr>');
            } else{
                jQuery('tbody[id="definidas"]').empty();
                jQuery.each(data, function (key, value) {

                    $('tbody[id="definidas"]').append('<tr id="d'+j+'">');
                    $('#d'+j+'').append('<td scope="row">' + value.ano + '</td>' +
                        '<td scope="row">' + value.nome_uc + '</td>' +
                        '<td scope="row">' + value.data + '</td>' +
                        '<td scope="row">' + value.hora + '</td>' +
                        '<td scope="row">' + value.sala + '</td>' +
                        '<td scope="row">' + value.descricao_ep + '</td>' +
                        '</tr>' );
                    j = j + 1;
                });
            }

        }
    });

    $("#success").fadeTo(2000, 800).slideUp(800, function (){
        $("#success").slideUp(800);
    });

});

// Adicionar avaliações ao carrinho
function next(){
    jQuery('tbody[id="cart"]').empty();
    jQuery('td[id="total_preco"]').empty();

    var arr = [];
    $('input[type="checkbox"]:checked').each(function () {
        arr.push($(this).attr('name'));
    });

    document.getElementById("list").style.display = "block";
    total = 0;

    for (i = 0; i < arr.length; i++ ){
        info = arr[i].split(".")
        $('tbody[id="cart"]').append('<tr id="c'+i+'">');
        $('#c'+i+'').append('<td class="info">'+ info[0] +'</td>');
        $('#c'+i+'').append('<td class="preco">'+ info[1] +'€</td>');
        $('#c'+i+'').append('</tr>');
        total = total + parseInt(info[1])
    }

    $('td[id="total_preco"]').replaceWith('<td class="preco">'+ total +'€</td>');
    console.log(arr);
}
