using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SogoHotel.Models;


namespace SogoHotel.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SearchHotel(HotelSearchViewModel model)
        {
            TempData["checkin"] = model.CheckIn.ToString("yyyy-MM-dd");
            TempData["checkout"] = model.CheckOut.ToString("yyyy-MM-dd");

            string destinationid = await GetDestIDHotel(model.City);
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchHotels?dest_id={destinationid}&search_type=CITY&arrival_date={model.CheckIn.ToString("yyyy-MM-dd")}&departure_date={model.CheckOut.ToString("yyyy-MM-dd")}&adults={model.AdultCount}&children_age={model.ChildrenCount}&languagecode=en-us"),
                Headers =
    {
        { "x-rapidapi-key", "ed7410b8eamshddce4bfa3980e7bp1f495fjsn7ed6c4965628" },
        { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var value = JsonConvert.DeserializeObject<HotelListViewModel>(body);
                return View(value.data.hotels.ToList());
            }
        }

        public async Task<string> GetDestIDHotel(string city)
        {
            
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"https://booking-com15.p.rapidapi.com/api/v1/hotels/searchDestination?query={city}"),
                Headers =
    {
        { "x-rapidapi-key", "ed7410b8eamshddce4bfa3980e7bp1f495fjsn7ed6c4965628" },
        { "x-rapidapi-host", "booking-com15.p.rapidapi.com" },
    },
            };
            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetDestinationViewModel>(body);
                return values.data[0].dest_id.ToString();
            }
        }

		//public async Task<IActionResult> DetailsHotel(string name, string image,string description,string price )
		//{


		//}

	}
}
