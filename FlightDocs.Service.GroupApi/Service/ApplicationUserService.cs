using FlightDocs.Service.GroupApi.Models.Dto;
using FlightDocs.Service.GroupApi.Service.IService;
using Newtonsoft.Json;


namespace FlightDocs.Service.GroupApi.Service
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IHttpClientFactory _httpClientFactory; 
        public ApplicationUserService(IHttpClientFactory httpClientFactory) { 
            _httpClientFactory = httpClientFactory;
        }

        public async  Task<ApplicationUserDto?> GetUserByEmail(string email)
        {
            var client = _httpClientFactory.CreateClient("ApplicationUser");
            var response = await client.GetAsync($"/api/ApplicationUserApi/get-by-email?email={email}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                try
                {
                    var resp = JsonConvert.DeserializeObject<ApplicationUserDto>(apiContent);
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
                //response.EnsureSuccessStatusCode(); // Throw an exception for non-successful status codes
                return null; // or return an appropriate response
            }
        }

        //public async Task<IEnumerable<ApplicationUserDto>> GetUserNameById(string id)
        //{
        //    var client = _httpClientFactory.CreateClient("ApplicationUser");
        //    var response = await client.GetAsync($"/api/ApplicationUserApi"); 
        //    var apiContent = await response.Content.ReadAsStringAsync();

        //    try
        //    {
        //        var resp = JsonConvert.DeserializeObject<IEnumerable<ApplicationUserDto>>(Convert.ToString(apiContent));

        //    }
        //    catch(Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);  
        //    }
        //    //return (IEnumerable<ApplicationUserDto>)resp;
        //    return JsonConvert.DeserializeObject<IEnumerable<ApplicationUserDto>>(apiContent);
        //}
        public async Task<ApplicationUserDto?> GetUserById(string id)
        {
            var client = _httpClientFactory.CreateClient("ApplicationUser");
            var response = await client.GetAsync($"/api/ApplicationUserApi/{id}").ConfigureAwait(false);

            if (response.IsSuccessStatusCode)
            {
                var apiContent = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                try
                {
                    var resp = JsonConvert.DeserializeObject<ApplicationUserDto>(apiContent);
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
