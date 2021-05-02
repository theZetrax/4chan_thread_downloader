using System;
using System.Diagnostics;

namespace HtmlParser
{
    public class Parser
    {
        protected ProcessStartInfo PerlStartInfo;

        /// <summary>
        /// Wrapper class for perl html parser.
        /// </summary>
        /// <param name="perlInterpreterPath">Path of the perl interpreter.</param>
        /// <param name="perlParserPath">Path of the html parser script.</param>
        public Parser(
            string perlInterpreterPath = "/usr/bin/perl",
            string perlParserPath = "perl_script/html_parser.pl")
        {
            this.PerlStartInfo = new ProcessStartInfo(perlInterpreterPath);
            this.PerlStartInfo.ArgumentList.Add(perlParserPath);
        }

        /// <summary>
        /// Adds argumnets to be passed to the perl parser script. 
        /// </summary>
        /// <param name="argumentName">Arguemnt name, i.e. <c>--io</c>.</param>
        /// <param name="argumentValue">Argument value for the argument name, leave empty if none</param>
        public void AddArgument(string argumentName, string argumentValue = "")
        {
            this.PerlStartInfo.ArgumentList.Add(argumentName);
            if (!argumentName.Length.Equals(0))
                this.PerlStartInfo.ArgumentList.Add(argumentValue);
        }

        /// <summary>
        /// Runs perl parser process.
        /// </summary>
        public virtual string Run()
        {
            this.PerlStartInfo.ArgumentList.Add("perl_script/html_parser.pl");
            this.PerlStartInfo.UseShellExecute = false;
            this.PerlStartInfo.RedirectStandardError = true;
            this.PerlStartInfo.RedirectStandardOutput = true;

            using (Process htmlParserProcess = Process.Start(this.PerlStartInfo))
            {
                // Read output and error stream
                var output = htmlParserProcess.StandardOutput.ReadToEnd();
                var error = htmlParserProcess.StandardError.ReadToEnd();

                // If error exists
                if (!error.Length.Equals(0))
                    throw new ParserException(
                        String.Format("Error from process: {0}", error));

                return output;
            }

            throw new ParserException("Parser process not running.");
        }
    }
}
