using System.ComponentModel.DataAnnotations.Schema;
using AutoLotDal.Models.Base;

namespace AutoLotDal.Models
{
    public class Order: EntityBase
    {
        public int CustId { get; set; }
        public int CarId { get; set; }
        [ForeignKey(nameof(CustId))]
        public virtual Customer Customer { get; set; }
        [ForeignKey(nameof(CarId))]
        public virtual Inventory Inventory { get; set; }
        
    }
}