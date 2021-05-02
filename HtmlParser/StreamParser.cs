using System;
using System.Diagnostics;
using System.IO;

namespace HtmlParser 
{
    public class StreamParser : Parser
    {
        protected string HtmlString;
        public StreamParser(string htmlString)
            : base()
        {
            this.PerlStartInfo.ArgumentList.Add("--io");
            this.HtmlString = htmlString;
        }

        public override string Run()
        {
            PerlStartInfo.UseShellExecute = false;
            PerlStartInfo.RedirectStandardError = true;
            PerlStartInfo.RedirectStandardInput = true;
            PerlStartInfo.RedirectStandardOutput = true;

            using (Process process = Process.Start(this.PerlStartInfo))
            {
                StreamWriter inputStream = process.StandardInput;
                StreamReader outputStream = process.StandardOutput;
                StreamReader errorStream = process.StandardError;
                
                // Sending html through perl (child) input stream
                inputStream.WriteLine(this.HtmlString);
                var output = outputStream.ReadToEnd();
                var error = errorStream.ReadToEnd();

                if (!error.Length.Equals(0))
                    throw new ParserException(
                        String.Format("Error from process: {0}", error));
                
                return output;
            }

            throw new ParserException("Parser process not running.");
        }
    }
}