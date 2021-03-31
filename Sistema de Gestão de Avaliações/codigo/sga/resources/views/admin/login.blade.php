<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
<head>
    <title>SGA - Login</title>
    <meta charset="utf-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>

    <style>

        .cont{
            width: 100vw;
            height: 100vh;

            background-image: url("https://cdn.wallpaperhub.app/cloudcache/3/c/4/6/d/0/3c46d0efb064843e9b907aa0479a816d746c9fb0.jpg");
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;

            display: flex;
            flex-direction: row;
            justify-content: center;
            align-items: center;
        }

        .box{
            width: 450px;
            height: 490px;
            background: white;
            box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);
        }

        body{
            margin: 0px;
        }

        #login-form{
            margin: 30px;
        }

        #auth{
            margin-top: -15px;
        }

        .jumbotron{
            padding: 20px;
        }

        p{
            font-size: 20px;
        }

        .btn{
            width: 100px;
        }

        input[type=text]{
            background: transparent;
            border: none;
            border-bottom: 1px solid gray;
        }

        input[type=email]{
            background: transparent;
            border: none;
            border-bottom: 1px solid gray;
        }

        input[type=password]{
            background: transparent;
            border: none;
            border-bottom: 1px solid gray;
        }


        img{
            /*background-image: url("https://www.vozdaplanicie.pt/images/280420150027-896-IPBejabanner.jpg");
            -webkit-background-size: cover;
            -moz-background-size: cover;
            -o-background-size: cover;
            background-size: cover;*/
            width: 450px;
            -webkit-filter: brightness(55%);
        }

        #title{
            padding: 50px;
            color: white;
            margin-top: -150px;
            position: absolute;
            width: 450px;
            text-align: center;
        }

    </style>

</head>
<body>

<div class="cont">
    <div class="box">
        <img src="https://www.vozdaplanicie.pt/images/280420150027-896-IPBejabanner.jpg">
        <h4 id="title">Sistema de Gestão de Atividades Letivas</h4>
        <form id="login-form" class="form" action="{{ route('admin.login.do') }}" method="post">
            @csrf
            <p class="text-center text-black" id="auth"><b>Autenticação</b></p>
            <div class="form-group">
                <label for="username" class="text-black"></label><br>
                <input type="email" name="email" id="username" class="form-control text-center" placeholder="Email de Utilizador">
            </div>
            <div class="form-group">
                <label for="password" class="text-black"></label><br>
                <input type="password" name="password" id="password" class="form-control text-center" placeholder="Palavra-chave"><br>
            </div>
            @if($errors->all())
                @foreach($errors->all() as $error)
                    <!-- Modal -->
                    <script type="text/javascript">
                        $(document).ready(function(){
                            $('#myModal').modal({show: true});
                        });
                    </script>

                        <div class="modal fade" tabindex="-1" role="dialog" id="myModal">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Erro de Autenticação</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                        {{$error}}
                                    </div>
                                    <div class="modal-footer">
                                        <button type="button" class="btn btn-secondary" data-dismiss="modal">Fechar</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                @endforeach
            @endif
            <div class="form-group text-center">
                <input type="submit" name="submit" class="btn btn-secondary" value="Entrar">
            </div>
        </form>
    </div>
</div>




</body>
</html>
