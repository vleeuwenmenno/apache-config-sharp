using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ApacheConfig_Sharp
{
    public class ConfigNode 
    {
        private String name;
        private String content;
        private List<ConfigNode> children = new List<ConfigNode>();

        private ConfigNode parent;

        private ConfigNode(String name, String content, ConfigNode parent) 
        {
            this.name = name;
            this.content = content;
            this.parent = parent;
        }

        public static ConfigNode createRootNode() 
        {
            return new ConfigNode(null, null, null);
        }

        public static ConfigNode createChildNode(String name, String content, ConfigNode parent) 
        {
            if (name == null)
                throw new NullReferenceException("name: null");

            if (content == null)
                throw new NullReferenceException("content: null");

            if (parent == null)
                throw new NullReferenceException("parent: null");

            ConfigNode child = new ConfigNode(name, content, parent);
            parent.addChild(child);

            return child;
        }

        /**
        * 
        * @return the configuration name; null if this is a root node
        */
        public String getName() {
            return name;
        }

        /**
        * 
        * @return the configuration content; null if this is a root node
        */
        public String getContent() {
            return content;
        }

        /**
        * 
        * @return The nodes parent; null if this is a root node
        */
        public ConfigNode getParent() {
            return parent;
        }

        /**
        * 
        * @return a list of child configuration nodes
        */
        public List<ConfigNode> getChildren() {
            return children;
        }

        /**
        * 
        * @return true if this is a root node; false otherwise
        */
        public bool isRootNode() {
            return parent == null;
        }

        public override String ToString() {
            return "ConfigNode {name=" + name + ", content=" + content + ", childNodeCount=" + children.Count + "}";
        }

        private void addChild(ConfigNode child) {
            children.Add(child);
        }
    }
}