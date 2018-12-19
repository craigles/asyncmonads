using System;

namespace AsyncMonads.Models
{
    public class Reservation
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }

        public int Quantity;
        public bool IsAccepted { get; set; }
    }
}