using Microsoft.Playwright;
using Navy.Test.Reports.Navy;
using System;

namespace Navy.Playwright
{
    public class WebPage 
    {
        public IPage Page { get; private set; }
        public IBrowserContext Context => Page.Context;
        public IBrowser Browser => Page.Context.Browser;

        public static T Create<T>(IPage page) where T : WebPage
        {
            var type = typeof(T);
            var result = Activator.CreateInstance(type);
            ((WebPage)result).Page = page;
            return (T)result;
        }
    }
}