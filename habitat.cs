class Habitat
{
    public int Id { get; }
    private static int _compteur_id = 0 ;

    private string _Nom;
    private int _Capacité;
    public int Capacité
    {
        get { return _Capacité; }
        protected set { _Capacité = value; }
    }
    private string _Type = "";
    public string Type
    {
        get { return _Type; }
        protected set { _Type = value; }
    }
    private int _prix_achat ;
    public int prix_achat
    {
        get { return _prix_achat; }
        protected set { _prix_achat = value; }
    }
    private int _prix_vente;
    public int prix_vente
    {
        get { return _prix_vente; }
        protected set { _prix_vente = value; }
    }

    public Aliment NourritureHabitat { get; protected set; }
    private List<Animal> _animaux;

    public IReadOnlyList<Animal> Animaux
    {
        get { return _animaux; }
    }

    public Habitat()
    {
        Id = ++_compteur_id;
        Console.Write("Entrez le nom de l'habitat :");
        _Nom = Console.ReadLine() ?? "Habitat";
        NourritureHabitat = new Graines();
        _animaux = new List<Animal>();

    }

    public Habitat(string nom)
    {
        Id = ++_compteur_id;
        _Nom = string.IsNullOrWhiteSpace(nom) ? "Habitat" : nom.Trim();
        NourritureHabitat = new Graines();
        _animaux = new List<Animal>();
    }
    public void AjouterAnimal(Animal animal)
    {
        if (_animaux.Count < Capacité)
        {
            _animaux.Add(animal);
            Console.WriteLine("L'animal a été ajouté à l'habitat.");
        }
        else
        {
            Console.WriteLine("Le habitat est plein, impossible d'ajouter l'animal.");
        }
    }
    public void RetirerAnimal(Animal animal)
    {
        if (_animaux.Contains(animal))
        {
            _animaux.Remove(animal);
            Console.WriteLine("L'animal a été retiré de l'habitat.");
        }
        else
        {
            Console.WriteLine("L'animal n'est pas présent dans l'habitat.");
        }
    }
    public void AfficherAnimaux()
    {
        Console.WriteLine($"Animaux dans l'habitat '{_Nom}' ({_animaux.Count}/{_Capacité}) :");
        if (_animaux.Count == 0)
        {
            Console.WriteLine("  Aucun animal dans cet habitat.");
            return;
        }
        foreach (var animal in _animaux)
        {
            Console.WriteLine($"- {animal.GetType().Name} (Id:{animal.Id}) - {animal.Age} mois - Sexe: {animal.Sexe}");
        }
    }

    public bool VerifierTypeAnimal(Animal animalcheck)
    {
        if (Type == "Tigre" && animalcheck is Tigre)
        {
            return true;
        }
        else if (Type == "Aigle" && animalcheck is Aigle)
        {
            return true;
        }
        else if (Type == "Poule" && animalcheck is Poule)
        {
            return true;
        }
        else
        {
            Console.WriteLine("L'animal ne correspond pas au type de l'habitat.");
            return false;
        }
    }

    public void ViderAnimaux()
    {
        _animaux.Clear();
    }

    public Animal? RetirerAnimalAleatoire(Random random)
    {
        if (_animaux.Count == 0)
        {
            return null;
        }

        int index = random.Next(_animaux.Count);
        Animal animal = _animaux[index];
        _animaux.RemoveAt(index);
        return animal;
    }

    public void FaireVieillirAnimaux()
    {
        foreach (var animal in _animaux)
        {
            animal.Vieillir();
        }
    }

}
class Hab_Tigre : Habitat
{
    public Hab_Tigre() : base("Enclos Tigre")
    {
        Capacité = 2;
        Type = "Tigre";
        prix_achat = 2000;
        prix_vente = 500;
        NourritureHabitat = new Viande(); 
        NourritureHabitat.Limite = 2160;
    }
}

class Hab_Aigle : Habitat
{
    public Hab_Aigle() : base("Voliere Aigle")
    {
        Capacité = 5;
        Type = "Aigle";
        prix_achat = 2000;
        prix_vente = 500;
        NourritureHabitat = new Viande();
        NourritureHabitat.Limite = 135;
    }
}

class Hab_poule : Habitat
{
    public Hab_poule() : base("Poulailler")
    {
        Capacité = 10;
        Type = "Poule";
        prix_achat = 300;
        prix_vente = 50;
        NourritureHabitat = new Graines();
        NourritureHabitat.Limite = 180;
    }
}