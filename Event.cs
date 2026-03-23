class Event
{
    protected static readonly Random _random = new Random();
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
        double chance = _random.NextDouble() * 100; // Génère un nombre entre 0 et 100
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

    public void AppliquerIncendie(Habitat habitat, Zoo zoo)
    {
        if (CalculerImpact() == 1)
        {
            Console.WriteLine("Un incendie s'est déclaré !");
            DetruireHabitat(habitat, zoo);
        }
        else
        {
            Console.WriteLine("Pas d'incendie cette fois.");
        }
    }
    private void DetruireHabitat(Habitat habitat, Zoo zoo)
    {
        habitat.ViderAnimaux(); // Tous les animaux de l'habitat sont perdus
        bool supprime = zoo.SupprimerHabitat(habitat);
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
    public void AppliquerVol(Habitat habitat)
    {
        if (CalculerImpact() == 1)
        {
            Console.WriteLine("Un vol s'est produit !");
            Animal? animalVole = habitat.RetirerAnimalAleatoire(_random);
            if (animalVole != null)
            {
                Console.WriteLine($"L'animal '{animalVole.Nom}' a été volé de l'habitat n°'{habitat.Id}'.");
            }
            else
            {
                Console.WriteLine($"L'habitat n°'{habitat.Id}' est vide, aucun animal à voler.");
            }
        }
        else
        {
            Console.WriteLine("Pas de vol cette fois.");
        }
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
    public void AppliquerNuisible(Habitat habitat)
    {
        if (CalculerImpact() == 1)
        {
            Console.WriteLine("Une infestation de nuisibles s'est produite !");
            //int grainesPerdues = (int)(habitat.Graines * 0.1); // Perte de 10% des graines
            //habitat.Graines -= grainesPerdues;
            //Console.WriteLine($"L'habitat n°'{habitat.Id}' a perdu {grainesPerdues} graines à cause des nuisibles.");
        }
        else
        {
            Console.WriteLine("Pas d'infestation de nuisibles cette fois.");
        }
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
    public void AppliquerViandePourrie(Habitat habitat)
    {
        if (CalculerImpact() == 1)
        {
            Console.WriteLine("La viande a été contaminée !");
            //int viandePerdue = (int)(habitat.Viande * 0.2); // Perte de 20% de la viande
            //habitat.Viande -= viandePerdue;
            //Console.WriteLine($"L'habitat n°'{habitat.Id}' a perdu {viandePerdue} unités de viande à cause de la contamination.");
        }
        else
        {
            Console.WriteLine("Pas de contamination de la viande cette fois.");
        }
    }
}