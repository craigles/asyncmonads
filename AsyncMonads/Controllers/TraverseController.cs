using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncMonads.Extensions;
using AsyncMonads.Models;
using Microsoft.AspNetCore.Mvc;

namespace AsyncMonads.Controllers
{
    [ApiController]
    public class TraverseController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository = new ReservationRepository();

        [HttpPost]
        [Route("api/traverse/post")]
        public async Task<IActionResult> Post(Reservation reservation)
        {
            var t1 = _reservationRepository.Create(reservation);
            var t2 = _reservationRepository.Create(reservation);
            var t3 = _reservationRepository.Create(reservation);

            var list = new List<Task<int>> { t1, t2, t3 };

            return Ok(await list.Traverse(i => i * 2));
        }
    }
}