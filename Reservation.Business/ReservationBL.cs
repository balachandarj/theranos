using System.Security.Cryptography.X509Certificates;
using Reservation.Business.Interface;
using Reservation.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reservation.Repository.Interface;

namespace Reservation.Business
{
    /// <summary>
    /// Business logics for reservation
    /// </summary>
    public class ReservationBL : IReservationBL
    {
        #region Fields
        IReservationRepository reservationRepository;
        #endregion

        #region Constructor
        public ReservationBL(IReservationRepository reservationRepository)
        {
            this.reservationRepository = reservationRepository;
            InitializeReservationBL();
        }
        #endregion

        #region Properties
        public List<BusinessHour> BusinessHours { get; set; }
        public List<TableDetail> TableDetails { get; set; }
        public string RepositoryPath { get; set; }
        #endregion

        #region Public methods

        /// <summary>
        /// Book the table reservation
        /// </summary>
        /// <param name="bookingDetail">Booking detais</param>
        /// <returns></returns>
        public ServiceResponse<bool> BookTableReservation(BookingDetail bookingDetail)
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                var result = reservationRepository.BookTableReservation(bookingDetail);
                serviceResponse.Result = result;

                if (result == true)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Booking successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "Unable to book the reservation";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }


        /// <summary>
        /// Update the booking details
        /// </summary>
        /// <param name="bookingDetail">Booking detail</param>
        /// <returns></returns>
        public ServiceResponse<bool> UpdateTableReservation(BookingDetail bookingDetail)
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                var result = reservationRepository.UpdateTableReservation(bookingDetail);
                serviceResponse.Result = result;

