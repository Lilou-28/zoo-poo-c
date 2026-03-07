class Program
{
	static void Main(string[] args)
	{
		AfficherTitre();

		bool quitter = false;
		while (!quitter)
		{
			AfficherMenuPrincipal();
			string? choix = Console.ReadLine();

			switch (choix)
			{
				case "1":
					DemarrerNouvellePartie();
					break;
				case "2":
					AfficherAide();
					break;
				case "3":
					Console.WriteLine("À bientôt !");
					quitter = true;
					break;
				default:
					Console.WriteLine("Choix invalide. Entrez 1, 2 ou 3.");
					break;
			}

			if (!quitter)
			{
				Console.WriteLine("\nAppuyez sur Entrée pour revenir au menu...");
				Console.ReadLine();
				Console.Clear();
				AfficherTitre();
			}
		}
	}

	static void AfficherTitre()
	{
		Console.WriteLine("==============================");
		Console.WriteLine("     BIENVENUE DANS LE ZOO    ");
		Console.WriteLine("==============================\n");
	}

	static void AfficherMenuPrincipal()
	{
		Console.WriteLine("Menu principal :");
		Console.WriteLine("1. Nouvelle partie");
		Console.WriteLine("2. Aide");
		Console.WriteLine("3. Quitter");
		Console.Write("Votre choix : ");
	}

	static void DemarrerNouvellePartie()
	{
		Console.Clear();
		AfficherTitre();
		Console.WriteLine("Création d'une nouvelle partie...\n");

		Zoo zoo = new Zoo();
		zoo.AfficherMenuAchat();

		Console.WriteLine("\nPartie démarrée avec succès.");
		Console.WriteLine("Retour au menu principal.");
	}

	static void AfficherAide()
	{
		Console.WriteLine("\nAide rapide :");
		Console.WriteLine("- Choisissez 'Nouvelle partie' pour créer votre zoo.");
		Console.WriteLine("- Suivez les questions affichées à l'écran.");
		Console.WriteLine("- Revenez ensuite au menu pour continuer.");
	}
}
