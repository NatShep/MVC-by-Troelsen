using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDal.Models.Base;

namespace AutoLotDal.Models
{
    [Table("CreditRisks")]

    public class CreditRisk:EntityBase
    {
        [StringLength(50)]
        [Index("IDX_CreditRisk_Name", IsUnique = true,Order=2)]
        public string LastName { get; set; }
        [StringLength(50)]
        [Index("IDX_CreditRisk_Name", IsUnique = true,Order=1)]
        public string FirstName { get; set; }
    }
}