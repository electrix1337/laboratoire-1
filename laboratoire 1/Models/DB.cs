using JsonDAL;
using System.Collections.Generic;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace MyCRUD_WebApp_JSON.Models
{
    public class DB
    {
        #region singleton setup
        private static readonly DB instance = new DB();
        public static DB Instance
        {
            get { return instance; }
        }
        #endregion
        #region Repositories
        public static GuitarsRepository Guitars { get; set; }
        public static SellersRepository Sellers { get; set; }
        #endregion
        #region initialization
        public DB()
        {
            Guitars = new GuitarsRepository();
            Sellers = new SellersRepository();
            InitRepositories(this);
        }
        private static void InitRepositories(DB db)
        {
            string serverPath = HostingEnvironment.MapPath(@"~/App_Data/");
            PropertyInfo[] myPropertyInfo = db.GetType().GetProperties();
            foreach (PropertyInfo propertyInfo in myPropertyInfo)
            {
                if (propertyInfo.PropertyType.Name.Contains("Repository"))
                {
                    MethodInfo method = propertyInfo.PropertyType.GetMethod("Init");
                    method.Invoke(propertyInfo.GetValue(db), new object[] { serverPath + propertyInfo.Name + ".json" });
                }
            }
        }
        #endregion
    }
    public class GuitarsRepository : Repository<Guitar>
    {
    }
    public class SellersRepository : Repository<Seller>
    {
        public SelectList ToSelectList(string caption, int selectedId = -1)
        {
            List<Seller> sellers = new List<Seller>(ToList());
            sellers.Insert(0, new Seller { Id = -1, Name = caption });
            return new SelectList(sellers, "Id", "Name", selectedId, new[] { -1 });
        }

        public int Count { get => ToList().Count; }
    }
}