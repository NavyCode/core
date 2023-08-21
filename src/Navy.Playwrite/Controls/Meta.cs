#region

using System;

#endregion

namespace Navy.Playwright
{
    public class Meta
    {
        public Meta(string name, string comment, string mapPath, TimeSpan? timeOut = null)
        {
            Name = name;
            Comment = comment;
            MapPath = mapPath;
            TimeOut = timeOut;
        }

        public TimeSpan? TimeOut { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; } 
        public string MapPath { get; set; }
    }
}