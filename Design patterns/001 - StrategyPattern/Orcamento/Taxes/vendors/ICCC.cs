namespace APP.Taxes.vendors {

    public class ICCC : ITax {

        public decimal Calculate(Budget budget) {

            var value = (decimal) 0.0;
            if (budget.value <= (decimal) 1000.00) {
                value = budget.value * (decimal) 0.05;
            }

            if (budget.value > (decimal) 1000.00 && budget.value <= (decimal) 3000.00) {
                return (budget.value * (decimal) 0.07);
            }
            
            if (budget.value > (decimal) 3000.00) {
                return (budget.value * (decimal) 0.08) + 30;
            }
            
            return value;
        }
    }

}