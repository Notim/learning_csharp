using System;

using Investimentos.Entities;

namespace Investimentos.InvestorProfiles.Vendors {

    public class AggressiveProfileStrategy : IInvestorProfileStrategy {

        public decimal calculate(BankAccount Account) {
            var rdn = new Random().Next(100);

            var fator = (float) 0.0;

            if (rdn <= 20) {
                fator = (float) 0.05;
            }

            if (rdn > 20 && rdn <= 30) {
                fator = (float) 0.03;
            }

            if (rdn > 30) {
                fator = (float) 0.06;
            }

            return (decimal) (fator) * Account.Balance;
        }
    }

}