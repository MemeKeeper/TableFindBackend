using System;
using System.IO;
using System.Windows.Forms;

namespace TableFindBackend.Logging
{
    public class TextFileWriter
    {
        private String textFile;
        public TextFileWriter()
        {
            textFile = @"Logs\Log_" + System.DateTime.Now.Date.ToString("dd-MM-yyyy") + ".txt";

            bool flag = false;
            int number = -1;

            while (flag == false)
            {
                if (File.Exists(textFile) == true)
                {
                    number++;
                    textFile = @"Logs\Log_" + System.DateTime.Now.Date.ToString("dd-MM-yyyy") + "_" + number + ".txt";
                }
                else
                {
                    flag = true;
                }
            }
            if (File.Exists("Logs") != true)
                Directory.CreateDirectory("Logs");

            StreamWriter writer = new StreamWriter(textFile);

            try
            {
                writer.WriteLine("Beginning of Log.\t" + System.DateTime.Now.TimeOfDay.ToString());
                writer.WriteLine("");
                writer.WriteLine("<><><><><><><><><><><><><><><><><><><><><><><><><><><><>");
                writer.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to write to log file: " + e.Message.ToString());
            }
        }

        public void WriteLineToFile(String text, bool timeOfDay)
        {

            bool open = IsFileLocked(new FileInfo(textFile));
            if (open == false)
            {
                StreamWriter writer = new StreamWriter(textFile, true);
                try
                {
                    if (timeOfDay == true)
                        writer.WriteLine(text + "\t" + System.DateTime.Now.TimeOfDay.ToString());
                    else
                        writer.WriteLine(text);
                    writer.Close();
                }
                catch (Exception e)
                {
                    MessageBox.Show("Failed to write to log file: " + e.Message.ToString());
                }
            }
        }
        protected virtual bool IsFileLocked(FileInfo file)
        {
            try
            {
                using (FileStream stream = file.Open(FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            }

            //file is not locked
            return false;
        }

        public void FormShutDown()
        {
            StreamWriter writer = new StreamWriter(textFile, true);

            try
            {
                writer.WriteLine("<><><><><><><><><><><><><><><><><><><><><><><><><><><><>");
                writer.WriteLine("");
                writer.WriteLine("End of Log.\t" + System.DateTime.Now.TimeOfDay.ToString());
                writer.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Failed to write to log file: " + e.Message.ToString());
            }
        }
    }
}
