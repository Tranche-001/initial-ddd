using System;

namespace studyRats.Library.Framework.Core.Domain.Entity
{
    public abstract class TypedIdValueBase : IEquatable<TypedIdValueBase>, IComparable
    {
        public Guid? Value { get; set; }

        public TypedIdValueBase()
        {
        }

        public TypedIdValueBase(string value)
        {
            if (value == null) return;
            Value = Guid.Parse(value);
        }

        public TypedIdValueBase(Guid value)
        {
            Value = value;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is TypedIdValueBase other && Equals(other);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(TypedIdValueBase other)
        {
            if (other == null) return false;
            return Value == other.Value;
        }

        public static bool operator ==(TypedIdValueBase obj1, TypedIdValueBase obj2)
        {
            if (Equals(obj1, null))
            {
                if (Equals(obj2, null))
                {
                    return true;
                }
                return false;
            }
            return obj1.Equals(obj2);
        }

        public static bool operator !=(TypedIdValueBase x, TypedIdValueBase y)
        {
            return !(x == y);
        }

        public override string ToString()
        {
            if (Value == null) return null;
            return Value.ToString();
        }

        public int CompareTo(object obj)
        {
            if (Value == null || obj == null) return 0;

            if (obj is TypedIdValueBase)
            {
                var objToCompare = (TypedIdValueBase)obj;
                if (objToCompare.Value == null) return 0;

                return string.Compare(Value.ToString(), objToCompare.Value.ToString());
            }
            return 0;
        }
    }
}
