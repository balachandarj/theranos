using NUnit.Framework;
using Reservation.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Repository.Test
{
    [TestFixture]
    public class ReservationRepositoryTest
    {

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
                    MobileNumber = "903-426-7303"
                }
            };

            var reservationRepository = new ReservationRepository();
            var result = reservationRepository.BookTableReservation(bookingDetail);

            Assert.IsTrue(result);
        
        }
    }
}
