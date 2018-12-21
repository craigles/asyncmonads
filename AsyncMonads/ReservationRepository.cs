using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncMonads.Models;

namespace AsyncMonads
{
    public interface IReservationRepository
    {
        Task<Reservation[]> ReadReservations(DateTime date);
        Task<int> Create(Reservation reservation);
        Task<int> Capacity();
    }

    public class ReservationRepository : IReservationRepository
    {
        private static int _reservationNumber = 1;

        private static readonly IList<Reservation> Reservations = new List<Reservation>{
            new Reservation
            {
                Date = DateTime.Now,
                Name = "Someone",
                Quantity = 2,
                IsAccepted = true
            },
            new Reservation
            {
                Date = DateTime.Now,
                Name = "Someone else",
                Quantity = 4,
                IsAccepted = true
            }};

        public async Task<Reservation[]> ReadReservations(DateTime date)
        {
            return await Task.FromResult(Reservations.ToArray());
        }

        public async Task<int> Create(Reservation reservation)
        {
            Reservations.Add(reservation);
            return await Task.FromResult(_reservationNumber++);
        }

        public async Task<int> Capacity()
        {
            return await Task.FromResult(20);
        }
    }
}