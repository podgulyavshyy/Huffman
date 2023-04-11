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

            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                using (BinaryWriter bw = new BinaryWriter(File.Open(outFile, FileMode.Create)))
                {
                    while ((line = sr.ReadLine()) != null)
                    {
                        string currLine = "";
                        foreach (char c in line)
                        {
                            currLine += dictionary[c];
                        }

                        // Convert the Huffman code string to a list of bools
                        List<bool> bitList = new List<bool>();
                        foreach (char bitChar in currLine)
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
                    if (splittedPair.Length != 2)
                    {
                        continue;
                    }
                    decodeDictionary[splittedPair[1]] = char.Parse(splittedPair[0]);
                }
            }

            using (BinaryReader br = new BinaryReader(File.Open(outFile, FileMode.Open)))
            {
                StringBuilder sb = new StringBuilder();
                string code = "";
                while (br.BaseStream.Position < br.BaseStream.Length)
                {
                    int bitsToRead = br.ReadByte();
                    // Check if the end of the stream has been reached
                    if (br.BaseStream.Position >= br.BaseStream.Length)
                    {
                        break;
                    }
                    byte currentByte = br.ReadByte();
                    for (int i = 0; i < bitsToRead; i++)
                    {
                        code += ((currentByte & (1 << (7 - i))) != 0) ? "1" : "0";
                        if (decodeDictionary.ContainsKey(code))
                        {
                            sb.Append(decodeDictionary[code]);
                            code = "";
                        }
                    }
                }

                File.WriteAllText(outDecodeFile, sb.ToString());
            }
        }
    }
}
