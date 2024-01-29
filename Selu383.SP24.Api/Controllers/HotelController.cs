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
            var newHotel = new Hotel()
            {
                name = hotelCreateDto.name,
                address = hotelCreateDto.address,
            };
            _context.Hotels.Add(newHotel);
            _context.SaveChanges();
            var hotelToReturn = new HotelDto
            {
                id=newHotel.id,
                name = hotelCreateDto.name,
                address = hotelCreateDto.address,
            };
            response.Data= hotelToReturn;
            return Created("", response);
        }
        [HttpPut("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] HotelUpdateDto hotelUpdateDto)
        {
            var response = new Response();
            var hotelToUpdate = _context.Hotels.FirstOrDefault(hotel => hotel.id == id);
            
            if (hotelToUpdate == null)
            {
                response.AddError("id", "Hotel not found");
                return BadRequest(response);

            }

            var updatedHotelDto = new HotelDto
            {
                id = hotelToUpdate.id,
                name = hotelToUpdate.name,
                address = hotelToUpdate.address
            };
            response.Data = updatedHotelDto;
            return Ok(response);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var response = new Response();
            var hotelToDelete = _context.Hotels.FirstOrDefault(hotel => hotel.id == id);

            if (hotelToDelete == null)
            {
                response.AddError("id", "Hotel not found");
                return BadRequest(response);
            }
            _context.Remove(hotelToDelete);
            _context.SaveChanges();
            response.Data = true;
            return Ok(response);
        }
    }
}