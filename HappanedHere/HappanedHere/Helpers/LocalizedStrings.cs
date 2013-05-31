using HappanedHere.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappanedHere.Helpers
{
    public class LocalizedStrings
    {
        public LocalizedStrings() { }

        private static AppResources localizedResources = new AppResources();

        public AppResources AppResources
        {
            get { return localizedResources; }
        }
    }
}
