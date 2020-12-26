using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Models
{
    public class MovieType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public int MovieId { get; set; }
    }
}
