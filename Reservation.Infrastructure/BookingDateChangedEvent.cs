using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure
{
    public class BookingDateChangedEvent : PubSubEvent<DateTime>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly BookingDateChangedEvent _event;
        static BookingDateChangedEvent()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<BookingDateChangedEvent>();
        }

        public static BookingDateChangedEvent Instance
        {
            get { return _event; }
        }
    }
}
