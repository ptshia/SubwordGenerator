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
	}
}
