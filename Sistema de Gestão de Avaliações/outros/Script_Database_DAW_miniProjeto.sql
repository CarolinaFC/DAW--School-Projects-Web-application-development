use G_avaliacao;

# INSERT INTO Users VALUES (1, 'José R. Parreira', '17191@daw.pt', null, 'teste123', null, null, null);
# INSERT INTO Users VALUES (2, 'Carolina F. Cerdeira', '17444@daw.pt', null, 'teste123', null, null, null);
# INSERT INTO Users VALUES (3, 'Luís Bruno', 'lbruno@daw.pt', null, 'docente123', null, null, null);
# INSERT INTO Users VALUES (4, 'João P. Barros', 'jpb@daw.pt', null, 'docente123', null, null, null);
# INSERT INTO Users VALUES (5, 'Diogo P. Manique', 'dpm@daw.pt', null, 'docente123', null, null, null);
# INSERT INTO Users VALUES (6, 'Nelson Nunes', 'neln@daw.pt', null, 'docente123', null, null, null);
# INSERT INTO Users VALUES (7, 'Anabela Amaral', 'arpa@daw.pt', null, 'docente123', null, null, null);
# INSERT INTO Users VALUES (8, 'Alberto S. Cruz', '20109@daw.pt', null, 'teste123', null, null, null);
# INSERT INTO Users VALUES (9, 'Ana C. S. Melo', '17203@daw.pt', null, 'teste123', null, null, null);
# INSERT INTO Users VALUES (10, 'Catarina F. Ferreira', '16233@daw.pt', null, 'teste123', null, null, null);
# INSERT INTO Users VALUES (11, 'Rodrigo P. Santoro', '20107@daw.pt', null, 'teste123', null, null, null);


CREATE TABLE Docentes (
    id_docente INT PRIMARY KEY,
    nome_completo VARCHAR (50) NOT NULL,
    morada VARCHAR(100) NOT NULL,
    telefone INT (9) NOT NULL,
    categoria VARCHAR(50) NOT NULL,
    grau_academico VARCHAR(100) NOT NULL,
    instituicao VARCHAR(100) NOT NULL,
    email VARCHAR (50) NOT NULL,
    pais VARCHAR(20) NOT NULL,
    cidade_estado VARCHAR(30) NOT NULL,
    id_ut_d BIGINT UNSIGNED NOT NULL,
    FOREIGN KEY (id_ut_d) REFERENCES Users(id)
);

INSERT INTO Docentes VALUES (1, 'Luís Carlos da Silva Bruno', 'Avenida do Alentejo 67', 912568469, 'Professor Adjunto', 'Doutoramento em Engenharia Informática e de Computadores', 'Instituto Superior Técnico, Universidade Técnica de Lisboa', 'lbruno@ipbeja.pt', 'Portugal', 'Évora', 3);
INSERT INTO Docentes VALUES (2, 'João Paulo Mestre Pinheiro Ramos e Barros', 'Rua Heróis Drama 33', 936587459, 'Professor Coordenador', 'Doutoramento em Engenharia Electrotécnica - Especialidade de Sistemas Digitais', 'Faculdade de Ciências e Tecnologia, Universidade Nova de Lisboa', 'jpb@ipbeja.pt', 'Portugal', 'Beja', 4);
INSERT INTO Docentes VALUES (3, 'Diogo Palma Revez de Pina Manique', 'Avenida Miguel Fernandes 9', 923654875, 'Assistente Convidado', 'Mestrado em Engenharia Informática', 'Instituto Superior Técnico, Universidade Técnica de Lisboa', 'dpm@ipbeja.pt', 'Portugal', 'Beja', 5);
INSERT INTO Docentes VALUES (4, 'Nelson Filipe Brito Nunes', 'Travessia do Cepo 21', 912547863, 'Professor Adjunto', 'Doutorado em Belas-Artes, especialidade em Teoria da Imagem', 'Faculdade de Belas-Artes da Universidade de Lisboa', 'neln@ipbeja.pt', 'Portugal', 'Aljustrel', 6);
INSERT INTO Docentes VALUES (5, 'Anabela Reis Pacheco de Amaral', 'Rua Dr. Brito Camacho 22B', 968275123, 'Professora Adjunto', 'Doutoramento em Ciências Químicas', 'Universidade da Extremadura', 'arpa@ipbeja.pt', 'Portugal', 'Beja', 7);

