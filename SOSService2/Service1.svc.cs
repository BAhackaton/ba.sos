using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace SOSService2
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
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
                               Distancia = x - (z.X.HasValue ? z.X.Value : 1) + (y - (z.Y.HasValue ? z.Y.Value : 1))
                           }).OrderBy(z => z.Distancia)).ToList();

            return result;
        }

        public static double GetDistancia(double x, double y, double x2, double y2)
        {
            double r = Math.Pow((x2 - x),2);
            double r2 = Math.Pow((y2 - y),2);

            return Math.Sqrt(Math.Pow((x2 - x), 2) + Math.Pow((y2 - y), 2));
        }
    }
}
