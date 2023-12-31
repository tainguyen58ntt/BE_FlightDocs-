using FlightDocs.Service.DocumentApi.Models.Dto;
using FlightDocs.Service.DocumentApi.Service.IService;
using Newtonsoft.Json;

namespace FlightDocs.Service.DocumentApi.Service
{
    public class FlightSerivice: IFlightService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public FlightSerivice(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<FlightResponseDto?> GetFlightByIdAsync(string id)
        {
            var client = _httpClientFactory.CreateClient("Flight");
            var response = await client.GetAsync($"/api/FlighApi/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                try
                {
                    var resp = JsonConvert.DeserializeObject<FlightResponseDto>(apiContent);
                    return resp;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    // Log the error or handle it appropriately
                    throw; // Rethrow the exception if needed
                }
            }
            else
            {
                // Log the non-successful status code
                Console.WriteLine($"Error: {response.StatusCode}");
                response.EnsureSuccessStatusCode(); // Throw an exception for non-successful status codes
                return null; // or return an appropriate response
            }
        }
    }
}
