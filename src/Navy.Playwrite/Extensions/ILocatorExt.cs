#region

using Microsoft.Playwright;  
using System;
using System.Threading.Tasks; 

#endregion

namespace Navy.Playwright
{
    /// <summary>
    ///     Base wrapper class for all CUIe* controls
    /// </summary>
    public static partial class ILocatorExt
    {
        public static async Task<string> WaitAttributeAsync(this ILocator el, string attributeName,
            string expectedAttributeContainsValue, string errorMessage = null, int timeoutSec = 10)
        {
            string actual = default;
            await Asserts.WaitTrueAsync(async () =>
            {
                actual = await el.GetAttributeAsync(attributeName, new LocatorGetAttributeOptions()
                {
                    Timeout = timeoutSec * 1000
                });
                return actual?.Contains(expectedAttributeContainsValue) ?? false;
            }, TimeSpan.FromSeconds(timeoutSec), errorMessage, TimeSpan.FromMilliseconds(100));
            return actual;
        }

        public static async Task<ILocator> WaitTextAsync(this ILocator el, string expectedText, TimeSpan? timeout = null, string errorMessage = null)
        {
            await Asserts.WaitNoErrorAsync(async () =>
            {
                var text = await el.TextContentAsync();
                if (text != expectedText)
                    throw new Exception();
            }, timeout ?? TimeSpan.FromSeconds(10), errorMessage, TimeSpan.FromMilliseconds(100));
            return el;
        }

        public static async Task<ILocator> ClickAsync(this ILocator locator, TimeSpan? timeout = null)
        {
            await locator.ClickAsync(new LocatorClickOptions()
            {
                Timeout = (float?)timeout?.TotalMilliseconds ?? null
            });
            return locator;
        }

        public static async Task ContextClickAsync(this ILocator locator)
        {
            await locator.ClickAsync(new LocatorClickOptions{ 
                Button = MouseButton.Right
            });
        }

        public static async Task<ILocator> WaitTextAsync(this ILocator el, string expectedText, string errorMessage, int timeoutSec = 10)
            => await el.WaitTextAsync(expectedText, TimeSpan.FromSeconds(timeoutSec), errorMessage);


        public static async Task<string> GetCssValueAsync(this ILocator el, string propertyName)
            => await el.EvaluateAsync<string>(@$"element => window.getComputedStyle(element).getPropertyValue('{propertyName}')");

    }
}