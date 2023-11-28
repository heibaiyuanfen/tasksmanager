using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1.Method
{
    public class showicon
    {
        // SHGetFileInfo函数的标志
        private const uint SHGFI_ICON = 0x100;
        private const uint SHGFI_SMALLICON = 0x1;
        // 使用SHGetFileInfo函数获取进程的图标
        [DllImport("shell32.dll")]
        private static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);

        // SHFILEINFO 结构定义
        [StructLayout(LayoutKind.Sequential)]
        private struct SHFILEINFO
        {
            public IntPtr hIcon;
            public IntPtr iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        }

        // 获取进程的图标的方法
         public Icon GetProcessIcon(Process process)
        { 
            SHFILEINFO shfi = new SHFILEINFO();
            uint flags = SHGFI_ICON | SHGFI_SMALLICON;

            // 获取进程的可执行文件路径
            string processFilePath = process.MainModule.FileName;

            // 获取进程的图标
            IntPtr result = SHGetFileInfo(processFilePath, 0, ref shfi, (uint)Marshal.SizeOf(shfi), flags);

            if (result != IntPtr.Zero)
            {
                // 将图标从句柄转换为Icon对象
                return Icon.FromHandle(shfi.hIcon);
            }
            else
            {
                return null;
            }
        }
    }
}
