using Investimentos.Entities;
using Investimentos.InvestorProfiles;

namespace Investimentos {

    public static class InvestmentDirector {

        public static BankAccount Invest(BankAccount account, IInvestorProfileStrategy strategy) {
            var rendimento = strategy.calculate(account);

            account.Balance += (decimal) (0.75 * (double) rendimento);

            return account;
        }
    }

}