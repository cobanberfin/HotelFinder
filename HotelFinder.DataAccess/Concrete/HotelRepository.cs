using HotelFinder.DataAccess.Abstract;
using HotelFinder.Entites;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelFinder.DataAccess.Concrete
{
    public class HotelRepository : IHotelRepository
    {
        public async Task<Hotel> CreateHotel(Hotel hotel)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                var create = await hotelDbContext.Hotels.AddAsync(hotel);
                hotelDbContext.SaveChanges();
                return hotel;
            }
        }

        public async Task DeleteHotel(int id)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                var deletedHotel = await GetById(id);
                hotelDbContext.Hotels.Remove(deletedHotel);
                await hotelDbContext.SaveChangesAsync();
            }
        }

        public async Task<List<Hotel>> GetAllHotels()
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                return await hotelDbContext.Hotels.ToListAsync();
            }
        }

        public async Task<Hotel> GetById(int id)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                return await hotelDbContext.Hotels.FindAsync(id)!; //find metodunu id alanı primarkey dıye kullandım.diğer turlu firstor ile gıtmem gerekırdı.
            }
        }

        public async Task<Hotel> GetHotelByName(string name)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                return await hotelDbContext.Hotels.FirstOrDefaultAsync(x => x.Name == name); 
            }
            
        }

        public async Task<Hotel> UpdateHotel(Hotel hotel)
        {
            using (var hotelDbContext = new HotelDbContext())
            {
                hotelDbContext.Update(hotel);
               await hotelDbContext.SaveChangesAsync();
                return hotel;
            }
        }

    }
}
