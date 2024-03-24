using System.Diagnostics;


namespace RobotoSkunk
{
	class Program
	{
		static readonly float time = 1;
		static readonly Random random = new();
		static readonly int total = 500000;

		static int needed = 0;

		public static int operations = 0;


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

			for (int i = 0; i < total; i++) {
				float start = GetRandom(0, 100);
				float end = start + GetRandom(0, 100 - start);

				Interval newInterval = new(start, end);

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
			root?.FindIntersectIntervals(time, intervals);

			searchIntervalsWatch.Stop();


			Console.WriteLine($"Looking for: {time}\n");

			Console.WriteLine($"Total nodes: {total}");
			Console.WriteLine($"Needed: {needed}");
			Console.WriteLine($"Found: {intervals.Count}");

			Console.WriteLine($"Executed operations: {operations}");
			Console.WriteLine($"Operations ratio: {(double)operations / total}\n");

			Console.WriteLine($"Generate tree elapsed time: {generateTreeWatch.ElapsedMilliseconds} ms");
			Console.WriteLine($"Search intervals elapsed time: {searchIntervalsWatch.ElapsedMilliseconds} ms\n");
		}
	}
}
