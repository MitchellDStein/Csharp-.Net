using System; // DateTime
using System.Collections.Generic; // List<T>, HashSet<T>
using System.Xml.Serialization; // XmlSerializer
using System.IO; // FileStream
using static System.Console;
using static System.Environment;
using static System.IO.Path;

namespace Exercise
{
    class Program
    {
        static void Main(string[] args)
        {
            // create a list of Shapes to serialize
            var listOfShapes = new List<Shape>
                {
                new Circle      { Color = "Red",    Radius = 2.5 },
                new Rectangle   { Color = "Blue",   Height = 20.0, Width = 10.0 },
                new Circle      { Color = "Green",  Radius = 8.0 },
                new Circle      { Color = "Purple", Radius = 12.3 },
                new Rectangle   { Color = "Blue",   Height = 45.0, Width = 18.0 }
                };

            var xs = new XmlSerializer(typeof(List<Shape>));

            string xmlPath = Combine(CurrentDirectory, "shapes.xml");

            using (FileStream stream = File.Create(xmlPath))
            {
                xs.Serialize(stream, listOfShapes);
            }

            WriteLine("Written {0:N0} bytes of XML to {1}", new FileInfo(xmlPath).Length, xmlPath);
        
            using (FileStream xmlLoad = File.Open(xmlPath, FileMode.Open))
            {
                var loadedShapes = (List<Shape>)xs.Deserialize(xmlLoad);
                foreach (var item in loadedShapes)
                {
                    WriteLine("{0} is a {1} with an area of {2:N3}.",
                        item.GetType().Name, item.Color, item.Area);
                }
            }
        }
    }
}
