using cinema_api.Models;
using cinema_api.Models.HelperModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cinema_api.Data
{
    public class DataContext : DbContext 
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Photo> Photos { get; set; }
        public DbSet<Show> Shows { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Hall> Halls{ get; set; }
        public DbSet<Seat> Seats{ get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<SeatReserved> SeatReserved { get; set; }
        public DbSet<Cast> Cast { get; set; }
    }
}
