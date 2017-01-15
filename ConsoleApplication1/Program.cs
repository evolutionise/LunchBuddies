using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LunchBuddies
{
	class Program
	{
		static void Main(string[] args)
		{
			//Not worrying about matching team mates (at this stage)
			//What if there is an uneven number of people?
			var rawBuddiesInputString = args[0];
			var buddies = rawBuddiesInputString.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
			var buddies1 = buddies.Take(buddies.Length/2).ToArray();
			var buddies2 = buddies.Skip(buddies.Length/2).ToArray();
			buddies1 = Shuffle(buddies1);
			var pairs = MatchPairs(buddies1, buddies2);
			foreach (var pair in pairs)
			{
				pair.Print();
			}
		}

		static string[] Shuffle(string[] list)
		{
			//Fisher Yates Shuffle! 
			//Takes each element and swaps it with one of the preceeding elements. 
			var random = new Random();
			for(var i = 0; i < list.Length; i++)
			{
				int j = random.Next(i);
				var tmp = list[j];
				list[j] = list[i];
				list[i] = tmp;
			}
			return list;
		}

		static IEnumerable<Pair> MatchPairs(string[] list1, string[] list2)
		{
			var pairs = list1.Select((t, i) => new Pair() {Person1 = t, Person2 = list2[i]}).ToArray();
			//what if list2 is longer than list1 by more than 1? Then this is screwed, but the problem is upstrea
			if (list2.Length > list1.Length)
			{
				pairs[pairs.Length] = new Triumvirate() {Pair = pairs.Last(), Person3 = list2.Last()};
			}
			return pairs;
		}
	}

	class Pair
	{
		public string Person1 { get; set; }
		public string Person2 { get; set; }

		public virtual void Print()
		{
			Console.WriteLine($"{Person1} is having lunch with {Person2} this week!");
		}
	}

	class Triumvirate : Pair
	{
		public Pair Pair { get; set; }
		public string Person3 { get; set; }

		public override void Print()
		{
			Console.WriteLine($"{Person1} is having lunch with {Person2} and {Person3} this week!");
		}
	}
}