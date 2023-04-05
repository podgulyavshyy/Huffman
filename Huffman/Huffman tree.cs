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

            // Create a list of nodes to store the characters and their counts
            List<Node> nodes = new List<Node>();

            // Read the text file
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    // Loop through each character in the line
                    foreach (char c in line)
                    {
                        Node node = nodes.Find(n => n.Character == c);
                        if (node != null)
                        {
                            node.Frequency++;
                        }
                        else
                        {
                            nodes.Add(new Node(c, 1));
                        }
                    }
                }
            }

            // Build the Huffman tree
            Node root = BuildHuffmanTree(nodes);

            // Print out the character frequencies
            PrintCharacterFrequencies(root);
            
            //Print Coding Table
            Encoding huffmanEncoding = new Encoding(root);
            huffmanEncoding.DisplaySymbolCodeTable();

        }

        static Node BuildHuffmanTree(List<Node> nodes)
        {
            // Build a binary heap to store the nodes based on their frequency
            BinaryHeap<Node> heap = new BinaryHeap<Node>(nodes.Count, new NodeComparer());

            foreach (Node node in nodes)
            {
                heap.Insert(node);
            }

            // Combine the nodes with the lowest frequency until there is only one node left (the root of the tree)
            while (heap.Count > 1)
            {
                Node left = heap.ExtractMin();
                Node right = heap.ExtractMin();
                Node parent = new Node('\0', left.Frequency + right.Frequency);
                parent.Left = left;
                parent.Right = right;
                heap.Insert(parent);
            }

            // The last node in the heap is the root of the Huffman tree
            return heap.ExtractMin();
        }

        static void PrintCharacterFrequencies(Node root)
        {
            // Traverse the tree in depth-first order and print out the character frequencies
            Stack<Node> stack = new Stack<Node>();
            stack.Push(root);
            while (stack.Count > 0)
            {
                Node node = stack.Pop();
                if (node.Character != '\0')
                {
                    Console.WriteLine("Character '{0}' appears {1} times in the text file.", node.Character,
                        node.Frequency);
                }

                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }

                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }
            }
        }

        public class NodeComparer : IComparer<Node>
        {
            public int Compare(Node x, Node y)
            {
                return x.Frequency.CompareTo(y.Frequency);
            }
        }
    }

    
}

