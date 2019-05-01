using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableCollectionSample
{
    /// <summary>
    /// MVVMパターンのViewModelに相当するクラス
    /// </summary>
    public class SampleViewModel
    {
        /// <summary>
        /// 通知元のモデル
        /// </summary>
        private SampleModel _model;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="model"> 通知元のModel </param>
        public SampleViewModel(SampleModel model)
        {
            this._model = model;
            this.Age = this._model.Age;
            this.Name = this._model.Name;
            this.GenusKind = this._model.GenusKind;
        }

        /// <summary>
        /// 通知用のNameプロパティ
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 通知用のAgeプロパティ
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 属の種類
        /// </summary>
        public FoxGenusKind GenusKind { get; set; }
    }
}
