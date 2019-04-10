using System.ComponentModel;

namespace MemberMgmtSystem.Models
{
    using System.ComponentModel.DataAnnotations;

    public partial class Customers
    {
        [Key]
        public int CustomerID { get; set; }

        //        [Required]
        //        [StringLength(40)]
        //        public string CompanyName { get; set; }

        [Required]
        [MinLength(3), MaxLength(15)]
        [DisplayName("Name")]
        public string ContactName { get; set; }

        //        [StringLength(30)]
        //        public string ContactTitle { get; set; }

        [StringLength(60)]
        public string Address { get; set; }

        //
        //        [StringLength(15)]
        //        public string City { get; set; }
        //
        //        [StringLength(15)]
        //        public string Region { get; set; }
        //
        //        [StringLength(10)]
        //        public string PostalCode { get; set; }
        //
        //        [StringLength(15)]
        //        public string Country { get; set; }
        //

        [RegularExpression(@"^\d{4}-\d{6}")]
        public string Phone { get; set; }

        //
        //        [StringLength(24)]
        //        public string Fax { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }
    }
}