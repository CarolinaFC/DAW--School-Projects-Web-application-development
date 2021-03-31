<?php

namespace App\Http\Controllers;

use App\Models\Anos_letivos;
use App\Models\Classificacoes;
use App\Models\Matricula_uc;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\DB;

class RegisterController extends Controller
{
    // Método responsável por ir buscar a informação dos cursos e épocas à base de dados e retornar à view nos selects respectivos registerCl
    public function registerClass()
    {
        //Verificar o tipo de Utilizador
        $user = new AuthController();
        $tipo = $user->checkUser();

        if ($tipo === "Docente"){
            $name = Auth::user()->name;

            /* ------------------------------- Query CURSOS ------------------------------------------- */
            $cursos = DB::table('Docentes')
                ->join('ucs_docentes', 'id_docentes', '=', 'Docentes.id_docente')
                ->join('ucs_anosletivos', 'id_uc_AnoLetivo', '=', 'ucs_docentes.id_uc_AnoL')
                ->join('unidades_curriculares', 'id_uc', '=', 'ucs_anosletivos.id_unidadec')
                ->join('cursos', 'id_curso', '=', 'unidades_curriculares.id_cur')
                ->join('Tipo_curso', 'id_tipoCurso', '=', 'Cursos.id_tCurso')

                ->select('Docentes.nome_completo', 'unidades_curriculares.nome_uc', 'cursos.nome_cursos', 'Tipo_curso.descricao_tipo', 'cursos.id_curso')
                ->get();

            /* ------------------------------- Query EPOCAS ------------------------------------------- */
            $epocas = DB::table('epocas')
                ->select('epocas.id_epoca', 'epocas.descricao_ep')
                ->get();

            return view('admin.classificacao.registerCl', compact('name', 'tipo', 'cursos', 'epocas'));
        }
        return redirect()->back()->with('error', 'Não tem permissão para entrar nesta página.');
    }

    // Método responsável por filtrar as unidades curriculares de acordo com o docente Autenticado e o curso que ele leciona
    public function ucsFilter($id) {
        /* ----------------------------------------------- Query Uc -------------------------------------------------------- */
        $ucs = DB::table('docentes')
            ->join('ucs_docentes', 'id_docentes', '=', 'docentes.id_docente')
            ->join('ucs_anosletivos', 'id_uc_AnoLetivo', '=', 'ucs_docentes.id_uc_AnoL')
            ->join('unidades_curriculares', 'id_uc', '=', 'ucs_anosletivos.id_unidadec')
            ->join('cursos', 'id_curso', '=', 'unidades_curriculares.id_cur')

            ->where('unidades_curriculares.id_cur', '=', $id)
            ->where('docentes.id_ut_d', '=', Auth::id())
            ->pluck('unidades_curriculares.nome_uc', 'unidades_curriculares.id_uc');    // Só preciso de ir buscar o nome das unidades curriculares para pôr no select e não toda a informaçao

        return json_decode($ucs, true);
    }

    // Método responsável por apresentar a tabela para inserir as classificações de acordo com o curso, uc e época
    public function showTableRegisterClass($curso, $uc, $epoca) {
        $tableRegister = DB::table('classificacoes')
            ->join('avaliacoes', 'id_avaliacao', '=', 'classificacoes.id_avaliacao_cl')
            ->join('epocas', 'id_epoca', '=', 'avaliacoes.id_epoca_av')
            ->join('alunos', 'id_aluno', '=', 'classificacoes.id_alunos_cl')
            ->join('ucs_anosletivos', 'id_uc_AnoLetivo', '=', 'avaliacoes.id_uc_anoLeti')
            ->join('anos_letivos', 'id_anoLetivo', '=', 'ucs_anosletivos.id_anoL')
            ->join('unidades_curriculares','id_uc', '=', 'ucs_anosletivos.id_unidadec')
            ->join('cursos', 'id_curso', '=', 'unidades_curriculares.id_cur')

            // Associa o id_curso à variavel $curso que é obtida através da rota que por consequência esta recebe por ajax na vista registerCl do select selecionado = cursoSelect
            ->where('cursos.id_curso', '=', $curso)

            // Associa o id_uc à variavel $uc que é obtida através da rota que por consequência esta recebe por ajax na vista registerCl do select selecionado = ucSelect
            ->where('unidades_curriculares.id_uc', '=', $uc)

            // Associa o id_epoca à variavel $epoca que é obtida através da rota que por consequência esta recebe por ajax na vista registerCl do select selecionado = epocaSelect
            ->where('epocas.id_epoca', '=', $epoca)

            // Vai buscar o ano-letivo recente (último ano-letivo)
            ->where('ucs_anosletivos.id_anoL', '=', Anos_letivos::all()->last()->id_anoLetivo)

            ->select('alunos.numero', 'alunos.nome_completo', 'avaliacoes.data')
            ->get();

        // dd($tableRegister);
        return json_decode($tableRegister, true);

    }

    // Método responsável por ir buscar a informação inserida na tabela e enviá-la para a Base de Dados
    public function sendTableRegisterClass(Request $request)
    {
        // Vai buscar o tamanho de todos os items a serem enviados desde selects a inputs e o token
        //$request->keys()

        $n = count($request->keys())-1; // vai buscar o indice do status_uc
        $n_uc = $request->keys()[$n];
        $index = explode("_", $n_uc); // isolar a selecção do status_uc

        // Percorre o indice do select do status_uc (select_Stateuc[x]) sendo x a quantidade de status_uc existentes
        // Restringe os dados por cada aluno
        for($i = 0; $i <= $index[2]; $i++) {

            $id_aluno = DB::table('alunos')->where('numero', '=', $request['numero_aluno_' . $i])->get();
            $id_av = DB::table('avaliacoes')->where('data', '=', $request->data_avaliacao)->get();

            $updateClass = [
                'nota' => $request['nota_' . $i],
                'status_epoca' => $request['select_Statepoca_' . $i]
            ];

            // Atualiza os campos da base de dados nota e status_epoca na tabela classificações de acordo com novas entradas e de acordo o id_aluno
            $nota_StatusEpoca = Classificacoes::where(['id_avaliacao_cl' => $id_av[0]->id_avaliacao, 'id_alunos_cl' => $id_aluno[0]->id_aluno])
                ->update($updateClass);

            $update_StatusUc = [
                'status_uc' => $request['select_Stateuc_' . $i]
            ];

            // Atualiza o campo status_uc na base de dados na tabela Matricula_uc de acordo com id_aluno
            $statusUC = Matricula_uc::where(['id_uc_AnoLet' => $id_av[0]->id_uc_anoLeti, 'id_alunos' => $id_aluno[0]->id_aluno])
                ->update($update_StatusUc);


            return redirect()->back()->with('success', 'Classificações Registadas com sucesso.');
        }
    }
}
