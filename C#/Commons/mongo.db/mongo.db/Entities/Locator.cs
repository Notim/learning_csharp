using System.ComponentModel.DataAnnotations;

using MongoDB.Bson;

namespace mongo.db.Entities {
    public class Locator {
        public ObjectId Id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string conteudo { get; set; }

        [Required]
        public string acessos { get; set; }

        public string toString() {
            return $"OId={Id} \nTitulo = {title}\nconteudo = {conteudo}\n{acessos}";
        }
    }
}