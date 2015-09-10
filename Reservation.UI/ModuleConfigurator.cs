using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.UI
{
    public class ModuleConfigurator:IModule
    {
        public IUnityContainer Container { get; private set; }
        public IRegionManager RegionManager { get; private set; }

        public ModuleConfigurator(IUnityContainer container, IRegionManager regionManager)
        {
            Container = container;
            RegionManager = regionManager;
        }
        public void Initialize()
        {
            var reservationView = Container.Resolve<ReservationView>();
            RegionManager.Regions["LeftRegion"].Add(reservationView);

            var bookingDetailView = Container.Resolve<BookingDetailView>();
            RegionManager.Regions["RightRegion"].Add(bookingDetailView);
        }


    }
}
