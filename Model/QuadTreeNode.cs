using ProLab2WindowsFormsApp.Helper;
using System.Collections.Generic;
using System.Drawing;
using System;

namespace ProLab2WindowsFormsApp.Model
{
    /// <summary>
    /// QuadTree içerisindeki her bir node'a karşılık gelen sınıf
    /// </summary>
    public sealed class QuadTreeNode
    {
        /// <summary>
        /// Node'un sınırlarını belirten Rectangle
        /// </summary>
        public RectangleF Rectangle { get; }

        /// <summary>
        /// Ekrana çizimde Rectangle'ın rengini tutan sınıf
        /// </summary>
        public Color Color { get; }

        /// <summary>
        /// Node'un alt node'larının tutulduğu liste
        /// </summary>
        private List<QuadTreeNode> nodes;

        /// <summary>
        /// Node'un içerisinde bulunan Item'ların listesi
        /// </summary>
        public List<Item> Items { get; }

        /// <summary>
        /// QuadTreeNode sınıfı için yapılandırıcı method
        /// </summary>
        /// <param name="rectangle">Node'ın sınırlarını belirten rectangle</param>
        public QuadTreeNode(RectangleF rectangle)
        {
            Rectangle = rectangle;
            Color = RandomColorHelper.GetRandomColor();
            nodes = new List<QuadTreeNode>(4);
            Items = new List<Item>();
        }

        /// <summary>
        /// Node'a yada altNode'larından uygun sınırdakine item ekleyen method
        /// </summary>
        /// <param name="yeniItem"></param>
        public void Insert(Item yeniItem)
        {
            //Eğer alt node'lar oluşturulmamış ise;
            if (nodes.Count == 0)
                CreateSubNodes();

            //Alt node listesinde tek tek dönülüyor
            foreach (QuadTreeNode node in nodes)
            {
                //Yeni eklenecek olan Item'ın sınırları için en uygun node aranıyor.
                if (node.Rectangle.Contains(yeniItem.Rectangle))
                {
                    //Eğer uygun bir alt node bulunduysa o node için Insert methodu çağrılıyor.
                    node.Insert(yeniItem);
                    return;
                }
            }

            //Alt Node'lar içinde en uygunu yada en alt'da bulunan'a gelindiğinde
            //onun listesine yeni item ekleniyor.
            Items.Add(yeniItem);
        }

        /// <summary>
        /// Parametre olarak gönderilen sınır içerisindeki node'larda bulunan
        /// elemanların listesini bulan method
        /// </summary>
        /// <param name="area">Elemanların aranacağı sınır</param>
        /// <returns>Bulunan item'lar</returns>
        public List<Item> Search(RectangleF area)
        {
            List<Item> result = new List<Item>();
            //İşaretlenen alanda bu node üzerindeki Item'lardan herhangi biri ile
            //kesişen var ise listeye eklenir.
            foreach (var item in Items)
            {
                if (area.IntersectsWith(item.Rectangle) || area.Contains(item.Rectangle))
                    result.Add(item);
            }

            //Node'un alt node listesinde geziliyor.
            foreach (QuadTreeNode node in nodes)
            {
                //Eğer node'un sınırları tanımlı değilse yada alt node'ları tanımlı değilse
                //bir sonraki bölgeye geç
                if (node.Rectangle.IsEmpty || node.nodes.Count == 0)
                    continue;

                //Eğer işaretlenen alan alt node'un sınırları içerisinde ise
                //alt node için yeni arama başlatılır ve gelen sonuç listeye eklenir.
                if (node.Rectangle.Contains(area))
                {
                    result.AddRange(node.Search(area));
                    break;
                }

                //Eğer işaretlenen alan altNode'un sınırlarını içine alıyor ise
                //altNode'daki bütün elemanlar listeye eklenir.
                if (area.Contains(node.Rectangle))
                {
                    result.AddRange(node.Items);
                    continue;
                }

                //Eğer işaretlenen alan ile altNode sınırının bir kısmı kesişiyor ise 
                //alt node için yeni arama başlatılır ve gelen sonuç listeye eklenir.
                if (node.Rectangle.IntersectsWith(area))
                {
                    result.AddRange(node.Search(area));
                }
            }
            return result;
        }
        
        /// <summary>
        /// Node için Insert methodu çağırıldığında Eğer node'un sınırları içerisinde
        /// 4 tane alt node oluşturulmadıysa bunları oluşturan methoddur.
        /// </summary>
        private void CreateSubNodes()
        {
            //En alt node'un sınırlarını belirtir ve daha alt sınırlar için node oluşturmasını engeller
            if ((Rectangle.Height * Rectangle.Width) <= 10)
                return;

            //Mevcut node'un sınırları içerisinde oluşturulacak olan yeni 4 bölgenin
            //yükseklik ve genişlik değerleri
            float yeniGenislik = (Rectangle.Width / 2f);
            float yeniYukseklik = (Rectangle.Height / 2f);

            //1. Bölgeyi temsil eden node oluşturulup, listeye ekleniyor.
            nodes.Add(new QuadTreeNode(new RectangleF(Rectangle.Location, new SizeF(yeniGenislik, yeniYukseklik))));
            //2. Bölgeyi temsil eden node oluşturulup, listeye ekleniyor.
            nodes.Add(new QuadTreeNode(new RectangleF(new PointF(Rectangle.Left, Rectangle.Top + yeniYukseklik), new SizeF(yeniGenislik, yeniYukseklik))));
            //3. Bölgeyi temsil eden node oluşturulup, listeye ekleniyor.
            nodes.Add(new QuadTreeNode(new RectangleF(new PointF(Rectangle.Left + yeniGenislik, Rectangle.Top), new SizeF(yeniGenislik, yeniYukseklik))));
            //4. Bölgeyi temsil eden node oluşturulup, listeye ekleniyor.
            nodes.Add(new QuadTreeNode(new RectangleF(new PointF(Rectangle.Left + yeniGenislik, Rectangle.Top + yeniYukseklik), new SizeF(yeniGenislik, yeniYukseklik))));
        }

        /// <summary>
        /// Gönderilen action'ını kendisinde ve alt node'ları için
        /// çalıştıran methoddur.
        /// </summary>
        /// <param name="action">Çalıştırılacak olan aksiyon(method)</param>
        public void ForEach(Action<QuadTreeNode> action)
        {
            //Gelen aksiyonu kendi için çalıştırıyor.
            action(this);

            //Gelen aksiyonu alt nodeları için çalıştırıyor.
            foreach (var node in nodes)
                node.ForEach(action);
        }
    }
}
