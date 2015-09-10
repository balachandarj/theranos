using Reservation.Client.Service.ReservationServiceReference;
using Reservation.Infrastructure;
using Reservation.Presentation.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Reservation.Presentation
{
    public class BookingViewModel : ViewModelBase
    {

        #region Constructor
        public BookingViewModel(BookingDetail bookingDetail)
        {
            this.BookingDetail = bookingDetail;
        }
        #endregion

        #region Properties
        BookingDetail bookingDetail;

        public BookingDetail BookingDetail
        {
            get { return bookingDetail; }
            set
            {
                bookingDetail = value;
                OnPropertyChanged("BookingDetail");
                LoadCustomerDetail();
            }
        }


        private RelayCommand _bookCommand;
        public ICommand BookCommand
        {
            get
            {
                if (_bookCommand == null)
                    _bookCommand = new RelayCommand(BookTable);

                return _bookCommand;
            }
        }


        public Action CloseAction { get; set; }


        private RelayCommand _cancelCommand;
        public ICommand CancelCommand
        {
            get
            {
                if (_cancelCommand == null)
                    _cancelCommand = new RelayCommand(CancelTable);

                return _cancelCommand;
            }
        }


        private RelayCommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                if (_closeCommand == null)
                    _closeCommand = new RelayCommand(CloseWindow);

                return _closeCommand;
            }
        }

        #endregion

        #region Private methods

        private void CloseWindow(object obj)
        {
            CloseAction();
        }
        private void CancelTable(object obj)
        {
            if (BookingDetail.BookingId != Guid.Empty)
            {
                ReservationServiceClient client = new ReservationServiceClient();
                client.CancelTableReservationCompleted += client_CancelTableReservationCompleted;
                client.CancelTableReservationAsync(BookingDetail.BookingDate, BookingDetail.BookingId);
                RefreshEvent.Instance.Publish(bookingDetail);
            }

        }

        private void client_CancelTableReservationCompleted(object sender, CancelTableReservationCompletedEventArgs e)
        {
            if (e.Result.OperationSuccess)
            {
                RefreshEvent.Instance.Publish(bookingDetail);
                CloseAction();
            }
        }

        private void BookTable(object obj)
        {
            ReservationServiceClient client = new ReservationServiceClient();
            client.BookTableReservationCompleted += client_BookTableReservationCompleted;
            client.BookTableReservationAsync(BookingDetail);

        }

        private void client_BookTableReservationCompleted(object sender, BookTableReservationCompletedEventArgs e)
        {
            if (e.Result.OperationSuccess)
            {
                RefreshEvent.Instance.Publish(bookingDetail);
                // Close booking window
                CloseAction();
            }
            else
            {
                //TODO Show failure message
            }
        }

        private void LoadCustomerDetail()
        {
            if (BookingDetail.BookingId != Guid.Empty)
            {
                ReservationServiceClient client = new ReservationServiceClient();
            }
            else
            {
                BookingDetail.BookingId = Guid.NewGuid();
                BookingDetail.CustomerDetail = new Person();
            }

            //client.GetBookingDetail()
        }

        #endregion

    }
}
