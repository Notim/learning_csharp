using System;

using APP.Taxes;
using APP.Taxes.vendors;

namespace APP {

    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Hello World!");

            ITax iss  = new ISS();
            ITax icms = new ICMS();
            ITax iccc = new ICCC();

            Console.WriteLine(BudgetCalculator.Calculate(new Budget {value = 2500}, iss));
            Console.WriteLine(BudgetCalculator.Calculate(new Budget {value = 2500}, iss));
            
            Console.WriteLine(BudgetCalculator.Calculate(new Budget {value = 1000}, iccc));
            Console.WriteLine(BudgetCalculator.Calculate(new Budget {value = 3000}, iccc));
            Console.WriteLine(BudgetCalculator.Calculate(new Budget {value = 3001}, iccc));
        }
    }

}