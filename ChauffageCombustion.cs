using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chauffage
{
    public abstract class ChauffageCombustion : Chauffage
    {
        public Combustible CombustibleCourant { get; set; }
    }

    public class PoeleABois : ChauffageCombustion
    {
        public PoeleABois()
        {
            Modes.Add(new Mode { Id = 1, Libellé = "Allumé" });
        }

        public override decimal CalculerConso(TimeSpan période)
        {
            throw new NotImplementedException();
        }

        public string ViderBacACendres()
        {
            return "Bac à cendre vidé";
        }
    }

    public class ChaudiereAFuel : ChauffageCombustion
    {
        public override decimal CalculerConso(TimeSpan période)
        {
            throw new NotImplementedException();
        }
    }
}
