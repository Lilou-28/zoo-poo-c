using System.ComponentModel;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

class Animal
{
    public int Id {get; }

    private static int _compteur_id = 0 ;
    private string _Nom;

    private int _naissance; 

    protected string _sexe;

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
        Console.Write("Entrez le nom de l'animal : ");
        _Nom = Console.ReadLine();
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
}