                if (result == true)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Update successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "Unable to update the reservation";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;

        }


        /// <summary>
        /// Cancel table reservation
        /// </summary>
        /// <param name="bookingDate">Booking date</param>
        /// <param name="bookingId">Booking id</param>
        /// <returns></returns>
        public ServiceResponse<bool> CancelTableReservation(DateTime bookingDate, Guid bookingId)
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                var result = reservationRepository.CancelTableReservation(bookingDate, bookingId);
                serviceResponse.Result = result;

                if (result == true)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Cancel successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "Unable to cancel the reservation";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;

        }

        /// <summary>
        /// Cancel All tabele reservations
        /// </summary>
        /// <param name="bookingDate">Booking date</param>
        /// <returns></returns>
        public ServiceResponse<bool> CancelAllTableReservations(DateTime bookingDate)
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<bool>();

            try
            {
                var result = reservationRepository.CancelAllTableReservations(bookingDate);
                serviceResponse.Result = result;
                if (result == true)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Cancel all successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "Unable to cancel all the reservation";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }


        /// <summary>
        /// Get table availablity
        /// </summary>
        /// <param name="bookingDate">booking date</param>
        /// <param name="checkInTime">Check in time</param>
        /// <returns></returns>
        public ServiceResponse<List<TableAvailablityDetail>> GetTableAvailablity(DateTime bookingDate, TimeSpan checkInTime)
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<List<TableAvailablityDetail>>();

            try
            {
                // Check the checkin time is between start and end time of business hours
                if (!CheckChekinTimeWithBusinessHours(bookingDate, checkInTime))
                {

                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "Check in time is out of business hours";
                    return serviceResponse;
                }

                // Get booking details
                var bookingDetails = reservationRepository.GetBookingDetails(bookingDate);

                var availableTableDetail = new List<TableAvailablityDetail>();


                // Iterate table wise availablity

                foreach (var tableDetail in TableDetails)
                {
                    // Get the booking detail for particular table
                    var bookingDetailForSelectedTable = bookingDetails.Where(bd => bd.TableId == tableDetail.TableId).ToList();

                    // Check the table is available for the booking date and specified check in time
                    var isAvailable = IsTableAvailable(bookingDetailForSelectedTable, bookingDate, checkInTime);

                    // Add table availablity for a specified time
                    availableTableDetail.Add(new TableAvailablityDetail() { TableId = tableDetail.TableId, MaxOccupancy = tableDetail.MaxOccupancy, IsAvailable = isAvailable });

                }

                serviceResponse.OperationSuccess = true;
                serviceResponse.Result = availableTableDetail;

                return serviceResponse;

            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }


        /// <summary>
        /// Get booking detail for a particular day
        /// </summary>
        /// <param name="bookingDate">bookind date</param>
        /// <returns></returns>
        public ServiceResponse<List<BookingDetail>> GetBookingDetails(DateTime bookingDate)
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<List<BookingDetail>>();

            try
            {
                var result = reservationRepository.GetBookingDetails(bookingDate);
                serviceResponse.Result = result;

                if (result.Count > 0)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "There is no bookind detail available";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }


        /// <summary>
        /// Get bookind datail for a specific booind id
        /// </summary>
        /// <param name="bookingDate"></param>
        /// <param name="bookingId"></param>
        /// <returns></returns>
        public ServiceResponse<BookingDetail> GetBookingDetail(DateTime bookingDate, Guid bookingId)
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<BookingDetail>();

            try
            {
                var result = reservationRepository.GetBookingDetail(bookingDate, bookingId);
                serviceResponse.Result = result;

                if (result != null)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "There is no bookind detail available";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }


        /// <summary>
        /// Get business hours from the configurations for a entire week
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<List<BusinessHour>> GetBusinessHours()
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<List<BusinessHour>>();

            try
            {
                var result = reservationRepository.GetBusinessHours();
                serviceResponse.Result = result;

                if (result.Count > 0)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "There is no business hours available";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }


        /// <summary>
        /// Get business hours from configurations for a particular day
        /// </summary>
        /// <param name="dayOftheWeek"></param>
        /// <returns></returns>
        public ServiceResponse<BusinessHour> GetBusinessHour(int dayOftheWeek)
        {

            var serviceResponse = new ServiceResponse<BusinessHour>();

            // Add business rules          

            try
            {
                serviceResponse.Result = BusinessHours.Where(bh => bh.DayOfTheWeek == dayOftheWeek).FirstOrDefault();
                return serviceResponse;
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }


        /// <summary>
        /// Get table details from confugurations
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<List<TableDetail>> GetTableDetails()
        {
            // Add business rules

            var serviceResponse = new ServiceResponse<List<TableDetail>>();

            try
            {
                var result = reservationRepository.GetTableDetails();
                serviceResponse.Result = result;

                if (result.Count > 0)
                {
                    serviceResponse.OperationSuccess = true;
                    serviceResponse.ServiceMessage = "Successful";
                }
                else
                {
                    serviceResponse.OperationSuccess = false;
                    serviceResponse.ServiceMessage = "There is no table detail available";
                }
            }
            catch (Exception ex)
            {
                serviceResponse.ErrorMessage = ex.Message;
            }

            return serviceResponse;
        }
        #endregion

        #region Private methods
        private void InitializeReservationBL()
        {
            BusinessHours = reservationRepository.GetBusinessHours();
            TableDetails = reservationRepository.GetTableDetails();
        }


        /// <summary>
        /// Check the check in time is within business hours
        /// </summary>
        /// <param name="bookingDate"></param>
        /// <param name="checkinTime"></param>
        /// <returns></returns>
        private bool CheckChekinTimeWithBusinessHours(DateTime bookingDate, TimeSpan checkinTime)
        {
            var dayOfTheWeek = (int)bookingDate.DayOfWeek;

            var maximumStayTimeInMinutes = Properties.BusinessSettings.Default.MaxStayTimeInMinutes;


            var businessHour = BusinessHours.Where(bh => bh.DayOfTheWeek == dayOfTheWeek).FirstOrDefault();

            if (checkinTime >= businessHour.OpenTime &&
                checkinTime <= businessHour.CloseTime.Subtract(new TimeSpan(0, maximumStayTimeInMinutes, 0)))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        /// <summary>
        /// Check the table availablity for a particular checkin time
        /// </summary>
        /// <param name="bookingDetails">bookind details for a day</param>
        /// <param name="bookingDate">bookind date</param>
        /// <param name="checkInTime">check in time</param>
        /// <returns></returns>
        private bool IsTableAvailable(List<BookingDetail> bookingDetails, DateTime bookingDate, TimeSpan checkInTime)
        {

            //Mimimum stay time from checkin time
            var maximumStayTimeInMinutes = Properties.BusinessSettings.Default.MaxStayTimeInMinutes;

            var dayOfTheWeek = (int)bookingDate.DayOfWeek;
            var businessHour = BusinessHours.Where(bh => bh.DayOfTheWeek == dayOfTheWeek).FirstOrDefault();

            foreach (var bookingDetail in bookingDetails)
            {
                if (!(checkInTime <= bookingDetail.CheckInTime.Subtract(new TimeSpan(0, maximumStayTimeInMinutes, 0))
                    || checkInTime >= bookingDetail.CheckInTime.Add(new TimeSpan(0, maximumStayTimeInMinutes, 0))))
                {
                    return false;
                }
            }

            return true;
        }
        #endregion
        
    }
}
