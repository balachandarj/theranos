﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reservation.Common
{
    public class TableAvailablityDetail
    {
        public int TableId { get; set; }

        public int MaxOccupancy { get; set; }

        public bool IsAvailable { get; set; }

    }
}
