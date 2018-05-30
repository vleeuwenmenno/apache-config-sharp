using System;
using ApacheConfig_Sharp;
using System.IO;
using System.Collections.Generic;

namespace ApacheConfig_SharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || args.Length > 1)
            {
                if (File.Exists("/etc/apache2/apache2.conf"))
                {
                    args = new string[] { "/etc/apache2/apache2.conf" };
                }
                else
                {
                    Console.WriteLine("Invalid parameters given");
                    return;
                }
            }

            StreamReader inputStream = new StreamReader(args[0]);

            ApacheConfigParser parser = new ApacheConfigParser();
            ConfigNode config = parser.parse(inputStream);

            foreach (ConfigNode child in config.getChildren()) 
            {
                List<ConfigNode> children = child.getChildren();
                
                if (child.getName() == "VirtualHost")
                {
                    Console.WriteLine(child.getName() + " { ");
                    Console.WriteLine("    Document root: " + children.Find(x => x.getName() == "DocumentRoot"));
                    Console.WriteLine("    Server name: " + children.Find(x => x.getName() == "ServerName"));

                    foreach (ConfigNode node in children.FindAll(x => x.getName() == "Directory"))
                    {
                        Console.WriteLine("    Directory {");
                        Console.WriteLine("        Path: " + node.getContent());
                        Console.WriteLine("        Rules: {");

                        foreach (ConfigNode childNode in node.getChildren())
                        {
                            Console.WriteLine("            " + childNode.getName());
                        }

                        Console.WriteLine("        }");
                        Console.WriteLine("    }");
                    }

                    Console.WriteLine("}\n");
                }
            }
        }
    }
}
