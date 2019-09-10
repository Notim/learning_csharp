CREATE TABLE USUARIO (
	id INT IDENTITY(1,1) NOT NULL
	, login VARCHAR(20) NOT NULL
	, senha VARCHAR(255) NOT NULL
	, dtExpiracao DATE DEFAULT '19000101'
	
	, CONSTRAINT pkUsuario PRIMARY KEY (id)
	, CONSTRAINT uqUsuarioLogin UNIQUE (Login)
);

CREATE TABLE COORDENADOR (
	id INT IDENTITY(1,1) NOT NULL
	, idUsuario INT NOT NULL
	, nome VARCHAR(255) NOT NULL
	, email VARCHAR(100) NOT NULL
	, celular CHAR(11) NOT NULL

	, CONSTRAINT pkCoordenador PRIMARY KEY (id)
	, CONSTRAINT fkUsuarioCoordenador FOREIGN KEY (idUsuario) REFERENCES USUARIO (id)
	, CONSTRAINT uqCoordenadorEmail UNIQUE (email)
	, CONSTRAINT uqCoordenadorCelular UNIQUE (celular)
);

CREATE TABLE ALUNO (
	id INT IDENTITY(1,1) NOT NULL
	, idUsuario INT NOT NULL
	, nome VARCHAR(255) NOT NULL
	, email VARCHAR (100) NOT NULL
	, celular CHAR (11) NOT NULL
	, ra INT NOT NULL
	, foto TEXT NULL

	, CONSTRAINT pkAluno PRIMARY KEY (id)
	, CONSTRAINT fkUsuarioAluno FOREIGN KEY (idUsuario) REFERENCES USUARIO (id)
	, CONSTRAINT uqAlunoEmail UNIQUE (email)
	, CONSTRAINT uqAlunoCelular UNIQUE (celular)
);

CREATE TABLE PROFESSOR (
	id INT IDENTITY(1,1) NOT NULL
	, idUsuario INT NOT NULL
	, email VARCHAR (100) NOT NULL
	, celular CHAR (11) NOT NULL
	, apelido VARCHAR (30) NULL

	, CONSTRAINT pkProfessor PRIMARY KEY (id)
	, CONSTRAINT fkUsuarioProfessor FOREIGN KEY (idUsuario) REFERENCES USUARIO (id)
	, CONSTRAINT uqProfessorEmail UNIQUE (email)
	, CONSTRAINT uqProfessorCelular UNIQUE (celular)
);

CREATE TABLE DISCIPLINA (
	id INT IDENTITY(1,1) NOT NULL
	, idCoordenador INT NOT NULL
	
	, nome VARCHAR (50) NOT NULL
	, data DATETIME  DEFAULT GETDATE()
	, statusDisciplina VARCHAR (20)  DEFAULT 'Aberta'
	, planoEnsino VARCHAR (200) NOT NULL
	, cargaHoraria INT
	, competencias VARCHAR (50)
	, habilidades VARCHAR (100)
	, emenda VARCHAR (100)
	, conteudoProgramatico VARCHAR (100)
	, bibliografiaBasica VARCHAR (100)
	, bibliografiaComplementar VARCHAR (100)
	, percentualPratico VARCHAR (10) 
	, percentualTeorico VARCHAR (10)


	, CONSTRAINT pkDisciplina PRIMARY KEY (id)
	, CONSTRAINT uqDisciplinaNome UNIQUE (nome)
	, CONSTRAINT fkCoordenadorDisciplina FOREIGN KEY (idCoordenador) REFERENCES COORDENADOR(id)

	, CONSTRAINT ckDisciplinaCargaHoraria CHECK((cargaHoraria = 40) or (cargaHoraria = 80))
	, CONSTRAINT ckDisciplinaPercentualPratico CHECK ((percentualPratico > 0) and (percentualPratico < 100))
	, CONSTRAINT ckDisciplinaPercentualTeorico CHECK ((percentualTeorico > 0) and (percentualTeorico < 100))
	
	, CONSTRAINT ckDisciplinaStatus CHECK(
		statusDisciplina like 'Aberta' or 
		statusDisciplina like 'Fechada'
	)
);

