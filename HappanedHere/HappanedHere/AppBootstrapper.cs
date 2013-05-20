using HappanedHere.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using Caliburn.Micro.BindableAppBar;
using System.Windows.Controls;
using Microsoft.Phone.Controls;

namespace HappanedHere
{
    public class AppBootstrapper : PhoneBootstrapper
    {
        PhoneContainer container;

        protected override void Configure()
        {
            container = new PhoneContainer(RootFrame);

            if (!Execute.InDesignMode)
                container.RegisterPhoneServices();

            container.PerRequest<MainPageViewModel>();
            container.PerRequest<ArPageViewModel>();
            //container.PerRequest<TabViewModel>();

            AddCustomConventions();
        }

        static void AddCustomConventions()
        {
            ConventionManager.AddElementConvention<HubTile>(
                Control.IsEnabledProperty, "DataContext", "Tap");
            ConventionManager.AddElementConvention<BindableAppBarButton>(
                Control.IsEnabledProperty, "DataContext", "Click");
            ConventionManager.AddElementConvention<BindableAppBarMenuItem>(
                Control.IsEnabledProperty, "DataContext", "Click");
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }
    }
}
