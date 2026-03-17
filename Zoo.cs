using System.ComponentModel.Design;
using System.Runtime.CompilerServices;

class Zoo
{
    private string _nom ; 

    private List<Habitat> _habitatsZoo;
    private Marchand _marchand;

    private Bank _bank;

    private Tour _tours;

    private static void TemporisationCourte()
    {
        Thread.Sleep(1500);
    }

    public Zoo()
    {
        Console.Write("Entrez le nom de votre nouveau Zoo :");
        string? nomSaisi = Console.ReadLine();
        _nom = string.IsNullOrWhiteSpace(nomSaisi) ? "Mon Zoo" : nomSaisi.Trim();
        _habitatsZoo = new List<Habitat>(); 
        _bank = new Bank();

        _marchand = new Marchand(_bank);
        _marchand.InitialiserAnimauxAVendre();
        _marchand.InitialiserHabitatsAVendre();
        _tours = new Tour();
    }

    public void AfficherMenu()
    {
        bool retour = false;

        while (!retour)
        {
            Console.Clear();
            Console.WriteLine("\n--- Menu Zoo ---");
            Console.WriteLine("1. Menu marchand");
            Console.WriteLine("2. Voir mes habitats");
            Console.WriteLine("3. Mon Zoo");
            Console.WriteLine("4. Tours suivants");
            Console.WriteLine("5. Retour");
            Console.Write("Votre choix : ");

            string? choix = Console.ReadLine();

            switch (choix)
            {
                case "1":
                    AfficherMenuMarchand();
                    break;
                case "2":
                    AfficherHabitatsDuZoo();
                    break;
                case "3":
                    AfficherInfoMenu();
                    break;
                case "4":
                    AfficherToursSuivants();
                    break;
                case "5":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    TemporisationCourte();
                    break;
            }
        }
    }
    
