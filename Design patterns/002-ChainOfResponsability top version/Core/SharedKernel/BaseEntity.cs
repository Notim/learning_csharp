using System;
using System.Collections.Generic;

namespace Core.SharedKernel {

    public abstract class BaseEntity {

        private int? _requestedHashCode;

        protected virtual int Id { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? ExcludedAt { get; set; }

        public bool Active { get; set; }

        public List<BaseDomainEvent> DomainEvents { get; private set; }

        public void AddDomainEvent(BaseDomainEvent eventItem) {
            DomainEvents = DomainEvents ?? new List<BaseDomainEvent>();

            DomainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(BaseDomainEvent eventItem) {
            DomainEvents?.Remove(eventItem);
        }

        public bool IsTransient() {
            return this.Id == default (int);
        }

        public override bool Equals(object obj) {
            if (!(obj is BaseEntity))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = (BaseEntity) obj;

            if (item.IsTransient() || this.IsTransient())
                return false;

            else
                return item.Id == this.Id;
        }

        public override int GetHashCode() {
            if (!IsTransient()) {
                if (!_requestedHashCode.HasValue)
                    _requestedHashCode = this.Id.GetHashCode() ^ 31;

                return _requestedHashCode.Value;
            } else
                return base.GetHashCode();
        }

        public static bool operator ==(BaseEntity left, BaseEntity right) {
            return left?.Equals(right) ?? Equals(right, null);
        }

        public static bool operator !=(BaseEntity left, BaseEntity right) {
            return !(left == right);
        }
    }

}