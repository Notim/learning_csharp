using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;

namespace mongo.db.Entities {

    public class Person {
        public ObjectId Id { get; set; }

        [Required]
        public string titulo { get; set; }

        [Required]
        public string conteudo { get; set; }

        [Required]
        public string acessos { get; set; }

        public string toString() {
            return $"OId={Id} \nTitulo = {titulo}\nconteudo = {conteudo}\n{acessos}";
        }
    }

}