using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventTest2
{
    class Program
    {
        static void Logger(string info)
        {
            Console.WriteLine(info);
        }//end of Logger

        static void Main(string[] args)
        {
            BoilerInfoLogger filelog = new BoilerInfoLogger("D:/Test/boiler.txt");//创建文本
            DelegateBoilerEvent boilerEvent = new DelegateBoilerEvent();
            boilerEvent.BoilerEventLog += new
            DelegateBoilerEvent.BoilerEventHanler(Logger);
            boilerEvent.BoilerEventLog += new
            DelegateBoilerEvent.BoilerEventHanler(filelog.Logger);
            boilerEvent.LogProcess();
            Console.ReadLine();
            filelog.Close();
        }
    }

    class Boiler
    {
        private int value;
        private int pressuer;
        public Boiler(int t, int p)
        {
            this.value = t;
            this.pressuer = p;
        }
        public int getValue()
        {
            return value;
        }
        public int getPressuer()
        {
            return pressuer;
        }
    }
    /// <summary>
    /// 定义委托事件
    /// </summary>
    class DelegateBoilerEvent
    {

        public delegate void BoilerEventHanler(string status);
        //基于委托定义Event
        public event BoilerEventHanler BoilerEventLog;

        public void LogProcess()
        {
            string remarks = "O .K";
            Boiler b = new Boiler(160, 12);
            int t = b.getValue();
            int p = b.getPressuer();
            if (t > 150 || t < 80 || p < 12 || p > 15)
            {
                remarks = "Need Maintenans";
            }
            OnBoilerEventLog("Logging Info:\n");

        }

        protected void OnBoilerEventLog(string v)
        {

            if (BoilerEventLog != null)
            {
                BoilerEventLog(v);
            }
        }
    }

    class BoilerInfoLogger
    {
        FileStream fs;
        StreamWriter sw;
        public BoilerInfoLogger(string fileName)
        {
            fs = new FileStream(fileName, FileMode.Append, FileAccess.Write);
            sw = new StreamWriter(fs);
        }

        public void Logger(string info)
        {
            sw.WriteLine(info);
        }
        public void Close()
        {
            sw.Close();
            fs.Close();

        }

    }

}
