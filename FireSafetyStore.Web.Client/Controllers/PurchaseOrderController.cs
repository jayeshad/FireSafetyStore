using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using System.IO;

namespace FireSafetyStore.Web.Client.Controllers
{
    [Authorize]
    public class PurchaseOrderController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

        // GET: PurchaseOrder
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category).Include(p => p.Supplier);
            return View(await products.ToListAsync());
        }

        // GET: PurchaseOrder/Details/5
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

        // GET: PurchaseOrder/Create
        public ActionResult Create()
        {
            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description");
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description");
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name");
            return View();
        }

        // POST: PurchaseOrder/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file == null || file.ContentLength <= 0)
                {
                    ModelState.AddModelError("FILEUPLOADERROR", "Product Image is Mandatory !");
                }
                else
                {
                    product.ItemId = Guid.NewGuid();
                    product.UpdatedAt = DateTime.UtcNow;
                    product.Image = new byte[file.ContentLength];
                    file.InputStream.Read(product.Image, 0, file.ContentLength);
                    string ImageName = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    product.OriginalFileName = ImageName;
                    string generatedname = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), extension);
                    string physicalPath = Server.MapPath("~/FileStore/Products/" + generatedname);
                    file.SaveAs(physicalPath);
                    product.ImagePath = "/FileStore/Products/" + generatedname;
                    product.IsActive = true;
                    db.Products.Add(product);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", product.SupplierId);
            return View(product);
        }

        // GET: PurchaseOrder/Edit/5
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
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", product.SupplierId);
            return View(product);
        }

        // POST: PurchaseOrder/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,ItemName,Description,SupplierId,BrandId,CategoryId,Quantity,Rate,Discount,OfferExpiryDate,Image,ImagePath,OriginalFileName,UpdatedAt,IsActive")] Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                db.Products.Attach(product);
                db.Entry(product).Property(x => x.ItemName).IsModified = true;
                db.Entry(product).Property(x => x.Description).IsModified = true;
                db.Entry(product).Property(x => x.BrandId).IsModified = true;
                db.Entry(product).Property(x => x.CategoryId).IsModified = true;
                db.Entry(product).Property(x => x.Rate).IsModified = true;
                db.Entry(product).Property(x => x.Quantity).IsModified = true;
                if (file != null)
                {
                    product.Image = new byte[file.ContentLength];
                    file.InputStream.Read(product.Image, 0, file.ContentLength);
                    string ImageName = Path.GetFileName(file.FileName);
                    string extension = Path.GetExtension(file.FileName);
                    string generatedname = string.Format("{0}{1}", Guid.NewGuid().ToString("N"), extension);
                    string physicalPath = Server.MapPath("~/FileStore/Products/" + generatedname);
                    file.SaveAs(physicalPath);
                    product.OriginalFileName = ImageName;
                    product.ImagePath = "FileStore/Products/" + generatedname;
                    db.Entry(product).Property(x => x.ImagePath).IsModified = true;
                    db.Entry(product).Property(x => x.Image).IsModified = true;
                }

                product.UpdatedAt = DateTime.UtcNow;
                db.Entry(product).Property(x => x.UpdatedAt).IsModified = true;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
            ViewBag.SupplierId = new SelectList(db.Suppliers, "SupplierId", "Name", product.SupplierId);
            return View(product);
        }

        // GET: PurchaseOrder/Delete/5
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

        // POST: PurchaseOrder/Delete/5
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
