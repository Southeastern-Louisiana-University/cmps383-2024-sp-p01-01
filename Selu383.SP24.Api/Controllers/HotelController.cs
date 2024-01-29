using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Dto;
using Microsoft.AspNetCore.Mvc;
using Selu383.SP24.Api.Common;

namespace Selu383.SP24.Api.Controllers
{
    [ApiController]
    [Route("api/hotels")]
    public class HotelController : ControllerBase
    {
        private readonly DataContext _context;
        public HotelController(DataContext context)
        {
            _context = context;
        }
        [HttpGet("{id}")]
        public IActionResult GetHotelById(
            [FromRoute] int id)
        {
            var response = new Response();
            var hotel = _context.Hotels.FirstOrDefault(x => x.id == id);
            if (hotel == null)
            {
                response.AddError("id", "There was a problem finding the hotel.");
                return NotFound(response);
            }
            var hotelGetDto = new HotelDto
            {
                id = hotel.id,
                name = hotel.name,
                address = hotel.address
            };
            response.Data = hotelGetDto;
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var response = new Response();
            response.Data = _context
                .Hotels
                .Select(x  => new HotelDto
                {
                    id = x.id,
                    name = x.name,
                    address = x.address
                })
                .ToList();
            return Ok(response);
        }
        [HttpPost]
        public IActionResult Create(
            [FromBody]HotelCreateDto hotelCreateDto)
        {
            var response = new Response();
            if (response.HasErrors)
            {
                return BadRequest(response);
            }
            var newHotel = new Hotel();
        }








    }
}