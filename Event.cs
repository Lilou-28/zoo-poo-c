using System.Runtime.InteropServices;

class Event
{
    protected string _name;
    protected double _probability;
    protected string _description;
    public Event()
    {
        this._name = "Event";
        this._probability = 0;
        this._description = "No description available";
    }

    public override string ToString()
    {
        return $"{_name} - {_description} (Probability: {_probability}%)";
    }
    public int CalculerImpact()
    {
        Random random = new Random();
        double chance = random.NextDouble() * 100; // Génère un nombre entre 0 et 100
        if (chance < _probability)
        {
            return 1; // L'événement se produit
        }
        return 0; // L'événement ne se produit pas
    }
}

class Incendie : Event
{
    public Incendie()
    {
        this._name = "Incendie";
        this._probability = 0.1; // 0.1% de chance d'avoir un incendie
        this._description = "Un incendie peut survenir, causant la perte d'un habitat et de tous les animaux qu'il contient.";
    }

    public void AppliquerIncendie(Habitat habitat, List<Habitat> habitatsDuZoo)
    {
        if (CalculerImpact() == 1)
        {
            Console.WriteLine("Un incendie s'est déclaré !");
            DetruireHabitat(habitat, habitatsDuZoo);
        }
        else
        {
            Console.WriteLine("Pas d'incendie cette fois.");
        }
    }
    private void DetruireHabitat(Habitat habitat, List<Habitat> habitatsDuZoo)
    {
        habitat.Animaux.Clear(); // Tous les animaux de l'habitat sont perdus
        bool supprime = habitatsDuZoo.Remove(habitat);
        if (supprime)
        {
            Console.WriteLine($"L'habitat n°'{habitat.Id}' a été détruit par l'incendie, et tous les animaux qu'il contenait ont été perdus.");
            return;
        }

        Console.WriteLine($"L'habitat n°'{habitat.Id}' a été détruit, mais il n'a pas pu être retiré de la liste du zoo.");
    }
}

class Vol : Event
{
    public Vol()
    {
        this._name = "Vol";
        this._probability = 0.1; // 0.1% de chance d'avoir un vol
        this._description = "Un vol peut se produire, entraînant la perte d'un animal.";
    }
}

class Nuisible : Event
{
    public Nuisible()
    {
        this._name = "Nuisible";
        this._probability = 20; // 20% de chance d'avoir une infestation de nuisibles
        this._description = "Une infestation de nuisibles peut survenir, causant des pertes de nourriture(10% des graines).";
    }
}

class ViandePourrie : Event
{
    public ViandePourrie()
    {
        this._name = "Viande Pourrie";
        this._probability = 10; // 10% de chance d'avoir de la viande pourrie
        this._description = "La viande a été contaminée, entraînant une perte de nourriture(20% de la viande).";
    }
}