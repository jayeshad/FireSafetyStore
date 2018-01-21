using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using System.IO;
using Kendo.Mvc.Extensions;

namespace FireSafetyStore.Web.Client.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

        // GET: Products
        public async Task<ActionResult> Index()
        {
            var products = db.Products.Include(p => p.Brand).Include(p => p.Category);
            return View(await products.ToListAsync());
        }


        private int GetCurrentStock(Guid itemId)
        {
            return db.Products.Count(x => x.ItemId == itemId);
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
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid && file != null && file.ContentLength > 0)
            {                
                product.ItemId = Guid.NewGuid();
                product.UpdatedAt = DateTime.UtcNow;
                product.Image = new byte[file.ContentLength];
                file.InputStream.Read(product.Image, 0, file.ContentLength);
                string ImageName = Path.GetFileName(file.FileName);
                string extension = Path.GetExtension(file.FileName);
                product.OriginalFileName = ImageName;
                string generatedname = string.Format("{0}{1}",Guid.NewGuid().ToString("N"), extension);
                string physicalPath = Server.MapPath("~/FileStore/Products/" + generatedname);
                file.SaveAs(physicalPath);
                product.ImagePath = "/FileStore/Products/" + generatedname;
                product.IsActive = true;
                db.Products.Add(product);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BrandId = new SelectList(db.Brands, "BrandId", "Description", product.BrandId);
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "Description", product.CategoryId);
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
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ItemId,ItemName,Description,BrandId,CategoryId,UnitId,Rate,Image,ImagePath,OriginalFileName,Stock,UpdatedAt,IsActive")] Product product,HttpPostedFileBase file)
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

        public byte[] GetByteArray(HttpPostedFileBase file)
        {
            byte[] data;
            using (Stream inputStream = file.InputStream)
            {
                MemoryStream memoryStream = inputStream as MemoryStream;
                if (memoryStream == null)
                {
                    memoryStream = new MemoryStream();
                    inputStream.CopyTo(memoryStream);
                }
                data = memoryStream.ToArray();
            }
            return data;
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
