class Bank
{
    private int Solde;
    private string monnaie; 

    public Bank()
    {
        Console.Write("Choisissez votre Monnaie (€, $, £ ou Autre) :");
        string? monnaieInput = Console.ReadLine();
        monnaie = string.IsNullOrWhiteSpace(monnaieInput) ? "€" : monnaieInput.Trim();
        Solde = 80000;
    }

    public bool RetirerArgent(int nb)
    {
        if (nb > Solde)
        {
            Console.WriteLine("Fonds insuffisants pour cette transaction.");
            return true;
        }
        Solde = Solde - nb;
        return false;
    }

    public void AjouterArgent(int nb)
    {
        Solde = Solde + nb;       
    }

    public string AfficherSolde()
    {
        return $"Votre solde actuel est de : {Solde} {monnaie}";
    }
}