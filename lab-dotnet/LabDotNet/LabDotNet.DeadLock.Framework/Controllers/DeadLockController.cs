using System.Threading.Tasks;
using System.Web.Http;

namespace LabDotNet.DeadLock.Controllers
{
	public class DeadLockController : ApiController
	{
		public DeadLockController()
		{
		}

		public static async Task DelayAsync()
		{
			await Task.Delay(5000);
		}

		[HttpGet]
		public string Get()
		{
			var delayTask = DelayAsync();
			delayTask.Wait();

			return "Dead Lock";
		}
	}
}