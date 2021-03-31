@extends('layouts.base')

@section('title', 'Viewer Classifications')

@section('file_css')
    <link href="{{ mix('css/viewCl.css') }}" rel="stylesheet">
@stop

@section('content')
    <div class="col-md-9 conteudo" >
        <h2>Visualizar Classificações</h2><br>

        <form class="form-inline" action="/action_page.php">
            @csrf
            <div class="column_card">
                <div class="card">
                    <div class="card-body" id="filtro">

                        <div class="row" id="row_card">

                            <div class="col-3">
                                <div class="form-group">
                                    <select class="form-control" id="ano_letivo" name="anoLetivo">
                                        <option selected>Ano-Letivo</option>
                                        @foreach($anos_letivos as $ano_letivo)
                                            <option value="{{$ano_letivo->id_anoLetivo}}">{{$ano_letivo->descricao_al}}</option>
                                        @endforeach
                                    </select>
                                </div>
                            </div>

                            <div class="col-2">
                                <div class="form-group">
                                    <select class="form-control" id="periodo" name="periodo">
                                        <option>Período</option>
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col-3">
                                <div class="form-group">
                                    <select class="form-control" id="ano_curricular" name="anoCurricular">
                                        <option>Ano Curricular</option>
                                        <option value="1">1</option>
                                        <option value="2">2</option>
                                        <option value="3">3</option>
                                    </select>
                                </div>
                            </div>

                            <div class="col-2">
                                <div class="form-group">
                                    <select class="form-control" id="epoca" name="epoca">
                                        <option selected>Época</option>
                                        @foreach($epocas as $epoca)
                                            <option value="{{$epoca->id_epoca}}">{{$epoca->descricao_ep}}</option>
                                        @endforeach
                                    </select>
                                </div>
                            </div>

                            <div class="col-2">
                                <div class="form-group">
                                    <input type="button" id="btn_procurar" class="form-control btn-secondary" value="Pesquisar">
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <table class="table table-striped table-responsive">
                <thead class="thead">
                <tr>
                    <th id="ano_curricular_table">Ano</th>
                    <th id="periodo_table">Período</th>
                    <th id="uc_table">Unidade Curricular</th>
                    <th id="ects_table">ECTS</th>
                    <th id="ano_letivo_table">Ano-Letivo</th>
                    <th id="data_table">Data de Avaliação</th>
                    <th id="epoca_table">Época</th>
                    <th id="status_table">Status da Disciplina</th>
                    <th id="nota_table">Nota</th>
                </tr>
                </thead>
                <tbody id="viewClassTable">
                    <tr>
                        <td colspan="9" id="loading"><div class="spinner-border spinner-border-sm"></div></td>
                    </tr>
                </tbody>
            </table>

        </form>
    </div>
@stop

@section('js')
    <script type="text/javascript" src="{{ asset('js/viewCl.js') }}"></script>
@stop
