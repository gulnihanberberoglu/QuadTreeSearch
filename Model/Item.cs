using ProLab2WindowsFormsApp.Helper;
using System.Drawing;

namespace ProLab2WindowsFormsApp.Model
{
    /// <summary>
    /// QuadTreeNode içerisinde taşınacak olan sınıftır.
    /// </summary>
    public class Item
    {
        /// <summary>
        /// Ekranda oluşturulacak olan çember için 
        /// boyut referansı
        /// </summary>
        public RectangleF Rectangle { get; }

        /// <summary>
        /// Ekranda oluşturulacak olan çemberin rengi
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Item sınıfı için yapılandırıcı methodu
        /// </summary>
        /// <param name="p">Ekranda tıklanan noktanın konum bilgisi</param>
        public Item(Point p)
        {
            Rectangle = new RectangleF(p.X, p.Y, 20, 20);
            Color = RandomColorHelper.GetRandomColor();
        }
    }
}
