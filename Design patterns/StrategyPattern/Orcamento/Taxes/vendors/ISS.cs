namespace APP.Taxes.vendors {

    public class ISS : ITax {

        public decimal Calculate(Budget budget) {
            return budget.value += (decimal) (0.10 * (double) budget.value);
        }
    }

}