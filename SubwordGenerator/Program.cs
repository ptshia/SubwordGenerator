using System;
using System.IO;
using System.Reflection;
using SubwordGenerator.Infrastructure;

namespace SubwordGenerator
{
    class Program
    {
		private static int size = 26;
        static void Main(string[] args)
        {
			string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Utilities/WordsDataSet.txt");
			string[] words = File.ReadAllLines(path);

			TrieNode root = new TrieNode();
			int wordsLength = words.Length;
			for (int i = 0; i < wordsLength; i++)
			{
				InsertIntoTrie(root, words[i]);
			}

            Console.WriteLine("Subword generator is now up! Input your letters");
            var inputString = Console.ReadLine();
            Console.WriteLine("Enter the minimum number of letters you want in a word");
			int.TryParse(Console.ReadLine(), out int minLength);

			Console.WriteLine("Do you want the dictionary feature?");
			var dictionaryFlagInput = Console.ReadLine();

			char[] inputLetters = inputString.ToCharArray();
			int inputLength = inputLetters.Length;

			bool dictionaryFlag = false;

			if (dictionaryFlagInput == "Y" || dictionaryFlagInput == "y")
			{
				dictionaryFlag = true;

			}
				PrintAllWords(inputLetters, root, inputLength, minLength, dictionaryFlag);
		}

		static void InsertIntoTrie(TrieNode root, string word)
		{
			int wordLength = word.Length;
			TrieNode trie = root;

			for (int i = 0; i < wordLength; i++)
			{
				int index = word[i] - 'a';

				if (trie.Child[index] == null)
				{
					trie.Child[index] = new TrieNode();
				}
				trie = trie.Child[index];
			}
			trie.leaf = true;
		}

		static void SearchWord(TrieNode root, bool[] hasFlag, string str, int minLength, bool dictionaryFlag)
		{
			if (root.leaf == true && str.Length >= minLength)
			{
				string definition = "";
				if (dictionaryFlag)
				{
					var connector = new DictionaryConnector();
				    definition = connector.SearchDefinition(str);
				}

				Console.WriteLine("Word: "+ str + " - Meaning: " +definition);
			}

			for (int i = 0; i < size; i++)
			{
				if (hasFlag[i] == true && root.Child[i] != null)
				{
					char c = (char)(i + 'a');
					SearchWord(root.Child[i], hasFlag, str + c, minLength, dictionaryFlag);
				}
			}
		}
 
		static void PrintAllWords(char[] inputLetters, TrieNode root, int inputLength, int minLength, bool dictionaryFlag)
		{
			bool[] hasFlag = new bool[size];

			for (int i = 0; i < inputLength; i++)
			{
				hasFlag[inputLetters[i] - 'a'] = true;
			}

			TrieNode trie = root;
			string str = "";
			for (int i = 0; i < size; i++)
			{
				if (hasFlag[i] == true && trie.Child[i] != null)
				{
					str += (char)(i + 'a');
					SearchWord(trie.Child[i], hasFlag, str, minLength, dictionaryFlag);
					str = "";
				}
			}
		}

	}
}
