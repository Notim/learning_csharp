using System;
using System.Data;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities {

    public class User : DefaultEntity {

        public int    id              { get; set; }
        public string name            { get; set; }
        public string surname         { get; set; }
        public string email           { get; set; }
        public string telephoneNumber { get; set; }
        public string city            { get; set; }
        public string state           { get; set; }

    }

}