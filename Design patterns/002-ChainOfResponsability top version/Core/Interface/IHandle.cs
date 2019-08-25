using Core.SharedKernel;

namespace Core.Interface {

    public interface IHandle<in T> where T : BaseDomainEvent {
        void Handle(T domainEvent);
    }

}