using System.Collections;
using System.ComponentModel.Design;
using System.IO.Compression;

class Animal
{
    public int Id {get; }

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


    public Animal()
    {
        Id = ++_compteur_id; 
        Console.Write("Entrez le nom de l'animal :");
        _Nom = Console.ReadLine() ?? "Animal";
        _age = 0;
    }

    public Animal(string nom, int age)
    {
        Id = ++_compteur_id;
        _Nom = string.IsNullOrWhiteSpace(nom) ? "Animal" : nom.Trim();
        _age = Math.Max(0, age);
    }
    public void Manger()
    {
    }
    public string Aleatoiresexe()
    {
        Random random = new Random();
        int n = random.Next(0, 2);
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
    public void Vieillir()
    {
        _age++;
    }
}

class Poule : Animal {
    public Poule (){
        _sexe = Aleatoiresexe();
            if (_sexe == "Male")
        {
            _poids_nouriture = 0.18;
            _majoriter = 6;
        }
        else
        {
            _poids_nouriture = 0.15;
            _majoriter = 6;
        }
        _fin_production = 96; 
        _mort = 180; 
        _faim = false; 
    }

    public Poule(string nom, int age) : base(nom, age)
    {
        _sexe = Aleatoiresexe();
        if (_sexe == "Male")
        {
            _poids_nouriture = 0.18;
            _majoriter = 6;
            _prix_vente = 20;
            _prix_achat = 100;
        }
        else
        {
            _poids_nouriture = 0.15;
            _majoriter = 6;
            _prix_vente = 10;
            _prix_achat = 20;
        }
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
            _poids_nouriture = 12;
            _majoriter = 48;
        }
        else
        {
            _poids_nouriture = 10;
            _majoriter = 72;
            
        }
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
            _poids_nouriture = 12;
            _majoriter = 48;
        }
        else
        {
            _poids_nouriture = 10;
            _majoriter = 72;
        }
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
            _poids_nouriture = 0.25;
        }
        else
        {
            _poids_nouriture = 0.3;
        }
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
            _poids_nouriture = 0.25;
        }
        else
        {
            _poids_nouriture = 0.3;
        }
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

