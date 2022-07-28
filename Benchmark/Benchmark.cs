using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Benchmark
{
    public partial class Benchmark : ServiceBase
    {
        public Benchmark()
        {
            InitializeComponent();
        }

        static async void WriteCharacters(int x)
        {
            // Delete the file if it exists.
            if (File.Exists($@"C:\Users\m.gasaro.ext\Documents\test_bench\MyTest{x}.txt"))
            {
                File.Delete($@"C:\Users\m.gasaro.ext\Documents\test_bench\MyTest{x}.txt");
            }

            //Create the file.
            using (FileStream fs = File.Create($@"C:\Users\m.gasaro.ext\Documents\test_bench\MyTest{x}.txt"))
            {
                AddText(fs, "This is some text");
                AddText(fs, "This is some more text,");
                AddText(fs, "\r\nand this is on a new line");
                AddText(fs, "\r\n\r\nThe following is a subset of characters:\r\n");

                for (int i = 1; i < 120; i++)
                {
                    AddText(fs, Convert.ToChar(i).ToString());
                }
            }

        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }

        private static readonly ILog log = LogManager.GetLogger(typeof(Benchmark));

        public void Start()
        {
            
            for (int i = 0; i < 100; i++)
            {
                WriteCharacters(i);

                log.Debug($@"c:\temp\MyTest{i}.txt");
            }
        }

        protected override void OnStop()
        {
        }
    }
}
