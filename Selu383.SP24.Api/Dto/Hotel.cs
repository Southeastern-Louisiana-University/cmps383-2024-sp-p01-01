namespace Selu383.SP24.Api.Dto
{

    public class Hotel
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
    public class HotelDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }

    public class HotelCreateDto
    {
        public int id { get; set; }
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }

    public class HotelUpdateDto
    {
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }

    public class HotelDeleteDto
    {
        public string name { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
    }
}
