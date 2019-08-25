using Investimentos.Entities;

namespace Investimentos.InvestorProfiles {

    public interface IInvestorProfileStrategy {
        decimal calculate(BankAccount Account);
    }

}