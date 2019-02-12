using DAL.Entities;

namespace DAL.Disciplina.Entities {

    public class Disciplina : DefaultEntity {
        
    }

    
}
/*
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
 */