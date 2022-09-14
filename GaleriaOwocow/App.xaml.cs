using System.Windows;

namespace GaleriaOwocow
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static DatabaseManager Database = new();

        public App()
        {
            Database.CheckDatabase();
        }
    }
}
