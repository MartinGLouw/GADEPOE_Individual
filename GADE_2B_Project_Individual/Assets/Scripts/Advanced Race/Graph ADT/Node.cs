using System.Collections.Generic;

public class Node<T>
{
    public T Value { get; set; }
    public List<Node<T>> Neighbours { get; set; }

    public Node(T value)
    {
        Value = value;
        Neighbours = new List<Node<T>>();
    }
}