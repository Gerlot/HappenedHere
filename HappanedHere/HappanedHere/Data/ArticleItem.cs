﻿using GART.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappanedHere.Data
{
    public class ArticleItem : ARItem
    {
        private string title;
        private string category;
        private string displayUrl;
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

        public string Category
        {
            get { return category; }
            set
            {
                category = value;
                NotifyPropertyChanged(() => Category);
            }
        }

        public string DisplayUrl
        {
            get { return displayUrl; }
            set
            {
                displayUrl = value;
                NotifyPropertyChanged(() => DisplayUrl);
            }
        }

        public string Url
        {
            get { return url; }
            set
            {
                url = value;
                NotifyPropertyChanged(() => Url);
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
