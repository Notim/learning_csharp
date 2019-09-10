using System;
using System.Globalization;

namespace APP.Entities {

    public class Person {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Mail { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime Birthday { get; set; }

        public override string ToString() {
            return this.Id + "\t" + this.Name + "\t" + this.Birthday.ToString(CultureInfo.InvariantCulture) + "\t" + this.Mail + "\t" + this.DocumentNumber;
        }
    }

}