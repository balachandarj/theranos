using Reservation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Repository.Interface
{
    public interface IReservationRepository
    {
        bool BookTableReservation(BookingDetail bookingDetail);

        bool UpdateTableReservation(BookingDetail bookingDetail);

        bool CancelTableReservation(DateTime bookingDate, Guid bookingId);

        bool CancelAllTableReservations(DateTime bookingDate);

        List<TableDetail> GetTableAvailablity(DateTime bookingDate, TimeSpan checkInTime);

        List<BookingDetail> GetBookingDetails(DateTime bookingDate);

        BookingDetail GetBookingDetail(DateTime bookingDate, Guid bookingId);

        List<BusinessHour> GetBusinessHours();

        List<TableDetail> GetTableDetails();

    }
}
