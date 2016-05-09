using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace ControllerTest
{
    public class DynamicDictionary : DynamicObject
    {
        // The inner dictionary.
        //Dictionary<string, object> dictionary
        //    = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase);

        System.Collections.Hashtable dictionary = new System.Collections.Hashtable(StringComparer.OrdinalIgnoreCase);
        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get
            {
                return dictionary.Count;
            }
        }

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name.ToLower();

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            if (dictionary.ContainsKey(name))
            {
                result = dictionary[name];
            }
            else
            {
                result = null;
            }

            result = dictionary[name];
            
            return true;
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            dictionary[binder.Name.ToLower()] = value;

            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }
    }

}
