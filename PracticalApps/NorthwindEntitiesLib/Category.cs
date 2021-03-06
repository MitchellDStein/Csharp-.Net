using System.Collections.Generic;

namespace Practical.Shared
{
  public class Category
  {
    public int CategoryID { get; set; }
    public string CategoryName { get; set; }
    public string Description { get; set; }

    // related entities
    public ICollection<Product> Products { get; set; }
  }
}