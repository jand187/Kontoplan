using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;

namespace KontoplanConsole
{
	class Program
	{
		private static List<EntryItem> entryItems = new List<EntryItem>();

		static void Main(string[] args)
		{
			Console.WriteLine("Kontoplan");


			while (true)
			{
				var command = Console.ReadLine();
				
				switch (command.ToLower().Split(' ').First())
				{
					case "add":
						AddEntry(command);
						break;

					case "list":
						ListEntries(command);
						break;

					case "sum":
						SumEntries(command);
						break;

					case "quit":
					case "q":
						return;
				}
			}
		}

		private static void SumEntries(string command)
		{
			Console.WriteLine($"\t\t\t{entryItems.Sum(i=>i.Amount):C}");
		}

		private static void ListEntries(string command)
		{
			entryItems.OrderBy(i => i.Date).ToList().ForEach(i=> Console.WriteLine(i));
		}

		private static void AddEntry(string command)
		{
			var item = EntryItem.Parse(command.Split(' ').Skip(1));
			entryItems.Add(item);

		}
	}

	public class EntryItem
	{
		public static EntryItem Parse(IEnumerable<string> args)
		{
			if (args.Count() != 3)
				Console.WriteLine($"Arguments incorrect '{string.Join(' ' , args)}'");

			return new EntryItem
			{
				Date = DateTime.Parse(args.First()),
				Text = args.Skip(1).First(),
				Amount = decimal.Parse(args.Skip(2).First()),
			};
		}

		public decimal Amount { get; set; }

		public string Text { get; set; }

		public DateTime Date { get; set; }

		public override string ToString()
		{
			return $"{Date:d}\t{Text}\t{Amount:C}";
		}
	}
}
