﻿using System;
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

                    Downloader downloader = new Downloader(urlString, outputPath);
                    HtmlParser.StreamParser streamParser = 
                        new HtmlParser.StreamParser(downloader.GetThread());
                    var imageLinks = streamParser.Run().Split("\n");

                    foreach(string link in imageLinks)
                    {
                        Console.WriteLine(link); // Outputting links
                    }
                });
        }
    }
}
