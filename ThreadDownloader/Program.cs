using System;
using CommandLine;

namespace ThreadDownloader
{
    class Program
    {
        static void Main(string[] args)
        {
            var result = Parser.Default
                .ParseArguments<ArgumentParser.Options>(args)
                .WithParsed(options => {
                    var urlString = options.UrlString;
                    var outputPath = options.OutputPath;

                    // Downloader downloader = new Downloader(urlString, outputPath);
                });

            
            // HtmlParser.Parser.Run();
            HtmlParser.FileParser fileParser = new HtmlParser.FileParser("4_27_2021_6_23_36_PM.htm");
            Console.WriteLine(fileParser.Run());
        }
    }
}
