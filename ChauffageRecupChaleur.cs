using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chauffage
{
    public abstract class ChauffageRecupChaleur : Chauffage
    {
        private Pompe _pompe;

        public Fluide Fluide { get; set; }
        //todo : dans le programme principal, créer un fluide et l'affecter à cette prop
        public double Pression { get; set; }
        public double SurfaceEchange { get; set; }
        public double TemperatureExtMin { get; set; }

        public ChauffageRecupChaleur()
        {
            _pompe = new Pompe();
        }
    }

    public class Pompe
    {
        public decimal DebitMaxi { get; set; }
    }

    public class PompeAChaleur : ChauffageRecupChaleur
    {
        public override decimal CalculerConso(TimeSpan période)
        {
            throw new NotImplementedException();
        }
    }

    public class EchangeurGeothermique : ChauffageRecupChaleur
    {
        public override decimal CalculerConso(TimeSpan période)
        {
            throw new NotImplementedException();
        }
    }
}
