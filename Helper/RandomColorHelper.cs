using System;
using System.Drawing;

namespace ProLab2WindowsFormsApp.Helper
{
    /// <summary>
    /// Rastgele renk üretiminden sorumlu yardımcı sınıftır
    /// </summary>
    public static class RandomColorHelper
    {
        /// <summary>
        /// Random değer üreten nesne
        /// </summary>
        static Random rand;

        /// <summary>
        /// Static yapılandırıcı, türe ilk erişimde 1 defa çalışır
        /// </summary>
        static RandomColorHelper()
        {
            rand = new Random(DateTime.Now.Millisecond);
        }

        /// <summary>
        /// Random reng üreten methoddur.
        /// </summary>
        /// <returns>Renk döner</returns>
        public static Color GetRandomColor()
        {
            return Color.FromArgb(
                255,
                rand.Next(255),
                rand.Next(255),
                rand.Next(255));
        }
    }
}
