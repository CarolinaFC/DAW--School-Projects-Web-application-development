<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;

class Avaliacoes extends Model
{
    use HasFactory;
    protected $table = "Avaliacoes";

    public function epoca()
    {
        return $this->belongsTo(Epocas::class);
    }

}
