using System;
using SubwordGenerator.Infrastructure;

namespace SubwordGenerator.Service
{
	public class WordsGenerator
	{
		private static readonly int size = 26;

		public void PrintAllWords(char[] inputLetters, TrieNode root, int inputLength, int minLength, bool dictionaryFlag)
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

		private void SearchWord(TrieNode root, bool[] hasFlag, string str, int minLength, bool dictionaryFlag)
		{
			FilterWord(root, str, minLength, dictionaryFlag);
			for (int i = 0; i < size; i++)
			{
				if (hasFlag[i] == true && root.Child[i] != null)
				{
					char c = (char)(i + 'a');
					SearchWord(root.Child[i], hasFlag, str + c, minLength, dictionaryFlag);
				}
			}
		}

		private void FilterWord(TrieNode root, string str, int minLength, bool dictionaryFlag)
        {
			if (root.leaf == true && str.Length >= minLength)
			{
                if (dictionaryFlag)
				{
					var connector = new DictionaryConnector();
                    string definition = connector.SearchDefinition(str);
                    Console.WriteLine("Word: " + str + " - Meaning: " + definition);
				}
                else
                {
					Console.WriteLine("Word: " + str);
				}
				
			}
		}
	}
}
