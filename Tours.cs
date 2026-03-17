class Tour
{
    public int nbTours { get; set; }
    public Tour()
    {
        nbTours = 0;
        Incendie incendie = new Incendie();
        Vol vol = new Vol();
        Nuisible nuisible = new Nuisible();
        ViandePourrie viandePourrie = new ViandePourrie();
    }
    public void ProchainTour()
    {
        nbTours++;
        Console.WriteLine($"\n--- Tour {nbTours} ---");

        // Appliquer les événements aléatoires à chaque habitat
        foreach (Habitat habitat in Zoo._habitatsZoo)
        {
            Incendie incendie = new Incendie();
            Vol vol = new Vol();
            Nuisible nuisible = new Nuisible();
            ViandePourrie viandePourrie = new ViandePourrie();

            // Appliquer un incendie
            incendie.AppliquerIncendie(habitat, Zoo._habitatsZoo);
            // Appliquer un vol
            vol.AppliquerVol(habitat);
            // Appliquer une infestation de nuisibles
            nuisible.AppliquerNuisible(habitat);
            // Appliquer une contamination de viande
            viandePourrie.AppliquerViandePourrie(habitat);

        }

    }
}