class Zoo
{
    private string _nom ; 

    private List<Habitat> _habitatsZoo;
    public IReadOnlyList<Habitat> HabitatsZoo
    {
        get { return _habitatsZoo; }
    }
    private Marchand _marchand;

    private Bank _bank;

    private Tour _tours;

    private Aliment StockNourritureGraines;
    private Aliment StockNourritureViande;

    private string _notificationSubvention = "";

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
        StockNourritureGraines = new Graine();
        StockNourritureViande = new Viande();
        _marchand = new Marchand(_bank);
        _marchand.InitialiserAnimauxAVendre();
        _marchand.InitialiserHabitatsAVendre();
        _tours = new Tour(this);

        _notificationSubvention = "Bienvenue ! Investissez dans des espèces protégées (Aigle, Tigre) pour obtenir des subventions chaque début d'année (Janvier).";
    }

    public void AfficherMenu()
    {
        bool retour = false;

        while (!retour)
        {
            Console.Clear();
            Console.WriteLine("\n--- Menu Zoo ---");
            if (!string.IsNullOrEmpty(_notificationSubvention))
            {
                Console.WriteLine($"*** NOTIFICATION : {_notificationSubvention} ***\n");
                _notificationSubvention = "";
            }
            Console.WriteLine("1. Menu marchand");
            Console.WriteLine("2. Voir mes habitats");
            Console.WriteLine("3. Mon Zoo");
            Console.WriteLine("4. Tours suivants");
            Console.WriteLine("0. Retour");
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
                case "0":
                    retour = true;
                    break;
                default:
                    Console.WriteLine("Choix invalide.");
                    TemporisationCourte();
                    break;
            }
        }
    }

    public bool SupprimerHabitat(Habitat habitat)
    {
        return _habitatsZoo.Remove(habitat);
    }
    
    private void AfficherToursSuivants()
    {
        Console.Clear();
        _tours.ProchainTour();
        Visiteur visiteurs = new Visiteur();
        visiteurs.VerifRevenu(_tours, _habitatsZoo, _bank);
        AppliquerSubventionsAnnuelles();
        Console.WriteLine("Les animaux vieillissent, et de nouveaux événements peuvent survenir...");
        Console.WriteLine("\nAppuyez sur Entrée pour continuer...");
        Console.ReadLine();
    }

    private void AppliquerSubventionsAnnuelles()
    {
        if (_tours.nbTours % 12 == 1)
        {
            int subventionTotale = 0;
            int nbAigles = 0;
            int nbTigres = 0;

            foreach (var habitat in _habitatsZoo)
            {
                foreach (var animal in habitat.Animaux)
                {
                    if (animal is Aigle)
                    {
                        subventionTotale += 2190;
                        nbAigles++;
                    }
                    else if (animal is Tigre)
                    {
                        subventionTotale += 43800;
                        nbTigres++;
                    }
                }
            }

            if (subventionTotale > 0)
            {
                _bank.AjouterArgent(subventionTotale);
                Console.WriteLine("\n--- Subventions Annuelles ---");
                Console.WriteLine($"Espèces protégées recensées : {nbAigles} Aigle(s), {nbTigres} Tigre(s).");
                Console.WriteLine($"Subvention de {subventionTotale} versée pour la préservation des espèces !");
                Console.WriteLine(_bank.AfficherSolde());

                _notificationSubvention = $"Vous avez reçu {subventionTotale} de subvention pour vos espèces protégées.";
            }
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
            Console.WriteLine("4. Acheter de la nourriture");
            Console.WriteLine("0. Retour");
            Console.WriteLine("");
            Console.WriteLine($"Qu'avez-vous a acheter ou vendre a M/Mme {_marchand.Nom}? ");
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
                    AcheterNourritureMenu();
                    break;
                case "0":
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
            Console.Write("0. Nourriture a dispositions");
            Console.Write("\nEntrez l'ID de l'animal pour voir ses infos (Entrée pour retour) : ");
            string? choix = Console.ReadLine();
            if (choix == "")
            {
                return;
            }
            else if (choix == "0")
            {
               AfficherMenuNourriture(habitat);
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
        Console.WriteLine("0. Retour");
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
            case "0":
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
        Console.WriteLine("- Stock de nourriture : " + StockNourritureGraines.stockcourrant + "/" + StockNourritureGraines.limite);
        Console.WriteLine("- Stock de viande : " + StockNourritureViande.stockcourrant + "/" + StockNourritureViande.limite);
        Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu...");
        Console.ReadLine();
    }
    private void AfficherMenuNourriture(Habitat habitat)
    {
        Console.Clear();
        Console.WriteLine($"Nourriture a disposition : {habitat.NourritureHabitat.stockcourrant}/{habitat.NourritureHabitat.limite}");
        Console.WriteLine("1. Ajouter de la nourriture");
        Console.WriteLine("2. Retirer de la nourriture");
        Console.WriteLine("0. Retour"); 

        Console.Write("Votre choix : ");
        string? choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                Console.Write("Quantite a ajouter : ");
                if (int.TryParse(Console.ReadLine(), out int quantiteAjout))
                {
                    if (quantiteAjout > 0)
                    {
                        habitat.NourritureHabitat.AjouterAliment(quantiteAjout);
                        Console.WriteLine("Nourriture ajoutee avec succes.");
                    }
                    else
                    {
                        Console.WriteLine("La quantite doit etre positive.");
                    }
                }
                break;

            case "2":
                Console.Write("Quantite a retirer : ");
                if (int.TryParse(Console.ReadLine(), out int quantiteRetirer))
                {
                    if (quantiteRetirer > 0)
                    {
                        habitat.NourritureHabitat.SupprimerAliment(quantiteRetirer);
                        Console.WriteLine("Nourriture retiree avec succes.");
                    }
                    else
                    {
                        Console.WriteLine("La quantite doit etre positive.");
                    }
                }
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Choix invalide.");
                TemporisationCourte();
                break;
        }
    }
    private void AcheterNourritureMenu()
    {
        Console.Clear();
        double prixGraines = new Graine().prix_kg;
        double prixViande = new Viande().prix_kg;
        Console.WriteLine("\n--- Acheter de la nourriture ---");
        Console.WriteLine($"Stock actuel de graines : {StockNourritureGraines.stockcourrant}/{StockNourritureGraines.limite}");
        Console.WriteLine($"Stock actuel de viande : {StockNourritureViande.stockcourrant}/{StockNourritureViande.limite}");
        Console.WriteLine($"1. Acheter des graines ({prixGraines}€ le kg)");
        Console.WriteLine($"2. Acheter de la viande ({prixViande}€ le kg)");
        Console.WriteLine("0. Retour");
        Console.Write("Votre choix : ");

        string? choix = Console.ReadLine();

        switch (choix)
        {
            case "1":
                Console.Write("Quantite de graines a acheter : ");
                if (int.TryParse(Console.ReadLine(), out int quantiteGraines))
                {
                    if (quantiteGraines <= 0)
                    {
                        Console.WriteLine("La quantite doit etre positive.");
                        break;
                    }

                    double prixKgGraines = prixGraines;
                    int coutTotal = (int)Math.Ceiling(quantiteGraines * prixKgGraines);
                    Console.WriteLine($"Cout total: {coutTotal} (prix: {prixKgGraines}/kg)");
                    if (_bank.RetirerArgent(coutTotal))
                    {
                        StockNourritureGraines.AjouterAliment(quantiteGraines);
                        Console.WriteLine("Graines achetees avec succes.");
                    }
                    else
                    {
                        Console.WriteLine("Solde insuffisant pour cet achat.");
                    }
                }
                break;

            case "2":
                Console.Write("Quantite de viande a acheter : ");
                if (int.TryParse(Console.ReadLine(), out int quantiteViande))
                {
                    if (quantiteViande <= 0)
                    {
                        Console.WriteLine("La quantite doit etre positive.");
                        break;
                    }

                    double prixKgViande = prixViande;
                    int coutTotal = (int)Math.Ceiling(quantiteViande * prixKgViande);
                    Console.WriteLine($"Cout total: {coutTotal} (prix: {prixKgViande}/kg)");
                    if (_bank.RetirerArgent(coutTotal))
                    {
                        StockNourritureViande.AjouterAliment(quantiteViande);
                        Console.WriteLine("Viande achetee avec succes.");
                    }
                    else
                    {
                        Console.WriteLine("Solde insuffisant pour cet achat.");
                    }
                }
                break;
            case "0":
                return;
            default:
                Console.WriteLine("Choix invalide.");
                TemporisationCourte();
                break;
        }
    }
}  
