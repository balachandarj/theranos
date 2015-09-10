using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Common
{
    public class BookingDetail
    {
        // Booking Id is only for identifing record
        public Guid BookingId { get; set; }

        public DateTime BookingDate { get; set; }

        public TimeSpan CheckInTime { get; set; }

        public int TableId { get; set; }

        public Person CustomerDetail { get; set; }
    }
}
