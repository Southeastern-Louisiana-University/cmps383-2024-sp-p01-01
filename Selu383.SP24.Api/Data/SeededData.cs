using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Dto;

namespace Selu383.SP24.Api.Data
{
    public static class SeededData
    {
        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<DataContext>();
            context.Database.Migrate();

            AddHotels(context);
        }

        private static void AddHotels(DataContext context)
        {
            var hotels = context.Set<Hotel>();
            //if (hotels.Any())
            //{
            //    return;
            //}

            hotels.Add(new Hotel
            {
                name = "The Roosevelt, New Orleans",
                address = "130 Roosevelt Way, New Orleans, LA 70112"
            });
            hotels.Add(new Hotel
            {
                name = "The Ritz-Carlton, New Orleans",
                address = "921 Canal St, New Orleans, LA 70112"
            });
            hotels.Add(new Hotel
            {
                name = "Four Seasons Hotel, New Orleans",
                address = "2 Canal St, New Orleans, LA 70130"
            });

            context.SaveChanges();
        }
    }
}
