using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Mines.View
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettupPage : Page
    {
        public SettupPage()
        {
            _showSettings = false;
            this.InitializeComponent();
            SettingsPanel.Visibility = Visibility.Collapsed;
        }

        private void StartButton_OnClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MainPage));
        }

        private bool _showSettings;
        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _showSettings = !_showSettings;
            SettingsPanel.Visibility = _showSettings ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
