using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc.Rendering;

namespace LegoPaging
{
    public interface ISectionBuilder
    {
        void Build(Action<TagBuilder> setup);
    }

    
}
