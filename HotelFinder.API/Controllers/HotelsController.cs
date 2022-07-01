using HotelFinder.Business.Abstract;
using HotelFinder.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelFinder.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _service;

        public HotelsController(IHotelService service)
        {
            _service = service;
        }

        [HttpGet("GetHotels")]
        public async Task<IActionResult>  GetHotels()
        {
            
            var hotels = await _service.GetAllHotels();
            return Ok(hotels);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetHotelById(int id)
        {
            var hotel =  await _service.GetHotelById(id);
            if (hotel != null)
            {
                return Ok(hotel);
            }
            return NotFound();//404
        }

        [HttpPost]
        public async Task<IActionResult>Post([FromBody] Hotel hotel)
        {
              var createhotel =await  _service.CreateHotel(hotel);
                return CreatedAtAction("Get", new { id = createhotel.Id }, createhotel);
           
        }


        [HttpPut]
        public async Task<IActionResult> Put([FromBody] Hotel hotel)
        {
            if ( await _service.GetHotelById(hotel.Id ) != null)
            {
                return Ok( await _service.UpdateHotel(hotel));
            }
            return NotFound();
           
        }

        [HttpDelete ("id")]
        public async Task Delete( int id)
        {
            await _service.DeleteHotel(id);
        }
    }

}
