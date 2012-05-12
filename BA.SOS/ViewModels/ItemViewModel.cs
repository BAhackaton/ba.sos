using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Device.Location;

namespace BA.SOS
{
    public class ItemViewModel : INotifyPropertyChanged
    {
        private string _Titulo;
        public string Titulo
        {
            get
            {
                return _Titulo;
            }
            set
            {
                if (value != _Titulo)
                {
                    _Titulo = value;
                    NotifyPropertyChanged("Titulo");
                }
            }
        }

        private string _Nombre;
        public string Nombre
        {
            get
            {
                return _Nombre;
            }
            set
            {
                if (value != _Nombre)
                {
                    _Nombre = value;
                    NotifyPropertyChanged("Nombre");
                }
            }
        }
      
        private string _Telefono;
        public string Telefono
        {
            get
            {
                return _Telefono;
            }
            set
            {
                if (value != _Telefono)
                {
                    _Telefono = value;
                    NotifyPropertyChanged("Telefono");
                }
            }
        }

        private GeoCoordinate _Ubicacion;
        public GeoCoordinate Ubicacion
        {
            get
            {
                return _Ubicacion;
            }
            set
            {
                if (value != _Ubicacion)
                {
                    _Ubicacion = value;
                    NotifyPropertyChanged("Ubicacion");
                }
            }
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