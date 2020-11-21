using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using SubwordGenerator.Service;

namespace SubwordGenerator
{
	class Program
	{
		static void Main(string[] args)
		{
			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Utilities/WordsDataSet.txt");
			string[] words = File.ReadAllLines(path);

			TrieNode root = new TrieNode();
			int wordsLength = words.Length;
			for (int i = 0; i < wordsLength; i++)
			{
				root.InsertIntoTrie(root, words[i]);
			}

			Console.WriteLine("Subword generator is now up! Input your letters");
			var inputString = Console.ReadLine();

			var isValid = false;
			while (!isValid)
			{
				if (!Regex.Match(inputString, @"^[a-zA-Z]+$").Success)
				{
					Console.WriteLine("Input is not valid. Please enter only alphabets");
					inputString = Console.ReadLine();
				}
				else
				{
					isValid = true;
				}
			}

			Console.WriteLine("Enter the minimum number of letters you want in a word");
			int minLength;
			var isminValid = int.TryParse(Console.ReadLine(), out minLength);
			isValid = false;
			while (!isValid)
            {
				if (!isminValid)
				{
					Console.WriteLine("Input is not valid. Please enter only numbers");
					int.TryParse(Console.ReadLine(), out minLength);
				}
				else
				{
					isValid = true;
				}			
			}

			Console.WriteLine("Enter a mandatory letter. Press 0 to skip");
			var mandatoryInput = Console.ReadLine();

			int inputLength = inputString.Length;
			WordsGenerator wordsGenerator = new WordsGenerator();
			wordsGenerator.FindAllWords(inputString.ToLowerInvariant(), root, inputLength, minLength, mandatoryInput.ToLowerInvariant());
		}
	}
}