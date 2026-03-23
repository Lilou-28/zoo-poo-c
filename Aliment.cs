public class aliment
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

    public List<viande> viandes;

    public List<graine> graines;

    int _limite ;

    double _prix_kg ;

    public aliment()
    {
        viandes = new List<viande>();
        graines = new List<graine>();
    }
    public aliment(int limite)
    {
        this.limite = limite;
        viandes = new List<viande>();
        graines = new List<graine>();
    }

    public void AjouterAliment(int valeurchoisie)
    {
        if ((valeurchoisie + stockcourrant) < limite)
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

public class viande : aliment
{
    public viande(){
        prix_kg = 5;
        limite = 4500;
        stockcourrant = 0;
    }
}

public class graine : aliment
{
    public graine()
    {
        prix_kg = 2.5;
        limite = 500;
        stockcourrant = 0;
    }
}