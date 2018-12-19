using System.Linq;
using System.Threading.Tasks;
using AsyncMonads.Models;

namespace AsyncMonads
{
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