CREATE TABLE Alunos (
    id_aluno INT PRIMARY KEY,
    nome_completo VARCHAR (50) NOT NULL,
    numero INT NOT NULL,
    ano_curricular INT NOT NULL,
    tipo_aluno VARCHAR (20) NOT NULL,
    morada VARCHAR (100) NOT NULL,
    telefone INT (9) NOT NULL,
    cidade_estado VARCHAR (30) NOT NULL,
    pais VARCHAR (20) NOT NULL,
    id_ut_a BIGINT UNSIGNED NOT NULL,
    FOREIGN KEY (id_ut_a) REFERENCES Users(id)
);

INSERT INTO Alunos VALUES (1, 'José Rodrigues Parreira', 17191, 3, 'Normal', 'Rua Fonte Ortesim', 912356987, 'Serpa', 'Portugal', 1);
INSERT INTO Alunos VALUES (2, 'Carolina Freixo Cerdeira', 17444, 1, 'Normal', 'Rua Rogério Paulo 7', 923546963, 'Almada', 'Portugal', 2);
INSERT INTO Alunos VALUES (3, 'Alberto Silva Cruz', 20109, 1, 'Normal', 'Rua de José Gomes 10', 935469630, 'Beja', 'Portugal', 8);
INSERT INTO Alunos VALUES (4, 'Ana Cristina Sofia Melo', 17203, 3, 'Normal', 'Rua da Ponte', 915862415, 'Beja', 'Portugal', 9);
INSERT INTO Alunos VALUES (5, 'Catarina Filipa Ferreira', 16233, 1, 'Normal', 'Rua Rogério 9', 926420365, 'Almada', 'Portugal', 10);
INSERT INTO Alunos VALUES (6, 'Rodrigo Pedro Santoro', 20107, 1, 'Normal', 'Avenida das Flores 5', 923987625, 'Faro', 'Portugal', 11);

CREATE TABLE Escolas(
    id_escola INT(6) PRIMARY KEY,
    nome_esc VARCHAR(40) NOT NULL,
    morada VARCHAR(50) NOT NULL,
    telefone INT(9) NOT NULL,
    fax INT(9) NOT NULL,
    email VARCHAR(50) NOT NULL,
    web VARCHAR(200) NOT NULL
);

INSERT INTO Escolas VALUE (1, 'Escola Superior Agrária', 'Rua Pedro Soares, 7800-295 Beja', 284314300, 284388207, 'secretariado.esa@ipbeja.pt', 'https://www.ipbeja.pt/UnidadesOrganicas/ESA/Paginas/default.aspx');
INSERT INTO Escolas VALUE (2, 'Escola Superior de Educação', 'Rua Pedro Soares, 7800-295 Beja', 284315001, 284326824, 'ese@ipbeja.pt', 'https://www.ipbeja.pt/UnidadesOrganicas/ESE/Paginas/default.aspx');
INSERT INTO Escolas VALUE (3, 'Escola Superior de Saúde', 'Rua Dr. José Correia Maltez, 7800-111 Beja', 284313280, 284329411, 'ess@ipbeja.pt', 'https://www.ipbeja.pt/UnidadesOrganicas/ESS/Paginas/default.aspx');
INSERT INTO Escolas VALUE (4, 'Escola Superior de Tecnologia e Gestão', 'Rua Pedro Soares, 7800-295 Beja', 284311540, 284361326, 'secretariado.estig@ipbeja.pt', 'https://www.ipbeja.pt/UnidadesOrganicas/ESTIG/Paginas/default.aspx');


