<?php

namespace App\Http\Controllers;

use App\Models\Alunos;
use App\Models\Anos_letivos;
use App\Models\Avaliacoes;
use App\Models\Classificacoes;
use App\Models\Ucs_anosletivos;
use Illuminate\Http\Request;
use Illuminate\Support\Arr;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\DB;
use PhpParser\Node\Stmt\Continue_;
use function Psy\debug;

class InscribeController extends Controller
{

    public function inscribeAvaliacao()
    {
        $user = new AuthController();
        $tipo = $user->checkUser();

        if ($tipo === "Aluno"){
            $name = Auth::user()->name;
            return view('admin.avaliacao.inscribeAv', compact('name', 'tipo'));
        }
        return redirect()->back()->with('error', 'Não tem permissão para entrar nesta página.');
    }

    public function loadAvaliacoes()
    {
        // Buscar o id do aluno
        $Aluno = Alunos::All()->where('id_ut_a', '=', Auth::id());
        $id_Aluno = intval( $Aluno[$Aluno->keys()[0]]->id_aluno);



        // Busca á base de dados do numero de avaliações que o aluno tem
        $class = collect(DB::table('Avaliacoes')
            ->join('ucs_anosletivos', 'id_uc_anoLeti', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
            ->join('matricula_uc', 'ucs_anosletivos.id_uc_AnoLetivo', '=', "matricula_uc.id_uc_AnoLet")
            ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'anos_letivos.id_anoLetivo')
            ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
            ->join('classificacoes', 'id_avaliacao', '=', 'classificacoes.id_avaliacao_cl')
            ->join('epocas', 'id_epoca_av', '=', 'epocas.id_epoca')
            ->where('ucs_anosletivos.id_AnoL', '=', Anos_letivos::all()->last()->id_anoLetivo)
            ->where('matricula_uc.id_alunos', '=', $id_Aluno)
            ->where('id_epoca_av', '>=', 2)
            ->select( 'anos_letivos.descricao_al','Unidades_curriculares.ano', 'Unidades_curriculares.semestre', 'Unidades_curriculares.nome_uc', 'avaliacoes.data', 'avaliacoes.hora', 'avaliacoes.sala', 'epocas.descricao_ep', 'avaliacoes.id_avaliacao', 'matricula_uc.status_uc')
            ->get());

        $diff = [];

        if (count($class) == 0)  {
            // vai colocar todas as avaliacoes disponiveis
            $av = DB::table('Avaliacoes')
                ->join('ucs_anosletivos', 'id_uc_anoLeti', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
                ->join('matricula_uc', 'ucs_anosletivos.id_uc_AnoLetivo', '=', "matricula_uc.id_uc_AnoLet")
                ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'anos_letivos.id_anoLetivo')
                ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
                ->join('epocas', 'id_epoca_av', '=', 'epocas.id_epoca')
                ->where('ucs_anosletivos.id_AnoL', '=', Anos_letivos::all()->last()->id_anoLetivo)
                ->where('matricula_uc.id_alunos', '=', $id_Aluno)
                ->where('id_epoca_av', '>=', 2)
                ->select( 'anos_letivos.descricao_al','Unidades_curriculares.ano', 'Unidades_curriculares.semestre', 'Unidades_curriculares.nome_uc', 'avaliacoes.data', 'avaliacoes.hora', 'avaliacoes.sala', 'epocas.descricao_ep', 'avaliacoes.id_avaliacao', 'matricula_uc.status_uc')
                ->get();


            return json_decode ($av, true);

        } else{

            $av = collect(DB::table('Avaliacoes')
                ->join('ucs_anosletivos', 'id_uc_anoLeti', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
                ->join('matricula_uc', 'ucs_anosletivos.id_uc_AnoLetivo', '=', "matricula_uc.id_uc_AnoLet")
                ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'anos_letivos.id_anoLetivo')
                ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
                ->join('epocas', 'id_epoca_av', '=', 'epocas.id_epoca')
                ->where('ucs_anosletivos.id_AnoL', '=', Anos_letivos::all()->last()->id_anoLetivo)
                ->where('matricula_uc.id_alunos', '=', $id_Aluno)
                ->where('id_epoca_av', '>=', 2)
                ->select( 'anos_letivos.descricao_al','Unidades_curriculares.ano', 'Unidades_curriculares.semestre', 'Unidades_curriculares.nome_uc', 'avaliacoes.data', 'avaliacoes.hora', 'avaliacoes.sala', 'epocas.descricao_ep', 'avaliacoes.id_avaliacao', 'matricula_uc.status_uc')
                ->get());



            foreach ($av as $key => $val){
                foreach ($class as $key1 => $val1){
                    if ($val->id_avaliacao != $val1->id_avaliacao){
                        array_push($diff, $val);
                    }
                    else{
                        $diff = [];
                    }
                }
            }

            return $diff;
        }

    }

    public function loadDefinidas()
    {
        $av = Avaliacoes::join('ucs_anosletivos', 'id_uc_anoLeti', '=', 'ucs_anosletivos.id_uc_AnoLetivo')
            ->join('anos_letivos', 'ucs_anosletivos.id_anoL', '=', 'anos_letivos.id_anoLetivo')
            ->join('epocas', 'id_epoca_av', '=', 'epocas.id_epoca')
            ->join('unidades_curriculares', 'ucs_anosletivos.id_unidadec', '=', 'unidades_curriculares.id_uc')
            ->join('classificacoes', 'id_avaliacao', '=', 'classificacoes.id_avaliacao_cl')
            ->join('alunos', 'classificacoes.id_alunos_cl', '=', 'alunos.id_aluno')
            ->where('alunos.id_ut_a', '=', Auth::id())
            ->where('ucs_anosletivos.id_AnoL', '=', Anos_letivos::all()->last()->id_anoLetivo)
            ->select('Unidades_curriculares.ano', 'Unidades_curriculares.nome_uc', 'avaliacoes.data', 'avaliacoes.hora', 'avaliacoes.sala', 'epocas.descricao_ep' )
            ->get();

        return json_decode ($av, true);
    }

    public function store(Request $request)
    {

        // Buscar o id da avaliação
        $key = $request->keys();
        $name = [];
        $c = 0;

        // Buscar o id do aluno
        $Aluno = Alunos::All()->where('id_ut_a', '=', Auth::id());
        $id_Aluno = intval( $Aluno[$Aluno->keys()[0]]->id_aluno);


        for ($i = 1; $i <= count($key) - 1; $i++) {
            $name[$c] = explode('_', $key[$i]);
            $id_av[$c] = $name[$c][ count($name[$c]) - 1];

            // Inserir a Inscrição/classificação
            $cl = new Classificacoes();
            $cl->id_avaliacao_cl = $id_av[$c];
            $cl->id_alunos_cl = $id_Aluno;
            $cl->nota = null;
            $cl->status_epoca = "Inscrito";
            $cl->save();

            $c += 1;

        }


        return redirect()->back()->with('success', 'Inscrição Realizada com Sucesso!');
    }


}
