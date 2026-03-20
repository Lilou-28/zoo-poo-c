class Tour
{
    public int nbTours { get; set; }
    public Zoo Zoo { get; }

    public Tour(Zoo zoo)
    {
        Zoo = zoo;
        nbTours = 0;
    }
    public void ProchainTour()
    {
        nbTours++;
        Console.WriteLine($"\n--- Tour {nbTours} ---");

        // Appliquer les événements aléatoires à chaque habitat
        foreach (Habitat habitat in Zoo.HabitatsZoo)
        {
            Incendie incendie = new Incendie();
            Vol vol = new Vol();
            Nuisible nuisible = new Nuisible();
            ViandePourrie viandePourrie = new ViandePourrie();

            // Appliquer un incendie
            incendie.AppliquerIncendie(habitat, Zoo.HabitatsZoo);
            // Appliquer un vol
            vol.AppliquerVol(habitat);
            // Appliquer une infestation de nuisibles
            nuisible.AppliquerNuisible(habitat);
            // Appliquer une contamination de viande
            viandePourrie.AppliquerViandePourrie(habitat);
        }
    }
}