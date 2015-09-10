using Reservation.Common;
using Reservation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        #region Fields
        private string repositoryPath;
        #endregion

        #region Constructor
        public ReservationRepository()
        {
            InitializeRepository();
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Book table reservation
        /// </summary>
        /// <param name="bookingDetail"></param>
        /// <returns>true / false</returns>
        public bool BookTableReservation(BookingDetail bookingDetail)
        {
            var bookingDate = bookingDetail.BookingDate;
            // Check the data file is exist or not, if not exist creates the data file
            CreateDataFileIfNotExis(bookingDate);

            // Cancel if the booking is exist
            CancelTableReservation(bookingDate, bookingDetail.BookingId);


            // Get the booking details
            var bookingDetails = GetBookingDetails(bookingDate);



            //Add booking detail into list of booking details
            bookingDetails.Add(bookingDetail);

            // Serialize and save the Json text into a data file
            var dataFileName = GetDataFileName(bookingDate);
            return SerializerHelper.WriteIntoFile(bookingDetails, dataFileName);
        }
        public bool UpdateTableReservation(BookingDetail bookingDetail)
        {
            // Cancel existing booking
            CancelTableReservation(bookingDetail.BookingDate, bookingDetail.BookingId);

            // Save the udated booking detail
            return BookTableReservation(bookingDetail);
        }

        public bool CancelTableReservation(DateTime bookingDate,Guid bookingId)
        {
            var bookingDetails = GetBookingDetails(bookingDate);
            // Find the booking detail using booking id
            var result = bookingDetails.Where(bd => bd.BookingId == bookingId).FirstOrDefault();

            // Remove the existing bookind detail
            bookingDetails.Remove(result);

            // Serialize and save the Json text into a data file
            var dataFileName = GetDataFileName(bookingDate);
            return SerializerHelper.WriteIntoFile(bookingDetails, dataFileName);

        }

        public bool CancelAllTableReservations(DateTime bookingDate)
        {
            var bookingDetails = GetBookingDetails(bookingDate);
           
            // Remove the existing bookind detail
            bookingDetails.Clear();

            // Serialize and save the Json text into a data file
            var dataFileName = GetDataFileName(bookingDate);
            return SerializerHelper.WriteIntoFile(bookingDetails, dataFileName);
        }

        public List<TableDetail> GetTableAvailablity(DateTime bookingDate, TimeSpan checkInTime)
        {
            return null;
        }

        /// <summary>
        /// Get booking detail for a date
        /// </summary>
        /// <param name="bookingDate"></param>
        /// <returns></returns>
        public List<BookingDetail> GetBookingDetails(DateTime bookingDate)
        {
            var dataFileName = GetDataFileName(bookingDate);
            // If the file doesn't exist, this method returns empty list of booking details 
            if (!File.Exists(dataFileName))
            {
                return new List<BookingDetail>();
            }
            return SerializerHelper.GetObjectFromFile<List<BookingDetail>>(dataFileName);
        }

        /// <summary>
        /// Get the booking detail for a date and bookind id
        /// </summary>
        /// <param name="bookingDate">Booking date</param>
        /// <param name="bookingId">Booking Id</param>
        /// <returns></returns>
        public BookingDetail GetBookingDetail(DateTime bookingDate, Guid bookingId)
        {
            var bookingDetails = GetBookingDetails(bookingDate);
            return bookingDetails.Where(bd => bd.BookingId == bookingId).FirstOrDefault();
        }

        /// <summary>
        /// Read the business hours from the Json file
        /// </summary>
        /// <returns></returns>
        public List<BusinessHour> GetBusinessHours()
        {            
            return SerializerHelper.GetObjectFromJsonText<List<BusinessHour>>(Repository.Properties.RepositorySettings.Default.BusinessHoursDetail);
        }

        /// <summary>
        /// Read the table capacity detail form Json file
        /// </summary>
        /// <returns></returns>
        public List<TableDetail> GetTableDetails()
        {            
            return SerializerHelper.GetObjectFromJsonText<List<TableDetail>>(Repository.Properties.RepositorySettings.Default.TableDetail);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Create the data file if not exist
        /// </summary>
        /// <param name="bookingDate">date of booking</param>
        /// <returns></returns>
        private bool CreateDataFileIfNotExis(DateTime bookingDate)
        {
            var dataFileName = GetDataFileName(bookingDate);
            if (!File.Exists(dataFileName))
            {
                // Create File to hold multiple booking details
                var bookingDetails = new List<BookingDetail>();
                SerializerHelper.WriteIntoFile(bookingDetails, dataFileName);
            }
            return true;
        }

        /// <summary>
        /// Get the data file name from the specified format
        /// </summary>
        /// <param name="bookingDate">Booking date</param>
        /// <returns></returns>
        private string GetDataFileName(DateTime bookingDate)
        {
            var fileName = string.Format("{0}{1}{2}_Reservation.Json", bookingDate.Year, bookingDate.Month, bookingDate.Day);
            return Path.Combine(repositoryPath, fileName);
        }

        /// <summary>
        /// Initialize the repository
        /// </summary>
        private void InitializeRepository()
        {

            this.repositoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "ReservationData");
            //Check repository path is exist or not
            // If Not create repository path
            if (!Directory.Exists(repositoryPath))
                Directory.CreateDirectory(repositoryPath);
        }


        #endregion



        
    }
}
