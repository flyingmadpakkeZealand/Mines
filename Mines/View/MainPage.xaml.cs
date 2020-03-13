using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Mines.ViewModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mines.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage gamePageInstance;
        public MainPage()
        {
            this.InitializeComponent();
            gamePageInstance = this;
        }

        private void UIElement_OnRightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            GameMapVM theNode = ((Button) sender).CommandParameter as GameMapVM;
            theNode.IsMarked = true;
        }
    }
}