CREATE TABLE CURSO (
	id INT IDENTITY(1,1) NOT NULL
	, nome VARCHAR (50) NOT NULL
	, CONSTRAINT pkCurso PRIMARY KEY (id)
	, CONSTRAINT uqCursoNome UNIQUE (nome)
);

CREATE TABLE DISCIPLINAOFERTADA(
	id INT IDENTITY(1,1) NOT NULL
	, idCoordenador INT NOT NULL
	, idProfessor INT NULL
	, idDisciplina INT NOT NULL
	, idCurso INT NOT NULL
	
	, dtInicioMatricula DATE NULL
	, dtFimMatricula DATE NULL
	, ano DATE NOT NULL
	, semestre INT NOT NULL
	, turma CHAR(1) NOT NULL
	, metodologia VARCHAR (100) NULL
	, recursos VARCHAR (100) NULL
	, criterioAvaliacao VARCHAR (100)
	, planoAulas VARCHAR (100)

	, CONSTRAINT pkDisciplinaOfertada PRIMARY KEY (id)
	, CONSTRAINT fkCoordenador	FOREIGN KEY (idCoordenador) REFERENCES COORDENADOR (id)
	, CONSTRAINT fkDisciplina	FOREIGN KEY (idDisciplina)	REFERENCES DISCIPLINA (id)
	, CONSTRAINT fkCurso		FOREIGN KEY (idCurso)		REFERENCES CURSO (id)

	, CONSTRAINT uqDisciplinaOfertada UNIQUE(idDisciplina, idCurso, ano, semestre, turma)
	, CONSTRAINT ckDisciplinaOfertadaAno		CHECK ((YEAR(ano) > 1900) and (YEAR(ano) < 2100))
	, CONSTRAINT ckDisciplinaOfertadaSemestre	CHECK ((semestre = 1) or (semestre = 2))
	, CONSTRAINT ckDisciplinaOfertadaTurma		CHECK( turma like '%[^A-Z]%')
);

CREATE TABLE SOLICITACAOMATRICULA(
	id INT IDENTITY(1, 1) NOT NULL
	, idAluno INT NOT NULL
	, idDisciplinaOfertada INT NOT NULL
	, dtSolicitacao DATE DEFAULT GETDATE() NOT NULL
	, idCoordenador INT NULL
	, statusSolicitacaoMatricula VARCHAR (20) DEFAULT 'SOLICITADA'
	
	, CONSTRAINT pksolicitacaomatricula				PRIMARY KEY (id)
	, CONSTRAINT fkAlunoSolicitacaoMatricula		FOREIGN KEY (idAluno) REFERENCES ALUNO(id)
	, CONSTRAINT fkCoordenadorSolicitacaoMAtricula	FOREIGN KEY (idCoordenador) REFERENCES COORDENADOR(id)

	, CONSTRAINT uqSolicitacaoMAtricula	UNIQUE(idAluno, idDisciplinaOfertada)
	, CONSTRAINT ckSolicitacaoMAtriculaStatus 
		CHECK(
			statusSolicitacaoMatricula like 'SOLICITADA' or
			statusSolicitacaoMatricula like 'APROVADA'   or
			statusSolicitacaoMatricula like 'REJEITADA' or 
			statusSolicitacaoMatricula like 'CANCELADA'
		)
);

CREATE TABLE ATIVIDADE (
	id INT NOT NULL
	, idProfessor INT NOT NULL
	
	, titulo VARCHAR (30) NOT NULL
	, descricao	VARCHAR (30) NULL
	, tipo VARCHAR(20) DEFAULT 'Resposta aberta' NOT NULL
	, extras TEXT
	
	, CONSTRAINT pkAtividade PRIMARY KEY (id)
	, CONSTRAINT fkProfessorAtividade FOREIGN KEY (idProfessor)	REFERENCES PROFESSOR (id)
	
	, CONSTRAINT uqTitulo UNIQUE (titulo)	
	, CONSTRAINT ckAtividadeTipo CHECK (
		tipo like 'Resposta aberta'   or
		tipo like 'Teste'
	)

);

