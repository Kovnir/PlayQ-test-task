using System;
using UnityEngine;

public class Container
{
    private class Node
    {
        public Node Next;
        public Node Prev;
        public bool Value;

        public Node(Node prev)
        {
            Value = UnityEngine.Random.value < 0.5f;
            Prev = prev;
        }
    }

    private Node current;
    private readonly int count;
    
    public Container(int count = 0)
    {
        if (count < 1)
        {
            count = (int) (UnityEngine.Random.value * 16) + 1;
        }
        
        Node prev = null;
        for (int i = 0; i < count; i++)
        {
            var currentNode = new Node(prev);
            if (prev != null)
            {
                prev.Next = currentNode;
            }
            if (current == null)
            {
                current = currentNode;
            }
            prev = currentNode;
        }
        prev.Next = current;
        current.Prev = prev;
    }

    public bool Value
    {
        get { return current.Value; }
        set { current.Value = value; }
    }

    public void MoveForward()
    {
        current = current.Next;
    }
    
    public void MoveBackward()
    {
        current = current.Prev;
    }
}