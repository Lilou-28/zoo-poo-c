public abstract class Aliment
{
    private double _prixKg;
    private int _limite;
    private int _stockCourant;

    public double PrixKg
    {
        get => _prixKg;
        set => _prixKg = value >= 0 ? value : 0;
    }

    public int Limite
    {
        get => _limite;
        set => _limite = value >= 0 ? value : 0;
    }

    public int StockCourant
    {
        get => _stockCourant;
        set => _stockCourant = value >= 0 ? value : 0;
    }

    protected Aliment(int limite)
    {
        Limite = limite;
        StockCourant = 0;
    }

    public bool AjouterAliment(int quantite)
    {
        if (quantite <= 0)
            return false;
        
        if ((quantite + StockCourant) <= Limite)
        {
            StockCourant += quantite;
            return true;
        }
        return false;
    }

    public bool SupprimerAliment(int quantite)
    {
        if (quantite <= 0)
            return false;
        
        if (quantite <= StockCourant)
        {
            StockCourant -= quantite;
            return true;
        }
        return false;
    }
}

public class Viande : Aliment
{
    public Viande() : base(2295)
    {
        PrixKg = 5;
    }
}

public class Graines : Aliment
{
    public Graines() : base(180)
    {
        PrixKg = 2.5;
    }
}