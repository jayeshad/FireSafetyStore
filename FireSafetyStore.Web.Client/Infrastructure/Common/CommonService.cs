using FireSafetyStore.Web.Client.Infrastructure.DbContext;
using FireSafetyStore.Web.Client.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FireSafetyStore.Web.Client.Infrastructure.Common
{
    public class CommonService
    {
        private static FiresafeDbContext db = new FiresafeDbContext();

        public static List<SelectListItem> PopulateBrands()
        {
            var brands = db.Brands.AsNoTracking().ToList();
            var brandsList = new List<SelectListItem>();
            brandsList.Add(new SelectListItem
            {
                Text = "All Brands",
                Value = ""
            });
            brands.ForEach(x => brandsList.Add(new SelectListItem
            {
                Text = x.Description,
                Value = x.BrandId.ToString()
            }));
            return brandsList;
        }

        public static List<SelectListItem> PopulateCategories()
        {
            var categories = db.Categories.AsNoTracking().ToList();
            var categoryList = new List<SelectListItem>();
            categoryList.Add(new SelectListItem
            {
                Text = "All Categories",
                Value = ""
            });
            categories.ForEach(x => categoryList.Add(new SelectListItem
            {
                Text = x.Description,
                Value = x.CategoryId.ToString()
            }));
            return categoryList;
        }

        public static int GetCartCount()
        {
            var currentCart = SessionManager<ShoppingCartViewModel>.GetValue(Constants.CartSessionKey);
            return (currentCart != null && currentCart.ShoppingCartItems.Any()) ? currentCart.ShoppingCartItems.Count() : 0;
        }

    }
}