using System;
using System.Windows.Forms;
using ProLab2WindowsFormsApp.Model;
using System.Drawing;
using System.Collections.Generic;

namespace ProLab2WindowsFormsApp
{
    public partial class FrmMain : Form
    {
        int sayac = 0;
        /// <summary>
        /// QuadTree'yi modelleyen obje
        /// </summary>
        private QuadTree quadTree;

        /// <summary>
        /// Fare sağ tuşu ile işaretlemeye başlanan ilk nokta
        /// </summary>
        Point startPoint;

        /// <summary>
        /// Fare sağ tuşu ile işaretlenen alan
        /// </summary>
        RectangleF selectionRect;

        /// <summary>
        /// Sağ tuş ile işaretlenen alan içerisindeki item'lar
        /// </summary>
        List<Item> selectedItems;

        /// <summary>
        /// Ekrana çizim yapan nesne
        /// </summary>
        Graphics Graphics; 

        /// <summary>
        /// FrmMain sınıfı için yapılandırı method
        /// </summary>
        public FrmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Ekran yüklendikten sonra çağırılacak olan method.
        /// </summary>
        private void FrmMain_Load(object sender, EventArgs e)
        {
            //Ekran yüklendikten sonra sayfa içerisindeki alan için quadTree oluşturulur.
            quadTree = new QuadTree(ClientRectangle);
        }

        /// <summary>
        /// Ekrana yeni çizim yapılırken çağrılacak olan method.
        /// </summary>
        private void FrmMain_Paint(object sender, PaintEventArgs e)
        {
            Graphics = e.Graphics;
            //Quad Tree içerisinde bulunan bütün node'larda çalıştırılacak olan method gönderilir.
            quadTree.ForEach(new Action<QuadTreeNode>(DrawNode));

            //Sağ tuş ile seçim yapılan alan boş değil ise
            if (!selectionRect.IsEmpty)
            {
                //Seçim yapılan alanı göstermek için sarı ve şeffaf bir rectangle çizilir
                using (Brush b = new SolidBrush(Color.FromArgb(128, Color.Yellow)))
                    e.Graphics.FillRectangle(b, Rectangle.Round(selectionRect));
            }

            //Seçim yapılan alan için QuadTree içerisinde yapılan arama sonucunda bulunan Item var ise;
            if (selectedItems != null)
            {
                //Herbir item tek tek gezilir
                foreach (Item obj in selectedItems)
                {
                    //Item içerisindeki sınırı içine alan kırmızı bir çember ekrana cizilir
                    Rectangle selectedRect = Rectangle.Round(obj.Rectangle);
                    selectedRect.Inflate(1, 1);
                    using (Pen p = new Pen(Color.Red, 2))
                        e.Graphics.DrawEllipse(p, selectedRect);
                }
            }
        }

        private void DrawNode(QuadTreeNode node)
        {
            //Eğer node içerisinde item bulunuyor ise
            if (node.Items != null)
            {
                //Bulunan herbir item tek tek gezilir
                foreach (Item item in node.Items)
                {
                    //Item içerisindeki sınırı içine alan bir çember ekrana cizilir
                    using (Brush b = new SolidBrush(item.Color))
                        Graphics.FillEllipse(b, Rectangle.Round(item.Rectangle));
                }
            }
            //Node'un sınırlarını belirten rectangle'ı ekrana çizer
            using (Pen p = new Pen(node.Color))
            {
                Rectangle inside = Rectangle.Round(node.Rectangle);
                inside.Inflate(-1, -1);
                Graphics.DrawRectangle(p, inside);
            }
        }

        /// <summary>
        /// Ekranın herhangi bir alanında fare buttonu serbest bırakıldığında çağırılacak olan method.
        /// </summary>
        private void FrmMain_MouseUp(object sender, MouseEventArgs e)
        {
            //Fare üzerindeki sağ tuş serbest bırakıldıkta sonra quadTree üzerinde seçilen alan
            //için arama yapılır.
            if (e.Button == MouseButtons.Right)
            {
                selectedItems = quadTree.Search(selectionRect);
            }
            //Sol tuş ile tıklanan nokta için quadTree üzerinde ekleme yapılır
            else
            {
                quadTree.Insert(new Item(e.Location));
            }

            Invalidate();
        }

        /// <summary>
        /// Ekranın herhangi bir alanında fare buttonu basıldığında çağırılacak olan method.
        /// </summary>
        private void FrmMain_MouseDown(object sender, MouseEventArgs e)
        {
            //Sağ tuşa basılmasına hangi noktada başlandığını tespit eder
            if (e.Button == MouseButtons.Right)
            {
                startPoint = e.Location;
            }
        }

        /// <summary>
        /// Ekranın herhangi bir alanında fare hareket ettirildiğinde çağırılacak olan method.
        /// </summary>
        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            //Sağ tuşa basılıyken fare hareket ettirildiğinde;
            if (e.Button == MouseButtons.Right)
            {
                //Seçim yapılan alanı oluşturur
                selectionRect = RectangleF.FromLTRB(
                    Math.Min(e.Location.X, startPoint.X),
                    Math.Min(e.Location.Y, startPoint.Y),
                    Math.Max(e.Location.X, startPoint.X),
                    Math.Max(e.Location.Y, startPoint.Y));

                Invalidate();
            }
        }
    }
}
