using System.Collections.Generic;
using System.Linq;

namespace SpecAid.Tooling.Base
{
    public class CompareRowResult
    {
        public List<CompareColumnResult> ColumnResults = new List<CompareColumnResult>();
        
        public int TotalErrors
        {
            get { return ColumnResults.Count(n => n.IsError); }
        }
    }
}
