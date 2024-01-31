using System;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Dto;
using Microsoft.AspNetCore.Mvc;

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
            var hotel = _context.Hotels.FirstOrDefault(x => x.id == id);
            if (hotel == null)
            {
                return NotFound("There was a problem finding the hotel.");
            }
            var hotelGetDto = new HotelDto
            {
                id = hotel.id,
                name = hotel.name,
                address = hotel.address
            };
            return Ok(hotelGetDto);
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var hotelDtos = _context.Hotels.Select(x => new HotelDto
            {
                id = x.id,
                name = x.name,
                address = x.address
            }).ToList();

            return Ok(hotelDtos);
        }
        [HttpPost]
        public IActionResult Create(
            [FromBody] HotelCreateDto hotelCreateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (hotelCreateDto.name.Length > 100)
            {
                return BadRequest("Hotel name is too long");
            }
            if (string.IsNullOrEmpty(hotelCreateDto.address))
            {
                return BadRequest("Hotel address is required");
            }
            var newHotel = new Hotel()
            {
                name = hotelCreateDto.name,
                address = hotelCreateDto.address,
            };
            _context.Hotels.Add(newHotel);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetHotelById), new { Id = newHotel.id }, newHotel);
        }
        [HttpPut("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] HotelUpdateDto hotelUpdateDto)
        {
            var hotelToUpdate = _context.Hotels.FirstOrDefault(hotel => hotel.id == id);
            if (hotelToUpdate == null)
            {
                return NotFound("Hotel not found");
            }
            if (hotelUpdateDto.name.Length > 100)
            {
                return BadRequest("Hotel name is too long");
            }
            hotelToUpdate.name = hotelUpdateDto.name;
            hotelToUpdate.address = hotelUpdateDto.address;
            _context.SaveChanges();
            var updatedHotelDto = new HotelDto
            {
                id = hotelToUpdate.id,
                name = hotelToUpdate.name,
                address = hotelToUpdate.address
            };
            return Ok(updatedHotelDto);
        }
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            var hotelToDelete = _context.Hotels.FirstOrDefault(hotel => hotel.id == id);

            if (hotelToDelete == null)
            {
                return NotFound("Hotel not found");
            }
            _context.Remove(hotelToDelete);
            _context.SaveChanges();
            return Ok("Hotel successfully removed");
        }
    }
}