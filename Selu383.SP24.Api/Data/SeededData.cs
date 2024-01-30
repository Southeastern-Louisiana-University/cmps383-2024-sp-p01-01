using Microsoft.EntityFrameworkCore;
using Selu383.SP24.Api.Data;
using Selu383.SP24.Api.Dto;

namespace SP23.P01.Web.Data
{
    public static class SeededData
    {
        public static void Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<DataContext>();
            //context.Database.Migrate();

            AddHotels(context);
        }

        private static void AddHotels(DataContext context)
        {
            var hotels = context.Set<Hotel>();
            if (hotels.Any())
            {
                return;
            }

            hotels.Add(new Hotel
            {
                name = "The Roosevelt New Orleans",
                address = "130 Roosevelt Way, New Orleans, LA 70112"
            });

            context.SaveChanges();
        }
    }
}
