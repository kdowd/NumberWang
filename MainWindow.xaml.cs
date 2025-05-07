using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NumberWang.Classes;


namespace NumberWang
{

    public partial class MainWindow : Window
    {
        // to stop compiler complaing make it readonly 
        readonly NumberGame GAME;

        public MainWindow()
        {
            InitializeComponent();
            // window is ready, let's go
            GAME = new NumberGame();
        }

        private void OnCloseApp(object sender, RoutedEventArgs e)
        {
            this.Close();
            //Application.Current.Shutdown();
        }

        private void OnReplay(object sender, RoutedEventArgs e)
        {
            GAME.Replay();

        }
    }
}