CREATE TABLE Tipo_Curso(
    id_tipoCurso INT PRIMARY KEY,
    descricao_tipo VARCHAR(50) NOT NULL
);

INSERT INTO Tipo_Curso VALUES (1, 'Técnico Superior Profissional');
INSERT INTO Tipo_Curso VALUES (2, 'Licenciatura');
INSERT INTO Tipo_Curso VALUES (3, 'Mestrado');
INSERT INTO Tipo_Curso VALUES (4, 'Pós-Graduação');

CREATE TABLE Cursos(
    id_curso INT PRIMARY KEY,
    nome_cursos VARCHAR(50) NOT NULL,
    sigla VARCHAR(6) NOT NULL,
    estado VARCHAR(100) NOT NULL,
    regime VARCHAR(50) NOT NULL,
    propina INT NOT NULL,
    web VARCHAR(200),
    email VARCHAR(50) NOT NULL,
    id_tCurso INT,
    FOREIGN KEY (id_tCurso) REFERENCES Tipo_Curso(id_tipoCurso),
    id_esc INT,
    FOREIGN KEY (id_esc) REFERENCES Escolas(id_escola)
);

INSERT INTO Cursos VALUES (1, 'Análises Laboratoriais', 'CTeSP-analab', 'Registado na DGES', 'Diurno', 349, 'https://www.ipbeja.pt/cursos/cstp/esa-cstp-analab/Paginas/default.aspx', '', 1, 1);
INSERT INTO Cursos VALUES (2, 'Som e Imagem', 'CTeSP-somima', 'Registado na Dges', 'Diurno', 349, 'https://www.ipbeja.pt/cursos/cstp/ese-cstp-somima/Paginas/default.aspx', '', 1, 2);
INSERT INTO Cursos VALUES (3, 'Engenharia Informática', 'LEI', 'A decorrer', 'Laboral', 697, 'https://www.ipbeja.pt/cursos/estig-ei/Paginas/default.aspx', 'informatica@ipbeja.pt', 2, 4);
INSERT INTO Cursos VALUES (4, 'Terapia Ocupacional', 'ess-toc', 'A decorrer', 'Laboral', 697, 'https://www.ipbeja.pt/cursos/ess-toc/Paginas/default.aspx', 'terapia.ocupacional@ipbeja.pt', 2, 3);
INSERT INTO Cursos VALUES (5, 'Engenharia Alimentar', 'esa-m_engali', 'A decorrer', 'Laboral', 780, 'https://www.ipbeja.pt/cursos/Mestrados/esa-m_engali/Paginas/default.aspx', '', 3, 1);
INSERT INTO Cursos VALUES (6, 'Internet das Coisas', 'estig-m_intcoisas', 'A decorrer', 'Laboral', 780, 'https://www.ipbeja.pt/cursos/Mestrados/estig-m_intcoisas/Paginas/default.aspx', '', 3, 4);
INSERT INTO Cursos VALUES (7, 'Turismo - Gestão e Inovação', 'estig-pg_turGestInov', 'Não abre no ano letivo 2020/2021', 'Pós-Laboral', 780, 'https://www.ipbeja.pt/cursos/estig-pg_turGestInov/Paginas/default.aspx', '', 4, 4);

CREATE TABLE Unidades_Curriculares (
    id_uc INT AUTO_INCREMENT PRIMARY KEY,
    nome_uc VARCHAR(50) NOT NULL,
    ano INT(1) NOT NULL,
    semestre INT(1) NOT NULL ,
    descritor INT NOT NULL,
    total_horas INT(4),
    hora_contacto VARCHAR(50),
    ECTS DOUBLE,
    id_cur INT(6),
    FOREIGN KEY (id_cur) REFERENCES Cursos(id_curso)
);

