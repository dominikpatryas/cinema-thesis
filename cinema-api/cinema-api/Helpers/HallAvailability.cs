using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Helpers
{
    public class HallAvailability
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public int MovieDuration { get; set; }
    }
}
