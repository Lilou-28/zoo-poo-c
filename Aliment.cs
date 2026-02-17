public class aliment
{
    public int prix_kg
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
    }

    int _prix_kg ;
    public void aliment()
    {
        aliment = new List<aliment>();
    }

    public List<aliment> _aliments;

    public void AfficherAliment()
    {
        foreach (aliment alim in aliment)
        {
            Console.WriteLine();
        }
    }

    
}

public class viande : aliment
{
    public viande(){
        prix_kg = 5;
    }
}

public class graine : aliment
{
    public graine()
    {
        prix_kg = 2.5;
    }
}