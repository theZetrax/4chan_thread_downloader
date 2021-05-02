using System;

namespace HtmlParser
{
    public class FileParser : Parser
    {
        public FileParser(string filePath)
            : base()
        {
            this.PerlStartInfo.ArgumentList.Add("--file");
            this.PerlStartInfo.ArgumentList.Add(filePath);
        }
    }
}