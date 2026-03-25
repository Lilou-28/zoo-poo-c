class Maladie
{
    protected string Nom;
    protected double DureeMois;
    protected double Probabilite;
    protected double TauxMortalite;

    public Maladie(string nom, double dureeMois, double probabiliteAnnuelle)
    {
        Nom = nom;
        DureeMois = dureeMois;
        Probabilite = ConvertirProbabiliteAnnuelleEnMensuelle(probabiliteAnnuelle);
        TauxMortalite = 0.10;
    }

    public bool TenterInfection(Animal animal, Habitat habitat, Random random)
    {
        if (animal.EstMalade)
        {
            return false;
        }

        if (random.NextDouble() >= Probabilite)
        {
            return false;
        }

        double dureeVariable = CalculerDureeVariableMois(random);
        animal.TomberMalade(Nom, dureeVariable);
        Console.WriteLine($"{animal.Nom} a contracte la {Nom} pour {dureeVariable:0.##} mois.");

        if (random.NextDouble() < TauxMortalite)
        {
            Console.WriteLine($"{animal.Nom} est mort des suites de la {Nom}.");
            habitat.RetirerAnimal(animal);
        }

        return true;
    }

    private double CalculerDureeVariableMois(Random random)
    {
        double facteur = 0.8 + (random.NextDouble() * 0.4);
        return Math.Max(0.1, Math.Round(DureeMois * facteur, 2));
    }

    private static double ConvertirProbabiliteAnnuelleEnMensuelle(double probabiliteAnnuelle)
    {
        return 1.0 - Math.Pow(1.0 - probabiliteAnnuelle, 1.0 / 12.0);
    }
}

class Mal_Tigre : Maladie
{
    public Mal_Tigre() : base("Maladie du Tigre", 0.5, 0.30)
    {
    }
}

class Mal_Aigle : Maladie
{
    public Mal_Aigle() : base("Maladie de l'Aigle", 1.0, 0.10)
    {
    }
}

class Mal_Poule : Maladie
{
    public Mal_Poule() : base("Maladie de la Poule", 0.17, 0.05)
    {
    }
}