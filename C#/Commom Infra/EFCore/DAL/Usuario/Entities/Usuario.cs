using System;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities {

    public class Usuario : DefaultEntity {
        public int      id          { get; set; }
        public string   login       { get; set; }
        public string   senha       { get; set; }
        public DateTime dtExpiracao { get; set; }
    }

}