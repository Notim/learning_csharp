using APP.Taxes;

namespace APP {

    public static class BudgetCalculator {
        public static decimal Calculate(Budget budget, ITax tax) {
            return tax.Calculate(budget);
        }
    }

}