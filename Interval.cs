
namespace RobotoSkunk
{
	public class Interval
	{
		public float Start { get; private set; }
		public float End { get; private set; }
		public float Max { get; private set; }

		public Interval? Left { get; private set; }
		public Interval? Right { get; private set; }


		public Interval (float start, float end)
		{
			Start = start;
			End = end;
			Max = end;
		}

		public override string ToString()
		{
			return $"[{Start}, {End} ({Max})]";
		}


		public int CompareTo(Interval other)
		{
			if (Start < other.Start) {
				return -1;
			}

			if (Start == other.Start) {
				return End <= other.End ? -1 : 1;
			}

			return 1;
		}


		public Interval AddNode(Interval newNode)
		{
			if (newNode.End > Max) {
				Max = newNode.End;
			}

			if (CompareTo(newNode) < 0) {
				if (Right == null) {
					Right = newNode;
				} else {
					Right.AddNode(newNode);
				}
			} else {
				if (Left == null) {
					Left = newNode;
				} else {
					Left.AddNode(newNode);
				}
			}


			return this;
		}


		public bool HasTime(float time)
		{
			return (Start <= time) && (End >= time);
		}


		public void GetIntersectIntervals(float time, List<Interval> result)
		{
			if (HasTime(time)) {
				result ??= new();

				result.Add(this);
			}

			if (Left != null && Left.Max >= time) {
				Left.GetIntersectIntervals(time, result);
			}

			Right?.GetIntersectIntervals(time, result);
		}


		public void PrintTree()
		{
			Left?.PrintTree();

			Console.Write(this);

			Right?.PrintTree();
		}
	}
}
