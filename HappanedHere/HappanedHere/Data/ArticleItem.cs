using GART.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappanedHere.Data
{
    public class ArticleItem : ARItem
    {
        private string title;
        private string lead;
        private Uri icon;

        public string Title
        {
            get { return title; }
            set
            {
                title = value;
                NotifyPropertyChanged(() => Title);
            }
        }

        public string Lead
        {
            get { return lead; }
            set
            {
                lead = value;
                NotifyPropertyChanged(() => Lead);
            }
        }

        public Uri Icon
        {
            get { return icon; }
            set
            {
                icon = value;
                NotifyPropertyChanged(() => Icon);
            }
        }
    }
}
