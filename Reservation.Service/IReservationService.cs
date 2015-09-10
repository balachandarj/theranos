using Reservation.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace Reservation.Service
{
   
    [ServiceContract]
    public interface IReservationService
    {
        [OperationContract]
        ServiceResponse<bool> BookTableReservation(BookingDetail bookingDetail);

        [OperationContract]
        ServiceResponse<bool> UpdateTableReservation(BookingDetail bookingDetail);

        [OperationContract]
        ServiceResponse<bool> CancelTableReservation(DateTime bookingDate, Guid bookingId);

        [OperationContract]
        ServiceResponse<bool> CancelAllTableReservations(DateTime bookingDate);

        [OperationContract]
        ServiceResponse<List<TableAvailablityDetail>> GetTableAvailablity(DateTime bookingDate, TimeSpan checkInTime);

        [OperationContract]
        ServiceResponse<List<BookingDetail>> GetBookingDetails(DateTime bookingDate);

        [OperationContract]
        ServiceResponse<BookingDetail> GetBookingDetail(DateTime bookingDate, Guid bookingId);

        [OperationContract]
        ServiceResponse<List<BusinessHour>> GetBusinessHours();

        [OperationContract]
        ServiceResponse<List<TableDetail>> GetTableDetails();

        [OperationContract]
        ServiceResponse<BusinessHour> GetBusinessHour(int dayOftheWeek);
    }


}
