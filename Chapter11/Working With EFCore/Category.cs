using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Working_With_EFCore
{
    public class Category
    {
        // these properties map to columns in the database
        public int CategoryID { get; set; }

        public string CategoryName { get; set; }

        // get column with data type ntext and set it to Description string
        [Column(TypeName = "ntext")]
        public string Description { get; set; }

        //defines a navigation property for related rows
        public virtual ICollection<Product> Products { get; set; }  //used to represent the products table

        public Category()
        {
            //to enable developers to add prodcts to a category we must initialize the navigation property to an empty list.
            this.Products = new List<Product>();
        }
    }
}