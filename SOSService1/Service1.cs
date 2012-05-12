using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace SOSService1
{
    public class Service1 : IService1
    {
        public List<ItemViewModel> GetDataPolicias(double x, double y, int tipo)
        {
            var context = new dbBASOSEntities();

            var result = ((from z in context.Lugares
                           where z.TipoID == tipo
                         select new ItemViewModel()
                                    {
                                        Nombre = z.Nombre,
                                        Telefono = z.Telefono,
                                        Titulo = z.Nombre,
                                        Ubicacion = z.Comuna,
                                        Distancia = GetDistancia(x, y, z.X.HasValue ? z.X.Value : 1, z.Y.HasValue ? z.Y.Value : 1)
                                    }).OrderBy(z => z.Distancia)).ToList();

            return result;
        }

        private double GetDistancia(double x, double y, double x2, double y2)
        {
            double r = (x2 - x) * (x2 - x);
            double r2 = (y2 - y) * (y2 - y);

            return Math.Sqrt(r + r2);
        }
    }
}