    private void AfficherToursSuivants()
    {
        Console.Clear();
        _tours.ProchainTour();
        Console.WriteLine("Les animaux vieillissent, et de nouveaux événements peuvent survenir...");
        Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
        Console.ReadLine();
        foreach (var habitat in _habitatsZoo)
        {
            habitat.FaireVieillirAnimaux();
        }
    }
    private void AfficherMenuMarchand()
    {
        bool retour = false;

        while (!retour)
        {
            Console.Clear();
            Console.WriteLine("\n--- Menu Marchand ---");
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
                    TemporisationCourte();
                    break;
            }
        }
    }

    private void AcheterHabitatMenu()
    {
        Console.Clear();
        Console.WriteLine("\nHabitats disponibles :");
        _marchand.AfficherHabitatsAAcheter();

        if (_marchand.NombreHabitatsAVendre == 0)
        {
            TemporisationCourte();
            return;
        }

        Console.Write("Numero de l'habitat a acheter : ");
        if (!int.TryParse(Console.ReadLine(), out int choix))
        {
            Console.WriteLine("Entree invalide.");
            TemporisationCourte();
            return;
        }

        if (choix == 0)
        {
            return;
        }

        bool ok = _marchand.AcheterHabitatParIndex(choix - 1, _habitatsZoo);
        Console.WriteLine(ok ? "Habitat achete avec succes." : "Retour au menu.");
        if (ok) Console.WriteLine(_bank.AfficherSolde());
        Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
        Console.ReadLine();
    }

    private void AcheterAnimalMenu()
    {
        Console.Clear();
        if (_habitatsZoo.Count == 0)
        {
            Console.WriteLine("Achetez d'abord un habitat avant d'acheter un animal.");
            TemporisationCourte();
            return;
        }

        Console.WriteLine("\nChoisissez un habitat pour accueillir l'animal :");
        for (int i = 0; i < _habitatsZoo.Count; i++)
        {
            int places = _habitatsZoo[i].Capacité - _habitatsZoo[i].Animaux.Count;
            Console.WriteLine($"{i + 1}. {_habitatsZoo[i].Type} ({places} place(s) libre(s))");
        }
        Console.WriteLine("0. Retour");

        Console.Write("Numero de l'habitat : ");
        if (!int.TryParse(Console.ReadLine(), out int indexHabitat))
        {
            Console.WriteLine("Entree invalide.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }

        if (indexHabitat == 0) return;

        if (indexHabitat < 1 || indexHabitat > _habitatsZoo.Count)
        {
            Console.WriteLine("Selection invalide.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }

        if (_habitatsZoo[indexHabitat - 1].Animaux.Count >= _habitatsZoo[indexHabitat - 1].Capacité)
        {
            Console.WriteLine("Cet habitat est plein.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }

        string typeHabitat = _habitatsZoo[indexHabitat - 1].Type;
        Console.WriteLine("\nAnimaux disponibles :");
        var indices = _marchand.AfficherAnimauxAAcheter(typeHabitat);

        if (indices.Count == 0)
        {
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }
        
        Console.Write("Numero de l'animal a acheter (0 pour annuler) : ");
        if (!int.TryParse(Console.ReadLine(), out int choixAnimal))
        {
            Console.WriteLine("Entree invalide.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }

        if (choixAnimal == 0) return;

        if (choixAnimal < 1 || choixAnimal > indices.Count)
        {
            Console.WriteLine("Selection invalide.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }

        int indexAnimal = indices[choixAnimal - 1];
        bool ok = _marchand.AcheterAnimalParIndex(indexAnimal, _habitatsZoo[indexHabitat - 1]);
        Console.WriteLine(ok ? "Animal achete avec succes." : "Achat annulé.");
        if (ok) Console.WriteLine(_bank.AfficherSolde());
        Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
        Console.ReadLine();
    }

    private void AfficherHabitatsDuZoo()
    {
        Console.Clear();
        if (_habitatsZoo.Count == 0)
        {
            Console.WriteLine("Aucun habitat dans le zoo.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }

        Console.WriteLine("\nHabitats du zoo :");
        for (int i = 0; i < _habitatsZoo.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_habitatsZoo[i].Type} ({_habitatsZoo[i].Animaux.Count}/{_habitatsZoo[i].Capacité} animaux)");
        }
        Console.WriteLine("0. Retour");

        Console.Write("Numero de l'habitat pour voir les animaux : ");
        
        if (!int.TryParse(Console.ReadLine(), out int indexHabitat))
        {
            Console.WriteLine("Entree invalide.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }

        if (indexHabitat == 0)
        {

            return;
        }

        if (indexHabitat < 1 || indexHabitat > _habitatsZoo.Count)
        {
            Console.WriteLine("Selection invalide.");
            Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
            Console.ReadLine();
            return;
        }
        if (_habitatsZoo[indexHabitat - 1] is Habitat habitat)
        {
            habitat.AfficherAnimaux();
            Console.Write("Entrez l'ID de l'animal pour voir ses infos (0 pour retour) : ");
            string? choix = Console.ReadLine();
            if (choix == "0")
            {
                return;
            }
            else if (int.TryParse(choix, out int id))
            {
                Animal? animalSelectionne = habitat.Animaux.FirstOrDefault(a => a.Id == id);
                if (animalSelectionne != null)
                {
                    animalSelectionne.AfficherInfos();
                }
                else
                {
                    Console.WriteLine("ID introuvable dans cet habitat.");
                    Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
                    Console.ReadLine();
                }
            }
        }
    }

    private void AfficherHabitatsVente()
    {
        if (_habitatsZoo.Count == 0)
        {
            Console.WriteLine("Aucun habitat dans le zoo.");
            TemporisationCourte();
            return;
        }

        Console.WriteLine("\nHabitats du zoo :");
        for (int i = 0; i < _habitatsZoo.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_habitatsZoo[i].Type} (Capacite: {_habitatsZoo[i].Capacité})");
        }
    }
    private void VendreMenu()
    {
        Console.Clear();
        Console.WriteLine("\n--- Menu Vente ---");
        Console.WriteLine("1. Vendre un animal");
        Console.WriteLine("2. Vendre un habitat");
        Console.WriteLine("3. Retour");
        Console.Write("Votre choix : ");

        string? choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                VendreAnimalMenu();
                break;
            case "2":
                VendreHabitatMenu();
                break;
            case "3":
                return;
            default:
                Console.WriteLine("Choix invalide.");
                TemporisationCourte();
                break;
        }
    }

    private void VendreHabitatMenu()
    {
        Console.Clear();
        Console.WriteLine("\nHabitats disponibles :");
        AfficherHabitatsVente();

        if (_habitatsZoo.Count == 0)
        {
            Console.WriteLine("Aucun habitat a vendre.");
            TemporisationCourte();
            return;
        }

        Console.Write("Numero de l'habitat a vendre : ");
        if (!int.TryParse(Console.ReadLine(), out int choix))
        {
            Console.WriteLine("Entree invalide.");
            TemporisationCourte();
            return;
        }

        if (choix == 0)
        {
            return;
        }

        bool ok = _marchand.VendreHabitatParIndex(choix - 1 , _habitatsZoo);
        Console.WriteLine(ok ? "Habitat vendu avec succes." : "Retour au menu.");
        Console.WriteLine(ok ? _bank.AfficherSolde() : "");
        TemporisationCourte();
        return;
    }

    private void VendreAnimalMenu()
    {
        Console.Clear();
        if (_habitatsZoo.Count == 0)
        {
            Console.WriteLine("Aucun habitat dans le zoo.");
            TemporisationCourte();
            return;
        }

        Console.WriteLine("\nChoisissez un habitat pour vendre un animal :");
        for (int i = 0; i < _habitatsZoo.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {_habitatsZoo[i].Type} ({_habitatsZoo[i].Animaux.Count} animal(aux))");
        }
        Console.WriteLine("0. Retour");

        Console.Write("Numero de l'habitat : ");
        if (!int.TryParse(Console.ReadLine(), out int indexHabitat))
        {
            Console.WriteLine("Entree invalide.");
            TemporisationCourte();
            return;
        }

        if (indexHabitat == 0) return;

        if (indexHabitat < 1 || indexHabitat > _habitatsZoo.Count)
        {
            Console.WriteLine("Selection invalide.");
            TemporisationCourte();
            return;
        }

        Habitat habitat = _habitatsZoo[indexHabitat - 1];
        if (habitat.Animaux.Count == 0)
        {
            Console.WriteLine("Aucun animal a vendre dans cet habitat.");
            TemporisationCourte();
            return;
        }

        Console.WriteLine("\nAnimaux disponibles a la vente :");
        foreach (var animal in habitat.Animaux)
        {
            Console.WriteLine($"ID: {animal.Id} - {animal.Nom} - Sexe: {animal.Sexe} - Age: {animal.Age} - Prix: {animal.Calculprix().Item2}€");
        }
        Console.Write("Entrez l'ID de l'animal a vendre (0 pour annuler) : ");
        
        string? choix = Console.ReadLine();
        
        if (choix == "0") return;

        if (!int.TryParse(choix, out int idAnimal))
        {
            Console.WriteLine("Entree invalide.");
            TemporisationCourte();
            return;
        }

        bool ok = _marchand.VendreAnimalParId(idAnimal, habitat);
        Console.WriteLine(ok ? "Animal vendu avec succes." : "Vente annulee.");
        TemporisationCourte();
        if (ok) Console.WriteLine(_bank.AfficherSolde());
    }

    private void AfficherInfoMenu()
    {
        Console.Clear();
        Console.WriteLine("\n---Infos---");
        Console.WriteLine($"- Nom de ton Zoo : {_nom}");
        Console.WriteLine($"- {_bank.AfficherSolde()}");
        Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu...");
        Console.ReadLine();
    }
}  
