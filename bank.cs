class Bank
{
    private int Solde;
    private string monnaie; 

    public Bank()
    {
        Console.Write("Choisissez votre Monnaie (€, $, £ ou Autre) :");
        monnaie = Console.ReadLine() ?? "€"; 
        Solde = 80000 ;
    }

    public void RetirerArgent(int nb)
    {
        Solde = Solde - nb;
    }

    public void AjouterArgent(int nb)
    {
        Solde = Solde + nb;       
    }
}