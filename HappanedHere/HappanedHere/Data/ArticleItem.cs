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
        private string url;
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

        public string URL
        {
            get { return url; }
            set
            {
                url = value;
                NotifyPropertyChanged(() => URL);
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
