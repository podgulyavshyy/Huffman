using System.IO;
using System.Collections.Generic;
using System.Text;

namespace Huffman
{
    public class EncodeDecode
    {
        private string outFile = "output.bin";
        private string outDecodeFile = "outputDecode.txt";
        private string codeTableFile = "codeTable.txt";
        private string outTableFile = "outputTable.txt";

        public void Encode(Dictionary<char, string> dictionary, string table, string filePath)
        {
            // Save the code table in a separate .txt file
            File.WriteAllText(outTableFile, table);

            string inputText = File.ReadAllText(filePath);

            using (BinaryWriter bw = new BinaryWriter(File.Open(outFile, FileMode.Create)))
            {
                string encodedText = "";
                foreach (char c in inputText)
                {
                    encodedText += dictionary[c];
                }
                

                // Convert the Huffman code string to a list of bools
                List<bool> bitList = new List<bool>();
                foreach (char bitChar in encodedText)
                {
                    bitList.Add(bitChar == '1');
                }

                // Write the number of bits to the binary file
                bw.Write((byte)bitList.Count);

                // Pack the bools into a byte and write it to the binary file
                byte currentByte = 0;
                int bitPosition = 0;

                foreach (bool bit in bitList)
                {
                    currentByte |= (byte)((bit ? 1 : 0) << (7 - bitPosition));
                    bitPosition++;

                    if (bitPosition == 8)
                    {
                        bw.Write(currentByte);
                        currentByte = 0;
                        bitPosition = 0;
                    }
                }

                // If there are any remaining bits, write them to the binary file
                if (bitPosition > 0)
                {
                    bw.Write(currentByte);
                }
            }
        }


        
        public void DecodeVerTwo()
        {
            Dictionary<string, char> decodeDictionary = new Dictionary<string, char>();

            using (StreamReader sr = new StreamReader(outTableFile))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] splittedPair = line.Split('=');

                    if (line.StartsWith("="))
                    {
                        string huffmanCode = line.Substring(1);
                        decodeDictionary[huffmanCode] = '\n';
                    }
                    else if (splittedPair.Length != 2)
                    {
                        continue;
                    }
                    else
                    {
                        decodeDictionary[splittedPair[1]] = char.Parse(splittedPair[0]);
                    }
                }
            }

            byte[] encodedBytes = File.ReadAllBytes(outFile);
            StringBuilder sb = new StringBuilder();
            string code = "";
            int bytePosition = 0;
            int bitPosition = 0;
            while (bytePosition < encodedBytes.Length)
            {
                int bitsToRead = encodedBytes[bytePosition];
                bytePosition++;

                while (bitPosition < bitsToRead )
                {
                    if (bytePosition == encodedBytes.Length)
                    {
                        break;
                    }
                    code += ((encodedBytes[bytePosition] & (1 << (7 - bitPosition))) != 0) ? "1" : "0";
                    bitPosition++;

                    if (decodeDictionary.ContainsKey(code))
                    {
                        char decodedChar = decodeDictionary[code];
                        sb.Append(decodedChar);
                        code = "";
                    }

                    if (bitPosition == 8)
                    {
                        bitPosition = 0;
                        bytePosition++;
                    }
                }
            }
            File.WriteAllText(outDecodeFile, sb.ToString());
        }


    }
}
