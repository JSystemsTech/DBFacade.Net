using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DomainFacade.Utils
{
    public abstract class Enumeration : IComparable
    {

        public int Id { get; private set; }

        protected Enumeration()
        { }

        protected Enumeration(int id)
        {
            Id = id;
        }

        public override string ToString() => Id.ToString();

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public |
                                             BindingFlags.Static |
                                             BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        public override bool Equals(object obj)
        {
            Enumeration otherValue = obj as Enumeration;

            if (otherValue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }
        

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        // Other utility methods ... 
    }

    
    

}
