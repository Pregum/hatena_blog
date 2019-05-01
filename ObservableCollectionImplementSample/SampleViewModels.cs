using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace ObservableCollectionSample
{
    public class SampleViewModels : INotifyPropertyChanged
    {
        /// <summary>
        /// Model
        /// </summary>
        private SampleModels _models;

        /// <summary>
        /// ViewModel
        /// </summary>
        private ObservableCollection<SampleViewModel> _viewModels;

        /// <summary>
        /// Modelからの操作履歴の文字列を受け取るフィールド
        /// </summary>
        private string _viewModelLog;

        /// <summary>
        /// View側で選択された文字列をModelに通知に受け渡すフィールドです。
        /// </summary>
        private int _selectedIndex;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampleViewModels()
        {
            this._viewModels = new ObservableCollection<SampleViewModel>();
            this.ViewModels = new ReadOnlyObservableCollection<SampleViewModel>(this._viewModels);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="models">SampleModelのコレクション</param>
        public SampleViewModels(SampleModels models)
        {
            this._models = models;
            this._viewModels = new ObservableCollection<SampleViewModel>();
            foreach (var model in models)
            {
                var sampleViewModel = new SampleViewModel(model);
                this._viewModels.Add(sampleViewModel);
            }

            this.ViewModels = new ReadOnlyObservableCollection<SampleViewModel>(this._viewModels);
            this._models.CollectionChanged += this.Models_CollectionChanged;
            ((INotifyPropertyChanged)this._models).PropertyChanged += this.SampleViewModels_PropertyChanged;
        }

        /// <summary>
        /// 通知用イベント
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Viewにバインドするプロパティ
        /// </summary>
        public ReadOnlyObservableCollection<SampleViewModel> ViewModels { get; }

        /// <summary>
        /// 操作履歴にバインドするプロパティ
        /// </summary>
        public string ViewModelLog
        {
            get { return _viewModelLog; }
            set
            {
                _viewModelLog = value;
                this.OnPropertyChanged();
            }
        }

        /// <summary>
        /// ViewのListBoxのSelectedIndex属性にバインドするプロパティ
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set { this._models.ChangeSelectedIndex(value); }
        }

        /// <summary>
        /// コレクションのアイテムの総数
        /// </summary>
        public int Count
        {
            get { return this._viewModels.Count; }
        }

        /// <summary>
        /// Viewに変更されたプロパティを通知します
        /// </summary>
        /// <param name="propertyName"> 変更されたプロパティ名 </param>
        private void OnPropertyChanged([CallerMemberName]string propertyName = "")
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// SampleModelsから受け取ったプロパティの変更通知を反映させます。
        /// </summary>
        /// <param name="sender"> SampleModels </param>
        /// <param name="e"> 変更されたプロパティ名 </param>
        private void SampleViewModels_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(this._models.DisplayEventLog):
                    this.ViewModelLog = ((SampleModels)sender).DisplayEventLog;
                    break;
                case nameof(this._models.SelectedIndex):
                    this._selectedIndex = ((SampleModels)sender).SelectedIndex;
                    this.OnPropertyChanged(nameof(this.SelectedIndex));
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// SampleModelsから受け取ったコレクションの変更通知を反映させます。
        /// </summary>
        /// <param name="sender"> SampleModels </param>
        /// <param name="e"> コレクションの変更情報 </param>
        private void Models_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    this._viewModels.Insert(e.NewStartingIndex, new SampleViewModel((SampleModel)e.NewItems[0]));
                    break;
                case NotifyCollectionChangedAction.Remove:
                    this._viewModels.RemoveAt(e.OldStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Replace:
                    var old = this._viewModels[e.OldStartingIndex];
                    this._viewModels[e.OldStartingIndex] = new SampleViewModel((SampleModel)e.NewItems[0]);
                    break;
                case NotifyCollectionChangedAction.Move:
                    var viewModel = this._viewModels[e.OldStartingIndex];
                    this._viewModels.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    this._viewModels.Clear();
                    break;
                default:
                    break;
            }
        }
    }
}
