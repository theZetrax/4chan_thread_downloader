using System;
using System.Net;
using System.IO;

namespace ThreadDownloader
{
    public class Downloader
    {
        readonly string urlString, outputPath;

        public Downloader(string urlString, string outputPath)
        {
            this.urlString = urlString; // Initilize webpage url
            this.outputPath = outputPath; // Where to save the webpage
        }

        // Downloads The Thread
        public string GetThread()
        {
            string htmlstring; // Html form the downloaded thread.
            using (WebClient client = new WebClient())
            {
                Console.WriteLine(System.String.Format("Moment: {0}", System.DateTime.Now.ToShortDateString()));
                Console.WriteLine("Downlaoding Thread, please wait...");

                htmlstring = client.DownloadString(this.urlString);
                Console.WriteLine("Thread Downloaded Successfully.");
            }

            return htmlstring;
        }

        public string GetDateTimeName()
        {
            // Generating File Name, Simple date & time joined together using '_'
            return System.DateTime.Now
                .ToString()
                .Replace(" ", "_")
                .Replace("/", "_")
                .Replace(":", "_"); // FIXME: File extension is static. Change according to file MIME Type
        }

        public bool DirectoryExists(string directoryPath)
        {
            return false;
        }

        // Method generates name for download path
        // using date and name
        // i.e. 20_04_2021_<thread_url>
        protected string GenerateThreadDirectoryName()
        {
            /**
             * 1. Generate Date
             * 2. Filter thread url & clean up
             * 3. Append thread url
             * 4. Return generated folder
             */

            // Removing characters that can disturb creating directories.
            string threadUrl = this.urlString;
            threadUrl = threadUrl.Replace(@"https://", "");
            threadUrl = threadUrl.Replace("/", "_");

            return String.Format(
                    "{0}_{1}",
                    (new String(threadUrl.ToLower())),
                    this.GetDateTimeName());
        }

        protected string GetDownloadDirectoryName()
        {
            return Path.Join(
                    Directory.GetCurrentDirectory(),
                    @"downloads",
                    this.GenerateThreadDirectoryName());
        }

        protected bool CreateDownloadDirectory()
        {
            string downloadDirectoryDirectory = this.GetDownloadDirectoryName();

            if (this.PrepareBaseDirectory())
            {
                // Check if directory already exists.
                if(Directory.Exists(downloadDirectoryDirectory))
                    return false; // If exists, don't create

                // If not exist, create directory
                Directory.CreateDirectory(downloadDirectoryDirectory);
                return true;
            }

            return false;
        }

        /**
         * Base directory is the main download directory.
         * It is located in <CWD>/downloads. If the directory is
         * not created, then this method will create it.
         */
        protected bool PrepareBaseDirectory()
        {
            string currentDirectroy = Directory.GetCurrentDirectory();
            string baseDirecroty = @"downloads";

            if (Directory
                    .Exists(Path
                        .Join(currentDirectroy, baseDirecroty)))
                return true;
            try
            {
                // Try creating directory
                Directory.CreateDirectory(Path
                        .Join(currentDirectroy, baseDirecroty)); // Join paths, more platform agnostic ;)
            } catch (Exception exc)
            {
                Console.WriteLine("Error Creating Directory: {0}", exc.ToString());
                return false; // Didn't create directory successfully
            }
            
            // If everything went as expected.
            return true;
        }

        public bool DownloadImages()
        {
            HtmlParser.StreamParser streamParser =
                new HtmlParser.StreamParser(this.GetThread());
            this.CreateDownloadDirectory(); // Creating Download Directory.
            
            Console.WriteLine("Parsing HTML from thread.");
            var links = streamParser.Run().Split("\n");
            Console.WriteLine("Thread parsed successfully.");

            using (var webClient = new WebClient())
            {
                Console.WriteLine("Downloading Thread Images.");
                var downloadDirectory =
                    Path.Join(@"downloads", this.GenerateThreadDirectoryName());
                foreach (string link in links)
                {
                    string fileName = link.Substring(link.LastIndexOf("/") + 1); // Getting file name from link.
                    // Displaying downloads
                    Console.WriteLine(String.Format(
                        "Downloading {0}",
                        link
                    ));

                    var downloadPath = downloadDirectory + "/" + fileName;
                    webClient.DownloadFile(String.Join("", @"https://", link), Path.Join(downloadDirectory, fileName));
                    Console.WriteLine("Done... Saved Image to 'downloads/{0}'", fileName);
                }

                Console.WriteLine("Download Done.");
            }

            return true;
        }
    }
}
