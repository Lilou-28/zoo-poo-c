public class Aliment
{
    public double prix_kg
    {
        get
        {
            return _prix_kg;
        }
        set
        {
            _prix_kg = value;
        }
    }

    public int limite
    {
        get
        {
            return _limite;
        }
        set
        {
            _limite = value;
        }
    }

    public int stockcourrant
    {
        get
        {
            return _stockcourrant;
        }
        set
        {
            _stockcourrant = value;
        }
    }

    int _stockcourrant;

    int _limite ;

    double _prix_kg ;

    public Aliment()
    {
    }

    public Aliment(int limite)
    {
        this.limite = limite;
    }

    public void AjouterAliment(int valeurchoisie)
    {
        if ((valeurchoisie + stockcourrant) <= limite)
        {
            stockcourrant = stockcourrant + valeurchoisie;
        }
    }

    public void SupprimerAliment(int valeurchoisie)
    {
        if (valeurchoisie <= stockcourrant)
        {
            stockcourrant = stockcourrant - valeurchoisie;
        }
    }
}

public class Viande : Aliment
{
    public Viande(){
        prix_kg = 5;
        limite = 4500;
        stockcourrant = 0;
    }
}

public class Graine : Aliment
{
    public Graine()
    {
        prix_kg = 2.5;
        limite = 500;
        stockcourrant = 0;
    }
}