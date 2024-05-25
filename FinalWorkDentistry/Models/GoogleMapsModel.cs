namespace FinalWorkDentistry.Models
{
    public class GoogleMapsModel
    {
        public int Id { get; set; }
        public string PlaceName { get; set; } // название места
       // public double Traffic { get; set; } // пассажиропоток
        public string Clinic { get; set; } // клиника
        public double GeoLong { get; set; } // долгота - для карт google
        public double GeoLat { get; set; } // широта - для карт google
    }
}
