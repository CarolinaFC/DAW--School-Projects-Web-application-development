<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Epocas extends Model
{
    use HasFactory;
    protected $table = "Epocas";


    public function avaliacoes()
    {
        return $this->hasMany(Avaliacoes::class);
    }
}
