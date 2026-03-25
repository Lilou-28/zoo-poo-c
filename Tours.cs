class Tour
{
    public int nbTours { get; private set; }
    public Zoo Zoo { get; }
    private static readonly Random _random = new Random();
    private readonly List<Maladie> _maladies;
    private string mois = "";
    public bool saison_haute { get; private set; }
    public Tour(Zoo zoo)
    {
        Zoo = zoo;
        nbTours = 0;
        _maladies = new List<Maladie>
        {
            new Mal_Tigre(),
            new Mal_Aigle(),
            new Mal_Poule()
        };
    }
    public void ProchainTour()
    {
        nbTours++;
        VerifierMois();
        Console.WriteLine($"\n--- Tour {nbTours} ({mois}) ---");

        if (Zoo.HabitatsZoo.Count == 0)
        {
            Console.WriteLine("Aucun habitat: aucun evenement ce tour.");
            return;
        }

        Habitat habitat = Zoo.HabitatsZoo[_random.Next(Zoo.HabitatsZoo.Count)];

        Incendie incendie = new Incendie();
        Vol vol = new Vol();
        Nuisible nuisible = new Nuisible();
        ViandePourrie viandePourrie = new ViandePourrie();
        int evenement = _random.Next(1, 6);

        if (evenement == 1)
        {
            incendie.AppliquerIncendie(habitat, Zoo);
        }
        else if (evenement == 2)
        {
            vol.AppliquerVol(habitat);
        }
        else if (evenement == 3)
        {
            nuisible.AppliquerNuisible(habitat);
        }
        else if (evenement == 5)
        {
            viandePourrie.AppliquerViandePourrie(habitat);
        }
        else
        {
            Console.WriteLine("Pas de mauvais évènement ce mois-ci !");
        }

        MettreAJourMaladiesEnCours();
        AppliquerMaladiesMensuelles();

        Zoo.NourrirAnimauxTour();

        Zoo._marchand.supprimerAnimauxAVendre();
        Zoo._marchand.supprimerHabitatsAVendre();

        Zoo._marchand.InitialiserAnimauxAVendre();
        Zoo._marchand.InitialiserHabitatsAVendre();
        
        foreach (var habitatDuZoo in Zoo.HabitatsZoo)
        {
            habitatDuZoo.FaireVieillirAnimaux();
        }
        foreach (var habitatDuZoo in Zoo.HabitatsZoo)
        {
            habitatDuZoo.Surpopulation();
        }
    }

    private void AppliquerMaladiesMensuelles()
    {
        foreach (Habitat habitat in Zoo.HabitatsZoo)
        {
            foreach (Animal animal in habitat.Animaux.ToList())
            {
                Maladie? maladie = TrouverMaladiePourAnimal(animal);
                if (maladie != null)
                {
                    maladie.TenterInfection(animal, habitat, _random);
                }
            }
        }
    }

    private void MettreAJourMaladiesEnCours()
    {
        foreach (Habitat habitat in Zoo.HabitatsZoo)
        {
            foreach (Animal animal in habitat.Animaux)
            {
                animal.ProgresserMaladie(1.0);
            }
        }
    }

    private Maladie? TrouverMaladiePourAnimal(Animal animal)
    {
        if (animal is Tigre)
        {
            return _maladies[0];
        }

        if (animal is Aigle)
        {
            return _maladies[1];
        }

        if (animal is Poule)
        {
            return _maladies[2];
        }

        return null;
    }

    public void VerifierMois()
    {
        saison_haute = false;
        int moisNumero = ((nbTours - 1) % 12) + 1;

        if (moisNumero == 1)
        {
            mois = "Janvier";
        }
        else if (moisNumero == 2)
        {
            mois = "Février";
        }
        else if (moisNumero == 3)
        {
            mois = "Mars";
        }
        else if (moisNumero == 4)
        {
            mois = "Avril";
        }
        else if (moisNumero == 5)
        {
            mois = "Mai";
        }
        else if (moisNumero == 6)
        {
            mois = "Juin";
        }
        else if (moisNumero == 7)
        {
            mois = "Juillet";
        }
        else if (moisNumero == 8)
        {
            mois = "Aout";
        }
        else if (moisNumero == 9)
        {
            mois = "Septembre";
        }
        else if (moisNumero == 10)
        {
            mois = "Octobre";
        }
        else if (moisNumero == 11)
        {
            mois = "Novembre";
        }
        else if (moisNumero == 12)
        {
            mois = "Décembre";
        }

        if (moisNumero >= 5 && moisNumero <= 9)
        {
            saison_haute = true;
        }
    }
}