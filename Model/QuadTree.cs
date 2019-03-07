using System;
using System.Collections.Generic;
using System.Drawing;

namespace ProLab2WindowsFormsApp.Model
{
    /// <summary>
    /// QuadTree'i algoritmasını modelleyen sınıf
    /// </summary>
    public sealed class QuadTree
    {
        /// <summary>
        /// QuadTree'nin modellediği alanın sınırlarını belirtir
        /// </summary>
        public RectangleF Rectangle { get; }

        /// <summary>
        /// QuadTree'nin root node'u
        /// </summary>
        public QuadTreeNode Root { get; }

        /// <summary>
        /// QuadTree sınıfı için yapılandırıcı method
        /// </summary>
        /// <param name="rectangle">QuadTree'nin modellediği alanın sınırları</param>
        public QuadTree(RectangleF rectangle)
        {
            Rectangle = rectangle;
            Root = new QuadTreeNode(Rectangle);
        }

        /// <summary>
        /// Root node'dan başlayarak alt node'larda dahil olmak üzere
        /// yeni eklenecek item için uygun node'u bulan method
        /// </summary>
        /// <param name="item"></param>
        public void Insert(Item item)
        {
            Root.Insert(item);
        }

        /// <summary>
        /// Parametre olarak gönderilen sınır içerisindeki node'larda bulunan
        /// elemanların listesini bulan method
        /// </summary>
        /// <param name="area">Elemanların aranacağı sınır</param>
        /// <returns>Bulunan item'lar</returns>
        public List<Item> Search(RectangleF area)
        {
            return Root.Search(area);
        }

        /// <summary>
        /// Gönderilen action'ını kendisinde ve alt node'ları için
        /// çalıştıran methoddur.
        /// </summary>
        /// <param name="action">Çalıştırılacak olan aksiyon(method)</param>
        public void ForEach(Action<QuadTreeNode> action)
        {
            Root.ForEach(action);
        }
    }
}
