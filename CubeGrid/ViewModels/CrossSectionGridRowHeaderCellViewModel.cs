using CubeGrid.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace CubeGrid.ViewModels
{
    public class CrossSectionGridRowHeaderCellViewModel : BindableBase, IDisposable
    {
        private readonly CrossSectionGridOptions options;

        public ReadOnlyReactivePropertySlim<string> Text { get; }

        public int RowIndex { get; }

        public CrossSectionGridRowHeaderCellViewModel(int rowIndex, CrossSectionGridOptions options)
        {
            this.RowIndex = rowIndex;
            this.options = options;
            this.Text = Observable.CombineLatest(this.options.OffsetX, this.options.OffsetY, this.options.OffsetZ, this.options.Area, Tuple.Create)
                .Select(tuple =>
                {
                    var (offsetX, offsetY, offsetZ, area) = tuple;
                    return area == Static.AreaKind.ZX ? $"{offsetZ + this.RowIndex}" : $"{offsetY + 15 - this.RowIndex}";
                }).ToReadOnlyReactivePropertySlim<string>();
        }

        #region IDisposable
        private readonly CompositeDisposable disposable = [];

        public void Dispose() => this.disposable.Dispose();
        #endregion
    }
}
