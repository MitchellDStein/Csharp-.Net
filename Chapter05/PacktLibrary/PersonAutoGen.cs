namespace Packt.Shared
{
    public partial class Person
    {
        // C# can use Properties to also work similarly to methods,
        // such as GetOrigin from PersonArgs.cs

        // a property defined using C# 1-5 syntax
        public string Origin
        {
            get
            {
                return $"{Name} was born on {HomePlanet}";
            }
        }

        // two properties defined using C# 6+ lambda expression syntax
        public string Greeting => $"{Name} says 'Hello!'";

        public int Age => System.DateTime.Today.Year - DateOfBirth.Year;

        // Settable Properties
        public string FavoriteIceCream { get; set; } // auto-syntax

        public string favoritePrimaryColor;
        public string FavoritePrimaryColor
        {
            get
            {
                return favoritePrimaryColor;
            }
            set
            {
                switch (value.ToLower())
                {
                    case "red":
                    case "green":
                    case "blue":
                        favoritePrimaryColor = value;
                        break;
                    default:
                        throw new System.ArgumentException(
                           $"{value} is not a primary color! " +
                           "Choose from: red, green, blue.");
                }
            }
        }

        // Indexers
        public Person this[int index]
        {
            get
            {
                return Children[index];
            }
            set
            {
                Children[index] = value;
            }
        }
    }
}