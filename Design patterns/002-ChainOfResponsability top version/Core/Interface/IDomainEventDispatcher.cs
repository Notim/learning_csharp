using Core.SharedKernel;

namespace Core.Interface {

    public interface IDomainEventDispatcher {
        void Dispatch(BaseDomainEvent domainEvent);
    }

}