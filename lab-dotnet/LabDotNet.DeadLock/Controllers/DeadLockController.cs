using Microsoft.AspNetCore.Mvc;

namespace LabDotNet.DeadLock.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class DeadLockController : ControllerBase
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
			ThreadPool.SetMaxThreads(1, 1);
			
			var delayTask = DelayAsync();
			delayTask.Wait();

			return "Dead Lock";
		}
	}
}