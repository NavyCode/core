#region

using System;
using System.Collections.Generic; 
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;


#endregion

namespace Navy.Playwright
{
    /// <summary>
    ///  Base wrapper class for all controls
    /// </summary>
    public class Control :  ILocator
    {
        public ILocator Element { get; set; }
        public Meta Meta { get; set; }

        public static Control Create(ILocator obj, Meta meta) => Create<Control>(obj, meta);
        public static T Create<T>(ILocator obj, Meta meta) where T : Control
        {
            var type = typeof(T);
            var result = (Control)Activator.CreateInstance(type);
            result.Element = obj;
            result.Meta = meta;
            return (T)result;
        }

        public static T Create<T>(Control control) where T : Control
        {
            return Create<T>(control.Element, control.Meta);
        }


        public ILocator First => Element.First;

        public ILocator Last => Element.Last;

        public IPage Page => Element.Page; 

        public Task<IReadOnlyList<ILocator>> AllAsync()
            => Element.AllAsync();

        public Task<IReadOnlyList<string>> AllInnerTextsAsync()
            => Element.AllInnerTextsAsync();

        public Task<IReadOnlyList<string>> AllTextContentsAsync()
            => Element.AllTextContentsAsync();

        public ILocator And(ILocator locator)
            => Element.And(locator);

        public Task BlurAsync(LocatorBlurOptions options = null)
            => Element.BlurAsync(options);

        public Task<LocatorBoundingBoxResult> BoundingBoxAsync(LocatorBoundingBoxOptions options = null)
            => Element.BoundingBoxAsync(options);

        public Task CheckAsync(LocatorCheckOptions options = null)
            => Element.CheckAsync(options);

        public Task ClearAsync(LocatorClearOptions options = null)
            => Element.ClearAsync(options);

        public Task ClickAsync(LocatorClickOptions options = null)
            => Element.ClickAsync(options);

        public Task<int> CountAsync()
            => Element.CountAsync();

        public Task DblClickAsync(LocatorDblClickOptions options = null)
            => Element.DblClickAsync(options);

        public Task DispatchEventAsync(string type, object eventInit = null, LocatorDispatchEventOptions options = null)
            => Element.DispatchEventAsync(type, eventInit, options);

        public Task DragToAsync(ILocator target, LocatorDragToOptions options = null)
            => Element.DragToAsync(target, options);

        public Task<IElementHandle> ElementHandleAsync(LocatorElementHandleOptions options = null)
            => Element.ElementHandleAsync(options);

        public Task<IReadOnlyList<IElementHandle>> ElementHandlesAsync()
            => Element.ElementHandlesAsync();

        public Task<T> EvaluateAsync<T>(string expression, object arg = null, LocatorEvaluateOptions options = null)
            => Element.EvaluateAsync<T>(expression, arg, options);

        public Task<T> EvaluateAllAsync<T>(string expression, object arg = null)
            => Element.EvaluateAllAsync<T>(expression, arg);

        public Task<IJSHandle> EvaluateHandleAsync(string expression, object arg = null, LocatorEvaluateHandleOptions options = null)
            => Element.EvaluateHandleAsync(expression, arg, options);

        public Task FillAsync(string value, LocatorFillOptions options = null)
            => Element.FillAsync(value, options);

        public ILocator Filter(LocatorFilterOptions options = null)
            => Element.Filter(options);

        public Task FocusAsync(LocatorFocusOptions options = null)
            => Element.FocusAsync(options);

        public IFrameLocator FrameLocator(string selector)
            => Element.FrameLocator(selector);

        public Task<string> GetAttributeAsync(string name, LocatorGetAttributeOptions options = null)
            => Element.GetAttributeAsync(name, options);

        public ILocator GetByAltText(string text, LocatorGetByAltTextOptions options = null)
            => Element.GetByAltText(text, options);

        public ILocator GetByAltText(Regex text, LocatorGetByAltTextOptions options = null)
            => Element.GetByAltText(text, options);

        public ILocator GetByLabel(string text, LocatorGetByLabelOptions options = null)
            => Element.GetByLabel(text, options);

        public ILocator GetByLabel(Regex text, LocatorGetByLabelOptions options = null)
            => Element.GetByLabel(text, options);

        public ILocator GetByPlaceholder(string text, LocatorGetByPlaceholderOptions options = null)
            => Element.GetByPlaceholder(text, options);

        public ILocator GetByPlaceholder(Regex text, LocatorGetByPlaceholderOptions options = null)
            => Element.GetByPlaceholder(text, options);

        public ILocator GetByRole(AriaRole role, LocatorGetByRoleOptions options = null)
            => Element.GetByRole(role, options);

        public ILocator GetByTestId(string testId)
            => Element.GetByTestId(testId);

        public ILocator GetByTestId(Regex testId)
            => Element.GetByTestId(testId);

        public ILocator GetByText(string text, LocatorGetByTextOptions options = null)
            => Element.GetByText(text, options);

        public ILocator GetByText(Regex text, LocatorGetByTextOptions options = null)
            => Element.GetByText(text, options);

        public ILocator GetByTitle(string text, LocatorGetByTitleOptions options = null)
            => Element.GetByTitle(text, options);

