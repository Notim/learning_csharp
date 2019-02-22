using System;

using DAL.Entities;

namespace DAL.Entities {

    public class Disciplina : DefaultEntity {
        public int      id                       { get; set; }
        public int      idCoordenador            { get; set; }
        public int      idStatusDisciplina       { get; set; }
        public string   nome                     { get; set; }
        public DateTime data                     { get; set; }
        public string   planoEnsino              { get; set; }
        public int      cargaHoraria             { get; set; }
        public string   competencias             { get; set; }
        public string   habilidades              { get; set; }
        public string   emenda                   { get; set; }
        public string   conteudoProgramatico     { get; set; }
        public string   bibliografiaBasica       { get; set; }
        public string   bibliografiaComplementar { get; set; }
        public string   percentualPratico        { get; set; }
        public string   percentualTeorico        { get; set; }

        public Coordenador      Coordenador      { get; set; }
        public StatusDisciplina StatusDisciplina { get; set; }
    }

}