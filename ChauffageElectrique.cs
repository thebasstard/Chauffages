using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chauffage
{
    public class ChauffageElectrique : Chauffage
    {
        double IntensitéFusible { get; set; }

        public override decimal CalculerConso(TimeSpan période)
        {
            // Renvoyer simplement le nombre de KWh consommés sur la période
            throw new NotImplementedException();
        }
    }
}