INSERT INTO Unidades_Curriculares VALUES (1, 'Desenvolvimento de Aplicações Web', 3, 1, 9119122, 150, 'TP:15; PL:45', 6, 3);
INSERT INTO Unidades_Curriculares VALUES (2, 'Introdução à Programação', 1, 1, 9119102, 150, 'TP:30; PL:45', 6, 3);
INSERT INTO Unidades_Curriculares VALUES (3, 'Captura e Edição de Imagens', 1, 1, 999501, 125, '60', 5, 2);
INSERT INTO Unidades_Curriculares VALUES (4, 'Enologia', 1, 1, 956703, 138, 'TP:30', 5.5, 5);
INSERT INTO Unidades_Curriculares VALUES (5, 'Projeto Integrado', 3, 1, 9119126, 100, 'OT:50', 4, 3);

CREATE TABLE Anos_Letivos (
    id_anoLetivo INT PRIMARY KEY,
    descricao_al varchar(10) NOT NULL,
    data_inicio DATE NOT NULL,
    data_fim DATE NOT NULL
);

INSERT INTO Anos_Letivos VALUES (1, '2019/2020', '2019-09-16', '2020-07-24');
INSERT INTO Anos_Letivos VALUES (2, '2020/2021', '2020-10-6', '2021-07-23');

CREATE TABLE Epocas (
    id_epoca INT AUTO_INCREMENT PRIMARY KEY,
    descricao_ep VARCHAR(30) NOT NULL
);

INSERT INTO Epocas VALUES (1, 'Normal');
INSERT INTO Epocas VALUES (2, 'Recurso');
INSERT INTO Epocas VALUES (3, 'Especial');

CREATE TABLE Ucs_AnosLetivos (
    id_uc_AnoLetivo INT PRIMARY KEY,
    id_anoL INT NOT NULL,
    id_unidadec INT NOT NULL,
    FOREIGN KEY (id_anoL) REFERENCES Anos_Letivos(id_anoLetivo),
    FOREIGN KEY (id_unidadec) REFERENCES Unidades_Curriculares(id_uc)
);

INSERT INTO Ucs_AnosLetivos VALUES (1, 1, 1);
INSERT INTO Ucs_AnosLetivos VALUES (2, 2, 1);
INSERT INTO Ucs_AnosLetivos VALUES (3, 1, 2);
INSERT INTO Ucs_AnosLetivos VALUES (4, 2, 2);
INSERT INTO Ucs_AnosLetivos VALUES (5, 1, 3);
INSERT INTO Ucs_AnosLetivos VALUES (6, 2, 4);
INSERT INTO Ucs_AnosLetivos VALUES (7, 1, 5);
INSERT INTO Ucs_AnosLetivos VALUES (8, 2, 5);


CREATE TABLE Ucs_Docentes (
    id_docentes INT,
    id_uc_AnoL INT,
    PRIMARY KEY (id_docentes, id_uc_AnoL),
    FOREIGN KEY (id_docentes) REFERENCES Docentes(id_docente),
    FOREIGN KEY (id_uc_AnoL) REFERENCES Ucs_AnosLetivos(id_uc_AnoLetivo)
);

INSERT INTO Ucs_Docentes VALUES (1, 1);
INSERT INTO Ucs_Docentes VALUES (1, 2);
INSERT INTO Ucs_Docentes VALUES (2, 3);
INSERT INTO Ucs_Docentes VALUES (2, 4);
INSERT INTO Ucs_Docentes VALUES (3, 4);
INSERT INTO Ucs_Docentes VALUES (4, 5);
INSERT INTO Ucs_Docentes VALUES (5, 6);
INSERT INTO Ucs_Docentes VALUES (2, 7);
INSERT INTO Ucs_Docentes VALUES (2, 8);

