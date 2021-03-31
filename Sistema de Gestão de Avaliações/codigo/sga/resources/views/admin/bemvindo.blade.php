@extends('layouts.base')

@section('title', 'HomePage')

@section('content')

        <div class="col-md-6 conteudo" >
            <h2>Bem Vindo</h2><br>
            <p>Este sítio web consiste numa Gestão de Atividades Letivas no contexto do IPBeja. <br> Esta plataforma só terá disponível/funcional a área da Gestão de Avaliações uma vez que foi a proposta deste projeto.</p>
            <p>Este texto é uma demonstração de uma descrição sobre esta plataforma e a sua utilidade. Todos os dados aqui utilizados foram retirados das plataformas do Instituto <br> Politécnico de Beja e das suas Unidades Orgânicas.</p>
        </div>

        <div class="col-md-3 links">
            <div class="card">
                <div class="card-body">
                    <h5 class="card-title">Mais informações do IPBeja</h5>
                    <ul>
                        <li><a href="http://www.ipbeja.pt">Sítio do IPBeja</a></li>
                        <li><a href="https://portal.ipbeja.pt/netpa/page">Sítio do portal do IPBeja (netPa)</a></li>
                        <li><a href="https://www.ipbeja.pt/UnidadesOrganicas/Paginas/default.aspx">Unidade Orgânicas</a></li>
                        <ul>
                            <li><a href="https://www.ipbeja.pt/UnidadesOrganicas/ESA/Paginas/default.aspx">ESA</a></li>
                            <li><a href="https://www.ipbeja.pt/UnidadesOrganicas/ESE/Paginas/default.aspx">ESE</a></li>
                            <li><a href="https://www.ipbeja.pt/UnidadesOrganicas/ESS/Paginas/default.aspx">ESS</a></li>
                            <li><a href="https://www.ipbeja.pt/UnidadesOrganicas/ESTIG/Paginas/default.aspx">ESTIG</a></li>
                        </ul>
                        <li><a href="https://cms.ipbeja.pt/">Sistema de Gestão de Cursos</a></li>
                    </ul>
                </div>
            </div>
        </div>
@stop
