namespace APP.Taxes {

    public interface ITax {
        decimal Calculate(Budget budget);
    }

}