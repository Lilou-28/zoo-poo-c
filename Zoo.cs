class Zoo
{
    private string _nom ; 

    private List<Habitat> _habitats;
    private Marchant _marchant;

    private Bank _bank;
    public Zoo()
    {
        Console.Write("Entrez le nom de votre nouveau Zoo :");
        _nom = Console.ReadLine() ?? "Mon Zoo";
        _habitats = new List<Habitat>(); 
        _bank = new Bank();

        _marchant = new Marchant(_bank);
        _marchant.InitialiserAnimauxAVendre();
        _marchant.InitialiserHabitatsAVendre();
    }

    public void AfficherMenuAchat()
    {
        bool retour = false;

        while (!retour)
        {
            Console.WriteLine("\n--- Menu Zoo ---");
            Console.WriteLine("1. Menu marchant");
            Console.WriteLine("2. Voir mes habitats");
            Console.WriteLine("3. Voir mon solde");
            Console.WriteLine("4. Retour");
            Console.Write("Votre choix : ");

            string? choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AfficherMenuMarchant();
                    break;
                case "2":
                    AfficherHabitatsDuZoo();
                    break;
                case "3":
                    Console.WriteLine(_bank.AfficherSolde());
                    break;
                case "4":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }
    }

    private void AfficherMenuMarchant()
    {
        bool retour = false;

        while (!retour)
        {
            Console.WriteLine("\n--- Menu Marchant ---");
            Console.WriteLine("1. Acheter un habitat");
            Console.WriteLine("2. Acheter un animal");
            Console.WriteLine("3. Vendre un animal ou habitat");
            Console.WriteLine("4. Retour");
            Console.Write("Votre choix : ");

            string? choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AcheterHabitatMenu();
                    break;
                case "2":
                    AcheterAnimalMenu();
                    break;
                case "3":
                    VendreMenu();
                    break;
                case "4":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }
    }

    private void AcheterHabitatMenu()
    {
        Console.WriteLine("\nHabitats disponibles :");
        _marchant.AfficherHabitatsAAcheter();

        if (_marchant.NombreHabitatsAVendre == 0)
        {
            return;
        }

        Console.Write("Numero de l'habitat a acheter : ");
        if (!int.TryParse(Console.ReadLine(), out int choix))
        {
            Console.WriteLine("Entree invalide.");
            return;
        }

        if (choix == 0)
        {
            return;
        }

        bool ok = _marchant.AcheterHabitatParIndex(choix - 1, _habitats);
        Console.WriteLine(ok ? "Habitat achete avec succes." : "Retour au menu.");
        Console.WriteLine(ok ? _bank.AfficherSolde() : "");
    
    }

    private void AcheterAnimalMenu()
    {
        if (_habitats.Count == 0)
        {
            Console.WriteLine("Achetez d'abord un habitat avant d'acheter un animal.");
            return;
        }

        Console.WriteLine("\nChoisissez un habitat pour accueillir l'animal :");
        for (int i = 0; i < _habitats.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_habitats[i].Type} (Capacite: {_habitats[i].Capacité})");
        }

        Console.Write("Numero de l'habitat : ");
        if (!int.TryParse(Console.ReadLine(), out int indexHabitat))
        {
            Console.WriteLine("Entree invalide.");
            return;
        }

        if (indexHabitat < 1 || indexHabitat > _habitats.Count)
        {
            Console.WriteLine("Selection invalide.");
            return;
        }

        Console.WriteLine("\nAnimaux disponibles :");
        _marchant.AfficherAnimauxAAcheter();

        if (_marchant.NombreAnimauxAVendre == 0)
        {
            return;
        }
        
        Console.Write("Numero de l'animal a acheter : ");
        if (!int.TryParse(Console.ReadLine(), out int indexAnimal))
        {
            Console.WriteLine("Entree invalide.");
            return;
        }

        bool ok = _marchant.AcheterAnimalParIndex(indexAnimal - 1, _habitats[indexHabitat - 1]);
        Console.WriteLine(ok ? "Animal achete avec succes." : "Achat annulé.");
        Console.WriteLine(ok ? _bank.AfficherSolde() : "");
    }

    private void AfficherHabitatsDuZoo()
    {
        if (_habitats.Count == 0)
        {
            Console.WriteLine("Aucun habitat dans le zoo.");
            return;
        }

        Console.WriteLine("\nHabitats du zoo :");
        for (int i = 0; i < _habitats.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_habitats[i].Type} (Capacite: {_habitats[i].Capacité})");
        }

        Console.Write("Numero de l'habitat pour voir les animaux : ");
        
        if (!int.TryParse(Console.ReadLine(), out int indexHabitat))
        {
            Console.WriteLine("Entree invalide.");
            return;
        }

        if (indexHabitat < 1 || indexHabitat > _habitats.Count)
        {
            Console.WriteLine("Selection invalide.");
            return;
        }
        if (_habitats[indexHabitat - 1] is Habitat habitat)
        {
            habitat.AfficherAnimaux();
        }
    }

    private void AfficherHabitatsVente()
    {
        if (_habitats.Count == 0)
        {
            Console.WriteLine("Aucun habitat dans le zoo.");
            return;
        }

        Console.WriteLine("\nHabitats du zoo :");
        for (int i = 0; i < _habitats.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_habitats[i].Type} (Capacite: {_habitats[i].Capacité})");
        }
    }
    private void VendreMenu()
    {
        Console.WriteLine("\n--- Menu Vente ---");
        Console.WriteLine("1. Vendre un animal");
        Console.WriteLine("2. Vendre un habitat");
        Console.WriteLine("3. Retour");
        Console.Write("Votre choix : ");

        string? choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                Console.WriteLine("Vente d'animaux desactivee pour simplifier le projet.");
                break;
            case "2":
                VendreHabitatMenu();
                break;
            case "3":
                return;
            default:
                Console.WriteLine("Choix invalide.");
                break;
        }
    }

    private void VendreHabitatMenu()
    {
        Console.WriteLine("\nHabitats disponibles :");
        AfficherHabitatsVente();

        if (_habitats.Count == 0)
        {
            Console.WriteLine("Aucun habitat a vendre.");
            return;
        }

        Console.Write("Numero de l'habitat a vendre : ");
        if (!int.TryParse(Console.ReadLine(), out int choix))
        {
            Console.WriteLine("Entree invalide.");
            return;
        }

        if (choix == 0)
        {
            return;
        }

        bool ok = _marchant.VendreHabitatParIndex(choix - 1 , _habitats);
        Console.WriteLine(ok ? "Habitat vendu avec succes." : "Retour au menu.");
        Console.WriteLine(ok ? _bank.AfficherSolde() : "");
        return;
    }
}
