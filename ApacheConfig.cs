using System;
using System.IO;
using System.Text.RegularExpressions;


namespace ApacheConfig_Sharp
{
    public class ApacheConfigParser 
    {
        private static String commentRegex = "#.*";
        private static String directiveRegex = "([^\\s]+)\\s*(.+)";
        private static String sectionOpenRegex = "<([^/\\s>]+)\\s*([^>]+)?>";
        private static String sectionCloseRegex = "</([^\\s>]+)\\s*>";

        private static Regex commentMatcher = new Regex(commentRegex);
        private static Regex directiveMatcher = new Regex(directiveRegex);
        private static Regex sectionOpenMatcher = new Regex(sectionOpenRegex);
        private static Regex sectionCloseMatcher = new Regex(sectionCloseRegex);

        public ApacheConfigParser() 
        { }

        public ConfigNode parse(StreamReader inputStream)
        {
            if (inputStream == null) 
            {
                throw new NullReferenceException("inputStream: null");
            }

            String line;
            ConfigNode currentNode = ConfigNode.createRootNode();

            while ((line = inputStream.ReadLine()) != null) 
            {
                if (commentMatcher.Match(line).Success) 
                {
                    continue;
                } 
                else if (sectionOpenMatcher.Match(line).Success) 
                {
                    Match m = sectionOpenMatcher.Match(line);
                    String name = m.Groups[1].Value;
                    String content = m.Groups[2].Value;
                    ConfigNode sectionNode = ConfigNode.createChildNode(name, content, currentNode);

                    currentNode = sectionNode;
                } 
                else if (sectionCloseMatcher.Match(line).Success) 
                {
                    currentNode = currentNode.getParent();
                } 
                else if (directiveMatcher.Match(line).Success) 
                {
                    Match m = directiveMatcher.Match(line);
                    String name = m.Groups[1].Value;
                    String content = m.Groups[2].Value;
                    ConfigNode.createChildNode(name, content, currentNode);
                }
                // TODO: Should an exception be thrown for unknown lines? Probably not?
            }
            inputStream.Close();  

            return currentNode;
        }
    }
}