        public ILocator GetByTitle(Regex text, LocatorGetByTitleOptions options = null)
            => Element.GetByTitle(text, options);

        public Task HighlightAsync()
            => Element.HighlightAsync();

        public Task HoverAsync(LocatorHoverOptions options = null)
            => Element.HoverAsync(options);

        public Task<string> InnerHTMLAsync(LocatorInnerHTMLOptions options = null)
            => Element.InnerHTMLAsync(options);

        public Task<string> InnerTextAsync(LocatorInnerTextOptions options = null)
            => Element.InnerTextAsync(options);

        public Task<string> InputValueAsync(LocatorInputValueOptions options = null)
            => Element.InputValueAsync(options);

        public Task<bool> IsCheckedAsync(LocatorIsCheckedOptions options = null)
            => Element.IsCheckedAsync(options);

        public Task<bool> IsDisabledAsync(LocatorIsDisabledOptions options = null)
            => Element.IsDisabledAsync(options);

        public Task<bool> IsEditableAsync(LocatorIsEditableOptions options = null)
            => Element.IsEditableAsync(options);

        public Task<bool> IsEnabledAsync(LocatorIsEnabledOptions options = null)
            => Element.IsEnabledAsync(options);

        public Task<bool> IsHiddenAsync(LocatorIsHiddenOptions options = null)
            => Element.IsHiddenAsync(options);

        public Task<bool> IsVisibleAsync(LocatorIsVisibleOptions options = null)
            => Element.IsVisibleAsync(options);

        public ILocator Locator(string selectorOrLocator, LocatorLocatorOptions options = null)
            => Element.Locator(selectorOrLocator, options);

        public ILocator Locator(ILocator selectorOrLocator, LocatorLocatorOptions options = null)
            => Element.Locator(selectorOrLocator, options);

        public ILocator Nth(int index)
            => Element.Nth(index);

        public ILocator Or(ILocator locator)
            => Element.Or(locator);

        public Task PressAsync(string key, LocatorPressOptions options = null)
            => Element.PressAsync(key, options);

        public Task<byte[]> ScreenshotAsync(LocatorScreenshotOptions options = null)
            => Element.ScreenshotAsync(options);

        public Task ScrollIntoViewIfNeededAsync(LocatorScrollIntoViewIfNeededOptions options = null)
            => Element.ScrollIntoViewIfNeededAsync(options);

        public Task<IReadOnlyList<string>> SelectOptionAsync(string values, LocatorSelectOptionOptions options = null)
            => Element.SelectOptionAsync(values, options);

        public Task<IReadOnlyList<string>> SelectOptionAsync(IElementHandle values, LocatorSelectOptionOptions options = null)
            => Element.SelectOptionAsync((IElementHandle)values, options);

        public Task<IReadOnlyList<string>> SelectOptionAsync(IEnumerable<string> values, LocatorSelectOptionOptions options = null)
            => Element.SelectOptionAsync(values, options);

        public Task<IReadOnlyList<string>> SelectOptionAsync(SelectOptionValue values, LocatorSelectOptionOptions options = null)
            => Element.SelectOptionAsync(values, options);

        public Task<IReadOnlyList<string>> SelectOptionAsync(IEnumerable<IElementHandle> values, LocatorSelectOptionOptions options = null)
            => Element.SelectOptionAsync(values, options);

        public Task<IReadOnlyList<string>> SelectOptionAsync(IEnumerable<SelectOptionValue> values, LocatorSelectOptionOptions options = null)
            => Element?.SelectOptionAsync(values, options);

        public Task SelectTextAsync(LocatorSelectTextOptions options = null)
            => Element.SelectTextAsync(options);

        public Task SetCheckedAsync(bool checkedState, LocatorSetCheckedOptions options = null)
            => Element.SetCheckedAsync(checkedState, options);

        public Task SetInputFilesAsync(string files, LocatorSetInputFilesOptions options = null)
            => Element.SetInputFilesAsync(files, options);

        public Task SetInputFilesAsync(IEnumerable<string> files, LocatorSetInputFilesOptions options = null)
            => Element.SetInputFilesAsync(files, options);

        public Task SetInputFilesAsync(FilePayload files, LocatorSetInputFilesOptions options = null)
            => Element.SetInputFilesAsync(files, options);

        public Task SetInputFilesAsync(IEnumerable<FilePayload> files, LocatorSetInputFilesOptions options = null)
            => Element.SetInputFilesAsync(files, options);

        public Task TapAsync(LocatorTapOptions options = null)
            => Element.TapAsync(options);

        public Task<string> TextContentAsync(LocatorTextContentOptions options = null)
            => Element.TextContentAsync(options);

        public Task TypeAsync(string text, LocatorTypeOptions options = null)
            => Element.TypeAsync(text, options);

        public Task UncheckAsync(LocatorUncheckOptions options = null)
            => Element.UncheckAsync(options);

        public Task WaitForAsync(LocatorWaitForOptions options = null)
            => Element.WaitForAsync(options);

        public Task<JsonElement?> EvaluateAsync(string expression, object arg = null, LocatorEvaluateOptions options = null)
            => Element.EvaluateAsync(expression, arg, options);
    }
}