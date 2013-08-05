using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Collections;

namespace Jasen.Framework.DynamicXml  
{
    public class DynamicXmlBuilder
    {

        public static XElement ToXml(DynamicExtendableObject dynamicObject)
        {
            XElement parentElement = new XElement(dynamicObject.ObjectName);
            AddXmlElement(parentElement, dynamicObject);
            return parentElement;
        }

        private static void AddXmlElement(XElement parentElement, DynamicExtendableObject dynamicObject)
        {
            foreach (DynamicExtendableObject obj in dynamicObject.Members)
            {
                XElement element = AddNewChildElement(parentElement, obj);
                if (element != null)
                {
                    AddXmlElement(element, obj);
                }
            }
        }

        private static XElement AddNewChildElement(XElement parentElement, DynamicExtendableObject member)
        {
            if (DynamicObjectHelper.HasDynamicExtendableObjectValue(member))
            {
                if (member.Members.Count == 0)
                {
                    XElement element = new XElement(member.ObjectName);
                    parentElement.Add(element);
                    dynamic currentObj = member.ObjectValue as DynamicExtendableObject;
                    element.Add(ToXml(currentObj));
                    return null;
                }
                else
                {
                    XElement element = new XElement(member.ObjectName);
                    parentElement.Add(element);
                    return element;
                }
            }
            if (DynamicObjectHelper.HasEnumerableExtendableObjectValue(member))
            {
                XElement element = new XElement(member.ObjectName);
                parentElement.Add(element);
                IEnumerable objects = member.ObjectValue as IEnumerable<DynamicExtendableObject>;
                foreach (DynamicExtendableObject item in objects)
                {
                    element.Add(ToXml(item));
                }

                return element;
            }           
            if (DynamicObjectHelper.HasEnumerableValue(member))
            {
                AddChildElements(parentElement, member);
            }
            else
            {
                XElement element = new XElement(member.ObjectName, member.ObjectValue);
                parentElement.Add(element);
            }
            return null;
        }

        private static void AddChildElements(XElement parentElement, DynamicExtendableObject dynamicObject)
        {
            IEnumerable values = dynamicObject.ObjectValue as IEnumerable;
            foreach (var item in values)
            {
                XElement element = new XElement(dynamicObject.ObjectName, item);
                parentElement.Add(element);
            }
        }
    }
}
