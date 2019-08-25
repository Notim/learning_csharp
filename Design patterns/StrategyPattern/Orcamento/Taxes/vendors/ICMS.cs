namespace APP.Taxes.vendors {

    public class ICMS : ITax {

        public decimal Calculate(Budget budget) {
            return budget.value += (decimal) (0.17 * (double) budget.value);
        }
    }

}