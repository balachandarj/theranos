using Dependency.Common;
using Microsoft.Practices.Unity;
using Reservation.Business.Interface;
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
    public class ReservationService : IReservationService
    {
        private readonly IReservationBL reservationBL;

        private static IUnityContainer _container;

       

        public ReservationService(): this(DependencyFactory.Resolve<IReservationBL>())
        {

        }

        public ReservationService(IReservationBL reservationBL)
        {
            this.reservationBL = reservationBL;
        }

        public ServiceResponse<bool> BookTableReservation(BookingDetail bookingDetail)
        {
            return reservationBL.BookTableReservation(bookingDetail);
        }

        public ServiceResponse<bool> UpdateTableReservation(BookingDetail bookingDetail)
        {
            return reservationBL.UpdateTableReservation(bookingDetail); ;
        }

        public ServiceResponse<bool> CancelTableReservation(DateTime bookingDate, Guid bookingId)
        {
            return reservationBL.CancelTableReservation(bookingDate,bookingId);
        }

        public ServiceResponse<bool> CancelAllTableReservations(DateTime bookingDate)
        {
            return reservationBL.CancelAllTableReservations(bookingDate);
        }

        public ServiceResponse<List<TableAvailablityDetail>> GetTableAvailablity(DateTime bookingDate, TimeSpan checkInTime)
        {
            return reservationBL.GetTableAvailablity(bookingDate,checkInTime);
        }

        public ServiceResponse<List<BookingDetail>> GetBookingDetails(DateTime bookingDate)
        {
            return reservationBL.GetBookingDetails(bookingDate);
        }

        public ServiceResponse<BookingDetail> GetBookingDetail(DateTime bookingDate, Guid bookingId)
        {
            return reservationBL.GetBookingDetail(bookingDate,bookingId);
        }

        public ServiceResponse<List<BusinessHour>> GetBusinessHours()
        {
            return reservationBL.GetBusinessHours();
        }

        public ServiceResponse<List<TableDetail>> GetTableDetails()
        {
            return reservationBL.GetTableDetails();
        }

        public ServiceResponse<BusinessHour> GetBusinessHour(int dayOftheWeek)
        {
            return reservationBL.GetBusinessHour(dayOftheWeek);
        }
    }
}
