using System;
using System.IO;
using System.Threading;
using Serilog;

namespace volumeMounting
{
    class Program
    {
        static void Main(string[] args)
        {
            var log = new LoggerConfiguration().WriteTo.File("logs/serilog.log").CreateLogger();
            log.Warning("THis is a Warning!");
            log.Information("THis information");


            FileStream ostrm;
            StreamWriter writer;
            TextWriter oldOut = Console.Out;
            try
            {
                ostrm = new FileStream ("./logs/test.log", FileMode.OpenOrCreate, FileAccess.Write);
                writer = new StreamWriter (ostrm);
            }
            catch (Exception e)
            {
                Console.WriteLine ("Cannot open file");
                Console.WriteLine (e.Message);
                return;
            }

            DateTime now = DateTime.Now;
            string today = now.ToString("dd/MM/yyyy");
            int iteration = 0;

            Console.SetOut (writer);
            Console.WriteLine("Hello World!");
            Console.WriteLine($"Today is {today}");
            Console.WriteLine("Starting Loop");

            while (iteration < 10) 
            {
                string time = DateTime.Now.ToString("h:mm:ss tt");
                Console.WriteLine($"T: {time}");
                Thread.Sleep(1000);
                iteration++;
            }

            Console.SetOut (oldOut);
            writer.Close();
            ostrm.Close();
            Console.WriteLine ("END");
        }
    }
}
