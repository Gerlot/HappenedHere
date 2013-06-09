using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappanedHere.Data
{
    public class HappenedHereRepository
    {

        public static void Initialize()
        {
            using (HappenedHereDataContext dataContext = new HappenedHereDataContext(HappenedHereDataContext.ConnectionString))
            {
                if (dataContext.DatabaseExists() == false)
                {
                    dataContext.CreateDatabase();

                    DatabaseArticle article1 = new DatabaseArticle
                    {
                        Title = "Bontják az MTA félkész épületét",
                        Category = "News",
                        DisplayUrl = "index.hu/belfold/budapest/2012/03/09/lebontjak_a_bme_felkesz_epuletet/",
                        Url = "http://index.hu/belfold/budapest/2012/03/09/lebontjak_a_bme_felkesz_epuletet/",
                        Latitude = 47.474114,
                        Longitude = 19.059922
                    };

                    dataContext.DatabaseArticles.InsertOnSubmit(article1);

                    dataContext.SubmitChanges();
                }
            }
        }

        public List<DatabaseArticle> GetDatabaseArticles()
        {
            using (HappenedHereDataContext dataContext = new HappenedHereDataContext(HappenedHereDataContext.ConnectionString))
            {
                var databaseArticles = (from a in dataContext.DatabaseArticles select a).ToList();
                return databaseArticles;
            }
        }
    }
}
