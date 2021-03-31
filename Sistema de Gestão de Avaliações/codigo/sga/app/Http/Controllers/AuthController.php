<?php

namespace App\Http\Controllers;

use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;

class AuthController extends Controller
{

    public function checkUser(){

        /**
         * Função para verificar o tipo de utilizador através de email
         * Se a primeira parte do email for numérica então é aluno, caso contrário é docente.
         * Por exemplo: 17191 é aluno e lbruno é docente
        **/

        $user = Auth::user();
        $name = explode('@', $user->email);

        if(is_numeric ( $name[0] ) === true){
            return "Aluno";
        } else{
            return "Docente";
        }
    }

    public function bemvindo()
    {
        // Verificar se o User esta autenticado
        if(Auth::check() === true){

            $tipo = $this->checkUser();
            $name = Auth::user()->name;
            return view('admin.bemvindo', compact('name', 'tipo'));

        }
        return redirect()->route('admin.login');
    }

    public function showloginform(){
        return view('admin.login');
    }

    public function login(Request $request){

        $data = [
            'email' => $request->email,
            'password' => $request->password
        ];

        // Autenticar com os dados Request
        if(Auth::attempt($data)){
            return redirect()->route('admin');
        }
        return redirect()->back()->withInput()->withErrors(['Os Dados introduzidos estão incorretos, tente novamente.']);
    }

    public function logout()
    {
        Auth::logout();
        return redirect()->route('admin');
    }


}