CREATE TABLE Matricula_UC (
    id_alunos INT,
    id_uc_AnoLet INT,
    status_uc VARCHAR(10) NOT NULL,
    created_at TIMESTAMP NULL,
    updated_at TIMESTAMP NULL,
    PRIMARY KEY (id_alunos, id_uc_AnoLet),
    FOREIGN KEY (id_alunos) REFERENCES Alunos(id_aluno),
    FOREIGN KEY (id_uc_AnoLet) REFERENCES Ucs_AnosLetivos(id_uc_AnoLetivo)
);

INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (1, 1, 'Reprovado');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (1, 2, 'Inscrito');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (1, 3, 'Aprovado');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (2, 6, 'Inscrito');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (1, 8, 'Inscrito');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (3, 4, 'Inscrito');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (4, 2, 'Inscrito');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (4, 8, 'Inscrito');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (5, 6, 'Inscrito');
INSERT INTO Matricula_UC(id_alunos, id_uc_AnoLet, status_uc) VALUES (6, 6, 'Inscrito');


CREATE TABLE Avaliacoes (
    id_avaliacao INT AUTO_INCREMENT PRIMARY KEY,
    data DATE NOT NULL,
    hora TIME NOT NULL,
    sala VARCHAR(4) NOT NULL,
    created_at TIMESTAMP NULL,
    updated_at TIMESTAMP NULL,
    id_epoca_av INT,
    id_uc_anoLeti INT,
    FOREIGN KEY (id_epoca_av) REFERENCES Epocas(id_epoca),
    FOREIGN KEY (id_uc_anoLeti) REFERENCES Ucs_AnosLetivos(id_uc_AnoLetivo)
);

INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (1, '2020-01-25', '10:30:00', 'L10', 1, 1);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (2, '2020-02-10', '10:00:00', 'S10', 2, 1);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (3, '2020-09-07', '10:30:00', 'S10', 3, 1);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (4, '2020-01-23', '09:00:00', 'S8, S9', 1, 3);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (5, '2020-01-15', '14:30:00', 'S10', 2, 3);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (6, '2020-01-03', '15:00:00', 'S5', 1, 5);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (7, '2020-07-08', '16:00:00', 'S4', 2, 5);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (8, '2021-02-11', '10:00:00', 'S10', 1, 2);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (9, '2021-02-04', '14:00:00', 'S10, S8', 1, 4);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (10, '2021-02-10', '10:00:00', 'S8', 1, 6);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (11, '2020-01-23', '10:30:00', 'L8', 1, 7);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (12, '2020-02-08', '10:00:00', 'S10', 2, 7);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (13, '2020-09-10', '10:30:00', 'S10', 3, 7);
INSERT INTO Avaliacoes(id_avaliacao, data, hora, sala, id_epoca_av, id_uc_anoLeti) VALUES (14, '2020-02-15', '10:30:00', 'S10', 1, 8);


CREATE TABLE classificacoes (
    id_avaliacao_cl INT,
    id_alunos_cl INT,
    nota DOUBLE,
    status_epoca VARCHAR(50),
    created_at TIMESTAMP NULL,
    updated_at TIMESTAMP NULL,
    PRIMARY KEY (id_avaliacao_cl, id_alunos_cl),
    FOREIGN KEY (id_avaliacao_cl) REFERENCES Avaliacoes(id_avaliacao),
    FOREIGN KEY (id_alunos_cl) REFERENCES Alunos(id_aluno)
);

INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (1, 1, 2.0, 'Reprovado');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (4, 1, 12.0, 'Aprovado');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (10, 2, null, 'Inscrito');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (8, 1, null, 'Inscrito');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (10, 5, null, 'Inscrito');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (10, 6, null, 'Inscrito');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (9, 3, null, 'Inscrito');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (4, 4, 16.0, 'Aprovado');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (8, 4, null, 'Inscrito');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (14, 4, null, 'Inscrito');
INSERT INTO classificacoes(id_avaliacao_cl, id_alunos_cl, nota, status_epoca) VALUES (14, 1, null, 'Inscrito');

