using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhongMachTu.Common.Helpers
{
    public static class Logger
    {
        private static readonly object lockLog = new object();
        public static void LogMsg(string msgInfo,string msgEx)
        {
            lock (lockLog)
            {
                try
                {
                    string currentDate = DateTime.Now.ToString("yyyy-MM-dd");

                    string dailyFolder = Path.Combine(Utils.GetPathLog(), currentDate);

                    if (!Directory.Exists(dailyFolder))
                    {
                        Directory.CreateDirectory(dailyFolder);
                    }
                    string path = Path.Combine(dailyFolder, "log.txt");
                    using (FileStream fStream = new FileStream(path, FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fStream))
                        {
                            sw.WriteLine($"[{DateTime.Now:HH:mm:ss}] {msgInfo+"----"+msgEx}");
                        }
                    }
                }
                catch { }
            }
        }
    }
}
