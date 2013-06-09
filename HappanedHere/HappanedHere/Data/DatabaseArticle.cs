using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;

namespace HappanedHere.Data
{
    [Table(Name = "DatabaseArticles")]
    public class DatabaseArticle
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true, DbType = "INT NOT NULL", CanBeNull = false)]  
        public Int32 ID { get; set; }

        [Column]
        public string Title { get; set; }

        [Column]
        public string Category { get; set; }

        [Column]
        public string DisplayUrl { get; set; }

        [Column]
        public string Url { get; set; }
        
        [Column]
        public double Latitude { get; set; }

        [Column]
        public double Longitude { get; set; }
    }
}
