using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Navy.Playwright;
using Navy.Test.Reports.Navy;

namespace Navy.Playwrite.Controls
{
    public class ControlCollection<T> where T : Control
    {
        ILocator _context;
        Meta _meta;

        public ControlCollection(ILocator context, Meta meta)
        {
            _meta = meta;
            _context = context;
        }

        public async Task<IReadOnlyList<T>> AllAsync()
        {
            var results = await _context.AllAsync();
            return results.Select(p => Control.Create<T>(p, _meta)).ToList();
        }


    }
}
