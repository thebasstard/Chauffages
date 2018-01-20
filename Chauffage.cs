using System;
using System.Collections.Generic;

namespace Chauffage
{
    public abstract class Chauffage : IProgrammable
    {
        private DateTime _derniereRevision;
        private List<Plage> _plagesProgramme;
        private List<Mode> _modes;
        private bool _programmeActif;
        private int _modeCourant;

        #region propriétés
        public List<Mode> Modes {
        	get { return _modes; }
        }
        public int ModeCourant {
            get { return _modeCourant; }
            set
            {
                if (_programmeActif)
                    throw new InvalidOperationException("Le mode courant ne peut pas être affecté lorsqu'un programme est actif");
                else if (value < 0 || value > Modes.Count)
                    throw new ArgumentOutOfRangeException("La valeur du mode est en dehors des modes prédéfinis pour ce chauffage");
                else
                    _modeCourant = value;
            }
        }
        public TimeSpan IntervalleMaintenances { get; set; }
        public DateTime ProchaineRevision
        {
            get
            {
                if (IntervalleMaintenances < TimeSpan.MaxValue)
                    return _derniereRevision + IntervalleMaintenances;
                else
                    return DateTime.MaxValue;
            }
        }
        public int PuissanceMaxi { get; set; }
        public decimal Rendement { get; set; }
        public virtual double RéglageIntensité { get; set; }
        #endregion

        public Chauffage()
        {
            _modes = new List<Mode>();
            _modes.Add(new Mode { Id = 0, Libellé = "Eteint" });
            IntervalleMaintenances = TimeSpan.MaxValue;

            _plagesProgramme = new List<Plage>();
        }

        public virtual void Reviser(DateTime dateRévision)
        {
            // On réinitialise la date de dernière révision
            _derniereRevision = dateRévision;
        }

        // Méthodes abstraites
        public abstract decimal CalculerConso(TimeSpan période);

        #region Implémentation de l'interface IProgrammable
 
        public List<Plage> PlagesProgramme
        {
            get { return _plagesProgramme; }
        }

        public Plage PlageProgrammeCourante
        {
            get
            {
                Plage p = GetPlageProgramme(DateTime.Now.TimeOfDay);

                if (p != null)
                    return p;
                else
                    throw new ArgumentOutOfRangeException(DateTime.Now.TimeOfDay.ToString(),
                    "Cette heure n'est couverte par aucune plage du programme");
            }
        }

        // recherche la plage de programme qui couvre une heure donnée
        private Plage GetPlageProgramme(TimeSpan heure)
        {
            Plage res = null;

            foreach (var p in _plagesProgramme)
            {
                if (heure >= p.HeureDébut && heure <= p.HeureFin)
                {
                    res = p;
                    break;
                }
            }

            return res;
        }

        public TimeSpan HeureChangementMode
        {
            get
            {
                TimeSpan ts = PlageProgrammeCourante.HeureFin;
                return new TimeSpan(ts.Hours, ts.Minutes + 1, 0);
            }
        }

        public bool ProgrammeActif
        {
            get { return _programmeActif; }

            set
            {
                if (value && !ProgrammeValide())
                    throw new InvalidOperationException("Le programme n'est pas valide");

                _programmeActif = value;
            }
        }

        public bool ProgrammeValide()
        {
            // Trie les plages du programme selon leurs heures de début
            _plagesProgramme.Sort();

            TimeSpan intervalle;

            // Vérifie que les plages sont bien contigües
            for (int i=0; i<_plagesProgramme.Count; i++)
            {
                // Met à 0 les secondes des heures de début et de fin (système programmable à la minute près)
                TimeSpan hdeb = _plagesProgramme[i].HeureDébut;
                _plagesProgramme[i].HeureDébut = new TimeSpan(hdeb.Hours, hdeb.Minutes, 0);

                TimeSpan hfin = _plagesProgramme[i].HeureFin;
                _plagesProgramme[i].HeureFin = new TimeSpan(hfin.Hours, hfin.Minutes, 0);

                if (i == 0) continue;
                // Si l'intervalle entre l'heure de début de la plage courante et l'heure de fin
                // de la plage précédente est >1 min, on renvoie faux.
                intervalle = _plagesProgramme[i].HeureDébut - _plagesProgramme[i - 1].HeureFin;
                if (Math.Abs(intervalle.TotalMinutes) > 1) return false;                    
            }

            // Vérifie si le programme est complet, c'est à dire couvre bien 24h
            // Il faut vérifier si l'intervalle entre l'heure de fin de la dernière plage et
            // l'heure de début de la première plage est >= 23h59 (=1439 min)
            intervalle = _plagesProgramme[_plagesProgramme.Count - 1].HeureFin - _plagesProgramme[0].HeureDébut;
            if (Math.Abs(intervalle.TotalMinutes) < 1439) return false;

            // Autre solution pour vérifier que les plages
            // de programmation ne se chevauchent pas et couvrent les 24h 
            /*TimeSpan heureFin = PlagesProgramme[PlagesProgramme.Count - 1].HeureFin;
            foreach (var plage in PlagesProgramme)
            {
                if ((plage.HeureDébut.TotalMinutes == 0 && heureFin.TotalMinutes == 1439) ||
                    (plage.HeureDébut - new TimeSpan(0, 1, 0) == heureFin && heureFin.TotalMinutes != 1439))
                    heureFin = plage.HeureFin;
                else
                    return false;
            }*/

            return true;
        }

        public int GetModeProgrammé(TimeSpan heure)
        {
            Plage p = GetPlageProgramme(heure);

            if (p != null)
                return p.Mode;
            else
                throw new ArgumentOutOfRangeException(heure.ToString(),
                "Cette heure n'est couverte par aucune plage du programme");
        }

        #endregion
    }

    public class Mode
    {
        public int Id { get; set; }
        public string Libellé { get; set; }
    }
}
