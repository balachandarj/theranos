using NUnit.Framework;
using Reservation.Common;
using Reservation.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Service.Test
{
    [TestFixture]
    public class ReservationServiceTest
    {
        [Test]
        public void OnGetBusinessHours_ReturnSuccess()
        {
            var reservationService = new ReservationService();
            var result=reservationService.GetBusinessHours();
            Assert.IsTrue(result.OperationSuccess);

        }
    }
}
