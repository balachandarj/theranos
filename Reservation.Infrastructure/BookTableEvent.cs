using Microsoft.Practices.Prism.PubSubEvents;
using Reservation.Client.Service.ReservationServiceReference;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure
{
    public class BookTableEvent : PubSubEvent<BookingDetail>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly BookTableEvent _event;
        static BookTableEvent()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<BookTableEvent>();
        }

        public static BookTableEvent Instance
        {
            get { return _event; }
        }

    }

}
