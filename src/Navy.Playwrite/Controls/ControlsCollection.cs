using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Navy.Playwright;
using Navy.Test.Reports.Navy;

namespace Navy.Playwrite.Controls
{
    public class ControlsCollection<T> where T : Control
    {
        ILocator _context;
        ITestReport _report;
        string _xPath;

        public ControlsCollection(ILocator context, string xPath, ITestReport report)
        {
            _report = report;
            _xPath = xPath;
            _context = context;
        }

        public async Task<IReadOnlyList<T>> AllAsync()
        {
            var children = _context.Get<Control>(_xPath);
            var results = await children.AllAsync();
            return results.Select(Control.Create<T>).ToList();
        }


    }
}
