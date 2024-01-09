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
using System.Reactive.Subjects;
using System.Windows.Media;
using System.ComponentModel;

namespace CubeGrid.ViewModels
{
    public class CrossSectionGridCellViewModel : BindableBase, IDisposable
    {
        private readonly CubicData data;
        private readonly CrossSectionGridOptions options;

        public int RowIndex { get; }

        public int ColumnIndex { get; }

        public ReadOnlyReactivePropertySlim<Color> CellColor { get; }

        public CrossSectionGridCellViewModel(int rowIndex, int columnIndex, CubicData data, CrossSectionGridOptions options)
        {
            this.RowIndex = rowIndex;
            this.ColumnIndex = columnIndex;
            this.data = data;
            this.options = options;
            this.CellColor = Observable.CombineLatest(this.data.Cells, this.options.OffsetX, this.options.OffsetY, this.options.OffsetZ, this.options.Area, Tuple.Create)
                .Select(tuple =>
                {
                    var (cells, offsetX, offsetY, offsetZ, area) = tuple;
                    return area switch
                    {
                        AreaKind.YZ => cells.Contains(new CubicCell(offsetX, offsetY + 15 - this.RowIndex, offsetZ + this.ColumnIndex)),
                        AreaKind.ZX => cells.Contains(new CubicCell(offsetX + this.ColumnIndex, offsetY, offsetZ + this.RowIndex)),
                        AreaKind.XY => cells.Contains(new CubicCell(offsetX + this.ColumnIndex, offsetY + 15 - this.RowIndex, offsetZ)),
                        _ => throw new InvalidEnumArgumentException()
                    } ? Colors.Red : Colors.Transparent;
                }).ToReadOnlyReactivePropertySlim<Color>();
        }

        #region IDisposable
        private readonly CompositeDisposable disposable = [];

        public void Dispose() => this.disposable.Dispose();
        #endregion
    }
}
