 using System.Collections.Generic;
using UnityEngine;

public class Graph<T>
{
    public List<Node<T>> Nodes { get; set; }

    public Graph()
    {
        Nodes = new List<Node<T>>();
    }

    public Node<T> AddNode(T value)
    {
        Node<T> node = new Node<T>(value);
        Nodes.Add(node);
        return node;
    }

    public void AddEdge(Node<T> from, Node<T> to)
    {
        from.Neighbours.Add(to);
    }

    public Node<T> GetRandomNeighbour(Node<T> node)
    {
        if (node.Neighbours.Count == 0)
        {
            return null;
        }
        int index = Random.Range(0, node.Neighbours.Count);
        return node.Neighbours[index];
        
    }
}