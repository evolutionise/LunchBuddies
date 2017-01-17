using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
			var buddies = rawBuddiesInputString.Split(new string[] {"\n"}, StringSplitOptions.None);
			buddies = Shuffle(buddies);
			var buddies1 = buddies.Take(buddies.Length/2).ToArray();
			var buddies2 = buddies.Skip(buddies.Length/2).ToArray();
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
			for (var i = 0; i < list.Length; i++)
			{
				int j = random.Next(i);
				var tmp = list[j];
				list[j] = list[i];
				list[i] = tmp;
			}
			return list;
		}

		static IEnumerable<LunchGroup> MatchPairs(string[] list1, string[] list2)
		{
			var pairs = new List<LunchGroup>();
			for (var i = 0; i < list1.Length; i++)
			{
				var group = new LunchGroup() { People = new string[] { list1[i], list2[i] } };
				pairs.Add(group);
			}
			//what if list2 is longer than list1 by more than 1? Then this is screwed, but the problem is upstream
			if (list2.Length <= list1.Length) return pairs;

			var lastIndex = pairs.Count - 1;
			var lastPair = pairs[lastIndex].People;
			var lastThree = new string[] {lastPair[0], lastPair[1], list2.Last()};
			pairs[lastIndex].People = lastThree;
			return pairs;
		}
	}

	class LunchGroup
	{
		public string[] People { get; set; }

		public void Print()
		{
			Console.WriteLine(People.Length == 2
				? $"{People[0]} is having lunch with {People[1]} this week!"
				: $"{People[0]} is having lunch with {People[1]} and {People[2]} this week!");
		}
	}
}