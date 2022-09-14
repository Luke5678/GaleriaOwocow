using System.Windows.Media.Imaging;

namespace GaleriaOwocow
{
    public class FruitModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }

        public BitmapImage Bitmap => DatabaseManager.BytesToBitmap(Image);
    }
}
