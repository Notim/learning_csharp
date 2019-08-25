namespace Core.Interface.GenericChainRepository {

    public interface IChainNode<T> {
        IChainNode<T> Next { get; set; }

        T Execute(T contracted);
    }

}