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
    /// Interaction logic for CountryData.xaml
    /// </summary>
    public partial class CountryData : Page
    {
        string countryName;

        public CountryData(string SearchCountryName)
        {
            InitializeComponent();
            countryName = SearchCountryName;
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                RootObject covidData = await CovidStatsData.GetCovidStatsData();
                List<CountryWiseData> countryDataList = covidData.Countries;
                List<CountryWiseData> data = countryDataList.Where(country => country.Country.ToUpper().Equals(countryName.ToUpper())).ToList();
                if (data.Count == 1)
                {
                    CountryName.Text = data[0].Country.ToString();
                    ConfirmedCases.Text = string.Format("{0:#,0}", data[0].TotalConfirmed);
                    ProgressIndicatorConf.Visibility = Visibility.Collapsed;
                    FatalCases.Text = string.Format("{0:#,0}", data[0].TotalDeaths);
                    ProgressIndicatorFatal.Visibility = Visibility.Collapsed;
                    RecoveredCases.Text = string.Format("{0:#,0}", data[0].TotalRecovered);
                    ProgressIndicatorRec.Visibility = Visibility.Collapsed;

                    NewConf.Text = string.Format("{0:#,0}", data[0].NewConfirmed);
                    StackPanelConf.Visibility = Visibility.Visible;
                    NewFatal.Text = string.Format("{0:#,0}", data[0].NewDeaths);
                    StackPanelFatal.Visibility = Visibility.Visible;
                    NewRec.Text = string.Format("{0:#,0}", data[0].NewRecovered);
                    StackPanelRec.Visibility = Visibility.Visible;
                }
            }
            catch(Exception ex)
            {
                NavigationService ns = NavigationService.GetNavigationService(this);
                ns.Navigate(new NetworkErrorPage());
                Console.WriteLine(ex);
            }
        }
    }
}
