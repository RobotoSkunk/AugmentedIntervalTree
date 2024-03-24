using System.Diagnostics;


namespace RobotoSkunk
{
	class Program
	{
		static readonly float time = 1;
		static readonly Random random = new();
		static readonly int total = 100000;

		static int needed = 0;
		static int wrong = 0;

		// static Interval GenerateInterval()
		// {
		// 	Interval newInterval = new(GetRandom(0, 50), GetRandom(50, 100));

		// 	// Console.WriteLine($"{newInterval} {(newInterval.HasTime(time) ? "(Has Time)" : "")}");

		// 	if (newInterval.HasTime(time)) {
		// 		needed++;
		// 	}

		// 	return newInterval;
		// }

		static float GetRandom(float min, float max)
		{
			return min + (float)random.NextDouble() * (max - min);
		}


		static void Main(string[] args)
		{
			Stopwatch generateTreeWatch = new();
			Stopwatch searchIntervalsWatch = new();

			generateTreeWatch.Start();


			Interval? root = null;
			// float maxTime = 100;

			for (int i = 0; i < total; i++) {
				Interval newInterval = new(GetRandom(0, 50), GetRandom(50, 100));
				// Interval newInterval = new(i, i + 1);

				if (newInterval.HasTime(time)) {
					needed++;
				}

				if (root == null) {
					root = newInterval;
				} else {
					root.AddNode(newInterval);
				}
			}

			generateTreeWatch.Stop();


			searchIntervalsWatch.Start();

			List<Interval> intervals = new();
			root?.GetIntersectIntervals(time, intervals);

			searchIntervalsWatch.Stop();


			// Console.WriteLine($"\nNodes overlaping {time}");

			foreach (Interval interval in intervals) {
				// Console.WriteLine(interval);

				if (!interval.HasTime(time)) {
					wrong++;
				}
			}

			Console.WriteLine($"Total: {total}");
			Console.WriteLine($"Needed {needed}");
			Console.WriteLine($"Found {intervals.Count}");
			Console.WriteLine($"Wrong {wrong}\n");
			Console.WriteLine($"Generate tree elapsed time: {generateTreeWatch.ElapsedMilliseconds} ms");
			Console.WriteLine($"Search intervals elapsed time: {searchIntervalsWatch.ElapsedMilliseconds} ms\n");
		}
	}
}
