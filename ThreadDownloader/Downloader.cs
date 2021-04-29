using System.Net;

namespace ThreadDownloader
{
    public class Downloader
    {
        readonly string urlString, outputPath;

        public Downloader(string urlString, string outputPath)
        {
            System.Console.WriteLine("Downlaoding Thread...");
            System.Console.WriteLine(System.String.Format("Date: {0}", System.DateTime.Now.ToShortDateString()));

            this.urlString = urlString; // Initilize webpage url
            this.outputPath = outputPath; // Where to save the webpage
            Run();
        }

        public void Run()
        {
            // Actual Downloading Goes on here.
            System.Console.WriteLine(GetDateTimeFileName());
            WebClient client = new WebClient();
            string htmlstring = client.DownloadString(this.urlString);
            System.Console.WriteLine(htmlstring);

            // Saving file with webpage string
            System.IO.File.WriteAllText(
                System.String.Format("{0}{1}", outputPath, this.GetDateTimeFileName()),
                htmlstring
            );
        }

        public string GetDateTimeFileName()
        {
            // Generating File Name, Simple date & time joined together using '_'
            return System.DateTime.Now
                .ToString()
                .Replace(" ", "_")
                .Replace("/", "_")
                .Replace(":", "_") + ".htm"; // FIXME: File extension is static. Change according to file MIME Type
        }

        public bool DirectoryExists(string directoryPath)
        {
            return false;
        }
    }
}