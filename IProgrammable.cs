using System;
using System.Collections.Generic;

namespace Chauffage
{
    public interface IProgrammable
    {
        /// <summary>
        /// Obtient la liste des plages de fonctionnement composant le programme
        /// </summary>
        List<Plage> PlagesProgramme { get; }
        /// <summary>
        /// Obtient la plage de fonctionnement en cours (à l'heure courante)
        /// </summary>
        Plage PlageProgrammeCourante { get; }
        /// <summary>
        /// Obtient l'heure du prochain changement de mode
        /// </summary>
        TimeSpan HeureChangementMode { get; }

        /// <summary>
        /// Obtient ou définit si le programme est actif ou non 
        /// </summary>
        bool ProgrammeActif { get; set; }

        /// <summary>
        /// Vérifie que les plages du programme couvrent bien 24h et ne se chevauchent pas
        /// </summary>
        /// <returns>vrai si le programme est valide, faux sinon</returns>
        bool ProgrammeValide();

        /// <summary>
        /// Obtient le mode programmé pour une heure donnée
        /// </summary>
        /// <param name="heure"></param>
        /// <returns>mode programmé</returns>
        int GetModeProgrammé(TimeSpan heure);
    }

    public class Plage : IComparable<Plage>
    {
        public TimeSpan HeureDébut { get; set; }
        public TimeSpan HeureFin { get; set; }
        public int Mode { get; set; }

        public int CompareTo(Plage other)
        {
            return HeureDébut.CompareTo(other.HeureDébut);
        }
    }
}
