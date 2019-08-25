
# Padrao strategy

Esse Padrão faz parte da gangue dos 4 (GoF) e é um padrao comportamental, ou seja, é um padrao usado em rotina de comportamento do sistema.

## Quando usar  

Quando se faz necessário usar muitas condições específicas para uma determinada tarefa ou regra de negócio.

## porque usar

é um padrão que evita que seja necessário vários ifs encadeados,
pois você aplica as regras específicas para cada tipo de problema em classes distintas.

## Como usar  

Criar uma interface que implemente metodos que será padrão para todos essas condicões específicas

## Problemas exemplos  

**_Realizador de investimentos_**:  
imagina que exista uma rotina que define qual vai ser a estrategia de intvestimento aplicado sobre um Saldo
e que existem estrategias diferentes de rendimento que dependem de qual perfil de investidor do cara que ta investindo.
Sendo: _Agressivo_, _Moderado_ e _conservador_.
cada um dos perfis tem regras de rendiment diferentes.
Foi criado uma iterface chamada `IInvestorProfileStrategy`

```CSharp
// essa é a entidade que vai ser usada nas estratégias
public class BankAccount {
    public decimal Balance { get; set; }
}
```

```CSharp
// Esse eh o molde para as classes
public interface IInvestorProfileStrategy {
    decimal calculate(BankAccount Account);
}
```

Foram criadas as implementacoes de acordo com as regras de negocio especificas para cada um dos perfis.

> Regras para perfil Conservador: sempre rendera 8% do saldo

```CSharp
public class ConservativeProfileStrategy : IInvestorProfileStrategy {
    public decimal calculate(BankAccount Account) {
        return (decimal) 0.08 * Account.Balance;
    }
}
```

> Regras para perfil Moderado: 50% de chance de render 2.5% e 50% de render 7%

```CSharp
public class ModerateProfileStrategy : IInvestorProfileStrategy {
    public decimal calculate(BankAccount Account) {
        bool escolhido = new Random().Next(100) > 50;

        return (decimal) (escolhido ? 0.025 : 0.07) * Account.Balance;
    }
}
```

> Regras para perfil Agressivo: 20% de chance de render 5%, 30% de chance de render 3% e 50% de chance de render 6%.

```CSharp
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
```

Após definir as estratégias é necessário fazer a chamada dela de modo genérico, não especificando a estratégia a ser usada.

```CSharp
// aplicador de investimentos
public static class InvestmentDirector {
    public static BankAccount Invest(BankAccount account, IInvestorProfileStrategy strategy) {
        var rendimento = strategy.calculate(account);

        // aqui é alicado uma regra global que diz que o rendimento idependente de como foi vai ser descontado 25% para impostos
        account.Balance += (decimal) (0.75 * (double) rendimento);

        return account;
    }
}
```

```CSharp
class Program {
    static void Main(string[] args) {
        var Account = new BankAccount {
            Balance = 500000
        };

        // aqui a gente usou a estratégia de perfil agressivo
        var BankAccountAfter = InvestmentDirector.Invest(Account, new AggressiveProfileStrategy())

        Console.WriteLine("TotalValue: " + BankAccountAfter.Balance.ToString("C"));
    }
}
```

Saída no console com perfil Agressivo

```Console
[notim@lenovo-ideapad: ~/GitHub/Design patterns/StrategyPattern/Investimentos]$ dotnet run
TotalValue: R$ 522.500,00
```

Saída no console com perfil Conservador

```Console
[notim@lenovo-ideapad: ~/GitHub/Design patterns/StrategyPattern/Investimentos]$ dotnet run
TotalValue: R$ 530.000,00
```

**_Realizador de orcamento com Calculador de impostos_**.
imagina onde exista uma caralhada de tipos de impostos, e que basicamente ele faz a mesma coisa no final que é
ser somado no valor total de um orçamento. Foi criado uma interface chamada ITax que possui um metodo chamado Calcular. Pra cada tipo de imposto diferente você implementa a interface e usa o método pra realizar as regras inclusive usar recursos externos.
