@extends('layouts.base')

@section('title', 'Inscribe Av')

@section('file_css')
    <link href="{{ mix('css/inscribeAv_style.css') }}" rel="stylesheet">
@stop

@section('content')
    <div class="col-md-7 conteudo" >

        <div class="row">
            <div class="col">
                <h2>Inscriçao nas Avaliações</h2>
                <a id="question" data-toggle="tooltip" data-html="true" data-placement="right" title="Selecione a avaliação que pretende inscrever e clique no botão <b>Continuar</b> depois basta confirmar os dados e clicar no botão <b>Inscrever</b>">
                    <i class="fa fa-question-circle fa-lg text-black-50" aria-hidden="true"></i>
                </a>
            </div>
            <div class="col"></div>
        </div>

        <br>

            <h4>Época de Recurso: </h4>
        <form method="POST"  class="form-inline"  action="{{ route('admin.InscribeAv.store') }}">
            @csrf
            <table class="table table-striped" id="aval">
                <thead class="thead">
                <tr>
                    <th scope="col">Seleção</th>
                    <th scope="col" class="ano">Ano</th>
                    <th scope="col" class="uc">UC</th>
                    <th scope="col" id="status_epoca">Data</th>
                    <th scope="col">Hora</th>
                    <th scope="col">Sala</th>
                    <th scope="col">Tipo</th>
                    <th scope="col">Preço</th>
                </tr>
                </thead>
                <tbody id="av">
                <!-- Desenvolver com BackEnd -->
                <td colspan="8" id="loading"><div class="spinner-border spinner-border-sm"></div></td>

                </tbody>
            </table>

            <button type="button" class="btn btn-secondary" id="continue" onclick="next()">Continuar</button>
            <br>

            <h4>Definidas: </h4>

            <table class="table table-striped">
                <thead class="thead">
                <tr>
                    <th scope="col">Ano</th>
                    <th scope="col">UC</th>
                    <th scope="col" id="status_epoca">Data</th>
                    <th scope="col">Hora</th>
                    <th scope="col">Sala</th>
                    <th scope="col">Tipo</th>
                </tr>
                </thead>
                <tbody id="definidas">
                <tr>
                    <td colspan="6"><div class="spinner-border spinner-border-sm"></div></td>
                </tr>

                </tbody>
            </table>
        </div>

    @if (\Session::has('success'))
        <div class="alert alert-success" id="success">
            {!! \Session::get('success') !!}
        </div>
    @endif

    <div class="col-md-2 list" id="list">
            <div class="card">
                <div class="card-header">
                    <h5 class="card-title">Total de Avaliações: </h5>
                </div>
                <div class="card-body">
                    <p><b>Época de Recurso:</b> </p>
                    <table class="cart">
                        <tbody id="cart">

                        </tbody>

                        <tfoot id="total">
                        <tr>
                            <td class="info">Total a Pagar: </td>
                            <td id="total_preco">0€</td>
                        </tr>
                        </tfoot>
                    </table>
                </div>
                <div class="card-footer">
                    <button type="submit" class="btn" id="inscrever">Inscrever</button>
                </div>
                </form>
            </div>
        </div>
@stop

@section('js')
    <script type="text/javascript" src="{{ asset('js/inscribeAv.js') }}"></script>
@stop

