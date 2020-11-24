using cinema_api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Dtos
{
    public class PhotoForAdd
    {
        public string Url { get; set; }
        public bool IsMain { get; set; }
        public DateTime DateAdded { get; set; }
        public string MovieTitle { get; set; }

        public PhotoForAdd()
        {
            DateAdded = DateTime.Now;
        }
    }
}
