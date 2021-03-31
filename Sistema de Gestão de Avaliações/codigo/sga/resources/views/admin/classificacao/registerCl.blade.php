@extends('layouts.base')

@section('title', 'Register Classifications')

@section('file_css')
    <link href="{{ mix('css/registerCl.css') }}" rel="stylesheet">
@stop

@section('content')
    <div class="col-md-9 conteudo" >

        <div class="row">
            <div class="col-6">
                <h2>Registar Classificações</h2>
            </div>
            <div class="col">
                <a id="question" data-toggle="tooltip" data-html="true" data-placement="right" title="Insira os dados sobre a avaliação e preencha os campos de cada aluno, por fim clique no botão <b>Registar</b>">
                    <i class="fa fa-question-circle fa-lg text-black-50" aria-hidden="true"></i>
                </a>
            </div>
        </div>


        <br>

        <form method="POST" class="form-inline" action="{{route('admin.registerCl.tableRegisterSend')}}">
            @csrf
            <div class="column_card">
                <div class="card">
                    <div class="card-body" id="filtro">

                        <div class="row" id="row_card">
                            <div class="col-3" id="curso_col">
                                <div class="form-group">
                                    <select class="form-control" id="curso" name="curso">
                                        <option selected>Curso</option>
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
                                                    <option value = '{{$curso->id_curso}}'>{{$curso->nome_cursos}}</option>
                                                @endif

                                                @if($tipo_curso !== $curso->descricao_tipo)
                                                    @php $tipo_curso = $curso->descricao_tipo; @endphp
                                                    <option disabled>{{$curso->descricao_tipo}}</option>

                                                    @if($nome !== $curso->nome_cursos)
                                                        @php $nome = $curso->nome_cursos; @endphp
                                                        <option value = '{{$curso->id_curso}}'>{{$curso->nome_cursos}}</option>
                                                    @endif

                                                @endif

                                            @endif
                                        @endforeach
                                    </select>
                                </div>
                            </div>

                            <div class="col-3" id="uc_col">
                                <div class="form-group">
                                    <select class="form-control" id="uc" name="uc">
                                        <option selected>Unidade Curricular</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col-3" id="epoca_col">
                                <div class="form-group">
                                    <select class="form-control" id="epoca" name="epoca">
                                        <option selected>Época</option>
                                        @foreach($epocas as $epoca)
                                            <option value="{{$epoca->id_epoca}}">{{$epoca->descricao_ep}}</option>
                                        @endforeach
                                    </select>
                                </div>
                            </div>

                            <div class="col-3" id="btn_add_col">
                                <div class="form-group">
                                    <input type="button" id="btn_search_add"  class="form-control btn-secondary" value="Visualizar Tabela">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <table class="table table-striped">
                <thead class="thead">
                <tr>
                    <th scope="col">Número do Aluno</th>
                    <th scope="col">Nome do Aluno</th>
                    <th scope="col">Data da Avaliação</th>
                    <th scope="col">Nota</th>
                    <th scope="col" id="status_epoca">Status da Época</th>
                    <th scope="col">Status da Disciplina</th>
                </tr>
                </thead>
                <tbody id="registarClass">
                <tr>
                    <td id="dadosNaoSelecionados" colspan="6">Selecione os dados acima correspondentes à avaliação</td>
                </tr>
                </tbody>
            </table>

            @if (\Session::has('success'))
                <div class="alert alert-success" id="success">
                    {!! \Session::get('success') !!}
                </div>
            @endif

            <button type="submit" class="btn" id="registar">Registar</button>
        </form>
    </div>
@stop

@section('js')
    <script type="text/javascript" src="{{ asset('js/registerCl.js') }}"></script>
@stop
