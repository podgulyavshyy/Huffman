namespace Huffman;

public class Node
{
    public char Character { get; set; }
    public int Frequency { get; set; }
    public Node Left { get; set; }
    public Node Right { get; set; }

    public Node(char character, int frequency)
    {
        Character = character;
        Frequency = frequency;
    }
}