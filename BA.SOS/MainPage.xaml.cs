using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using BA.SOS.SOSService;
using Microsoft.Phone.Controls;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Tasks;

namespace BA.SOS
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            map1.CredentialsProvider = new ApplicationIdCredentialsProvider("Am3HlOgSn8UMFEl4xLC0lBQ-eidgqD1psvMMbg59N_0sxjxQPccgr9bhtwfdq-Yi");

            // Set the data context of the listbox control to the sample data
            DataContext = App.ViewModel;
            this.Loaded += new RoutedEventHandler(MainPage_Loaded);
            App.ViewModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ViewModel_PropertyChanged);
        }

        void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "IsDataPoliciaLoaded")
            {
                if (App.ViewModel.IsDataPoliciaLoaded)
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        pLoading.Visibility = Visibility.Collapsed;
                    });
                else
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        pLoading.Visibility = Visibility.Visible;
                    });
            }
        }

        private GeoCoordinateWatcher loc = null;

        // Load data for the ViewModel Items
        private void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            LocalizameAhora();
        }

        private void btLocalizame_Click(object sender, RoutedEventArgs e)
        {
            LocalizameAhora();
        }

        private void LocalizameAhora()
        {
            if (loc == null)
            {
                loc = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
                loc.StatusChanged += loc_StatusChanged;
            }
            if (loc.Status == GeoPositionStatus.Disabled)
            {
                loc.StatusChanged -= loc_StatusChanged;
                MessageBox.Show("Location services must be enabled on your phone.");
                return;
            }
            loc.Start();
        }

        void loc_StatusChanged(object sender, GeoPositionStatusChangedEventArgs e)
        {
            if (e.Status == GeoPositionStatus.Ready)
            {
                Pushpin p = new Pushpin();
                p.Template = this.Resources["pinMyLoc"] as ControlTemplate;
                p.Location = loc.Position.Location;
                mapControl.Items.Add(p);
                map1.SetView(loc.Position.Location, 17.0);
                loc.Stop();
                LoadData();
            }
        }

        private void LoadData ()
        {
            App.ViewModel.LoadPoliciaData(loc.Position.Location.Latitude, loc.Position.Location.Longitude);
            App.ViewModel.LoadHospitalData();
        }

        private void lbPolicia_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lbPolicia.SelectedIndex;
            if (index == -1)
                return;

            ItemViewModel unItem = App.ViewModel.ItemsPolicia[index];

            PhoneCallTask pct = new PhoneCallTask();  
            pct.DisplayName = unItem.Nombre;  
            pct.PhoneNumber = unItem.Telefono;  
            pct.Show();

            lbPolicia.SelectedIndex = -1;
        }

        private void lbHospitales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = lbHospitales.SelectedIndex;
            if (index == -1)
                return;

            ItemViewModel unItem = App.ViewModel.ItemsPolicia[index];

            PhoneCallTask pct = new PhoneCallTask();
            pct.DisplayName = unItem.Nombre;
            pct.PhoneNumber = unItem.Telefono;
            pct.Show();

            lbHospitales.SelectedIndex = -1;
        }

        private void bt911_Click(object sender, RoutedEventArgs e)
        {
            PhoneCallTask pct = new PhoneCallTask();
            pct.DisplayName = "911";
            pct.PhoneNumber = "911";
            pct.Show();
        }
    }
}