using System;
using System.Diagnostics;

namespace HtmlParser
{
    public class Parser
    {
        protected ProcessStartInfo PerlStartInfo;

        public Parser(
            string perlInterpreterPath = "/usr/bin/perl",
            string perlParserPath = "perl_script/html_parser.pl")
        {
            this.PerlStartInfo = new ProcessStartInfo(perlInterpreterPath);
            this.PerlStartInfo.ArgumentList.Add(perlParserPath);
        }

        public void AddArgument(string argumentName, string argumentValue = "")
        {
            this.PerlStartInfo.ArgumentList.Add(argumentName);
            if (!argumentName.Length.Equals(0))
                this.PerlStartInfo.ArgumentList.Add(argumentValue);
        }

        public virtual string Run()
        {
            this.PerlStartInfo.ArgumentList.Add("perl_script/html_parser.pl");
            this.PerlStartInfo.UseShellExecute = false;
            this.PerlStartInfo.RedirectStandardError = true;
            this.PerlStartInfo.RedirectStandardOutput = true;

            using (Process htmlParserProcess = new Process())
            {
                htmlParserProcess.StartInfo = PerlStartInfo;
                bool processStarted = htmlParserProcess.Start();

                var output = htmlParserProcess.StandardOutput.ReadToEnd();
                var error = htmlParserProcess.StandardError.ReadToEnd();

                if (!error.Length.Equals(0))
                    throw new ParserException(
                        String.Format("Error from process: {0}", error));

                return output;
            }

            throw new ParserException("Parser process not running.");
        }
    }
}
