using Microsoft.AspNetCore.Mvc;

namespace PortExhaust.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController : ControllerBase
    {
        private readonly ILogger<CepController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _fakeApiBaseUrl;
        private readonly string _fakeApiToken;

        public CepController(ILogger<CepController> logger, IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _fakeApiBaseUrl = configuration.GetValue<string>("FAKE_API_BASE_URL");
            _fakeApiToken = configuration.GetValue<string>("FAKE_API_TOKEN");
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            using var _ = _logger.BeginScope(new Dictionary<string, object> { { "description", "instance-per-request" } });

            using (var httpClient = new HttpClient())
            {
                httpClient.DefaultRequestHeaders.Add("x-token", _fakeApiToken);
                _logger.LogInformation("Starting viacep");

                var response = await httpClient.GetAsync($"{_fakeApiBaseUrl}/ceps");
                var content = await response.Content.ReadAsStringAsync();

                _logger.LogInformation($"viacep response {content}");

                return Ok(content);
            }
        }

        [HttpGet("2")]
        public async Task<IActionResult> Get2()
        {
            using var _ = _logger.BeginScope(new Dictionary<string, object> { {  "description", "single-instance" } });

            var httpClient = _httpClientFactory.CreateClient();
            httpClient.DefaultRequestHeaders.Add("x-token", _fakeApiToken);

            _logger.LogInformation("Starting viacep");

            var response = await httpClient.GetAsync($"{_fakeApiBaseUrl}/ceps");
            var content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"viacep response {content}");

            return Ok(content);
        }
    }
}