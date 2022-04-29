using System;
using System.Collections.Generic;
using System.Linq;

namespace Catalyte.Apparel.Data.Model
{
    /// <summary>
    /// This is a representation of a page.
    /// </summary>
    public class Page : BaseEntity
    {
        public int TotalItems { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }
        public int TotalPages { get; private set; }
        public int StartPage { get; private set; }
        public int EndPage { get; private set; }
        public int StartIndex { get; private set; }
        public int EndIndex { get; private set; }
        public IEnumerable<int> Pages { get; private set; }
    }
}