<!DOCTYPE html>
<html lang="{{ str_replace('_', '-', app()->getLocale()) }}">
<head>
    <meta charset="UTF-8">
    <title>SGA - @yield('title')</title>

    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.5.1/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.16.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">

    <!------------------------ STYLE --------------------->
    @yield('file_css')
    <link href="{{ mix('css/style.css') }}" rel="stylesheet">

</head>

<body>
<!------------------------ NAVBAR Horizontal --------------------->
<nav class="navbar navbar-expand-lg navbar-fixed-top">
    <div class="container-fluid">
        <!-- Brand -->
        <a class="navbar-brand" href="{{route('admin')}}" id="hello"><b>IPBeja - Gestão das Atividades Letivas</b></a>
        <ul class="mx-auto"></ul>
        <a href="{{ route('admin.logout') }}"><button class="text-center" id="logout"><b>Sair</b></button></a>
    </div>
</nav>


<!------------------------ NAVBAR Vertical --------------------->
<div class="row" id="navba">
    <div class="col-md-2 col-xs-1" id="sidebar">

        <div class="row" id="user">
            <div class="col-3 text-center"><i class="fa fa-user-circle fa-2x"></i></div>
            <div class="col-9">
                <h5>{{$name}}</h5>
                <p>{{$tipo}}</p>
            </div>
        </div>


        <div class="list-group">
            <a href="#menu1" class="list-group-item">
                <i class="fa fa-list-ul"></i>
                <span class="hidden-sm-down">Planos Curriculares</span>
            </a>
            <a href="#menu2" class="list-group-item" data-parent="#sidebar">
                <i class="fa fa-graduation-cap"></i>
                <span class="hidden-sm-down">Matrículas</span>
            </a>
            <a href="#menu3" class="list-group-item">
                <i class="fa fa-window-maximize"></i>
                <span class="hidden-sm-down">Aulas</span>
            </a>
            <a href="#menu4" class="list-group-item" data-toggle="collapse" data-parent="#sidebar">
                <i class="fa fa-file-text"></i>
                <span class="hidden-sm-down">Avaliações</span>
            </a>
            <div class="collapse" id="menu4">
                <a href="{{ route('admin.inscribeAv') }}" class="list-group-item" data-parent="#menu2">Inscrever nas Avaliações</a>
                <a href="{{ route('admin.viewCl') }}" class="list-group-item" data-parent="#menu2">Visualizar Classificações</a>
            </div>
            <a href="#menu5" class="list-group-item" data-toggle="collapse" data-parent="#sidebar">
                <i class="fa fa-users"></i>
                <span class="hidden-sm-down">Docentes</span>
            </a>
            <div class="collapse" id="menu5">
                <a href="{{ route('admin.createAv') }}" class="list-group-item" data-parent="#menu2">Marcar Avaliações</a>
                <a href="{{ route('admin.registerCl') }}" class="list-group-item" data-parent="#menu2">Registar Classificações</a>
            </div>
            <a href="#menu6" class="list-group-item">
                <i class="fa fa-building"></i>
                <span class="hidden-sm-down">Espaços Letivos</span>
            </a>

            <br>

            <script>
                $(document).ready(function (){
                   $("#erro").fadeTo(2000, 500).slideUp(500, function (){
                       $("#erro").slideUp(500);
                   });

                    $(function () {
                        $('[data-toggle="tooltip"]').tooltip()
                    })

                });
            </script>

            @if(session()->has('error'))
                <div class="alert alert-danger" id="erro">
                    <b>{{ session()->get('error') }}</b>
                </div>
            @endif

        </div>
    </div>

    <div class="text-center" id="footer">
        <h5>2020-2021</h5>
        <a>Contato</a>
    </div>

    <!------------------------ CONTEUDO --------------------->
    @yield('content')

</div>

@yield('js')

</body>
</html>
