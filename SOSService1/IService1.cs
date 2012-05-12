using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOSService1
{
    [ServiceContract]
    public interface IService1
    {
        [OperationContract]
        List<ItemViewModel> GetDataPolicias(double x, double y, int tipo);
    }

    [DataContract]
    public class ItemViewModel
    {
        private string _Titulo;
        [DataMember]
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
               }
            }
        }

        private string _Nombre;
        [DataMember]
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
                }
            }
        }
      
        private string _Telefono;
        [DataMember]
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
                }
            }
        }

        private string _Ubicacion;
        [DataMember]
        public string Ubicacion
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
                }
            }
        }

        private double _Distancia;
        public double Distancia
        {
            get
            {
                return _Distancia;
            }
            set
            {
                if (value != _Distancia)
                {
                    _Distancia = value;
                }
            }
        }
    }
}
