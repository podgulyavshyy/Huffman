namespace Huffman;

public class EncodeDecode
{
    private string outFile = "output.txt";
    private string outDecodeFile = "outputDecode.txt";
    private Dictionary<string, char> decode = new Dictionary<string, char>();
    public void Encode(Dictionary<char, string> dictionary, string table, string filePath)
    {
        using (StreamReader sr = new StreamReader(filePath))
        {
            string line;
            File.AppendAllText(outFile, table + Environment.NewLine);
            while ((line = sr.ReadLine()) != null)
            {
                string currLine = "";
                // Loop through each character in the line
                foreach (char c in line)
                {
                    decode[dictionary[c]] = c;
                    currLine += dictionary[c];
                }
                File.AppendAllText(outFile, currLine + Environment.NewLine);
            }
        }
    }

    public void Decode(Dictionary<char, string> dictionary)
    {
        using (StreamReader sr = new StreamReader(outFile))
        {
            string line;
            string code = "";
            while ((line = sr.ReadLine()) != null)
            {
                string currLine = "";
                
                // Loop through each character in the line
                foreach (char c in line)
                {
                    code += c;
                    if (decode.ContainsKey(code))
                    {
                        currLine += decode[code];
                        code = "";
                    }
                    // currLine += decode[c];
                }
                File.AppendAllText(outDecodeFile, currLine + Environment.NewLine);
            }
        }
    }
    public void DecodeVerTwo()
    {
        using (StreamReader sr = new StreamReader(outFile))
        {
            string codes = sr.ReadLine();
            string[] codesArr = codes.Split('$');
            foreach (var pair in codesArr)
            {
                string[] splittedPair = pair.Split('=');
                if (splittedPair[0] == ";")
                {
                    continue;
                }
                decode[splittedPair[1]] = char.Parse(splittedPair[0]);
            }
            string line;
            string code = "";
            while ((line = sr.ReadLine()) != null)
            {
                string currLine = "";
                
                // Loop through each character in the line
                foreach (char c in line)
                {
                    code += c;
                    if (decode.ContainsKey(code))
                    {
                        currLine += decode[code];
                        code = "";
                    }
                    // currLine += decode[c];
                }
                File.AppendAllText(outDecodeFile, currLine + Environment.NewLine);
            }
        }
    }
}