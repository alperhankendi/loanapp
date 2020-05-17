using System;

namespace Loan.Core
{
    public abstract class IdentityBase<T> : IEquatable<IdentityBase<T>>
    {
        protected IdentityBase()
        {
            
        }
        public IdentityBase(T id)
        {
            Id = id;
        }
        public T Id { get; protected set; }

        public bool Equals(IdentityBase<T> id)
        {
            if (ReferenceEquals(this, id)) return true;
            if (ReferenceEquals(null, id)) return false;
            return Id.Equals(id.Id);
        }

        public override bool Equals(object anotherObject)
        {
            return Equals(anotherObject as IdentityBase<T>);
        }

        public override int GetHashCode()
        {
            return (GetType().GetHashCode() * 907) + Id.GetHashCode();
        }

        public override string ToString()
        {
            return GetType().Name + " [Id=" + Id + "]";
        }
    }
}