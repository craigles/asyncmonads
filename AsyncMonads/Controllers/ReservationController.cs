using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AsyncMonads.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsyncMonads.Controllers
{
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository = new ReservationRepository();
        private MaîtreD _maîtreD = new MaîtreD(20);

        [HttpPost]
        public async Task<IActionResult> PostV1(Reservation reservation)
        {
            var reservations = await _reservationRepository.ReadReservations(reservation.Date);

            Maybe<Reservation> m = _maîtreD.TryAccept(reservations, reservation);

            return await m
                .Select(async r => await _reservationRepository.Create(r))
                .Match<Task<IActionResult>>(
                    nothing: Task.FromResult(this.InternalServerError("Could not create reservation")),
                    just: async id => Ok(await id));
        }

        [HttpPost]
        public async Task<IActionResult> PostV2(Reservation reservation)
        {
            return await _reservationRepository.ReadReservations(reservation.Date)
                .Select(rs => _maîtreD.TryAccept(rs, reservation))
                .SelectMany(_reservationRepository.Create)
                .Match(this.InternalServerError("Could not create reservation"), Ok());

        }
    }
}
