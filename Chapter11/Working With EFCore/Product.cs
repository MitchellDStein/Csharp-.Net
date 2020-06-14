using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Working_With_EFCore
{
    // represents a row in the products table in Northwind DB
    public class Product
    {
        public int ProductID { get; set; }

        [Required]
        [StringLength(40)]
        public string ProductName { get; set; }

        [Column(TypeName="int")]
        public int SupplierID { get; set; }

        [Column("UnitPrice", TypeName = "money")]
        public decimal? Cost { get; set; }      // check if NULL

        [Column("UnitsInStock")]
        public short? Stock { get; set; }       // check if NULL

        public bool Discontinued { get; set; }

        // these two define the foreign key relationship to the categories table
        public int CategoryID { get; set; }
        public virtual Category Category { get; set; }
    }
}