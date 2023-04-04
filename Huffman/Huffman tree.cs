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
            
            // Create an array to store the key-value pairs from the dictionary
            KeyValuePair<char, int>[] charArray = charCounts.ToArray();
            
            BuildMaxHeap(charArray);

            // Print out the max heap
            int x = 1;
            int y = 1;
            foreach (KeyValuePair<char, int> kvp in charArray)
            {
                Console.WriteLine("Character '{0}' appears {1} times in the text file.", kvp.Key, kvp.Value);
                
                
                Console.WriteLine(kvp.Key);
                if (y == 0)
                {
                    x *= 2;
                    y = x;
                    Console.WriteLine();
                }
                y -= 1;
                
            }
        }
        static void BuildMaxHeap(KeyValuePair<char, int>[] array)
        {
            // Start at the parent of the last leaf node
            int startIndex = (array.Length - 2) / 2;
            
            for (int i = startIndex; i >= 0; i--)
            {
                HeapifyDown(array, i);
            }
        }

        static void HeapifyDown(KeyValuePair<char, int>[] array, int index)
        {
            int leftChildIndex = (2 * index) + 1;
            int rightChildIndex = (2 * index) + 2;
            int largestIndex = index;

            // Compare the parent node to its left child
            if (leftChildIndex < array.Length && array[leftChildIndex].Value > array[largestIndex].Value)
            {
                largestIndex = leftChildIndex;
            }

            // Compare the parent node to its right child
            if (rightChildIndex < array.Length && array[rightChildIndex].Value > array[largestIndex].Value)
            {
                largestIndex = rightChildIndex;
            }

            // If the parent node is not the largest, swap it with the largest child
            if (largestIndex != index)
            {
                Swap(array, index, largestIndex);
                HeapifyDown(array, largestIndex);
            }
        }

        static void Swap(KeyValuePair<char, int>[] array, int i, int j)
        {
            (array[i], array[j]) = (array[j], array[i]);
        }
    }
}