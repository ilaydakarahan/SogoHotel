namespace SogoHotel.Models
{
	public class HotelSearchViewModel
	{
		public string City { get; set; }
		public DateTime CheckIn { get; set; }
		public DateTime CheckOut { get; set; }
		public int AdultCount { get; set; }
		public int ChildrenCount { get; set; }

	}
}
