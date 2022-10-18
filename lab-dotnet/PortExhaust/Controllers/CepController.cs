using Microsoft.AspNetCore.Mvc;

namespace PortExhaust.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CepController : ControllerBase
    {
        private readonly ILogger<CepController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public CepController(ILogger<CepController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        [HttpGet("{cep}")]
        public async Task<IActionResult> Get(string cep)
        {
            using var _ = _logger.BeginScope(new Dictionary<string, object> { { "cep", cep } });

            using (var httpClient = new HttpClient())
            {
                _logger.LogInformation("Starting viacep");

                var response = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
                var content = await response.Content.ReadAsStringAsync();

                _logger.LogInformation($"viacep response {content}");

                return Ok(content);
            }
        }

        [HttpGet("2/{cep}")]
        public async Task<IActionResult> Get2(string cep)
        {
            using var _ = _logger.BeginScope(new Dictionary<string, object> { { "cep", cep } });

            var httpClient = _httpClientFactory.CreateClient();
            _logger.LogInformation("Starting viacep");

            var response = await httpClient.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var content = await response.Content.ReadAsStringAsync();

            _logger.LogInformation($"viacep response {content}");

            return Ok(content);
        }
    }
}