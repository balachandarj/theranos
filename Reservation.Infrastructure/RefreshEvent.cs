using Microsoft.Practices.Prism.PubSubEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Infrastructure
{
    public class RefreshEvent : PubSubEvent<object>
    {
        private static readonly EventAggregator _eventAggregator;
        private static readonly RefreshEvent _event;
        static RefreshEvent()
        {
            _eventAggregator = new EventAggregator();
            _event = _eventAggregator.GetEvent<RefreshEvent>();
        }

        public static RefreshEvent Instance
        {
            get { return _event; }
        }

    }
}
