using System;
using System.Threading.Tasks;
using AsyncMonads.Models;

namespace AsyncMonads
{
    public interface IReservationRepository
    {
        Task<Reservation[]> ReadReservations(DateTime date);
        Task<Maybe<int>> Create(Reservation reservation);
    }

    public class ReservationRepository : IReservationRepository
    {
        public async Task<Reservation[]> ReadReservations(DateTime date)
        {
            return await Task.FromResult(new[] {new Reservation
            {
                Date = DateTime.Now,
                Name = "Craig",
                Quantity = 2
            }});
        }

        public async Task<Maybe<int>> Create(Reservation reservation)
        {
            return await Task.FromResult(new Maybe<int>(1));
        }
    }
}