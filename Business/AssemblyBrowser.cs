using System.Reflection;

namespace Browser
{
    public static class AssemblyBrowser
    {
        public static Node Browse(string path)
            {
                  var assembly = Assembly.LoadFrom(path);
                  var assemblyInfo = new AssemblyInfo(assembly.GetName().Name);

                  foreach (var type in assembly.GetTypes())
                  {
                        var namespaceName = type.Namespace ?? "No namespace";
                        assemblyInfo.AddType(namespaceName, type.Name);

                        foreach (var f in
                              type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                        {
                              assemblyInfo.AddItemToLastAddedType(
                                    namespaceName,
                                    new Node(f.Name + " : " + f.FieldType),
                                    ItemType.Field);
                        }

                        foreach (var p in
                        type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static))
                        {
                              assemblyInfo.AddItemToLastAddedType(
                                          namespaceName,
                                          new Node(p.Name + " : " + p.PropertyType),
                                          ItemType.Property);
                        }


                        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                        foreach (var method in methods)
                        {
                              var methodNode = new Node(method.Name);
                              var parameters = method.GetParameters();
                              foreach (var param in parameters)
                              {
                                    methodNode.AddNode(new Node(param.Name + " : " + param.ParameterType));
                              }
                              assemblyInfo.AddItemToLastAddedType(namespaceName, methodNode, ItemType.Method);
                        }

                        var ctors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
                        foreach (var ctor in ctors)
                        {
                              var ctorNode = new Node(ctor.Name);
                              var parameters = ctor.GetParameters();
                              foreach (var param in parameters)
                              {
                                    ctorNode.AddNode(new Node(param.Name + " : " + param.ParameterType));
                              }
                              assemblyInfo.AddItemToLastAddedType(namespaceName, ctorNode, ItemType.Ctor);
                        }
                  }

                  return assemblyInfo.ToNodes();
            }
    }
}