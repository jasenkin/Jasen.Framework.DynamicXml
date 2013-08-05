using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Dynamic;

namespace Jasen.Framework.DynamicXml 
{
    public class DynamicExtendableObject:DynamicObject
    {
        public DynamicExtendableObject(string name)
            :this(name,null)
        {      
        }

        public DynamicExtendableObject(string name,object value)
        {
            this.ObjectName = name;
            this.ObjectValue = value;
            this.Members = new List<DynamicExtendableObject>();
        }

        public string ObjectName
        {
            get;
            set;
        }

        public object ObjectValue 
        {
            get;
            set;
        }

        public object Container 
        {
            get;
            set;
        }

        public List<DynamicExtendableObject> Members   
        {
            get;
            set;
        }

        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            DynamicExtendableObject obj =
                this.Members.FindLast(p => p.ObjectName == binder.Name);
            if (obj != null)
            {
                obj.ObjectValue = value;
                return true;
            }

            DynamicExtendableObject item = new DynamicExtendableObject(binder.Name, value);
            item.Container = this;
            this.Members.Add(item);
            return true;
        }

        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            DynamicExtendableObject obj =
                this.Members.FindLast(p => p.ObjectName == binder.Name);
            if (obj != null)
            {
                result = obj.ObjectValue;
                return true;
            }

            DynamicExtendableObject item = new DynamicExtendableObject(binder.Name, null);
            item.ObjectValue = item;
            item.Container = this;
            this.Members.Add(item);
            result = item;
            return true;
        }
    }
}
