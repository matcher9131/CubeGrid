using CubeGrid.Static;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CubeGrid.Models
{
    public class CubicData : BindableBase, IDisposable
    {
        public ReactivePropertySlim<string> RawData { get; }

        public ReadOnlyReactivePropertySlim<IEnumerable<CubicCell>> Cells { get; }

        public CubicData()
        {
            this.RawData = new("");
            this.Cells = this.RawData.Select(rawData => rawData
                .Split([Environment.NewLine, "\t"], StringSplitOptions.RemoveEmptyEntries)
                .Select(s => CubicCell.ParseOrNull(s))
                .OfType<CubicCell>()
            ).ToReadOnlyReactivePropertySlim<IEnumerable<CubicCell>>();
        }

        #region IDisposable
        private readonly CompositeDisposable disposable = [];

        public void Dispose() => this.disposable.Dispose();
        #endregion
    }
}
