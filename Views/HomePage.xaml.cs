using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }
        

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RootObject covidData = await CovidStatsData.GetCovidStatsData();
                ConfirmedCases.Text = string.Format("{0:#,0}", covidData.Global.TotalConfirmed);
                ProgressIndicatorConf.Visibility = Visibility.Collapsed;
                FatalCases.Text = string.Format("{0:#,0}", covidData.Global.TotalDeaths);
                ProgressIndicatorFatal.Visibility = Visibility.Collapsed;
                RecoveredCases.Text = string.Format("{0:#,0}", covidData.Global.TotalRecovered);
                ProgressIndicatorRec.Visibility = Visibility.Collapsed;
            }
            catch(Exception ex)
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new NetworkErrorPage());
                Console.WriteLine(ex);
            }

            /* List<CountryData> countryDataList = covidData.Countries; */
        }
    }
}
