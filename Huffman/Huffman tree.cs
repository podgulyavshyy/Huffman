using System;
using System.Collections.Generic;
using System.IO;

namespace Huffman
{
    public class HuffmanTree
    {
        public static void Main()
        {
            // Specify the path of the text file to read
            string filePath = "Poem.txt";

            // Create a new dictionary to store the characters and their counts
            Dictionary<char, int> charCounts = new Dictionary<char, int>();

            // Read the text file
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Loop through each character in the line
                    foreach (char c in line)
                    {
                        if (charCounts.ContainsKey(c))
                        {
                            charCounts[c]++;
                        }
                        else
                        {
                            charCounts.Add(c, 1);
                        }
                    }
                }
            }
            
            foreach (KeyValuePair<char, int> kvp in charCounts)
            {
                Console.WriteLine("Character '{0}' appears {1} times in the text file.", kvp.Key, kvp.Value);
            }
        }
    }
}