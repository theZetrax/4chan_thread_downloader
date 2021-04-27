using CommandLine;
using CommandLine.Text;

namespace ArgumentParser
{
    public class Options
    {
        [Option('u', "url", Required = true, HelpText = "URL to 4Chan thread.")]
        public string UrlString { get; set; }

        [Option('o', "output", Default = "./", HelpText = "Download path for 4Chan images.")]
        public string OutputPath { get; set; }

        [Usage(ApplicationAlias = "4ChanThreadDownloader")]
        public static System.Collections.Generic.IEnumerable<Example> Examples
        {
            get
            {
                return new System.Collections.Generic.List<Example>()
                {
                    new Example("Provide 4Chan thread url.", new Options { UrlString = "https://boards.4channel.org/his/thread/10975249" })
                };
            }
        }
    }
}