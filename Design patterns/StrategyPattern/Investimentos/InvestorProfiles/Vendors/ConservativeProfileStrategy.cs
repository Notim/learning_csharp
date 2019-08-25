using System;

using Investimentos.Entities;

namespace Investimentos.InvestorProfiles.Vendors {

    public class ConservativeProfileStrategy : IInvestorProfileStrategy {

        public decimal calculate(BankAccount Account) {
            return (decimal) 0.08 * Account.Balance;
        }
    }

}