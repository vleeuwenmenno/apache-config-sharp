apache-config-sharp
=============

A port of the originl https://github.com/stackify/apache-config
It is a simple config parser for Apache HTTP Server config files.

Usage
-----
The parser builds a tree of apache configuration.  This example lists all virtualhosts and displays their document root and server name.

```csharp
StreamReader inputStream = new StreamReader(args[0]);

ApacheConfigParser parser = new ApacheConfigParser();
ConfigNode config = parser.parse(inputStream);

foreach (ConfigNode child in config.getChildren()) 
{
    List<ConfigNode> children = child.getChildren();
                
    if (child.getName() == "VirtualHost")
    {
        Console.WriteLine(child.getName());
        Console.WriteLine("    Document root: " + children.Find(x => x.getName() == "DocumentRoot"));
        Console.WriteLine("    Server name: " + children.Find(x => x.getName() == "ServerName"));
    }
}
```

License
=======

    Copyright 2013 Stackify, LLC.

    Licensed under the Apache License, Version 2.0 (the "License");
    you may not use this file except in compliance with the License.
    You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

    Unless required by applicable law or agreed to in writing, software
    distributed under the License is distributed on an "AS IS" BASIS,
    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
    See the License for the specific language governing permissions and
    limitations under the License.
