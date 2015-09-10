using MahApps.Metro.Controls;
using Microsoft.Practices.Prism.PubSubEvents;
using Reservation.Client.Service.ReservationServiceReference;
using Reservation.Infrastructure;
using Reservation.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Reservation.UI
{
    /// <summary>
    /// Interaction logic for ReservationView.xaml
    /// </summary>
    public partial class ReservationView : UserControl
    {

          

        ReservationViewModel viewModel;
        public ReservationView(ReservationViewModel viewModel)
        {
            InitializeComponent();
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            SubscribeEvents();
        }

       
        private void SubscribeEvents()
        {
            BookTableEvent.Instance.Subscribe(OpenCustomerView);
        }

        private void OpenCustomerView(BookingDetail obj)
        {
            
            BookingViewModel viewModel = new BookingViewModel(obj);
            BookingView view = new BookingView(viewModel);
            view.ShowDialog();
        }
       
    }
}
