class Animal
{
    public int Id {get; }
    private static readonly Random _random = new Random();

    private static int _compteur_id = 0 ;
    private string _Nom;

    public string Nom
    {
        get { return _Nom; }
    }

    private int _age;
    public int Age
    {
        get { return _age; }
    }

    protected string _sexe = "";

    public string Sexe
    {
        get { return _sexe; }
    }

    protected int _prix_achat ;

    protected int _prix_vente;

    
   
    protected double _poids_nouriture;

    protected bool _faim;

    protected int _majoriter;

    protected int _fin_production;

    protected int _mort;

    protected string _type_nourriture = "";

    protected int _nouriture_consommee_mois;

    public bool _malade;
    private double _moisMaladieRestants;
    private string _maladieActive = "";

    public bool EstMalade
    {
        get { return _malade; }
    }

    protected Animal()
    {
        Id = ++_compteur_id; 
        Console.Write("Entrez le nom de l'animal :");
        _Nom = Console.ReadLine() ?? "Animal";
        _age = 0;
    }

    protected Animal(string nom, int age)
    {
        Id = ++_compteur_id;
        _Nom = string.IsNullOrWhiteSpace(nom) ? "Animal" : nom.Trim();
        _age = Math.Max(0, age);
    }

    public int QuantiteNourritureNecessaire
    {
        get { return (int)Math.Ceiling(_poids_nouriture); }
    }

    private string TypeAliment(Aliment aliment)
    {
        if (aliment is Viande)
        {
            return "Viande";
        }

        if (aliment is Graines)
        {
            return "Graines";
        }

        return "";
    }

    public bool Nourrir(Aliment aliment, Habitat habitat, out string message)
    {

        string typeNourritureHabitat = TypeAliment(habitat.NourritureHabitat);
        if (_type_nourriture != typeNourritureHabitat)
        {
            _faim = true;
            message = $"Type de nourriture incompatible: {Nom} mange {_type_nourriture} mais l'habitat fournit {typeNourritureHabitat}.";
            return false;
        }

        int quantiteNecessaire = QuantiteNourritureNecessaire;
        int quantiteDisponible = aliment.StockCourant;

        if (quantiteDisponible <= 0)
        {
            _faim = true;
            message = $"Aucun stock disponible pour nourrir {Nom}.";
            return false;
        }

        int quantiteMangee = Math.Min(quantiteNecessaire, quantiteDisponible);
        if (!aliment.SupprimerAliment(quantiteMangee))
        {
            _faim = true;
            message = $"Impossible de retirer {quantiteMangee} de nourriture pour {Nom}.";
            return false;
        }

        _nouriture_consommee_mois += quantiteMangee;

        if (quantiteMangee < quantiteNecessaire)
        {
            _faim = true;
            message = $"{Nom} a mange {quantiteMangee} kg sur {quantiteNecessaire} kg et a encore faim.";
            return false;
        }

        _faim = false;
        message = $"{Nom} a ete nourri avec succes ({quantiteMangee} kg).";
        return true;
    }

    public string Aleatoiresexe()
    {
        int n = _random.Next(0, 2);
        if (n == 0)
        {
            return "Male";
        }
        else
        {
            return "Femelle"; 
        }
    }
    public bool ChangerNomAnimal(string nouveauNom)
    {
        if (string.IsNullOrWhiteSpace(nouveauNom))
        {
            return false;
        }

        _Nom = nouveauNom.Trim();
        return true;
    }

    public void TomberMalade(string nomMaladie, double dureeEnMois)
    {
        _malade = true;
        _maladieActive = nomMaladie;
        _moisMaladieRestants = Math.Max(0.1, dureeEnMois);
    }

    public void ProgresserMaladie(double moisEcoules)
    {
        if (!_malade)
        {
            return;
        }

        _moisMaladieRestants -= Math.Max(0.1, moisEcoules);
        if (_moisMaladieRestants > 0)
        {
            return;
        }

        _malade = false;
        _moisMaladieRestants = 0;
        Console.WriteLine($"{Nom} est gueri de {_maladieActive}.");
        _maladieActive = "";
    }

    public bool ChangerNomAnimal()
    {
        Console.Write("Choisissez le nouveau nom de votre Animal :");
        string? nouveauNom = Console.ReadLine();
        return ChangerNomAnimal(nouveauNom ?? "");
    }

    public virtual (int _prix_achat, int _prix_vente) Calculprix(){
        return (_prix_achat, _prix_vente);
    }
    public void AfficherInfos()
    {
        Console.Clear();
        Console.WriteLine("=== Informations sur l'animal ===");
        Console.WriteLine($"Id: {Id}");
        Console.WriteLine($"Nom: {_Nom}");
        Console.WriteLine($"Age: {Age} mois");
        Console.WriteLine($"Sexe: {Sexe}");
        Console.WriteLine($"Prix d'achat: {_prix_achat}");
        Console.WriteLine($"Prix de vente: {_prix_vente}");
        Console.WriteLine("1. Changer le nom de l'animal");
        Console.WriteLine("0. Retour");
        string? sChoix = Console.ReadLine();
        switch (sChoix)
        {
            case "0":
                return;
            case "1":
                Console.Write("Nouveau nom: ");
                string? nouveauNom = Console.ReadLine();
                if (ChangerNomAnimal(nouveauNom ?? ""))
                {
                    Console.WriteLine("Le nom de l'animal a été changé avec succès.");
                }
                else
                {
                    Console.WriteLine("Nom invalide. Le nom de l'animal n'a pas été changé.");
                }
                Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
                Console.ReadLine();
                return;
            default:
                Console.WriteLine("Choix invalide. Appuyez sur Entrée pour continuer...");
                Console.ReadLine();
                return;
        }
    }
    public void Vieillir(Habitat habitat)
    {
        _age++;
        if (Age >= _mort)
        {
            Console.WriteLine($"{Nom} est mort de vieillesse à l'âge de {Age} mois.");
            habitat.RetirerAnimal(this);
        }
    }
}

