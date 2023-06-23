using System.IO;

namespace Navy.Test.Reports.Allure
{
    public class AllureResultAttachment
    {
        public AllureResultAttachment(string name, byte[] bytes)
        {
            Name = name;
            Bytes = bytes;
        }

        public string Name { get; set; }

        public string Type
        {
            get
            {
                var ext = Path.GetExtension(Name).ToLower();
                switch (ext)
                {
                    case ".html":
                    case ".htm":
                        return "text/html";
                    default: return "";
                }
            }
        }

        public byte[] Bytes { get; set; }
    }
}