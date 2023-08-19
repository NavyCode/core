using Microsoft.Playwright;
using Navy.Test.Reports.Navy;
using System;

namespace Navy.Playwright
{
    public class WebPage  
    {
        public WebPage(IPage page)
        {
            Page = page;  
        }
        public IPage Page { get; private set; }
        public IBrowserContext Context => Page.Context;
        public IBrowser Browser => Page.Context.Browser;

        public static T Create<T>(IPage driver) where T : WebPage
        {
            var type = typeof(T);
            var result = Activator.CreateInstance(type, driver); 
            return (T)result;
        }
    }
}