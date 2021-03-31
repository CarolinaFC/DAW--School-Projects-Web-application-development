@extends('layouts.base')

@section('title', 'Create Av')

@section('file_css')
    <link href="{{ mix('css/createAv_style.css') }}" rel="stylesheet">
@stop

@section('content')
    <div class="col-md-9 conteudo" >

        <div class="row">
            <div class="col-10">
                <h2>Marcar Avaliações</h2>

            </div>
            <div class="col">
                <a id="question" data-toggle="tooltip" data-html="true" data-placement="right" title="Insira os dados necessários abaixo e clique no botão <b>Marcar</b>">
                    <i class="fa fa-question-circle fa-lg text-black-50" aria-hidden="true"></i>
                </a>
            </div>
        </div>
    <br>


    <form method="POST" class="form-inline" action="{{ route('admin.createAv.store') }}">
        @csrf
        <div class="card">
            <div class="card-body" id="filtro">

                <div class="row">

                    <div class="col-3">

                        <div class="form-group">
                            <select class="form-control" id="cursos" name="curso" >
                                <option selected disabled class="opt">Curso</option>
                                @php
                                    $firstname = explode(' ', $name);
                                    $tipo_curso = "";
                                    $nome = "";
                                @endphp

                                @foreach($cursos as $curso)
                                    @php $firstname2 = explode(' ', $curso->nome_completo); @endphp
                                    @if($firstname2[0] === $firstname[0])

                                        @if($tipo_curso === $curso->descricao_tipo and $nome !== $curso->nome_cursos)
                                            @php $nome = $curso->nome_cursos; @endphp
                                            <option id="curso" value = '{{$curso->id_curso}}'>{{$curso->nome_cursos}}</option>
                                        @endif
                                        @if($tipo_curso !== $curso->descricao_tipo)
                                            @php $tipo_curso = $curso->descricao_tipo; @endphp
                                            <option disabled>{{$curso->descricao_tipo}}</option>
                                            @if($nome !== $curso->nome_cursos)
                                                @php $nome = $curso->nome_cursos; @endphp
                                                <option id="curso" value = '{{$curso->id_curso}}'>{{$curso->nome_cursos}}</option>
                                            @endif
                                        @endif
                                    @endif
                                @endforeach

                            </select>
                        </div>
                        <div class="form-group">
                            <select class="form-control" id="uc" name="uc">
                                <option selected disabled>Unidade Curricular</option>
                            </select>

                        </div>

                        <a href="#fil" id="filtable" data-toggle="collapse" >Filtrar Tabela <i class="fa fa-arrow-circle-down" aria-hidden="true"></i></a>
                        <a href="#" id="verTable" onclick="loadtable()">Ver Todas <i class="fa fa-refresh" aria-hidden="true"></i></a>


                    </div>

                    <div class="col-3">
                        <div class="form-group">
                            <select class="form-control" id="epoca" name="epoca">
                                <option selected disabled>Época</option>
                                <!-- Desenvolver com BackEnd -->
                                @foreach($epocas as $epoca)
                                    <option value="{{$epoca->id_epoca}}"> {{$epoca->descricao_ep}} </option>
                                @endforeach
                            </select>
                        </div>

                        <div class="form-group">
                            <input type="text" class="form-control" placeholder="Insira a sala" id="sala" name="sala">
                        </div>

                    </div>


                    <div class="col-3">
                        <div class="form-group" id="ip_hora">
                            <label for="hora"><b>Hora da Avaliação:</b></label> <br>
                            <input type="time" class="form-control" placeholder="Insira uma Hora" id="hora" name="hora">
                        </div>
                    </div>


                    <div class="col-3">
                        <div class="form-group" id="ip_data">
                            <label for="data"><b>Data da Avaliação:</b></label> <br>
                            <input type="date" class="form-control" placeholder="Insira a data" id="data" name="data">
                        </div>
                    </div>

                </div>
            </div>
        </div>


        <div id="fil" class="collapse">
            <div class="card" id="filtroTable">
                <div class="card-body">
                     <div class="row">

                        <div class="col-8">

                            <select class="form-control" id="AnoLetivo" name="anoletivo" >
                                <option selected disabled class="opt">Ano Letivo</option>
                                @foreach($anosLetivos as $ano)
                                    <option class="opt" value="{{$ano->id_anoLetivo}}"> {{$ano->descricao_al}}</option>
                                @endforeach
                            </select>

                        </div>


                        <div class="col-4">
                            <button type="button" class="btn btn-secondary" id="filtrar">Filtrar</button>
                        </div>

                    </div>


                </div>
            </div>
        </div>


        <table class="table table-striped table-responsive" id="aval">
            <thead class="thead">
            <tr>
                <th scope="col" class="th-sm">Ano Letivo</th>
                <th scope="col" class="th-sm">Ano</th>
                <th scope="col" class="th-sm">Período</th>
                <th scope="col" class="th-sm">Unidade Curricular</th>
                <th scope="col" class="th-sm">Data de Avaliação</th>
                <th scope="col" class="th-sm">Hora</th>
                <th scope="col" class="th-sm">Sala</th>
                <th scope="col" class="th-sm">Época</th>
            </tr>
            </thead>
            <tbody id="avaliacao">
                <tr>
                    <td colspan="8" class="td_h">Insira primeiro o curso </td>
                </tr>
            </tbody>
        </table>

        @if (\Session::has('success'))
            <div class="alert alert-success" id="success">
                {!! \Session::get('success') !!}
            </div>
        @endif

        <button type="submit" class="btn" id="marcar">Marcar</button>
    </form>
</div>
@stop

@section('js')
    <script type="text/javascript" src="{{ asset('js/createAv.js') }}"></script>
@stop


