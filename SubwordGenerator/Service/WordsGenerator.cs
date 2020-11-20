using System;
using System.Collections.Generic;
using System.Linq;
using SubwordGenerator.Infrastructure;

namespace SubwordGenerator.Service
{
	public class WordsGenerator
	{
		private static readonly int size = 26;

		public void PrintAllWords(string inputString, TrieNode root, int inputLength, int minLength, bool dictionaryFlag)
		{
			char[] inputLetters = inputString.ToCharArray();
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
					SearchWord(trie.Child[i], hasFlag, str, minLength, dictionaryFlag, inputString);
					str = "";
				}
			}
		}

		private void SearchWord(TrieNode root, bool[] hasFlag, string str, int minLength, bool dictionaryFlag, string inputString)
		{
			FilterWord(root, str, minLength, dictionaryFlag, inputString);
			for (int i = 0; i < size; i++)
			{
				if (hasFlag[i] == true && root.Child[i] != null)
				{
					char c = (char)(i + 'a');
					SearchWord(root.Child[i], hasFlag, str + c, minLength, dictionaryFlag, inputString);
				}
			}
		}

		private void FilterWord(TrieNode root, string str, int minLength, bool dictionaryFlag, string inputString)
		{
			if (root.leaf == true && str.Length >= minLength)
			{
				var isEqual = LetterOccuranceChecker(inputString, str);
				if (isEqual)
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

		private bool LetterOccuranceChecker(string inputString, string word)
        {
			int[] count = new int[256];
			for (int i = 0; i < inputString.Length; i++)
			{
				count[inputString[i]]++;
			}
			for (int i = 0; i < word.Length; i++)
			{
				if (count[word[i]] == 0)
				{
					return false;
				}
				count[word[i]]--;
			}
			return true;
		}
	}
}
