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
    public class UnitMasterController : Controller
    {
        private FiresafeDbContext db = new FiresafeDbContext();

        // GET: UnitMaster
        public async Task<ActionResult> Index()
        {
            return View(await db.UnitMasters.ToListAsync());
        }

        // GET: UnitMaster/Details/5
        public async Task<ActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitMaster unitMaster = await db.UnitMasters.FindAsync(id);
            if (unitMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitMaster);
        }

        // GET: UnitMaster/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UnitMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "UnitId,Description")] UnitMaster unitMaster)
        {
            if (ModelState.IsValid)
            {
                unitMaster.UnitId = Guid.NewGuid();
                db.UnitMasters.Add(unitMaster);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(unitMaster);
        }

        // GET: UnitMaster/Edit/5
        public async Task<ActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitMaster unitMaster = await db.UnitMasters.FindAsync(id);
            if (unitMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitMaster);
        }

        // POST: UnitMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "UnitId,Description")] UnitMaster unitMaster)
        {
            if (ModelState.IsValid)
            {
                db.Entry(unitMaster).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unitMaster);
        }

        // GET: UnitMaster/Delete/5
        public async Task<ActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UnitMaster unitMaster = await db.UnitMasters.FindAsync(id);
            if (unitMaster == null)
            {
                return HttpNotFound();
            }
            return View(unitMaster);
        }

        // POST: UnitMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            UnitMaster unitMaster = await db.UnitMasters.FindAsync(id);
            db.UnitMasters.Remove(unitMaster);
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
