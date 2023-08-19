#region

using Microsoft.Playwright; 

#endregion

namespace Navy.Playwright
{
    public static class IPageExt
    {
        public static T Get<T>(this IPage page) where T : WebPage
        {
            return WebPage.Create<T>(page);
        }
    }
}