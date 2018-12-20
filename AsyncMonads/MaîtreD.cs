using System.Linq;
using AsyncMonads.Models;

namespace AsyncMonads
{
    /// <summary>
    /// A more functional implementation of <see cref="MaîtreDObjectOriented"/>.
    /// No dependencies or async. Using the <see cref="Maybe{T}"/> monad to cater for nulls.
    ///
    /// https://youtu.be/F9bznonKc64?t=3237
    /// </summary>
    public class MaîtreD
    {
        private readonly int _capacity;

        public MaîtreD(int capacity)
        {
            _capacity = capacity;
        }

        public Maybe<Reservation> TryAccept(Reservation[] reservations, Reservation reservation)
        {
            int reservedSets = reservations.Sum(r => r.Quantity);

            if (_capacity < reservedSets + reservation.Quantity)
                return new Maybe<Reservation>();

            reservation.IsAccepted = true;

            return new Maybe<Reservation>(reservation);
        }
    }
}