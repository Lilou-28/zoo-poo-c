class Visiteur
{
    private int prix_enfant;
    private int prix_adulte;

    public Visiteur()
    {
        prix_enfant = 13;
        prix_adulte = 17;
    }

    public int CalculerVisiteursTotaux(List<Habitat> habitatsZoo)
    {
        int visiteursTotaux = 0;

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

        return visiteursTotaux;
    }

    public int CalculerRevenuMensuel(List<Habitat> habitatsZoo)
    {
        int visiteursTotaux = CalculerVisiteursTotaux(habitatsZoo);

        // Groupes fixes : 2 adultes + 2 enfants.
        int groupesComplets = visiteursTotaux / 4;
        int revenuParGroupe = (2 * prix_adulte) + (2 * prix_enfant);

        return groupesComplets * revenuParGroupe;
    }

    public void VerifRevenu(Tour tours, List<Habitat> habitatsZoo, Bank bank)
    {
        if (!tours.saison_haute)
        {
            Console.WriteLine("Hors saison haute : aucun revenu visiteurs ce tour.");
            return;
        }

        int visiteursTotaux = CalculerVisiteursTotaux(habitatsZoo);
        if (visiteursTotaux == 0)
        {
            Console.WriteLine("Aucun visiteur ce tour (pas d'animaux attractifs).");
            return;
        }
        
        int revenu = CalculerRevenuMensuel(habitatsZoo);
        int groupesComplets = visiteursTotaux / 4;

        bank.AjouterArgent(revenu);

        Console.WriteLine($"Visiteurs: {visiteursTotaux} ({groupesComplets} groupe(s) de 4)");
        Console.WriteLine($"Revenu visiteurs ajoute: {revenu}.");
        Console.WriteLine(bank.AfficherSolde());
    }
}