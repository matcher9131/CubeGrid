using CubeGrid.Static;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGrid.Models
{
    public class CrossSectionGridOptions : BindableBase
    {
        public ReactivePropertySlim<int> OffsetX { get; }

        public ReactivePropertySlim<int> OffsetY { get; }

        public ReactivePropertySlim<int> OffsetZ { get; }

        public ReactivePropertySlim<AreaKind> Area { get; }

        public CrossSectionGridOptions()
        {
            this.OffsetX = new();
            this.OffsetY = new();
            this.OffsetZ = new();
            this.Area = new(AreaKind.ZX);
        }
    }
}
