using System;
using System.Collections.Generic;
using System.Linq;

using Core.GenericChainRepository.ChainNodeVendors;

namespace Core.GenericChainRepository {

    public class ChainRepository<T> : IChainRepository<T> where T : class, new() {

        private readonly T _contracted;

        private IChainNode<T> _lastnode { get; set; } = new DefaultChainNode<T>();
        
        private Queue<IChainNode<T>> _internQueue { get; set; }
        
        public ChainRepository(T contractedParam) {
            this._contracted  = contractedParam;
            this._internQueue = new Queue<IChainNode<T>>();
        }

        private IChainNode<T> Next() {
            var TiradoDaPilha = this._internQueue.TryDequeue(out var ou) ? ou : this._lastnode;

            return TiradoDaPilha;
        }

        public IChainRepository<T> Next(IChainNode<T> chainNode) {
            this._internQueue.Enqueue(chainNode);

            return this;
        }

        public IChainRepository<T> Next(Func<T, T> chainExpr) {
            var FuncNode = new DefaultChainNodeFuncAbstract<T> {
                NextExpr = chainExpr
            };

            this.Next(FuncNode);

            return this;
        }
        
        public IChainRepository<T> Finish(IChainNode<T> chainNode) {
            this._lastnode = chainNode;

            return this;
        }

        public IChainRepository<T> AddNodes(IEnumerable<IChainNode<T>> chainNodes) {
            chainNodes.ToList().ForEach(node => Next(node));

            return this;
        }

        public T Run() {
            var First = Next();
            var Node  = First;

            while (Node.GetType() != _lastnode.GetType()) {
                Node.Next = Next();
                Node      = Node.Next;
            }

            return First.Execute(_contracted);
        }
    }

}