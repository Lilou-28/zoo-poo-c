class Marchant
{
    private string _nom; 
    private Bank _bank;
    private List<Habitat> HabitatAVendre;
    private List<Animal> AnimalsAVendre;

    public Marchant()
    {
        Console.Write("Choisissez le nom de votre marchant :"); 
        _nom = Console.ReadLine() ?? "Marchant"; 
        _bank = new Bank();
        HabitatAVendre = new List<Habitat>(); 
        AnimalsAVendre = new List<Animal>(); 
    }

    public void InitialiserAnimauxAVendre()
    {
        Console.WriteLine("\n--- Initialisation des animaux du marchant ---");

        int[] agesEnMois = { 6, 48, 168 };

        AjouterTigresParAge(agesEnMois);
        AjouterAiglesParAge(agesEnMois);
        AjouterPoulesParAge(agesEnMois);
        Console.WriteLine($"Stock du marchant prêt : {AnimalsAVendre.Count} animaux.");
    }

    public void InitialiserHabitatsAVendre()
    {
        HabitatAVendre.Add(new Hab_Tigre());
        HabitatAVendre.Add(new Hab_Aigle());
        HabitatAVendre.Add(new Hab_poule());
    }

    public int NombreAnimauxAVendre => AnimalsAVendre.Count;
    public int NombreHabitatsAVendre => HabitatAVendre.Count;

    public void AfficherAnimauxAVendre()
    {
        for (int i = 0; i < AnimalsAVendre.Count; i++)
        {
            Animal animal = AnimalsAVendre[i];
            var (_, prixVente) = animal.Calculprix();
            Console.WriteLine($"{i + 1}. {animal.GetType().Name} - Age: {animal.Age} mois - Prix: {prixVente}");
        }
    }

    public void AfficherHabitatsAVendre()
    {
        for (int i = 0; i < HabitatAVendre.Count; i++)
        {
            Habitat habitat = HabitatAVendre[i];
            Console.WriteLine($"{i + 1}. Habitat {habitat.Type} - Capacite: {habitat.Capacité} - Prix: {habitat.prix_vente}");
        }
    }

    private void AjouterTigresParAge(int[] agesEnMois)
    {
        foreach (int age in agesEnMois)
        {
            for (int i = 1; i <= 3; i++)
            {
                AnimalsAVendre.Add(new Tigre($"Tigre_{age}m_{i}", age));
            }
        }
    }

    private void AjouterAiglesParAge(int[] agesEnMois)
    {
        foreach (int age in agesEnMois)
        {
            for (int i = 1; i <= 3; i++)
            {
                AnimalsAVendre.Add(new Aigle($"Aigle_{age}m_{i}", age));
            }
        }
    }
    private void AjouterPoulesParAge(int[] agesEnMois)
    {
        foreach (int age in agesEnMois)
        {
            for (int i = 1; i <= 3; i++)
            {
                AnimalsAVendre.Add(new Poule($"Poule_{age}m_{i}", age));
            }
        }
    }
    public void AcheterAnimal(Animal animalchoisi, Habitat habitatchoisi)
    {
        var (_, prixVente) = animalchoisi.Calculprix();
        _bank.RetirerArgent(prixVente);
        habitatchoisi.AjouterAnimal(animalchoisi);
        AnimalsAVendre.Remove(animalchoisi);
    }

    public void AcheterHabitat(Habitat habitatchoisi, List<Habitat> habitatsDuZoo)
    {
        _bank.RetirerArgent(habitatchoisi.prix_vente);
        habitatsDuZoo.Add(habitatchoisi);
        HabitatAVendre.Remove(habitatchoisi);
    }

    public bool AcheterAnimalParIndex(int indexAnimal, Habitat habitatChoisi)
    {
        if (indexAnimal < 0 || indexAnimal >= AnimalsAVendre.Count)
        {
            return false;
        }

        AcheterAnimal(AnimalsAVendre[indexAnimal], habitatChoisi);
        return true;
    }

    public bool AcheterHabitatParIndex(int indexHabitat, List<Habitat> habitatsDuZoo)
    {
        if (indexHabitat < 0 || indexHabitat >= HabitatAVendre.Count)
        {
            return false;
        }

        AcheterHabitat(HabitatAVendre[indexHabitat], habitatsDuZoo);
        return true;
    }
}