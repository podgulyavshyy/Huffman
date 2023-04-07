using System.IO;
using System.Collections.Generic;

namespace Huffman
{
    public class EncodeDecode
    {
        private string outFile = "output.bin";
        private string outDecodeFile = "outputDecode.txt";

        public void Encode(Dictionary<char, string> dictionary, string table, string filePath)
        {
            using (StreamReader sr = new StreamReader(filePath))
            {
                string line;
                using (BinaryWriter bw = new BinaryWriter(File.Open(outFile, FileMode.Create)))
                {
                    bw.Write(table);
                    bw.Write('\n');

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

                        // Write the number of bits in the last byte to the binary file
                        bw.Write((byte)bitPosition);

                        bw.Write('\n');
                    }
                }
            }
        }
        public void DecodeVerTwo()
        {
            Dictionary<string, char> decodeDictionary = new Dictionary<string, char>();

            using (BinaryReader br = new BinaryReader(File.Open(outFile, FileMode.Open)))
            {
                string codes = br.ReadString();
                string[] codesArr = codes.Split(';');
                foreach (var pair in codesArr)
                {
                    string[] splittedPair = pair.Split('$');
                    if (splittedPair.Length != 2)
                    {
                        continue;
                    }
                    decodeDictionary[splittedPair[1]] = char.Parse(splittedPair[0]);
                }

                while (br.BaseStream.Position != br.BaseStream.Length - 1)
                {
                    string currLine = "";

                    // Read a byte from the binary file
                    byte currentByte = br.ReadByte();

                    // Read the number of bits in the last byte if this is the last byte
                    int numBitsToProcess = 8;
                    if (br.BaseStream.Position == br.BaseStream.Length - 1)
                    {
                        numBitsToProcess = br.ReadByte();
                    }

                    // Convert the byte to a list of bools
                    List<bool> bitList = new List<bool>();
                    for (int i = 0; i < numBitsToProcess; i++)
                    {
                        bitList.Add((currentByte & (1 << (7 - i))) != 0);
                    }

                    // Construct the Huffman code string from the list of bools
                    string code = "";
                    foreach (bool bit in bitList)
                    {
                        code += bit ? "1" : "0";
                        if (decodeDictionary.ContainsKey(code))
                        {
                            currLine += decodeDictionary[code];
                            code = "";
                        }
                    }

                    File.AppendAllText(outDecodeFile, currLine + Environment.NewLine);
                }
            }
        }

    }
}
