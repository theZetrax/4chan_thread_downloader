namespace ThreadDownloader
{
    public class Downloader
    {
        public Downloader(string urlString, string outputPath)
        {
            System.Console.WriteLine("Downlaoding Thread...");
            System.Console.WriteLine(System.String.Format("Date: {0}", System.DateTime.Now.ToShortDateString()));

            Run();
        }

        public void Run()
        {
            // Actual Downloading Goes on here.
            System.Console.WriteLine(GetDateTimeFileName());
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