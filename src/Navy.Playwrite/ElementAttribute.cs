
using System;


namespace Navy.Playwright
{
    public class ElementAttribute : Attribute
    {
        public string XPath;
        public string Id;
        public string Name;
        public string ControlType;
        public string Description;
        public string Framework;

        public ElementAttribute(string name, string xPath, string id, string controlType, string description, string framework)
        {
            Name = name;
            XPath = xPath;
            Id = id;
            ControlType = controlType;
            Description = description;
            Framework = framework;
        }
    }
}
