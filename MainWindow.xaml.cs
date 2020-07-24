using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Covid19_Tracker
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainPageFrame.Navigate(new HomePage());
        }

        private void HomeButton_Click(object sender, RoutedEventArgs e)
        {
            MainPageFrame.Navigate(new HomePage());
        }

        private void NavigateToCountryPage()
        {
            if (InternetAvailability.IsInternetAvailable())
            {
                if (Countries.Names.Any(s => s.Equals(CountrySearchBox.Text, StringComparison.OrdinalIgnoreCase)))
                {
                    MainPageFrame.Navigate(new CountryData((string)CountrySearchBox.Text));
                    CountrySearchBox.Text = "";
                    Keyboard.ClearFocus();
                }
                else
                {
                    MainPageFrame.Navigate(new CountryNotFoundErrorPage());
                    CountrySearchBox.Text = "";
                    Keyboard.ClearFocus();
                }
            }
            else
            {
                MainPageFrame.Navigate(new NetworkErrorPage());
            }
            
        }

        private void AutoCompleteTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                NavigateToCountryPage();
            }
        }

        private void CountrySearchButton_Click(object sender, RoutedEventArgs e)
        {
            NavigateToCountryPage();
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            Console.WriteLine("Height: " + Window.ActualHeight);
            Console.WriteLine("Width: " + Window.ActualWidth);
            if (Window.ActualWidth <= 679)
            {
                CountrySearchBox.Width = 150;
            }
            else
            {
                CountrySearchBox.Width = 250;
            }
        }
    }
}

class InternetAvailability
{
    [DllImport("wininet.dll")]
    private extern static bool InternetGetConnectedState(out int description, int reservedValue);

    public static bool IsInternetAvailable()
    {
        int description;
        return InternetGetConnectedState(out description, 0);
    }
}
