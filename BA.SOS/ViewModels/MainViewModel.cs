using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.ServiceModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using BA.SOS.SOSService;


namespace BA.SOS
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public MainViewModel()
        {
            this.ItemsPolicia = new ObservableCollection<ItemViewModel>();
            this.ItemsHospital = new ObservableCollection<ItemViewModel>();
        }

        public ObservableCollection<ItemViewModel> ItemsPolicia { get; private set; }
        public ObservableCollection<ItemViewModel> ItemsHospital { get; private set; }


        private bool _IsDataPoliciaLoaded;

        public bool IsDataPoliciaLoaded
        {
            get { return _IsDataPoliciaLoaded; }
            private set {
                if (value != _IsDataPoliciaLoaded)
                {
                    _IsDataPoliciaLoaded = value;
                    NotifyPropertyChanged("IsDataPoliciaLoaded");
                }
            }
        }

        private bool _IsDataHospitalLoaded;

        public bool IsDataHospitalLoaded
        {
            get { return _IsDataHospitalLoaded; }
            private set {
                if (value != _IsDataHospitalLoaded)
                {
                    _IsDataHospitalLoaded = value;
                    NotifyPropertyChanged("IsDataHospitalLoaded");
                }
            }
        }

        public void LoadPoliciaData(double x, double y)
        {
            this.IsDataPoliciaLoaded = false;

            var client = new Service1Client();

            client.GetDataPoliciasCompleted += new EventHandler<GetDataPoliciasCompletedEventArgs>(client_GetDataPoliciasCompleted);
            client.GetDataPoliciasAsync(x, y, 1);
        }

        void client_GetDataPoliciasCompleted(object sender, GetDataPoliciasCompletedEventArgs e)
        {
            Deployment.Current.Dispatcher.BeginInvoke(() =>
                                                          {
                                                              this.ItemsPolicia.Clear();
                                                              foreach (var item in e.Result)
                                                              {
                                                                  this.ItemsPolicia.Add(item);
                                                              }
                                                          });
            this.IsDataPoliciaLoaded = true;
        }

        public void LoadHospitalData()
        {
            this.ItemsHospital.Add(new ItemViewModel() { Titulo = "hospital uno", Nombre = "hospital uno", Telefono = "5238-7706" });
            this.ItemsHospital.Add(new ItemViewModel() { Titulo = "hospital dos", Nombre = "hospital dos", Telefono = "5238-7706" });
            this.ItemsHospital.Add(new ItemViewModel() { Titulo = "hospital tres", Nombre = "hospital tres", Telefono = "5238-7706" });
            this.ItemsHospital.Add(new ItemViewModel() { Titulo = "hospital cuatro", Nombre = "hospital cuatro", Telefono = "5238-7706" });

            this.IsDataHospitalLoaded = true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (null != handler)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}