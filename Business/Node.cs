using System;
using System.Collections.Generic;

namespace Browser
{
    public class Node
    {
        public string Text { get; }

        public List<Node> Nodes { get; set; }

        public Node()
        {
            Nodes = new List<Node>();
        }

        public Node(string text)
        {
            Nodes = new List<Node>();
            Text = text;
        }

        public void AddNode(Node node)
        {
            Nodes.Add(node);
        }
    }
}