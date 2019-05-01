using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ObservableCollectionSample
{
    /// <summary>
    /// ObservableCollectionを拡張したクラス
    /// </summary>
    public class SampleModels : ObservableCollection<SampleModel>
    {
        /// <summary>
        /// 操作履歴
        /// </summary>
        private string _dispalyEventLog;

        /// <summary>
        /// SelectedIndex用のバッキングフィールド
        /// </summary>
        private int _selectedIndex;

        /// <summary>
        /// 操作履歴を保持するインスタンス
        /// </summary>
        private StringBuilder _log = new StringBuilder();

        /// <summary>
        /// 継承したObservableCollectionの実装
        /// </summary>
        public SampleModels() : base()
        {
        }

        /// <summary>
        /// 継承したObservableCollectionの実装
        /// </summary>
        public SampleModels(List<SampleModel> list) : base(list)
        {
            this._log.AppendLine(string.Join(Environment.NewLine, list.Select(x => $"model : {x.Name}が追加されました。").ToList()));
            this.DisplayEventLog = this._log.ToString();
        }

        /// <summary>
        /// 継承したObservableCollectionの実装
        /// </summary>
        public SampleModels(IEnumerable<SampleModel> collection) : base(collection)
        {
            this._log.AppendLine(string.Join(Environment.NewLine, collection.Select(x => $"model : {x.Name}が追加されました。").ToList()));
            this.DisplayEventLog = this._log.ToString();
        }

        /// <summary>
        /// 選択されているIndexを通知するプロパティ
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            private set
            {
                _selectedIndex = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(SelectedIndex)));
            }
        }

        /// <summary>
        /// 操作履歴を通知するプロパティ
        /// </summary>
        public string DisplayEventLog
        {
            get { return _dispalyEventLog; }
            private set
            {
                _dispalyEventLog = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(DisplayEventLog)));
            }
        }


        /// <summary>
        /// 操作履歴をクリアします。
        /// </summary>
        public void LogClear()
        {
            this._log.Clear();
            this.DisplayEventLog = this._log.ToString();
        }

        /// <summary>
        /// 選択されているIndexを変更します。
        /// </summary>
        /// <param name="index"> 変更後のIndex </param>
        public void ChangeSelectedIndex(int index)
        {
            if (index < 0)
            {
                this.SelectedIndex = 0;
                this._log.AppendLine($"{index}番目の要素がコレクションの範囲外だったので0に設定しました。");
            }
            else if (index >= this.Count)
            {
                this.SelectedIndex = this.Count - 1;
                this._log.AppendLine($"{index}番目の要素がコレクションの範囲外だったので{this.Count - 1}を設定しました。");
            }
            else
            {
                this.SelectedIndex = index;
                this._log.AppendLine($"{index}番目の要素が選択されました。");
            }
            this.DisplayEventLog = this._log.ToString();
        }

        /// <summary>
        /// 基底クラスのメソッドに操作履歴処理を追加します。
        /// </summary>
        /// <param name="index"> 追加先のindex </param>
        /// <param name="item"> 追加するインスタンス </param>
        protected override void InsertItem(int index, SampleModel item)
        {
            base.InsertItem(index, item);
            this._log.AppendLine($"{index}番目に{item}が追加されました。");
            this.DisplayEventLog = this._log.ToString();
        }

        /// <summary>
        /// 基底クラスのメソッドに操作履歴処理を追加します。
        /// このメソッドは、RemoveAt()メソッド等から呼ばれます。
        /// </summary>
        /// <param name="index"> 削除するindex </param>
        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            this._log.AppendLine($"{index}番目のオブジェクトが削除されました。");
            this.DisplayEventLog = this._log.ToString();
        }

        /// <summary>
        /// 基底クラスのメソッドに操作履歴処理を追加します。
        /// このメソッドは、Clear()メソッドからで呼ばれます。
        /// </summary>
        protected override void ClearItems()
        {
            base.ClearItems();
            this._log.AppendLine($"コレクションがクリアされました。");
            this.DisplayEventLog = this._log.ToString();
        }

        protected override void SetItem(int index, SampleModel item)
        {
            base.SetItem(index, item);
            this._log.AppendLine($"{index}番目のオブジェクトが{item}に置換されました。");
            this.DisplayEventLog = this._log.ToString();
        }

        protected override void MoveItem(int oldIndex, int newIndex)
        {
            base.MoveItem(oldIndex, newIndex);
            this._log.AppendLine($"{oldIndex}番目から{newIndex}番目へ移動しました。");
            this.DisplayEventLog = this._log.ToString();
        }
    }
}
