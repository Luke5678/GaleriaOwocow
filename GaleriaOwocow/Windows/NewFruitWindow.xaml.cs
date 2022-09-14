using System;
using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;

namespace GaleriaOwocow.Windows
{
    /// <summary>
    /// Interaction logic for NewFruitWindow.xaml
    /// </summary>
    public partial class NewFruitWindow : Window
    {
        public NewFruitWindow()
        {
            InitializeComponent();

            NameInput.Text = string.Empty;
            ColorInput.Text = string.Empty;
            DescriptionInput.Text = string.Empty;
            Image.Source = null;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            App.Database.CreateFruit(NameInput.Text, ColorInput.Text, DescriptionInput.Text, (BitmapImage)Image.Source);
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = false;
            openFileDialog.Filter = "Image Files(*.BMP;*.JPG;*.JPEG;*.PNG)|*.BMP;*.JPG;*.JPEG;*.PNG";
            
            if (openFileDialog.ShowDialog() == true)
            {
                var path = openFileDialog.FileName;
                Image.Source = new BitmapImage(new Uri(path));
            }
        }
    }
}
