using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;


namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private string GetTaskNumberFromProcessName(string processName)
        {
            // 这是一个示例方法，你可以根据实际情况提取任务号
            // 这里我们简单地返回进程名称作为任务号
            return processName;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {



            // 清空ListBox以便显示新的任务号和进程号
            taskListBox.Items.Clear();

            // 获取所有正在运行的进程
            var processes = Process.GetProcesses();

            // 遍历进程并提取任务号和进程号
            foreach (var process in processes)
            {
                try
                {
                    string processName = process.ProcessName;
                    int processId = process.Id;

                    string taskNumber = GetTaskNumberFromProcessName(processName);

                    if (!string.IsNullOrEmpty(taskNumber))
                    {
                        // 创建一个TaskInfo对象来存储任务号和进程号
                        taskinfo taskInfo = new taskinfo
                        {
                            TaskNumber = taskNumber,
                            ProcessId = processId
                        };

                        taskListBox.Items.Add(taskInfo);
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常，例如无法访问某些进程的权限问题
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}