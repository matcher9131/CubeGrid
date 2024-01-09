using CubeGrid.Models;
using Prism.Commands;
using Prism.Mvvm;
using Reactive.Bindings;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CubeGrid.ViewModels
{
    public class DataInputFieldViewModel : BindableBase
    {
        private readonly CubicData data;

        public ReactivePropertySlim<string> RawData { get; }

        public DataInputFieldViewModel(CubicData data)
        {
            this.data = data;
            this.RawData = new(this.data.RawData.Value);
            this.RawData.Subscribe(rawData => { this.data.RawData.Value = rawData; });
        }
    }
}
