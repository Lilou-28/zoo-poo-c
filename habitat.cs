class Habitat
{
    private int _Capacité;
    public int Capacité
    {
        get { return _Capacité; }
        set { _Capacité = value; }
    }
    private string _Type;
    public string Type
    {
        get { return _Type; }
        set { _Type = value; }
    }
    private int _prix_achat ;
    public int prix_achat
    {
        get { return _prix_achat; }
        set { _prix_achat = value; }
    }
    private int _prix_vente;
    public int prix_vente
    {
        get { return _prix_vente; }
        set { _prix_vente = value; }
    }

    private List<Animal> _animaux;
    public List<Animal> Animaux
    {
        get { return _animaux; }
        set { _animaux = value; }
    }

    private int _Stock; 
    public int Stock
    {
        get { return _Stock; }
        set { _Stock = value; }
    }
    public Habitat()
    {
        Animaux = new List<Animal>();
    }
    public void AjouterAnimal(Animal animal)
    {
        if (Animaux.Count < Capacité)
        {
            Animaux.Add(animal);
            Console.WriteLine("L'animal a été ajouté à l'habitat.");
        }
        else
        {
            Console.WriteLine("Le habitat est plein, impossible d'ajouter l'animal.");
        }
    }
    public void RetirerAnimal(Animal animal)
    {
        if (Animaux.Contains(animal))
        {
            Animaux.Remove(animal);
        }
        else
        {
            Console.WriteLine("L'animal n'est pas présent dans l'habitat.");
        }
    }

}
class Hab_Tigre : Habitat
{
    public Hab_Tigre()
    {
        Capacité = 2;
        Type = "Tigre";
        prix_achat = 2000;
        prix_vente = 500;
    }
}

class Hab_Aigle : Habitat
{
    public Hab_Aigle()
    {
        Capacité = 5;
        Type = "Aigle";
        prix_achat = 2000;
        prix_vente = 500;
    }
}

class Hab_poule : Habitat
{
    public Hab_poule()
    {
        Capacité = 10;
        Type = "Poule";
        prix_achat = 300;
        prix_vente = 50;
    }
}