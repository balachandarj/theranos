using Microsoft.Practices.Prism.PubSubEvents;
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
    public class ReservationViewModel : ViewModelBase
    {

        #region Constructor
        public ReservationViewModel()
        {
            DisplayBusinessHour();
            SubscribeEvents();
        }


        #endregion

        #region Properties

        private DateTime bookingDate = DateTime.Now;
        public DateTime BookingDate
        {
            get { return bookingDate; }
            set
            {
                if (value.Year == 1)
                    BookingDate = DateTime.Now;
                if (bookingDate != value)
                {
                    bookingDate = value;
                    OnPropertyChanged("BookingDate");
                    BookingDateChangedEvent.Instance.Publish(value);
                    LoadTableAvailablityDetails();
                    DisplayBusinessHour();
                }
            }
        }


        private DateTime checkInDateTime = DateTime.Now;

        public DateTime CheckInDateTime
        {
            get { return checkInDateTime; }
            set
            {
                checkInDateTime = value;
                OnPropertyChanged("CheckInDateTime");
                LoadTableAvailablityDetails();
            }
        }


        private string businessHour;

        public string BusinessHour
        {
            get { return businessHour; }
            set
            {
                businessHour = value;
                OnPropertyChanged("BusinessHour");
            }
        }


        private TimeSpan checkInTime;
        public TimeSpan CheckInTime
        {
            get { return CheckInDateTime.TimeOfDay; }
        }


        ObservableCollection<TableAvailablityDetail> tableAvailablityDetails;

        public ObservableCollection<TableAvailablityDetail> TableAvailablityDetails
        {
            get { return tableAvailablityDetails; }
            set
            {
                tableAvailablityDetails = value;
                OnPropertyChanged("TableAvailablityDetails");
            }
        }

        #endregion

        #region Commands

        private RelayCommand _OpenBookCommand;
        public ICommand OpenBookCommand
        {
            get
            {
                if (_OpenBookCommand == null)
                    _OpenBookCommand = new RelayCommand(LoadBookingWindow);

                return _OpenBookCommand;
            }
        }

        
        #endregion

        #region Private methods

        private void DisplayBusinessHour()
        {
            ReservationServiceClient client = new ReservationServiceClient();
            client.GetBusinessHourCompleted += client_GetBusinessHourCompleted;
            client.GetBusinessHourAsync((int)BookingDate.DayOfWeek);
        }

        private void RefreshScreen(object obj)
        {
            LoadTableAvailablityDetails();
        }

        private void LoadBookingWindow(object obj)
        {
            var tabledetail = (TableAvailablityDetail)obj;
            var bookingDetail = new BookingDetail() { TableId = tabledetail.TableId, BookingDate = BookingDate, CheckInTime = CheckInTime };
            BookTableEvent.Instance.Publish(bookingDetail);
        }

        void client_GetBusinessHourCompleted(object sender, GetBusinessHourCompletedEventArgs e)
        {
            var result = e.Result.Result;
            BusinessHour = string.Format("Business Hours : {0} - {1}", result.OpenTime, result.CloseTime);
        }
        #endregion

        #region Service Intractions
        private void LoadTableAvailablityDetails()
        {
            ReservationServiceClient client = new ReservationServiceClient();
            client.GetTableAvailablityCompleted += client_GetTableAvailablityCompleted;
            client.GetTableAvailablityAsync(BookingDate, CheckInTime);
        }

        void client_GetTableAvailablityCompleted(object sender, GetTableAvailablityCompletedEventArgs e)
        {
            var result = e.Result;
            if (result.Result != null)
                TableAvailablityDetails = new ObservableCollection<TableAvailablityDetail>(result.Result);
            else
                TableAvailablityDetails = null;
        }


        #endregion

        #region Event subscription
        private void SubscribeEvents()
        {
            RefreshEvent.Instance.Subscribe(RefreshScreen);
        }

       
        #endregion

    }
}
