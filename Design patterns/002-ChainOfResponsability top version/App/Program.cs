namespace App {

    public static class Program {
        private static void Main(string[] args) {
            var budgetPrg = new OrcamentoProgram();
            budgetPrg.Run();

            var conversor = new ConversorProgram();
            conversor.Run();
        }
    }

}