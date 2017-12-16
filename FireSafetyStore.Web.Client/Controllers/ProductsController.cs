using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FireSafetyStore.Web.Client;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;

namespace FireSafetyStore.Web.Client.Controllers
{
    public class ProductsController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.UnitMaster);
            return View(await products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            ViewBag.UnitId = new SelectList(db.UnitMasters, "UnitId", "Description");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ItemId,ItemName,Description,BrandId,CategoryId,UnitId,Rate,ImageUrl,Stock,UpdatedAt,IsActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.ItemId = Guid.NewGuid();
                product.UpdatedAt = DateTime.UtcNow;
                product.IsActive = true;
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.UnitId = new SelectList(db.UnitMasters, "UnitId", "Description", product.UnitId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.UnitId = new SelectList(db.UnitMasters, "UnitId", "Description", product.UnitId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,ItemName,Description,BrandId,CategoryId,UnitId,Rate,ImageUrl,Stock,UpdatedAt,IsActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.UpdatedAt = DateTime.UtcNow;
                db.Entry(product).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.UnitId = new SelectList(db.UnitMasters, "UnitId", "Description", product.UnitId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await db.Products.FindAsync(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            Product product = await db.Products.FindAsync(id);
            db.Products.Remove(product);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
