using System;
using System.Diagnostics;

namespace HtmlParser
{
    public class Parser
    {
        public Parser()
        {
            
        }

        public static void Run()
        {
            ProcessStartInfo perlStartInfo = new ProcessStartInfo(@"/usr/bin/perl");
            perlStartInfo.ArgumentList.Add("perl_script/html_parser.pl");
            perlStartInfo.UseShellExecute = false;
            perlStartInfo.RedirectStandardError = true;
            perlStartInfo.RedirectStandardOutput = true;

            using (Process htmlParserProcess = new Process())
            {
                htmlParserProcess.StartInfo = perlStartInfo;
                bool processStarted = htmlParserProcess.Start();

                var output = htmlParserProcess.StandardOutput.ReadToEnd();
                var error = htmlParserProcess.StandardError.ReadToEnd();
                Console.WriteLine(output);
            }
        }
    }
}
