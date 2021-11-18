using System.Collections.Generic;
using System.Linq;

namespace Browser
{
    public class AssemblyInfo
    {
        private string assemblyName;
        public readonly Dictionary<string, Node> Namespaces = new();

        public AssemblyInfo(string name)
        {
            assemblyName = name;
        }

        public void AddType(string namespaceName, string typeName)
        {
            if (!Namespaces.ContainsKey(namespaceName))
            {
                Namespaces.Add(namespaceName, new Node(namespaceName));
            }

            var type = new Node(typeName);
            type.AddNode(new Node("Fields"));
            type.AddNode(new Node("Properties"));
            type.AddNode(new Node("Methods"));
            type.AddNode(new Node("Constructors"));
            Namespaces[namespaceName].AddNode(type);
        }

        public void AddItemToLastAddedType(string namespaceName, Node itemToAdd, ItemType nodeType)
        {
            Namespaces[namespaceName].Nodes.Last().Nodes[(int)nodeType].AddNode(itemToAdd);
        }

        public Node ToNodes()
        {
            var result = new Node(assemblyName) {Nodes = Namespaces.Values.ToList()};
            return result;
        }
    }
}