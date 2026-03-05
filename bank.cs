class bank
{
    private int quantité ;
    private string monnaie; 

    public bank()
    {
        Console.Write("Choisissez votre Monnaie (€, $, £ ou Autre) :");
        monnaie = Console.ReadLine() ?? "€"; 
        quantité = 80000 ;
    }

    public void RetirerArgent(int nb)
    {
        quantité = quantité - nb;
    }

    public void AjouterArgent(int nb)
    {
        quantité = quantité + nb; 
    }
}