using System.Xml.Serialization;
namespace Exercise
{

    [XmlInclude(typeof(Circle))]
    [XmlInclude(typeof(Rectangle))]
    public abstract class Shape
    {
        public string Color { get; set; }
        public abstract double Area { get; }
    }
}