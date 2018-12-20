using System.Linq;
using System.Threading.Tasks;
using AsyncMonads.Models;

namespace AsyncMonads
{
    public interface IMaîtreDObjectOriented
    {
        Task<int?> TryAccept(Reservation reservation);
    }

    /// <summary>
    /// https://youtu.be/F9bznonKc64?t=177
    /// </summary>
    public class MaîtreDObjectOriented : IMaîtreDObjectOriented
    {
        private readonly IReservationRepository _reservationRepository;

        public MaîtreDObjectOriented(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<int?> TryAccept(Reservation reservation)
        {
            var reservations = await _reservationRepository.ReadReservations(reservation.Date);
            var capacity = await _reservationRepository.Capacity();
            int reservedSets = reservations.Sum(r => r.Quantity);

            if (capacity < reservedSets + reservation.Quantity)
                return await Task.FromResult((int?)null);

            reservation.IsAccepted = true;

            return await _reservationRepository.Create(reservation);
        }
    }
}