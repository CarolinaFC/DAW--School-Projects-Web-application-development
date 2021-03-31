<?php

namespace App\Http\Controllers;

use App\Models\Anos_letivos;
use App\Models\Avaliacoes;
use App\Models\Docentes;
use App\Models\Epocas;
use App\Models\Ucs_anosletivos;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class CreateController extends Controller
{

    public function createAvaliacao()
    {
        // Verificar qual o tipo de utilizador
        $user = new AuthController();
        $tipo = $user->checkUser();

        if ($tipo === "Docente"){

            // Buscar o nome do utilizador
            $name = Auth::user()->name;

            // Buscar á base de dados todas as informações necessárias para a view
            $cursos = Docentes::join('ucs_docentes', 'id_docente', '=', 'ucs_docentes.id_docentes')
                ->join('ucs_anosletivos', 'id_uc_AnoL', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
                ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
                ->join('Cursos', 'unidades_curriculares.id_cur', '=', 'Cursos.id_curso')
                ->join('Tipo_curso', 'Cursos.id_tCurso', '=', 'Tipo_curso.id_tipoCurso')
                ->select('Docentes.nome_completo', 'unidades_curriculares.nome_uc', 'Cursos.nome_cursos', 'Tipo_curso.descricao_tipo', 'Cursos.id_curso')
                ->get();


            $epocas = Epocas::all();
            $anosLetivos = Anos_letivos::all();

            return view('admin.avaliacao.createAv', compact('name', 'tipo', 'cursos', 'epocas', 'anosLetivos'));
        }

        return redirect()->back()->with('error', 'Não tem permissão para entrar nesta página.');
    }


    public function store(Request $request)
    {
        // Busca a base de dados do id da uc por ano letivo atraves dos valores dos selects
        $ucs_al = Ucs_anosletivos::join('unidades_curriculares', 'id_unidadec', '=', 'unidades_curriculares.id_uc')
            ->join('avaliacoes', 'id_uc_AnoLetivo', '=', 'avaliacoes.id_uc_anoLeti')
            ->where('id_unidadec', '=', $request->uc)
            ->where('id_AnoL', '=', Anos_letivos::all()->last()->id_anoLetivo)
            ->get('id_uc_AnoLetivo');

        $uc = $ucs_al[0]->id_uc_AnoLetivo;

        // Adicionar á base de dados a nova avaliação
        $newAv = new Avaliacoes();
        $newAv->data = $request->data;
        $newAv->hora = $request->hora;
        $newAv->sala = $request->sala;
        $newAv->id_epoca_av = $request->epoca;
        $newAv->id_uc_anoLeti = $uc;
        $newAv->save();

        return redirect()->back()->with('success', 'Avaliação Adicionada com Sucesso! NOTA: Coloque o curso e verifique se foi adicionada.');
    }


    public function filter($id)
    {

        // Buscar á base de dados todas as unidades curriculares pertencentes a um determinado curso e professor
        $uc = Docentes::join('ucs_docentes', 'id_docente', '=', 'ucs_docentes.id_docentes')
            ->join('ucs_anosletivos', 'ucs_docentes.id_uc_AnoL', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
            ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
            ->where('ucs_anosletivos.id_anoL', '=', Anos_letivos::all()->last()->id_anoLetivo)
            ->where('Docentes.id_ut_d', '=', Auth::id())
            ->where('Unidades_curriculares.id_cur', '=', $id)
            ->pluck('unidades_curriculares.nome_uc',  'ucs_anosletivos.id_unidadec' );

        return json_decode ($uc, true);
    }


    public function table($id)
    {

        // Buscar á base de dados todas as avaliações presentes num determinado curso no anio letive presente
        $av = Avaliacoes::join('ucs_anosletivos', 'id_uc_anoLeti', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
            ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'anos_letivos.id_anoLetivo')
            ->join('epocas', 'id_epoca_av', '=', 'epocas.id_epoca')
            ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
            ->where('ucs_anosletivos.id_AnoL', '=', Anos_letivos::all()->last()->id_anoLetivo)
            ->where('unidades_curriculares.id_cur', '=', $id)
            ->select( 'anos_letivos.descricao_al','Unidades_curriculares.ano', 'Unidades_curriculares.semestre', 'Unidades_curriculares.nome_uc', 'avaliacoes.data', 'avaliacoes.hora', 'avaliacoes.sala', 'epocas.descricao_ep' )
            ->get();


        return json_decode ($av, true);
    }


    public function tablefilter($anoL, $uc)
    {
        // Filtrar as avaliações de acordo com o ano letivo e com a unidade curricular
        $av = Avaliacoes::join('ucs_anosletivos', 'id_uc_anoLeti', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
            ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'anos_letivos.id_anoLetivo')
            ->join('epocas', 'id_epoca_av', '=', 'epocas.id_epoca')
            ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
            ->where('ucs_anosletivos.id_AnoL', '=', $anoL)
            ->where('unidades_curriculares.id_uc', '=', $uc)
            ->select( 'anos_letivos.descricao_al','Unidades_curriculares.ano', 'Unidades_curriculares.semestre', 'Unidades_curriculares.nome_uc', 'avaliacoes.data', 'avaliacoes.hora', 'avaliacoes.sala', 'epocas.descricao_ep' )
            ->get();

        return json_decode ($av, true);
    }

}
