using Reservation.Client.Service.ReservationServiceReference;
using Reservation.Infrastructure;
using Reservation.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservation.Presentation
{
    public class BookingDetailsViewModel : ViewModelBase
    {
        #region Constructor
        public BookingDetailsViewModel()
        {
            bookingDate = DateTime.Now;
            LoadBookingDetails(bookingDate);
            SubscribeEvents();

        }
        #endregion

        #region Private fields
        private DateTime bookingDate;
        #endregion

        #region Private methods

        private void BookingDateChanged(DateTime bookingDate)
        {
            this.bookingDate = bookingDate;
            LoadBookingDetails(bookingDate);
        }

        private void LoadBookingDetails(DateTime bookingDate)
        {
            ReservationServiceClient client = new ReservationServiceClient();
            client.GetBookingDetailsCompleted += client_GetBookingDetailsCompleted;
            client.GetBookingDetailsAsync(bookingDate);
        }

        private void RefreshScreen(object obj)
        {
            LoadBookingDetails(bookingDate);
        }

        private void client_GetBookingDetailsCompleted(object sender, GetBookingDetailsCompletedEventArgs e)
        {
            BookingDetails = new ObservableCollection<BookingDetail>(e.Result.Result);
        }

        private void LoadBookingWindow(object obj)
        {
            if (SelectedBookingDetail != null)
                BookTableEvent.Instance.Publish(SelectedBookingDetail);

        }
        #endregion
        
        #region Properties

        private ObservableCollection<BookingDetail> bookingDetails;

        public ObservableCollection<BookingDetail> BookingDetails
        {
            get { return bookingDetails; }
            set
            {
                bookingDetails = value;
                OnPropertyChanged("BookingDetails");
            }
        }

        private BookingDetail selectedBookingDetail;

        public BookingDetail SelectedBookingDetail
        {
            get { return selectedBookingDetail; }
            set
            {
                selectedBookingDetail = value;
                OnPropertyChanged("SelectedBookingDetail");
            }
        }

        private RelayCommand _viewCommand;
        public ICommand ViewCommand
        {
            get
            {
                if (_viewCommand == null)
                    _viewCommand = new RelayCommand(LoadBookingWindow);

                return _viewCommand;
            }
        }

        #endregion

        #region Event subscription
        private void SubscribeEvents()
        {
            BookingDateChangedEvent.Instance.Subscribe(BookingDateChanged);
            RefreshEvent.Instance.Subscribe(RefreshScreen);
        }        
        #endregion
    }
}
