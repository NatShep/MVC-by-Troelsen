using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using AutoLotDal.Models;

namespace AutoLotDal.Repos
{
    public class InventoryRepo: BaseRepo<Inventory>
    {
        public override List<Inventory> GetAll() => Context.Inventory.OrderBy(x => x.PetName).ToList();
    }
}