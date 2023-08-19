#region

using System;

#endregion

namespace Navy.Playwright
{
    public class ControlInfo
    {
        public static ControlInfo Default => new ControlInfo();

        public TimeSpan? TimeOut { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; } 
        public string MapPath { get; set; }

        public static ControlInfo ByTimeOut(TimeSpan timeOut)
        {
            return new ControlInfo
            {
                TimeOut = timeOut
            };
        }

        public ControlInfo AndName(string name, string comment, string mapPath)
        {
            Name = name;
            Comment = comment;
            MapPath = mapPath;
            return this;
        }
    }
}