using System;
using System.IO;
using System.Reflection;
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
			Console.WriteLine("Enter the minimum number of letters you want in a word");
			int.TryParse(Console.ReadLine(), out int minLength);
			Console.WriteLine("Do you want the dictionary feature?");
			var dictionaryFlagInput = Console.ReadLine();

			int inputLength = inputString.Length;
			bool dictionaryFlag = false;
			if (dictionaryFlagInput == "Y" || dictionaryFlagInput == "y")
			{
				dictionaryFlag = true;
			}
			WordsGenerator wordsGenerator = new WordsGenerator();
			wordsGenerator.PrintAllWords(inputString, root, inputLength, minLength, dictionaryFlag);
		}
	}
}