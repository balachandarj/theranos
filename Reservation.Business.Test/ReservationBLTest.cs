using NUnit.Framework;
using Reservation.Business.Interface;
using Reservation.Common;
using Reservation.Repository;
using Reservation.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Business.Test
{
    [TestFixture]
    public class ReservationBLTest
    {
        IReservationRepository reservationRepository;
        IReservationBL reservationBL;

        #region Test setup
        /// <summary>
        /// This method will be called before execute the each test method
        /// </summary>
        [SetUp]
        public new void TestSetup()
        {
            reservationRepository = new ReservationRepository();
            reservationBL = new ReservationBL(reservationRepository);
            
            var bookingDate = DateTime.Today;

            // Cancel all reservation
            reservationBL.CancelAllTableReservations(bookingDate);
        }

        /// <summary>
        /// This method will be called before execute the each test method
        /// </summary>
        [TearDown]
        public new void TestTearDown()
        {

        }

        /// <summary>
        /// This method will be called after execute the all the test method
        /// </summary>
        [TestFixtureTearDown]
        public new void TestFixtureTearDown()
        {

        }
        #endregion

        #region Positive test methods

        [Test]
        public void OnBookingTableReservationWithValidValue_ReturnSuccess()
        {
            var bookingDetail = new BookingDetail()
            {
                BookingId = Guid.NewGuid(),
                BookingDate = new DateTime(2015, 08, 29),
                CheckInTime = new TimeSpan(13, 30, 00),
                TableId = 1,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            var result = reservationBL.BookTableReservation(bookingDetail);
            Assert.IsTrue(result.OperationSuccess);

        }

        [Test]
        public void OnUpdateTableReservationWithNewMobileNumber_ReturnSuccess()
        {
            var bookingId = Guid.NewGuid();
            var bookingDetail = new BookingDetail()
            {
                BookingId = bookingId,
                BookingDate = new DateTime(2015, 08, 29),
                CheckInTime = new TimeSpan(13, 30, 00),
                TableId = 1,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            // Book the table reservation
            reservationBL.BookTableReservation(bookingDetail);

            // Modify the Mobile number 
            var mobileNumber = "111 222 3333";
            bookingDetail.CustomerDetail.MobileNumber = mobileNumber;

            // Update the reservation
            reservationBL.UpdateTableReservation(bookingDetail);

            // Get the booking detail by passing booking id

            var bookingDetailAfterModification = reservationBL.GetBookingDetail(bookingDetail.BookingDate, bookingId);

            Assert.AreEqual(bookingDetailAfterModification.Result.CustomerDetail.MobileNumber, mobileNumber);

        }


        [Test]
        public void OnCancelTableReservation_ReturnSuccess()
        {
            var bookingId = Guid.NewGuid();
            var bookingDetail = new BookingDetail()
            {
                BookingId = bookingId,
                BookingDate = new DateTime(2015, 08, 29),
                CheckInTime = new TimeSpan(13, 30, 00),
                TableId = 1,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            // Book the table reservation
            reservationBL.BookTableReservation(bookingDetail);

            // Cancel reservation
            reservationBL.CancelTableReservation(bookingDetail.BookingDate, bookingId);


            // Get the booking detail by passing booking id, it should return null value once cancelled
            var bookingDetailAfterCancellation = reservationBL.GetBookingDetail(bookingDetail.BookingDate, bookingId);

            Assert.IsNull(bookingDetailAfterCancellation.Result);

        }


        [Test]
        public void OnCancelAllReservation_ReturnSuccess()
        {
            var bookingDate = DateTime.Today;

            // Cancel reservation
            reservationBL.CancelAllTableReservations(bookingDate);

            // Get booking details for a day (today), returns 0 records after cancel all the records
            var bookingDetails = reservationBL.GetBookingDetails(bookingDate);

            Assert.AreEqual(bookingDetails.Result.Count, 0);

        }

        [Test]
        public void OnGetBookingDetails_ReturnSuccess()
        {
            var bookingDate = DateTime.Today;

            var bookingDetail1 = new BookingDetail()
            {
                BookingId = Guid.NewGuid(),
                BookingDate = bookingDate,
                CheckInTime = new TimeSpan(13, 30, 00),
                TableId = 1,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            var bookingDetail2 = new BookingDetail()
            {
                BookingId = Guid.NewGuid(),
                BookingDate = bookingDate,
                CheckInTime = new TimeSpan(13, 30, 00),
                TableId = 1,
                CustomerDetail = new Person()
                {

                    FirstName = "Ramesh",
                    MobileNumber = "903-444-7303"
                }
            };
            // Book the table reservation
            reservationBL.BookTableReservation(bookingDetail1);
            reservationBL.BookTableReservation(bookingDetail2);

            // Get the booking detail by passing booking date, it should return one ot more records
            var bookingDetailAfterAddingTwoRecords = reservationBL.GetBookingDetails(bookingDate);

            Assert.IsTrue(bookingDetailAfterAddingTwoRecords.Result.Count==2);

        }
        
        [Test]
        public void OnGetBookingDetail_ReturnSuccess()
        {
            var bookingDate = DateTime.Today;
            var bookingId = Guid.NewGuid();

            var bookingDetail = new BookingDetail()
            {
                BookingId = bookingId,
                BookingDate = bookingDate,
                CheckInTime = new TimeSpan(13, 30, 00),
                TableId = 3,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };


            // Book the table reservation
            reservationBL.BookTableReservation(bookingDetail);


            // Get the booking detail by passing booking date and booking id
            var bookingDetailAfterAddingOneRecord = reservationBL.GetBookingDetail(bookingDate, bookingId);

            // Check the table id value
            Assert.AreEqual(bookingDetailAfterAddingOneRecord.Result.TableId, 3);

        }


        [Test]
        public void OnGetBusinessHours_ReturnSuccess()
        {
            var result = reservationBL.GetBusinessHours();
            //Check it returns one or more records
            Assert.IsTrue(result.Result.Count > 0);
        }

        [Test]
        public void OnGetTableDetails_ReturnSuccess()
        {
            var result = reservationBL.GetTableDetails();
            //Check it returns one or more records
            Assert.IsTrue(result.Result.Count > 0);

        }

        [Test]
        public void OnGetTableAvailablity_WithValidLaterCheckinTime_ReturnSuccess()
        {
            // Table id :3
            // Already Booked time : 10:30
            // Maximum stay time : 60 Mins
            // Requested Checkin time : 11:30
            // Expected value is True
            var bookingDate = DateTime.Today;
            var bookingId = Guid.NewGuid();

            var bookingDetail = new BookingDetail()
            {
                BookingId = bookingId,
                BookingDate = bookingDate,
                CheckInTime = new TimeSpan(10, 30, 00),
                TableId = 3,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            // Booking for 10:30 am and Table id : 3
            reservationBL.BookTableReservation(bookingDetail);

            // Get availablity for 11:30
            var availableTables=reservationBL.GetTableAvailablity(bookingDate, new TimeSpan(11, 30, 00));

            // Check table id : 3 is available or not
            var table3Availablity = availableTables.Result.Where(tbl => tbl.TableId == 3).FirstOrDefault();
           
            Assert.IsTrue(table3Availablity.IsAvailable);
        }


        [Test]
        public void OnGetTableAvailablity_WithValidEarlierCheckinTime_ReturnSuccess()
        {
            // Table id :3
            // Already Booked time : 11:30
            // Maximum stay time : 60 Mins
            // Requested Checkin time : 10:30
            // Expected value is True
            var bookingDate = DateTime.Today;
            var bookingId = Guid.NewGuid();

            var bookingDetail = new BookingDetail()
            {
                BookingId = bookingId,
                BookingDate = bookingDate,
                CheckInTime = new TimeSpan(11, 30, 00),
                TableId = 3,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            // Booking for 10:30 am and Table id : 3
            reservationBL.BookTableReservation(bookingDetail);

            // Get availablity for 10:30
            var availableTables = reservationBL.GetTableAvailablity(bookingDate, new TimeSpan(10, 30, 00));

            // Check table id : 3 is available or not
            var table3Availablity = availableTables.Result.Where(tbl => tbl.TableId == 3).FirstOrDefault();

            Assert.IsTrue(table3Availablity.IsAvailable);




        }

        [Test]
        public void OnGetTableAvailablity_WithInValidLaterCheckinTime_ReturnFalse()
        {
            // Table id :3
            // Already Booked time : 10:30
            // Maximum stay time : 60 Mins
            // Requested Checkin time : 11:00
            // Expected value is False
            var bookingDate = DateTime.Today;
            var bookingId = Guid.NewGuid();

            var bookingDetail = new BookingDetail()
            {
                BookingId = bookingId,
                BookingDate = bookingDate,
                CheckInTime = new TimeSpan(10, 30, 00),
                TableId = 3,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            // Booking for 10:30 am and Table id : 3
            reservationBL.BookTableReservation(bookingDetail);

            // Get availablity for 11:00
            var availableTables = reservationBL.GetTableAvailablity(bookingDate, new TimeSpan(11, 00, 00));

            // Check table id : 3 is available or not
            var table3Availablity = availableTables.Result.Where(tbl => tbl.TableId == 3).FirstOrDefault();

            Assert.IsFalse(table3Availablity.IsAvailable);




        }

        [Test]
        public void OnGetTableAvailablity_WithInValidEarlierCheckinTime_ReturnFalse()
        {
            // Table id :3
            // Already Booked time : 11:00
            // Maximum stay time : 60 Mins
            // Requested Checkin time : 10:30
            // Expected value is False
            var bookingDate = DateTime.Today;
            var bookingId = Guid.NewGuid();

            var bookingDetail = new BookingDetail()
            {
                BookingId = bookingId,
                BookingDate = bookingDate,
                CheckInTime = new TimeSpan(11, 00, 00),
                TableId = 3,
                CustomerDetail = new Person()
                {

                    FirstName = "Balachandar",
                    MobileNumber = "903-555-7303"
                }
            };

            // Booking for 10:30 am and Table id : 3
            reservationBL.BookTableReservation(bookingDetail);

            // Get availablity for 10:30
            var availableTables = reservationBL.GetTableAvailablity(bookingDate, new TimeSpan(10, 30, 00));

            // Check table id : 3 is available or not
            var table3Availablity = availableTables.Result.Where(tbl => tbl.TableId == 3).FirstOrDefault();

            Assert.IsFalse(table3Availablity.IsAvailable);




        }


        [Test]
        public void OnGetTableAvailablity_WithOutOfBusinessHoursEarly_ReturnFalse()
        {            
            // Maximum stay time : 60 Mins
            // Requested Checkin time : 07:30
            // Business hours 10:00 am to 10:00 pm
            // Expected value is False
            var bookingDate = DateTime.Today;
            var bookingId = Guid.NewGuid();

            // Get availablity for 07:30
            var availableTables = reservationBL.GetTableAvailablity(bookingDate, new TimeSpan(07, 30, 00));
            Assert.IsFalse(availableTables.OperationSuccess);

        }

        [Test]
        public void OnGetTableAvailablity_WithOutOfBusinessHoursLater_ReturnFalse()
        {
            // Maximum stay time : 60 Mins
            // Requested Checkin time : 22:30
            // Business hours 10:00 am to 10:00 pm
            // Expected value is False
            var bookingDate = DateTime.Today;
            var bookingId = Guid.NewGuid();

            // Get availablity for 22:30
            var availableTables = reservationBL.GetTableAvailablity(bookingDate, new TimeSpan(22, 30, 00));
            Assert.IsFalse(availableTables.OperationSuccess);

        }

        #endregion


    }
}
