
class Animal 
{
    public int MoisDepuisArrivee { get; private set; } = 0;
    public int Id {get; }
    protected static readonly Random _random = new Random();

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
    protected double gestation; 
    protected int _fin_reproduction;
    protected double TauxMortaliteInfantile;

    protected bool fidele;

    protected int porte;

    public bool EnGestation;
    protected int _moisGestationRestants = 0;

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
        // Si femelle en gestation, elle mange 2x plus
        if (_sexe == "Femelle" && EnGestation)
        {
            quantiteNecessaire *= 2;
        }
        int quantiteDisponible = aliment.StockCourant;

        if (quantiteDisponible <= 0)
        {
            _faim = true;
            message = $"Aucun stock disponible pour nourrir {Nom}.";
            // Si femelle en gestation, elle perd le fœtus
            if (_sexe == "Femelle" && EnGestation)
            {
                EnGestation = false;
                _moisGestationRestants = 0;
                message += " (Elle a perdu le fœtus par manque de nourriture)";
            }
            return false;
        }

        int quantiteMangee = Math.Min(quantiteNecessaire, quantiteDisponible);
        if (!aliment.SupprimerAliment(quantiteMangee))
        {
            _faim = true;
            message = $"Impossible de retirer {quantiteMangee} de nourriture pour {Nom}.";
            // Si femelle en gestation, elle perd le fœtus
            if (_sexe == "Femelle" && EnGestation)
            {
                EnGestation = false;
                _moisGestationRestants = 0;
                message += " (Elle a perdu le fœtus par manque de nourriture)";
            }
            return false;
        }

        _nouriture_consommee_mois += quantiteMangee;

        if (quantiteMangee < quantiteNecessaire)
        {
            _faim = true;
            message = $"{Nom} a mange {quantiteMangee} kg sur {quantiteNecessaire} kg et a encore faim.";
            // Si femelle en gestation, elle perd le fœtus
            if (_sexe == "Femelle" && EnGestation)
            {
                EnGestation = false;
                _moisGestationRestants = 0;
                message += " (Elle a perdu le fœtus par manque de nourriture)";
            }
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
        MoisDepuisArrivee++;
        if (Age >= _mort)
        {
            Console.WriteLine($"{Nom} est mort de vieillesse à l'âge de {Age} mois.");
            habitat.RetirerAnimal(this);
        }
    }
    public bool PeutSeReproduire()
    {
        // Un animal peut se reproduire s'il est mature, pas trop vieux, pas malade, pas affamé, et (si femelle) pas déjà en gestation
        if (Age >= _majoriter && Age < _fin_reproduction && EstMalade == false && _faim == false && MoisDepuisArrivee > 0 && (_sexe == "Male" || EnGestation == false))
        {
            return true;
        }
        return false;
    }

    // Système de reproduction
    public virtual bool TenterReproduction(Animal partenaire, Habitat habitat)
    {
        // Vérifie que les deux animaux sont de la même espèce
        if (this.GetType() != partenaire.GetType())
        {
            Console.WriteLine("Les animaux ne sont pas de la même espèce.");
            return false;
        }
        // Vérifie que les sexes sont différents
        if (this.Sexe == partenaire.Sexe)
        {
            Console.WriteLine("Les deux animaux sont du même sexe.");
            return false;
        }
        // Trouve la femelle
        Animal femelle = this.Sexe == "Femelle" ? this : partenaire;
        Animal male = this.Sexe == "Male" ? this : partenaire;
        // Vérifie que les deux peuvent se reproduire
        if (!this.PeutSeReproduire() || !partenaire.PeutSeReproduire())
        {
            Console.WriteLine("Un des animaux ne peut pas se reproduire.");
            return false;
        }
        // Vérifie que la femelle n'est pas déjà en gestation
        if (femelle.EnGestation)
        {
            Console.WriteLine($"{femelle.Nom} est déjà en gestation.");
            return false;
        }
        // Vérifie qu'il y a de la place pour les petits
        int placesLibres = habitat.Capacité - habitat.Animaux.Count;
        if (placesLibres <= 0)
        {
            Console.WriteLine("Pas assez de place dans l'habitat pour accueillir de nouveaux petits.");
            return false;
        }
        // Lance la gestation
        femelle.EnGestation = true;
        femelle._moisGestationRestants = (int)Math.Ceiling(femelle.gestation);
        Console.WriteLine($"Félicitations ! {femelle.Nom} attend des petits.");
        return true;
    }

