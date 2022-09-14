using System.Windows;

namespace GaleriaOwocow.Windows
{
    /// <summary>
    /// Interaction logic for ViewFruitWindow.xaml
    /// </summary>
    public partial class ViewFruitWindow : Window
    {
        public ViewFruitWindow(FruitModel fruit)
        {
            InitializeComponent();

            Name.Content = fruit.Name;
            Color.Text = fruit.Color;
            Description.Text = fruit.Description;
            Image.Source = fruit.Bitmap;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
