using BmpToJpg.ViewModels;
using System.Windows;
using System.Windows.Forms;

namespace BmpToJpg
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _mainViewModel;
        public MainWindow(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
            InitializeComponent();

            this.DataContext = _mainViewModel;
        }


        private void Btn_Select_Path(object sender, RoutedEventArgs e)
        {
            _mainViewModel.Btn_SelectFolder();
        }

        private void Btn_Save_Path(object sender, RoutedEventArgs e)
        {
            _mainViewModel.Btn_SaveFolder();
        }

        private void Btn_Convert(object sender, RoutedEventArgs e)
        {
            _mainViewModel.Bmp2Jpg();
        }

        private void TextBox_PreviewTextInput(object sender, System.Windows.Input.TextCompositionEventArgs e)
        {
            _mainViewModel.TextBoxIntCheck(e);
        }
    }
}
