using Microsoft.Extensions.PlatformAbstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
namespace KindCMS.Utility
{
    public class MachineUtility
    {
        public static string DWhandle(long Bytes)
        {
            double Usage = Bytes;
            return DWhandle(Usage);
        }
        public static string DWhandle(double Bytes)
        {
            double Usage = Bytes;
            string Dw = "B";
            if (Usage > 1024 * 3)
            {
                Dw = "KB";
                Usage /= 1024;
            }
            if (Usage > 1024 * 3)
            {
                Dw = "MB";
                Usage /= 1024;
            }
            if (Usage > 1024 * 3)
            {
                Dw = "GB";
                Usage /= 1024;
            }
            if (Usage > 1024 * 3)
            {
                Dw = "TB";
                Usage /= 1024;
            }
            if (Usage > 1024 * 3)
            {
                Dw = "PB";
                Usage /= 1024;
            }
            if (Usage > 1024 * 3)
            {
                Dw = "EB";
                Usage /= 1024;
            }
            Usage = Math.Round(Usage, 2);
            return Usage + Dw;
        }
        /// <summary>
        /// 获取当前进程的内存使用量（带单位）
        /// </summary>
        /// <returns></returns>
        public static string ProcessMemoryUsege()
        {
            long Usage = ProcessMemoryBytes();
           
            return DWhandle(Usage);
        }
        /// <summary>
        /// 获取当前进程所用内存的字节数
        /// </summary>
        /// <returns></returns>
        public static long ProcessMemoryBytes()
        {
            var p = System.Diagnostics.Process.GetCurrentProcess();
            long MemoryBytes = 0;
            MemoryBytes = p.PrivateMemorySize64;
            return MemoryBytes;
        }
        /// <summary>
        /// 获取当前堆上数据所有内存字节数
        /// </summary>
        /// <returns></returns>
        public static long StackMemoryBytes()
        {
            long MemoryBytes = GC.GetTotalMemory(false);
            return MemoryBytes;
        }
        /// <summary>
        /// 获取当前堆上数据所有内存使用量（带单位）
        /// </summary>
        /// <returns></returns>
        public static string StackMemoryUsage()
        {
            long Usage = StackMemoryBytes();
            return DWhandle(Usage);
        }


        public static string CPUTime()
        {
            //System.Diagnostics.PerformanceCounter
            //System.Diagnostics.Process.get
            return System.Diagnostics.Process.GetCurrentProcess().TotalProcessorTime.TotalSeconds.ToString("f3");
        }

        public static int CPUCount()
        {
            return Environment.ProcessorCount;
        }


        public static IRuntimeEnvironment RuntimeEnv()
        {



            return PlatformServices.Default.Runtime;
        }
    }
}
