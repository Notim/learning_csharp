namespace Core.Interface.GenericChainRepository.ChainNodeVendors {

    // this is a template to make your new Implementation Chain Nodes
    internal class DefaultChainNode<T> : IChainNode<T> where T : class, new() {

        public IChainNode<T> Next { get; set; }

        public T Execute(T contracted) {
            
            // return Next.Execute(contracted);
                
            return new T();
        }
    }

}