<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\DB;

class ViewerController extends Controller
{
    // Método responsável por ir buscar a informação à Base de dados e retornar à view nos selects respectivos viewCl
    public function viewClass()
    {
        $user = new AuthController();
        $tipo = $user->checkUser();

        if ($tipo === "Aluno"){
            $name = Auth::user()->name;

            /* ------------------------------- Query ANO-LETIVO ------------------------------------------- */
            $anos_letivos = DB::table('anos_letivos')
                ->select('anos_letivos.id_anoLetivo', 'anos_letivos.descricao_al')
                ->get();

            /* ------------------------------- Query EPOCA ------------------------------------------- */
            $epocas = DB::table('epocas')
                ->select('epocas.id_epoca', 'epocas.descricao_ep')
                ->get();

            return view('admin.classificacao.viewCl', compact('name', 'tipo', 'anos_letivos', 'epocas'));
        }
        return redirect()->back()->with('error', 'Não tem permissão para entrar nesta página.');

    }

    // Método responsável por ir buscar a informação à Base de dados e retornar à view todas as classificações existentes daquele determinado aluno
    public function showTableViewClass()
    {
        $cl = collect(DB::table('classificacoes')
            ->join('alunos', 'id_alunos_cl', '=', 'id_aluno')
            ->join('avaliacoes', 'id_avaliacao_cl', '=', 'id_avaliacao')
            ->join('ucs_anosletivos','avaliacoes.id_uc_AnoLeti', '=', 'id_uc_AnoLetivo')
            ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'id_uc')
            ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'id_anoLetivo')
            ->join('epocas', 'avaliacoes.id_epoca_av', '=', 'id_epoca')

            ->where('alunos.id_ut_a', '=', Auth::id())
            ->where('status_epoca', '<>', 'Inscrito')
            ->orderBy('id_epoca_av', 'desc')
            ->select('unidades_curriculares.ano', 'unidades_curriculares.semestre', 'unidades_curriculares.nome_uc', 'unidades_curriculares.ects', 'avaliacoes.data', 'epocas.descricao_ep','anos_letivos.descricao_al', 'classificacoes.nota', 'avaliacoes.id_uc_anoLeti')
            ->get());

        $uc = collect(DB::table('ucs_anosletivos')
            ->join('unidades_curriculares', 'id_unidadec', '=', 'id_uc')
            ->join('matricula_uc', 'id_uc_AnoLet', '=', 'id_uc_AnoLetivo')
            ->join('alunos', 'matricula_uc.id_alunos', '=', 'id_aluno')
            ->where('alunos.id_ut_a', '=', Auth::id())
            ->orderBy('id_anoL')
            ->orderBy('ano')
            ->get());

        $c = 0;
        $d = 0;
        $array = [];
        $array[0] = 0;
        $arr=[];

        // Vai buscar e colocar num novo ARRAY a ultima classificacao realizada seja ela de epoca normal, recurso ou melhoria
        foreach ($uc as $key1 => $value1) {
            foreach ($cl as $key => $value) {
                if($value1->id_uc_AnoLetivo == $value->id_uc_anoLeti) {
                    if ($array[$d] != $value1->id_uc_AnoLetivo) {
                        $array[$c] = $value1->id_uc_AnoLetivo;
                        array_push($arr, $value);
                        $c++;
                        $d = $c - 1;
                    }
                }
            }
        }
        return $arr;
    }

    // Método responsável por ir buscar a informação à Base de dados e retornar à view todas as classificações de acordo com os FILTROS de PESQUISA selecionados
    public function showTableViewClassSearch($ano_letivo, $periodo, $ano_curr, $epoca)
    {

        $cl = collect(DB::table('classificacoes')
            ->join('alunos', 'id_alunos_cl', '=', 'id_aluno')
            ->join('avaliacoes', 'id_avaliacao_cl', '=', 'id_avaliacao')
            ->join('ucs_anosletivos','avaliacoes.id_uc_AnoLeti', '=', 'id_uc_AnoLetivo')
            ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'id_uc')
            ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'id_anoLetivo')
            ->join('epocas', 'avaliacoes.id_epoca_av', '=', 'id_epoca')

            // Associa as variaveis que são obtidas através da rota que por consequência esta recebe por ajax na vista viewCl do select selecionado
            ->where('anos_letivos.id_anoLetivo', '=', $ano_letivo)
            ->where('unidades_curriculares.semestre', '=', $periodo)
            ->where('unidades_curriculares.ano', '=', $ano_curr)
            ->where('epocas.id_epoca', '=', $epoca)

            ->where('alunos.id_ut_a', '=', Auth::id())
            ->where('status_epoca', '<>', 'Inscrito')
            ->orderBy('id_epoca_av', 'desc')
            ->select('unidades_curriculares.ano', 'unidades_curriculares.semestre', 'unidades_curriculares.nome_uc', 'unidades_curriculares.ects', 'avaliacoes.data', 'epocas.descricao_ep','anos_letivos.descricao_al', 'classificacoes.nota', 'avaliacoes.id_uc_anoLeti')
            ->get());

        $uc = collect(DB::table('ucs_anosletivos')
            ->join('unidades_curriculares', 'id_unidadec', '=', 'id_uc')
            ->join('matricula_uc', 'id_uc_AnoLet', '=', 'id_uc_AnoLetivo')
            ->join('alunos', 'matricula_uc.id_alunos', '=', 'id_aluno')
            ->where('alunos.id_ut_a', '=', Auth::id())
            ->orderBy('id_anoL')
            ->orderBy('ano')
            ->get());

        $c = 0;
        $d = 0;
        $array = [];
        $array[0] = 0;
        $arr=[];

        // Vai buscar e colocar num novo ARRAY a ultima classificacao realizada seja ela de epoca normal, recurso ou melhoria
        foreach ($uc as $key1 => $value1) {
            foreach ($cl as $key => $value) {
                if($value1->id_uc_AnoLetivo == $value->id_uc_anoLeti) {
                    if ($array[$d] != $value1->id_uc_AnoLetivo) {
                        $array[$c] = $value1->id_uc_AnoLetivo;
                        array_push($arr, $value);
                        $c++;
                        $d = $c - 1;
                    }
                }
            }
        }
        return $arr;

    }
}
