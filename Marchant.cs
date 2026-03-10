class Marchant
{
    private string _nom; 
    private Bank _bank;
    private List<Habitat> HabitatAVendre;
    private List<Animal> AnimalsAVendre;

    public Marchant(Bank bank)
    {
        Console.Write("Choisissez le nom de votre marchant :"); 
        _nom = Console.ReadLine() ?? "Marchant"; 
        _bank = bank;
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

    public void AfficherAnimauxAAcheter()
    {
        for (int i = 0; i < AnimalsAVendre.Count; i++)
        {
            Animal animal = AnimalsAVendre[i];
            var (_, prixVente) = animal.Calculprix();
            Console.WriteLine($"{i + 1}. {animal.GetType().Name} - Age: {animal.Age} mois - Prix: {prixVente}");
        }
    }

    public void AfficherHabitatsAAcheter()
    {
        Console.WriteLine($"Votre solde actuel est de : {_bank.AfficherSolde()}");
        for (int i = 0; i < HabitatAVendre.Count; i++)
        {
            Habitat habitat = HabitatAVendre[i];
            Console.WriteLine($"{i + 1}. Habitat {habitat.Type} - Capacite: {habitat.Capacité} - Prix: {habitat.prix_vente}");
        }
        Console.WriteLine("0. Retour");
    }

    public void AfficherHabitatsAVendre()
    {
        Console.WriteLine($"Votre solde actuel est de : {_bank.AfficherSolde()}");
        for (int i = 0; i < HabitatAVendre.Count; i++)
        {
            Habitat habitat = HabitatAVendre[i];
            Console.WriteLine($"{i + 1}. Habitat {habitat.Type} - Capacite: {habitat.Capacité} - Prix: {habitat.prix_vente}");
        }
        Console.WriteLine("0. Retour");
    }
    public void AfficherAnimalAVendre()
    {
        for (int i = 0; i < AnimalsAVendre.Count; i++)
        {
            Animal animal = AnimalsAVendre[i];
            var (_, prixVente) = animal.Calculprix();
            Console.WriteLine($"{i + 1}. {animal.GetType().Name} - Age: {animal.Age} mois - Prix: {prixVente}");
        }
        Console.WriteLine("0. Retour");
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
    public bool AcheterAnimal(Animal animalchoisi, Habitat habitatchoisi)
    {
        var (_, prixVente) = animalchoisi.Calculprix();
        bool check = habitatchoisi.verifType(animalchoisi, habitatchoisi);
        if (!check)        {
            Console.WriteLine("L'animal ne correspond pas au type de l'habitat."); 
            return false;
        }
        _bank.RetirerArgent(prixVente);
        habitatchoisi.AjouterAnimal(animalchoisi);
        AnimalsAVendre.Remove(animalchoisi);
        return true;
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

        return AcheterAnimal(AnimalsAVendre[indexAnimal], habitatChoisi);
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

    public bool VendreHabitatParIndex(int indexHabitat, List<Habitat> habitatsDuZoo)
    {
        if (indexHabitat < 0 || indexHabitat >= habitatsDuZoo.Count)
        {
            return false ;
        }

        VendreHabitat(habitatsDuZoo[indexHabitat], habitatsDuZoo);
        return true;
    }

    private void VendreHabitat(Habitat habitatchoisi, List<Habitat> habitatsDuZoo)
    {
        if (habitatchoisi.Animaux.Count > 0)
        {
            Console.WriteLine("Impossible de vendre un habitat qui contient des animaux. Veuillez d'abord retirer les animaux de cet habitat.");
            return;
        }
        _bank.AjouterArgent(habitatchoisi.prix_vente);
        habitatsDuZoo.Remove(habitatchoisi);
    }

}