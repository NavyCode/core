
using System;


namespace Navy.Playwright
{
    public class ElementAttribute : Attribute
    {
        public string XPath;
        public string Name;
        public string Role; 

        public ElementAttribute(string name, string mapPath, string role)
        {
            Name = name;
            XPath = mapPath;
            Role = role;
        }
    }
}
