using ObservableCollectionSample;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ObservableCollectionImplementSample
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Model
        /// </summary>
        private SampleModels _sampleModels;

        /// <summary>
        /// ViewModel
        /// </summary>
        private SampleViewModels _sampleViewModels;

        public MainWindow()
        {
            InitializeComponent();

            this._sampleModels = new SampleModels(Enumerable.Range(1, 10).Select(x => new SampleModel("太郎" + x.ToString().PadLeft(2, '0'), x + 10, (FoxGenusKind)(x % 6))));
            this._sampleViewModels= new SampleViewModels(this._sampleModels);
            this.DataContext = this._sampleViewModels;
        }

        /// <summary>
        /// 上に移動ボタンをクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            this._sampleModels.Move(this._sampleViewModels.SelectedIndex, this._sampleViewModels.SelectedIndex > 0 ? this._sampleViewModels.SelectedIndex - 1 : 0);
        }

        /// <summary>
        /// 下に移動ボタンをクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            var oldIndex = this._sampleViewModels.SelectedIndex;
            this._sampleModels.Move(oldIndex, oldIndex + 1 < this._sampleViewModels.Count ? oldIndex + 1 : this._sampleViewModels.Count - 1);
        }

        /// <summary>
        /// 削除ボタンをクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Remove_Click(object sender, RoutedEventArgs e)
        {
            this._sampleModels.RemoveAt(this._sampleViewModels.SelectedIndex);
        }

        /// <summary>
        /// 置換ボタンをクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Random_Click(object sender, RoutedEventArgs e)
        {
            if (this._sampleViewModels.SelectedIndex < 0)
            {
                return;
            }
            var ram = new Random();
            int index = this._sampleViewModels.SelectedIndex;
            this._sampleModels[index] = new SampleModel( "new太郎" + ram.Next(20), ram.Next(30, 70), (FoxGenusKind)ram.Next(0, 6));
        }

        /// <summary>
        /// 作成ボタンをクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Create_Click(object sender, RoutedEventArgs e)
        {
            var ram = new Random();
            this._sampleModels.Add(new SampleModel("追加_" + ram.Next(11, 70), ram.Next(1, 99), ((FoxGenusKind)ram.Next(0, 6))));
        }

        /// <summary>
        /// ログクリアボタンをクリック時のイベント
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_LogClear_Click(object sender, RoutedEventArgs e)
        {
            this._sampleModels.LogClear();
        }

        private void Button_Clear_Click(object sender, RoutedEventArgs e)
        {
            this._sampleModels.Clear();
        }
    }
}
