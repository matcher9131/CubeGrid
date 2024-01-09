using CubeGrid.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using CubeGrid.Static;

namespace CubeGrid.ViewModels
{
    public class CrossSectionGridViewModel : BindableBase, IDisposable
    {
        private CrossSectionGridOptions options;

        public ReactiveCollection<CrossSectionGridColumnHeaderCellViewModel> ColumnHeaderCells { get; set; }

        public ReactiveCollection<CrossSectionGridRowHeaderCellViewModel> RowHeaderCells { get; set; }

        public ReactiveCollection<CrossSectionGridCellViewModel> Cells { get; }

        public ReadOnlyReactivePropertySlim<string> HorizontalAxisLabel { get; }

        public ReadOnlyReactivePropertySlim<string> VerticalAxisLabel { get; }

        public CrossSectionGridViewModel(CubicData data, CrossSectionGridOptions options)
        {
            this.options = options;
            this.ColumnHeaderCells = [.. Enumerable.Range(0, 16).Select(i => new CrossSectionGridColumnHeaderCellViewModel(i, options))];
            this.RowHeaderCells = [.. Enumerable.Range(0, 16).Select(i => new CrossSectionGridRowHeaderCellViewModel(i, options))];
            this.Cells = [.. Enumerable.Range(0, 16).SelectMany(i => Enumerable.Range(0, 16).Select(j => new CrossSectionGridCellViewModel(i, j, data, options)))];
            this.HorizontalAxisLabel = this.options.Area.Select(area => area switch
                {
                    AreaKind.YZ => "Z",
                    _ => "X"
                }).ToReadOnlyReactivePropertySlim<string>();
            this.VerticalAxisLabel = this.options.Area.Select(area => area switch
                {
                    AreaKind.ZX => "Z",
                    _ => "Y"
                }).ToReadOnlyReactivePropertySlim<string>();
        }

        #region IDisposable
        private readonly CompositeDisposable disposable = [];

        public void Dispose() => this.disposable.Dispose();
        #endregion
    }
}
