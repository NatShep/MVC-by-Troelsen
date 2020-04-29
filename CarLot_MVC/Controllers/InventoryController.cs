using System;
using System.Data.Entity.Infrastructure;
using System.Net;
using System.Web.Mvc;
using AutoLotDal.Models;
using AutoLotDal.Repos;

namespace CarLot_MVC.Controllers
{
    public class InventoryController : Controller
    {
        
        private readonly InventoryRepo _repo = new InventoryRepo();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _repo.Dispose();
            base.Dispose(disposing);
        }

        // GET
        public ActionResult Index()
        {
            return View(_repo.GetAll());
        }
        
        //GET Inventory/Details/5
        public ActionResult Details(int? id)
        {
            if (id==null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Inventory inventory = _repo.GetOne( id);
            if (inventory == null)
                return HttpNotFound();
            return View(inventory);
        }
        
        //Get: Inventory/Create
        public ActionResult Create()
        {
            return View();
        }
        
        //Post: 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Make,Color,PetName")] Inventory inventory)
        {
            if (!ModelState.IsValid) return View(inventory);
            try
            {
                _repo.Add(inventory);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,$"Unable to create record: {e.Message}");
                return View(inventory);
            }
            return RedirectToAction("Index");
        }
        
        //Get: Inventory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id==null)
               return   new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
                return HttpNotFound();
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Make,Color,PetName,Timestamp")]
            Inventory inventory)
        {
            if (!ModelState.IsValid)
                return View(inventory);
            try
            {
                _repo.Save(inventory);
            }
            catch (DbUpdateConcurrencyException e)
            {
                ModelState.AddModelError(string.Empty, $"Unable to save the record.Another user has updated it.");
                return View(inventory);
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty,$"Unable to Save the record: {e.Message}");
                return View(inventory);
            }

            return RedirectToAction("index");
        }
        
        //Get: Inventory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            Inventory inventory = _repo.GetOne(id);
            if (inventory == null)
                return HttpNotFound();
            var ordersCount = inventory.Orders.Count;
           
            return View(inventory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete([Bind(Include = "Id,TimeStamp")] Inventory inventory)
        {
            
            var ordersCount= _repo.GetOne(inventory.Id).Orders.Count;
     //       if (ordersCount > 0)
       //     {
         //       ModelState.AddModelError(string.Empty, $"Unable to delete the record. This records has {ordersCount} orders");
           //     return View(inventory);
          //  }
            try
            {
                _repo.Delete(inventory);
            }
            catch (DbUpdateConcurrencyException e)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete the record.Another user has updated it.");
            }
            catch (Exception e)
            {
                ModelState.AddModelError(string.Empty, $"Unable to delete the record: {e.Message}");
            }

            return RedirectToAction("Index");
        }
    }
}