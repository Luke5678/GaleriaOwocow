using System.Windows;
using System.Windows.Controls;

namespace GaleriaOwocow.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;
            FruitListView.ItemsSource = App.Database.Fruits;
            App.Database.ReloadFruits();
        }

        public static void RefreshView()
        {
            Instance.FruitListView.Items.Refresh();
        }

        private void CreateFruitButton_Click(object sender, RoutedEventArgs e)
        {
            var window = new NewFruitWindow();
            window.Show();
        }

        private void DetailsButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var fruit = button.DataContext as FruitModel;

            var window = new ViewFruitWindow(fruit);
            window.Show();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var fruit = button.DataContext as FruitModel;

            var window = new EditFruitWindow(fruit);
            window.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var fruit = button.DataContext as FruitModel;

            App.Database.DeleteFruit(fruit);
        }
    }
}