    public virtual Animal BebeCree()
    {
        throw new NotImplementedException("BebeCree doit être surchargée dans chaque espèce.");
    }

    public void VerifierGestation(Habitat habitat)
    {
        if (EnGestation == true)
        {
            _moisGestationRestants--;
            if (_moisGestationRestants <= 0)
            {
                EnGestation = false;
                MettreBas(habitat);
            }
        }
    }
    private void MettreBas(Habitat habitat)
{
    Console.WriteLine($"{Nom} met bas !");
    for (int i = 0; i < porte; i++)
    {
        // Chance de survie basée sur TauxMortaliteInfantile
        if (_random.NextDouble() > TauxMortaliteInfantile)
        {
            Animal bebe = BebeCree();
            habitat.AjouterAnimal(bebe);
            Console.WriteLine($"Un nouveau {bebe.GetType().Name} est né !");
        }
        else
        {
            Console.WriteLine("Hélas, un petit n'a pas survécu.");
        }
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
            gestation = 1.5; // 6 semaines ≈ 1.5 mois
            porte = 2; // 2 œufs par ponte
            TauxMortaliteInfantile = 0.5;
        }
        _type_nourriture = "Graines";
        _fin_production = 96;
        _fin_reproduction = 96;
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
            gestation = 1.5;
            porte = 2;
            TauxMortaliteInfantile = 0.5;
        }
        _type_nourriture = "Graines";
        _fin_production = 96;
        _fin_reproduction = 96;
        _mort = 180;
        _faim = false;
    }

    public override Animal BebeCree()
    {
        return new Poule();
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
            gestation = 3; // 3 mois
            porte = _random.Next(1, 4); // 1 à 3 petits
            TauxMortaliteInfantile = 0.33;
        }
        _type_nourriture = "Viande";
        _prix_achat = 3000;
        _prix_vente = 1500;
        _fin_production = 14 * 12;
        _mort = 25*12;
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
            gestation = 3; 
            porte = _random.Next(1, 4); 
            TauxMortaliteInfantile = 0.33;
        }
        _type_nourriture = "Viande";
        _prix_achat = 3000;
        _prix_vente = 1500;
        _fin_production = 168;
        _mort = 300;
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
    public override Animal BebeCree()
    {
        // On crée un bébé tigre de 0 mois
        return new Tigre("Petit Tigre", 0);
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
            _majoriter = 48;
        }
        else
        {
            _poids_nouriture = 0.3 * 30;
            _majoriter = 48;
            gestation = 1.5; // 45 jours ≈ 1.5 mois
            porte = 1; // 1 petit par ponte
            TauxMortaliteInfantile = 0.5;
        }
        _type_nourriture = "Viande";
        _prix_achat = 1000;
        _prix_vente = 500;
        _majoriter = 48;
        _fin_production = 168;
        _fin_reproduction = 168;
        _mort = 300;
        _faim = false;
    }

    public Aigle(string nom, int age) : base(nom, age)
    {
        _sexe = Aleatoiresexe();
        if (_sexe == "Male")
        {
            _poids_nouriture = 0.25 * 30;
            _majoriter = 48;
        }
        else
        {
            _poids_nouriture = 0.3 * 30;
            _majoriter = 48;
            gestation = 1.5;
            porte = 1;
            TauxMortaliteInfantile = 0.5;
        }
        _type_nourriture = "Viande";
        _prix_achat = 1000;
        _prix_vente = 500;
        _majoriter = 48;
        _fin_production = 168;
        _fin_reproduction = 168;
        _mort = 300;
        _faim = false;
    }

    public override Animal BebeCree()
    {
        return new Aigle();
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

