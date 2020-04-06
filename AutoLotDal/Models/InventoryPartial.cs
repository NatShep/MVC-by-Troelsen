using System.ComponentModel.DataAnnotations.Schema;

namespace AutoLotDal.Models
{
    public partial class Inventory
    {
        [NotMapped] public string FullName => $"{Make}+({Color})";
        public override string ToString()
        {
            return $"{this.PetName ?? "**No Name"} is AutoLotDal {this.Color} {this.Make} with ID {this.Id}.";
        }
    }
}