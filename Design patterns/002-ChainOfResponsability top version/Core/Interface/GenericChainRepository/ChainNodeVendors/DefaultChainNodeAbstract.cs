using System;

namespace Core.Interface.GenericChainRepository.ChainNodeVendors {

    public abstract class DefaultChainNodeAbstract<T> : IChainNode<T> {

        public IChainNode<T> Next { get; set; }

        public abstract T Execute(T contracted);
    }

    public class DefaultChainNodeFuncAbstract<T> : DefaultChainNodeAbstract<T> where T : new() {

        public override T Execute(T contracted) {
            
            contracted = NextExpr(contracted);

            return Next.Execute(contracted);
        }

        public Func<T, T> NextExpr { get; set; } = tin => new T();
    }

}