namespace SubwordGenerator
{
	public class TrieNode
	{
		public TrieNode[] Child = new TrieNode[26];
		public bool leaf;
		public TrieNode()
		{
			leaf = false;
			for (int i = 0; i < 26; i++)
			{
				Child[i] = null;
			}
		}

		public void InsertIntoTrie(TrieNode root, string word)
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
	}
}
