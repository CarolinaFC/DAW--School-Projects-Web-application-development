<?php

use App\Http\Controllers\AuthController;
use App\Http\Controllers\PageController;
use App\Http\Controllers\RegisterController;
use App\Http\Controllers\ViewerController;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Route;
use App\Http\Controllers\CreateController;
use App\Http\Controllers\InscribeController;

/*
|--------------------------------------------------------------------------
| Web Routes
|--------------------------------------------------------------------------
|
| Here is where you can register web routes for your application. These
| routes are loaded by the RouteServiceProvider within a group which
| contains the "web" middleware group. Now create something great!
|
*/

Route::get('/', function () {
    return view('welcome');
});

Auth::routes();

Route::get('/home', [App\Http\Controllers\HomeController::class, 'index'])->name('home');

// Autenticação
Route::get('/admin', [AuthController::class, 'bemvindo'])->name('admin');
Route::get('/admin/login', [AuthController::class, 'showloginform'])->name('admin.login');
Route::get('/admin/logout', [AuthController::class, 'logout'])->name('admin.logout');
Route::post('/admin/login/do', [AuthController::class, 'login'])->name('admin.login.do');

// Marcar Avaliacao
Route::get('/admin/CreateAv', [CreateController::class, 'createAvaliacao'])->name('admin.createAv');

// Filtos da maracacao de avaliacoes
Route::get('/admin/CreateAv/filter/{id}', [CreateController::class, 'filter'])->name('admin.createAv.filter');
Route::get('/admin/CreateAv/filter/table/{id}', [CreateController::class, 'table'])->name('admin.createAv.filter.table');
Route::get('/admin/CreateAv/filter/table/{anoL}/{uc}', [CreateController::class, 'tablefilter'])->name('admin.createAv.filter.tablefilter');

// Adicicao da avaliacao a base de dados
Route::post('/admin/CreateAv/store', [CreateController::class, 'store'])->name('admin.createAv.store');

// Inscrever nas Avaliações
Route::get('/admin/InscribeAv', [InscribeController::class, 'inscribeAvaliacao'])->name('admin.inscribeAv');

// Carregamentos das tabelas
Route::get('/admin/InscribeAv/definidas', [InscribeController::class, 'loadDefinidas'])->name('admin.InscribeAv.definidas');
Route::get('/admin/InscribeAv/avaliacoes', [InscribeController::class, 'loadAvaliacoes'])->name('admin.InscribeAv.avaliacoes');

// Adicicao da inscricao a base de dados
Route::post('/admin/InscribeAv/store', [InscribeController::class, 'store'])->name('admin.InscribeAv.store');


// Rotas necessárias para o controlador de Registar Classificações
// Página de Registo de classificações
Route::get('/admin/RegisterClass', [RegisterController::class, 'registerClass'])->name('admin.registerCl');
// Filtrar as Ucs de acordo o utilizador Docente
Route::get('/admin/RegisterClass/filter/{id}', [RegisterController::class, 'ucsFilter'])->name('admin.registerCl.filter');
// mostrar a tabela das classificações
Route::get('/admin/RegisterClass/showTableRegisterClass/{curso}/{uc}/{epoca}', [RegisterController::class, 'showTableRegisterClass'])->name('admin.registerCl.tableRegisterShow');
// Enviar informação da tabela para a base de dados
Route::post('/admin/RegisterClass/sendTableRegisterClass', [RegisterController::class, 'sendTableRegisterClass'])->name('admin.registerCl.tableRegisterSend');

// Rotas necessárias para o controlador de Visualizar Classificações
// Página de Visualização de Classificações
Route::get('/admin/viewClass', [ViewerController::class, 'viewClass'])->name('admin.viewCl');
// Mostrar a tabela de Classificações
Route::get('/admin/viewClass/showTableViewClass', [ViewerController::class, 'showTableViewClass'])->name('admin.viewCl.tableClassificacoes');
// Mostrar a tabela de acordo com a pesquisa
Route::get('/admin/viewClass/showTableViewClass/{ano_letivo}/{periodo}/{ano_curr}/{epoca}', [ViewerController::class, 'showTableViewClassSearch'])->name('admin.viewCl.tableClassificacoesPesquisar');

