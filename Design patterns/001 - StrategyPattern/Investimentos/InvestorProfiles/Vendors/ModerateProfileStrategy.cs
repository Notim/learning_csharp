using System;

using Investimentos.Entities;

namespace Investimentos.InvestorProfiles.Vendors {

    public class ModerateProfileStrategy : IInvestorProfileStrategy {

        public decimal calculate(BankAccount Account) {
            bool escolhido = new Random().Next(100) > 50;

            return (decimal) (escolhido ? 0.025 : 0.07) * Account.Balance;

        }
    }

}