using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chauffage
{
    public enum CategoriesCombustible { Fuel, Gaz, Bois, Composite }
    public enum Unites { Litre, Metre3, Stere, Kg }

    /// <summary>
    /// Entité POCO modélisant un combustible
    /// </summary>
    public class Combustible
    {
        public decimal CapaciteCalorifique { get; set; }
        public string Code { get; set; }
        public string Libelle { get; set; }
        public decimal MasseVolumique { get; set; }
        public Unites Unite { get; set; }
        public CategoriesCombustible Categorie { get; set; }
    }

    public class Fluide
    {
        public decimal ConductiviteThermique { get; set; }
        public string Nom { get; set; }
        public decimal Viscosite { get; set; }
    }
}
