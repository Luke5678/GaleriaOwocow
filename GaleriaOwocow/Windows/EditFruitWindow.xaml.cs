using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace GaleriaOwocow.Windows
{
    /// <summary>
    /// Interaction logic for EditFruitWindow.xaml
    /// </summary>
    public partial class EditFruitWindow : Window
    {
        private readonly FruitModel _fruit;

        public EditFruitWindow(FruitModel fruit)
        {
            InitializeComponent();

            _fruit = fruit;
            NameInput.Text = fruit.Name;
            ColorInput.Text = fruit.Color;
            DescriptionInput.Text = fruit.Description;
            Image.Source = fruit.Bitmap;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _fruit.Name = NameInput.Text;
            _fruit.Color = ColorInput.Text;
            _fruit.Description = DescriptionInput.Text;

            App.Database.UpdateFruit(_fruit, (BitmapImage)Image.Source);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog
            {
                Multiselect = false,
                Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.PNG)|*.BMP;*.JPG;*.JPEG;*.PNG"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                var path = openFileDialog.FileName;
                Image.Source = new BitmapImage(new Uri(path));
            }
        }
    }
}
