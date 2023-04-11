
namespace Huffman
{
    public class Encoding
    {
        // Dictionary (coded)
        private Dictionary<char, string> symbolCodeTable = new Dictionary<char, string>();

        // Constructor
        public Encoding(Node root)
        {
            Visit(root, "");
        }
        
        private void Visit(Node node, string code)
        {
            if (node != null)
            {
                // Check for last node
                if (node.Left == null && node.Right == null)
                {
                    symbolCodeTable[node.Character] = code;
                }
                
                Visit(node.Left, code + "0");
                
                Visit(node.Right, code + "1");
            }
        }
    
        // Print coded table
        public void DisplaySymbolCodeTable()
        {
            foreach (KeyValuePair<char, string> kvp in symbolCodeTable)
            {
                Console.WriteLine("Symbol '{0}' has code '{1}'", kvp.Key, kvp.Value);
            }
        }
        
        public string GetSymbolCodeTable()
        {
            string table = "";
            foreach (KeyValuePair<char, string> kvp in symbolCodeTable)
            {
                table += kvp.Key;
                table += "=";
                table += kvp.Value;
                table += Environment.NewLine;
            }

            return table;
        }


        public Dictionary<char, string> GetDic()
        {
            return this.symbolCodeTable;
        }
    }
}