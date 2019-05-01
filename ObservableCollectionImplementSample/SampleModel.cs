using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ObservableCollectionSample
{
    /// <summary>
    /// MVVMパターンのModelに相当するクラス
    /// </summary>
    public class SampleModel
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="name"> 名前 </param>
        /// <param name="age"> 年齢 </param>
        /// <param name="genusKind"> 属の種類 </param>
        public SampleModel(string name, int age, FoxGenusKind genusKind)
        {
            this.Name = name;
            this.Age = age;
            this.GenusKind = genusKind;
        }

        /// <summary>
        /// 名前
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 年齢
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// 属の種類
        /// </summary>
        public FoxGenusKind GenusKind { get; set; }

        /// <summary>
        /// 操作履歴にプロパティ名を表示します。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"Name : {Name}, Age : {Age}, Genus : {GenusKind}";
        }
    }

    /// <summary>
    /// キツネ属の種類
    /// </summary>
    public enum FoxGenusKind
    {
        /// <summary>
        /// キツネ属
        /// </summary>
        Vulpes,
        /// <summary>
        /// オオミミギツネ属
        /// </summary>
        Otocyon,
        /// <summary>
        /// カニクイキツネ属
        /// </summary>
        Cerdocyon,
        /// <summary>
        /// クルペオギツネ属
        /// </summary>
        Lycalopex,
        /// <summary>
        /// フォークランドキツネ属
        /// </summary>
        Dusicyon,
        /// <summary>
        /// ハイイロギツネ属
        /// </summary>
        Urocyon,
    }
}
