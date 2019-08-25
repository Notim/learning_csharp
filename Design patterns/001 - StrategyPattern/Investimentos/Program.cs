using System;

using Investimentos.Entities;
using Investimentos.InvestorProfiles.Vendors;

namespace Investimentos {

    class Program {
        static void Main(string[] args) {
            var Account = new BankAccount {Balance = 500000};

            Console.WriteLine(InvestmentDirector.Invest(Account, new AggressiveProfileStrategy()).Balance);
        }
    }

}