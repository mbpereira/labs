namespace Example
{
	public class Program
	{
		async static Task NonBlockingProcessAsync()
		{
			for (var i = 0; i < 10; i++)
			{
				var delay = i * 500;
				Console.WriteLine($"delaying {i} * {500} = {delay}");
				await Task.Delay(delay);
			}
		}

		async static Task MakeAnything(Func<Task> process)
		{
			var task = process();

			for (var i = 0; i < 10; i++)
			{
				var delay = i * 500;
				Console.WriteLine($"Working... {delay}");
				await Task.Delay(delay);
			}

			await task;
		}

		static Task BlockingProcessAsync()
		{
			for (var i = 0; i < 10; i++)
			{
				var delay = i * 500;
				Console.WriteLine($"delaying {i} * {500} = {delay}");
				Task.Delay(delay).Wait();
			}

			return Task.CompletedTask;
		}

		public async static Task Main()
		{
			Console.WriteLine($"{nameof(BlockingProcessAsync)} Execution:");
			await MakeAnything(BlockingProcessAsync);

			Console.WriteLine($"{nameof(NonBlockingProcessAsync)} Execution: ");
			await MakeAnything(NonBlockingProcessAsync);

			Console.WriteLine($"{nameof(BlockingProcessAsync2)} Execution:");
			await MakeAnything(BlockingProcessAsync2);

			Console.WriteLine($"{nameof(NonBlockingProcessAsync2)} Execution: ");
			await MakeAnything(NonBlockingProcessAsync2);
		}

		async static Task NonBlockingProcessAsync2()
		{
			var tasks = Enumerable.Range(0, 10).Select(async i =>
			{
				var delay = i * 500;
				Console.WriteLine($"delaying {i} * {500} = {delay}");
				await Task.Delay(delay);
			});

			await Task.WhenAll(tasks);
		}

		async static Task BlockingProcessAsync2()
		{
			var tasks = Enumerable.Range(0, 10).Select(i =>
			{
				var delay = i * 500;
				Console.WriteLine($"delaying {i} * {500} = {delay}");
				Task.Delay(delay).Wait();
				return Task.CompletedTask;
			});

			await Task.WhenAll(tasks);
		}
	}
}