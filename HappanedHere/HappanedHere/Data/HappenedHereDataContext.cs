using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;

namespace HappanedHere.Data
{
    public class HappenedHereDataContext : DataContext
    {
        public const string ConnectionString = "isostore:/articles.sdf";

        public HappenedHereDataContext(string ConnectionString) : base (ConnectionString)
        {
        }

        public Table<DatabaseArticle> DatabaseArticles { get; set; }
    }
}
