using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Jasen.Framework.DynamicXml 
{
    public class DynamicObjectHelper
    {
        public static bool HasEnumerableExtendableObjectValue(DynamicExtendableObject member)
        {
            if (!HasObjectValue(member))
            {
                return false;
            }
            IEnumerable obj = member.ObjectValue as IEnumerable<DynamicExtendableObject>;
            return obj != null;
        }

        public static bool HasDynamicExtendableObjectValue(DynamicExtendableObject member)
        {
            if (!HasObjectValue(member))
            {
                return false;
            }

            return member.ObjectValue.GetType() == typeof(DynamicExtendableObject);
        }

        public static bool HasEnumerableValue(DynamicExtendableObject member)
        {
            if (IsStringType(member))
            {
               return false;
            }

            IEnumerable obj=member.ObjectValue as IEnumerable;
            return obj != null;
        }

        private static bool IsStringType(DynamicExtendableObject member)
        {
            if (!HasObjectValue(member))
            {
                return false;
            }
            return member.ObjectValue.GetType() == typeof(string);
        }

        private static bool HasObjectValue(DynamicExtendableObject member)
        {
            return (member != null) && (member.ObjectValue != null);
        }
    }
}
