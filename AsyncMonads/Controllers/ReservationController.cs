using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncMonads.Extensions;
using AsyncMonads.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsyncMonads.Controllers
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository = new ReservationRepository();
        private readonly MaîtreD _maîtreD;

        public ReservationController()
        {
            _maîtreD = new MaîtreD(_reservationRepository.Capacity().Result);
        }

        /// <summary>
        /// https://youtu.be/F9bznonKc64?t=3114
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/reservation/post")]
        public async Task<IActionResult> Post(Reservation reservation)
        {
            var reservations = await _reservationRepository.ReadReservations(reservation.Date);

            Maybe<Reservation> m = _maîtreD.TryAccept(reservations, reservation);

            return await m
                .Select(async r => await _reservationRepository.Create(r))
                .Match<Task<IActionResult>>(
                    nothing: Task.FromResult(this.InternalServerError("Could not create reservation")),
                    just: async id => Ok(await id));
        }

        /// <summary>
        /// https://youtu.be/F9bznonKc64?t=3356
        /// </summary>
        /// <param name="reservation"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/reservation/PostWithTaskExtensions")]
        public async Task<IActionResult> PostWithTaskExtensions(Reservation reservation)
        {
            return await _reservationRepository.ReadReservations(reservation.Date)
                .Select(rs => _maîtreD.TryAccept(rs, reservation))
                .SelectMany(m => m.Traverse(r => _reservationRepository.Create(r)))
                .Match(this.InternalServerError("Could not create reservation"), id => Ok(id));
        }
    }
}
