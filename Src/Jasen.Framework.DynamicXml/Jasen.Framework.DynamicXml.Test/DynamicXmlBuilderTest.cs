using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Jasen.Framework.DynamicXml.Test
{
    [TestClass()]
    public class DynamicXmlBuilderTest
    {
        /// <summary>
        ///ToXml 的测试
        ///</summary>
        [TestMethod()]
        public void ToXmlTest()
        {
            List<string> messages = new List<string>();
            for (int index = 0; index < 3; index++)
            {
                messages.Add(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            }

            List<DynamicExtendableObject> objects = new List<DynamicExtendableObject>();
            for (int index = 0; index < 2; index++)
            {
                dynamic tempObject = new DynamicExtendableObject("Entry");
                tempObject.String = messages;
                objects.Add(tempObject);
            }

            dynamic dynamicObject = new DynamicExtendableObject("Object");
            dynamicObject.PropertyName1 = "property name 1";
            dynamicObject.PropertyName2 = "property name 2";
            dynamicObject.Arguments = objects;

            XElement element = DynamicXmlBuilder.ToXml(dynamicObject);
            string result = element.ToString();
           
            XDocument document = XDocument.Parse(result);

            XElement parentElement = document.Element("Object");
            Assert.IsNotNull(parentElement);

            XElement propertyName1Element = parentElement.Element("PropertyName1");
            Assert.IsNotNull(propertyName1Element);
            Assert.IsTrue(string.Equals(propertyName1Element.Value, "property name 1"));

            XElement propertyName2Element = parentElement.Element("PropertyName2");
            Assert.IsNotNull(propertyName2Element);
            Assert.IsTrue(string.Equals(propertyName2Element.Value, "property name 2"));

            XElement argumentsElement = parentElement.Element("Arguments");
            Assert.IsNotNull(argumentsElement);

            var elements = argumentsElement.Elements("Entry").ToList();

            Assert.AreEqual(elements.Count, 2);

            Assert.AreEqual(elements[0].Elements("String").ToList().Count, 3);
            Assert.AreEqual(elements[1].Elements("String").ToList().Count, 3);
        }
    }
}
