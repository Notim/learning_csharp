using System;
using System.Collections.Generic;

namespace Core.GenericChainRepository {

    public interface IChainRepository<T> {
        IChainRepository<T> Next(IChainNode<T> chainNode);

        IChainRepository<T> Next(Func<T, T> chainExpr);

        IChainRepository<T> Finish(IChainNode<T> chainNode);
        
        IChainRepository<T> AddNodes(IEnumerable<IChainNode<T>> chainNodes);

        T Run();
    }

}