class Poule : Animal {
    public Poule (){
        _sexe = Aleatoiresexe();
            if (_sexe == "Male")
        {
            _poids_nouriture = 0.18 * 30;
            _majoriter = 6;
        }
        else
        {
            _poids_nouriture = 0.15 * 30;
            _majoriter = 6;
        }
        _type_nourriture = "Graines";
        _fin_production = 96; 
        _mort = 180; 
        _faim = false; 
    }

    public Poule(string nom, int age) : base(nom, age)
    {
        _sexe = Aleatoiresexe();
        if (_sexe == "Male")
        {
            _poids_nouriture = 0.18 * 30;
            _majoriter = 6;
            _prix_vente = 20;
            _prix_achat = 100;
        }
        else
        {
            _poids_nouriture = 0.15 * 30;
            _majoriter = 6;
            _prix_vente = 10;
            _prix_achat = 20;
        }
        _type_nourriture = "Graines";
        _fin_production = 96;
        _mort = 180;
        _faim = false;
    }

    public override (int _prix_achat, int _prix_vente) Calculprix (){
        if (Age < 6){
            _prix_achat = 20; 
            _prix_vente = 10;
            return (_prix_achat, _prix_vente);
        }

        return (_prix_achat, _prix_vente);
    }
}
class Tigre : Animal
{
    public Tigre()
    {
        _sexe = Aleatoiresexe();
        if (_sexe == "Male")
        {
            _poids_nouriture = 12 * 30;
            _majoriter = 48;
        }
        else
        {
            _poids_nouriture = 10 * 30;
            _majoriter = 72;
            
        }
        _type_nourriture = "Viande";
        _prix_achat = 3000;
        _prix_vente = 1500;
        _fin_production = 14;
        _mort = 25;
        _faim = false; 
    }

    public Tigre(string nom, int age) : base(nom, age)
    {
        _sexe = Aleatoiresexe();
        if (_sexe == "Male")
        {
            _poids_nouriture = 12 * 30;
            _majoriter = 48;
        }
        else
        {
            _poids_nouriture = 10 * 30;
            _majoriter = 72;
        }
        _type_nourriture = "Viande";
        _prix_achat = 3000;
        _prix_vente = 1500;
        _fin_production = 14;
        _mort = 25;
        _faim = false;
    }
    public override (int _prix_achat, int _prix_vente) Calculprix (){
        if (Age < 6){
            _prix_achat = 3000; 
            _prix_vente = 1500;
            return (_prix_achat, _prix_vente);
        }
        if (Age >= 48 && Age < 168){
            _prix_achat = 120000; 
            _prix_vente = 60000;
            return (_prix_achat, _prix_vente);
        }
        if (Age > 168){
            _prix_achat = 60000; 
            _prix_vente = 10000;
            return (_prix_achat, _prix_vente);
        }

        return (_prix_achat, _prix_vente);
    }
}

class Aigle : Animal
{
    public Aigle ()
    {
        _sexe = Aleatoiresexe();
        if (_sexe == "Male")
        {
            _poids_nouriture = 0.25 * 30;
        }
        else
        {
            _poids_nouriture = 0.3 * 30;
        }
        _type_nourriture = "Viande";
        _prix_achat = 1000; 
        _prix_vente = 500;
        _majoriter = 48;
        _fin_production = 14; 
        _mort = 25;
        _faim = false; 
    }

    public Aigle(string nom, int age) : base(nom, age)
    {
        _sexe = Aleatoiresexe();
        if (_sexe == "Male")
        {
            _poids_nouriture = 0.25 * 30;
        }
        else
        {
            _poids_nouriture = 0.3 * 30;
        }
        _type_nourriture = "Viande";
        _prix_achat = 1000;
        _prix_vente = 500;
        _majoriter = 48;
        _fin_production = 14;
        _mort = 25;
        _faim = false;
    }
    public override (int _prix_achat, int _prix_vente) Calculprix (){
        if (Age < 6){
            _prix_achat = 1000; 
            _prix_vente = 500;
            return (_prix_achat, _prix_vente);
        }
        if (Age >= 48 && Age < 168){
            _prix_achat = 4000; 
            _prix_vente = 2000;
            return (_prix_achat, _prix_vente);
        }
        if (Age > 168){
            _prix_achat =2000; 
            _prix_vente = 400;
            return (_prix_achat, _prix_vente);
        }

        return (_prix_achat, _prix_vente);
    }
}

