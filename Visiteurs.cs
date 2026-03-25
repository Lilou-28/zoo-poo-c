class Visiteur
{
    private int prix_enfant;
    private int prix_adulte;

    public Visiteur()
    {
        prix_enfant = 13;
        prix_adulte = 17;
    }

    public int CalculerVisiteursTotaux(List<Habitat> habitatsZoo, bool saison_haute)
    {
        double visiteursTotaux = 0;
        if (!saison_haute)
        {
            foreach (Habitat habitat in habitatsZoo)
            {
            double multiplicateurVisiteurs = habitat.Type switch
            {
                "Tigre" => 5,
                "Aigle" => 0.5,
                "Poule" => 7,
                _ => 0
            };

            visiteursTotaux += habitat.Animaux.Count * multiplicateurVisiteurs;
            }
            
            return (int)visiteursTotaux;
        }
        foreach (Habitat habitat in habitatsZoo)
        {
            int multiplicateurVisiteurs = habitat.Type switch
            {
                "Tigre" => 30,
                "Aigle" => 15,
                "Poule" => 2,
                _ => 0
            };

            visiteursTotaux += habitat.Animaux.Count * multiplicateurVisiteurs;
        }

        return (int)visiteursTotaux;
    }

    public int CalculerRevenuMensuel(List<Habitat> habitatsZoo)
    {
        int visiteursTotaux = CalculerVisiteursTotaux(habitatsZoo, true);

        // Groupes fixes : 2 adultes + 2 enfants.
        int groupesComplets = visiteursTotaux / 4;
        int revenuParGroupe = (2 * prix_adulte) + (2 * prix_enfant);

        return groupesComplets * revenuParGroupe;
    }

    public void VerifRevenu(Tour tours, List<Habitat> habitatsZoo, Bank bank)
    {
        int visiteursTotaux = CalculerVisiteursTotaux(habitatsZoo, tours.saison_haute);
        int revenu = CalculerRevenuMensuel(habitatsZoo);
        int groupesComplets = visiteursTotaux / 4;

        if (!tours.saison_haute)
        {
            Console.WriteLine("Hors saison haute : moins de visiteurs et revenus réduits.");
            Console.WriteLine($"Visiteurs: {visiteursTotaux}.");
            Console.WriteLine($"Revenu visiteurs ajoute: {revenu}{bank.monnaie}.");
            bank.AjouterArgent(revenu);
            Console.WriteLine(bank.AfficherSolde());
            return;
        }

        if (visiteursTotaux == 0)
        {
            Console.WriteLine("Aucun visiteur ce tour (pas d'animaux attractifs).");
            return;
        }
        
        bank.AjouterArgent(revenu);

        Console.WriteLine($"Visiteurs: {visiteursTotaux} ({groupesComplets} groupe(s) de 4)");
        Console.WriteLine($"Revenu visiteurs ajoute: {revenu}{bank.monnaie}.");
        Console.WriteLine(bank.AfficherSolde());
    }
}