CREATE TABLE ATIVIDADEVINCULADA (
	id INT IDENTITY (1,1) NOT NULL,
	idAtividade INT NOT NULL,
	idProfessor INT NOT NULL,
	idDisciplinaOfertada INT NOT NULL,

	rotulo VARCHAR (50) NOT NULL,
	statusAtividade DATETIME NOT NULL,
	dtinicioRespostas	DATETIME NOT NULL,
	dtPrimeiraRpostas DATETIME NOT NULL,

	CONSTRAINT pkAtividadeVinculada PRIMARY KEY (id),
	CONSTRAINT fkAtividadeAtividadeVinculada FOREIGN KEY (idAtividade)REFERENCES ATIVIDADE (id),
	CONSTRAINT fkProfessorAtividadeVinculada FOREIGN KEY (id)REFERENCES PROFESSOR (id),
	CONSTRAINT fkDisciplinaOfertadaAtividadeVinculada FOREIGN KEY (id)REFERENCES DISCIPLINAOFERTADA (id),
	
	CONSTRAINT uqAtividadeVinculada UNIQUE (rotulo,idAtividade,idDisciplinaOfertada)
);

CREATE TABLE ENTREGA (
	id INT IDENTITY (1,1) NOT NULL,
	idAluno INT NOT NULL,
	idAtividadeVinculada INT NOT NULL,
	titulo VARCHAR(20) ,
	resposta TEXT,
	dtEntrega DATETIME DEFAULT GETDATE(),
	statusEntrega VARCHAR(20) DEFAULT 'Entregue',
	idProfessor INT NULL,
	nota NUMERIC(2,2) NULL,
	dtAvaliacao DATE NULL,
	obs VARCHAR (30) NULL,

	CONSTRAINT pkEntrega PRIMARY KEY (id),
	CONSTRAINT fkAlunoEntrega FOREIGN KEY (idAluno) REFERENCES ALUNO (id),
	CONSTRAINT fkAtividadeVinculadaEntrega FOREIGN KEY (idAtividadevinculada) REFERENCES ATIVIDADEVINCULADA (id),
	CONSTRAINT fkProfessorEntrega FOREIGN KEY (idProfessor)REFERENCES PROFESSOR (id),

	CONSTRAINT ckEntregaStatusEntrega CHECK (
			statusEntrega like 'Entregue' or
			statusEntrega like 'Corrigido'
	),
	CONSTRAINT ckEntregaNota CHECK (nota > 0.00 and nota < 10.00)
	, CONSTRAINT uqEntrega UNIQUE(idAluno, idAtividadevinculada)
);

CREATE TABLE MENSAGEM (
	id INT IDENTITY(1, 1) NOT NULL,
	idAluno INT NOT NULL,
	idProfessor INT NOT NULL,
	assunto VARCHAR (50) NOT NULL,
	referencias VARCHAR (50) NOT NULL,
	conteudo TEXT,
	statusMensagem DATETIME DEFAULT'ENVIADO' NOT NULL,
	DtEnvio DATETIME DEFAULT GETDATE(),
	DtResposta DATETIME NULL,
	resposta TEXT,
	
	CONSTRAINT pkMensagem PRIMARY KEY (id),
	CONSTRAINT fkAlunoMensagem FOREIGN KEY (idAluno) REFERENCES ALUNO (id),
	CONSTRAINT fkProfessorMensagem FOREIGN KEY (idProfessor) REFERENCES PROFESSOR (id),
	CONSTRAINT ckStatusMensagemMensagem CHECK (
		statusMensagem like 'Enviado' or 
		statusMensagem like 'Lido' or 
		statusMensagem like 'Respondido'
	)
);