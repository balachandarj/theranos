using Reservation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Business.Interface
{
    public interface IReservationBL
    {
        ServiceResponse<bool> BookTableReservation(BookingDetail bookingDetail);

        ServiceResponse<bool> UpdateTableReservation(BookingDetail bookingDetail);

        ServiceResponse<bool> CancelTableReservation(DateTime bookingDate, Guid bookingId);

        ServiceResponse<bool> CancelAllTableReservations(DateTime bookingDate);

        ServiceResponse<List<TableAvailablityDetail>> GetTableAvailablity(DateTime bookingDate, TimeSpan checkInTime);

        ServiceResponse<List<BookingDetail>> GetBookingDetails(DateTime bookingDate);

        ServiceResponse<BookingDetail> GetBookingDetail(DateTime bookingDate,Guid bookingId);

        ServiceResponse<List<BusinessHour>> GetBusinessHours();

        ServiceResponse<List<TableDetail>> GetTableDetails();

        ServiceResponse<BusinessHour> GetBusinessHour(int dayOftheWeek);
    }
}
