class Zoo
{
    private string _nom ; 

    private List<Habitat> _habitats;

    public Zoo()
    {
        Console.Write("Entrez le nom de votre nouveau Zoo :");
        _nom = Console.ReadLine();
        _habitats = new List<Habitat>(); 
        
    }
}  