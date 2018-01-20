using System;

namespace Chauffage
{
    class Program
    {
        static void Main(string[] args)
        {
            //--------------------------------------------------------
            // Tests de l'implémentation  des classes

            var chaudiere = new ChaudiereAFuel();
            chaudiere.IntervalleMaintenances = new TimeSpan(365, 0, 0, 0);
            chaudiere.Reviser(DateTime.Today.AddDays(-365));
            Console.WriteLine("Prochaine révision le {0}", chaudiere.ProchaineRevision); // Doit afficher la date du jour

            Combustible fuel = new Combustible
            {
                Code = "F0",
                Libelle = "Fuel domestique",
                Categorie = CategoriesCombustible.Fuel,
                Unite = Unites.Litre,
                MasseVolumique = 0.845m,
                CapaciteCalorifique = 11.86m
            };
            chaudiere.CombustibleCourant = fuel;

            //--------------------------------------------------------
            // Test de l'implémentation de l'interface IProgrammable

            chaudiere.Modes.Add(new Mode { Id = 1, Libellé = "Eco" });
            chaudiere.Modes.Add(new Mode { Id = 2, Libellé = "Confort" });
            // Le mode 0 Eteint est défini dans le constructeur de Chauffage

            Plage p1 = new Plage
            {
                HeureDébut = new TimeSpan(0, 0, 0),
                HeureFin = new TimeSpan(4, 59, 0),
                Mode = 0
            };

            Plage p2 = new Plage
            {
                HeureDébut = new TimeSpan(5, 0, 0),
                HeureFin = new TimeSpan(7, 59, 0),
                Mode = 1
            };

            Plage p3 = new Plage
            {
                HeureDébut = new TimeSpan(8, 0, 0),
                HeureFin = new TimeSpan(23, 59, 0),
                Mode = 2
            };

            chaudiere.PlagesProgramme.Add(p1);
            chaudiere.PlagesProgramme.Add(p2);
            chaudiere.PlagesProgramme.Add(p3);

            TimeSpan[] horaires = new TimeSpan[3];
            horaires[0] = new TimeSpan(0, 15, 58);
            horaires[1] = new TimeSpan(5, 28, 20);
            horaires[2] = new TimeSpan(13, 23, 45);
            
            foreach (var h in horaires)
                Console.WriteLine("Mode programmé pour {0} : {1}", h, chaudiere.GetModeProgrammé(h));

            Console.WriteLine("Plage de programme courante : {0} - {1}",
                chaudiere.PlageProgrammeCourante.HeureDébut,
                chaudiere.PlageProgrammeCourante.HeureFin);

            try
            {
                chaudiere.ProgrammeActif = true;
                // Changer les plages p1 à p3 ci-dessus pour que le programme soit incomplet
                // et vérifier que cela lève une exception

                chaudiere.ModeCourant = 2; // pas autorisé si le programme est actif
                // Essayer également d'affecter une valeur qui n'est pas dans la liste des modes
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadKey();
        }
    }
}
