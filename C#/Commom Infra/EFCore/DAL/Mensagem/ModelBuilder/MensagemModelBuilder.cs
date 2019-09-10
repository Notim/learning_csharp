using DAL.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.ModelBuilders {
    
    sealed class MensagemModelBuilder : IEntityTypeConfiguration<Mensagem> {
        public void Configure(EntityTypeBuilder<Mensagem> builder) {

            builder.ToTable("tb_mensagem", schema : "Mensagem");

            builder.HasKey(model => model.id);
            builder.Property(c => c.id).ValueGeneratedOnAdd();

            builder.HasOne(c => c.Aluno)
                   .WithMany()
                   .HasForeignKey(c => c.idAluno)
                   .OnDelete(DeleteBehavior.Restrict);
            
            builder.HasOne(c => c.Professor)
                   .WithMany()
                   .HasForeignKey(c => c.idProfessor)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(c => c.StatusMensagem)
                   .WithMany()
                   .HasForeignKey(c => c.idStatusMensagem)
                   .OnDelete(DeleteBehavior.Restrict);
            
        }
    }

}



/*
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
);*/