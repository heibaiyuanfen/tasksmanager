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
using System.IO;


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
            // 将TextChanged事件关联到搜索方法
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            // 检查是否选择了一个任务
            if (taskListBox.SelectedItem != null)
            {
                taskinfo selectedTask = (taskinfo)taskListBox.SelectedItem;

                try
                {
                    // 根据任务号结束任务和进程s
                    Process[] processes = Process.GetProcessesByName(selectedTask.TaskNumber);

                    foreach (Process process in processes)
                    {
                        process.Kill(); // 结束进程
                    }

                    // 从列表中移除已结束的任务
                    taskListBox.Items.Remove(selectedTask);
                }
                catch (Exception ex)
                {
                    // 处理异常，例如无法结束任务或进程的权限问题
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = searchTextBox.Text.ToLower();

            // 清空ListBox以便显示新的搜索结果
            taskListBox.Items.Clear();

            // 获取所有正在运行的进程
            var processes = Process.GetProcesses();

            // 遍历进程并提取任务号和进程号，并根据搜索关键字进行过滤
            foreach (var process in processes)
            {
                try
                {
                    string processName = process.ProcessName;
                    int processId = process.Id;

                    string taskNumber = GetTaskNumberFromProcessName(processName);

                    // 检查搜索关键字是否出现在任务号或进程号中
                    if (!string.IsNullOrEmpty(taskNumber) && (taskNumber.ToLower().Contains(searchText) || processId.ToString().Contains(searchText)))
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
                    // 处理异常，例如无法访问某些进程的权限问题s
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // 指定D盘的路径
            string dDrivePath = @"D:\";

            try
            {
                //// 获取D盘下的所有文件
                //string[] files = Directory.GetFiles(dDrivePath);

                // 获取D盘下的所有文件和文件夹
                string[] items = Directory.GetFileSystemEntries(dDrivePath);

                // 清空ListBox以便显示新的文件列表
                showfile.Items.Clear();

                // 将文件和文件夹名添加到ListBox中
                foreach (string item in items)
                {
                    showfile.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                // 处理异常，例如无法访问D盘或其他问题
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        //进入目标文件
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // 获取所选文件夹
            string selectedFolder = (string)showfile.SelectedItem;

            if (!string.IsNullOrEmpty(selectedFolder) && Directory.Exists(selectedFolder))
            {
                try
                {
                    // 获取所选文件夹下的所有文件夹和文件
                    string[] items = Directory.GetFileSystemEntries(selectedFolder);

                    // 清空ListBox以便显示新的文件和文件夹列表
                    showfile.Items.Clear();

                    // 将文件和文件夹名添加到ListBox中
                    foreach (string item in items)
                    {
                        showfile.Items.Add(item);
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常，例如无法访问文件夹或其他问题
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            // 获取输入的任务号
            string taskNumber = taskNumberTextBox.Text;

            if (!string.IsNullOrEmpty(taskNumber))
            {
                try
                {
                    // 获取与任务号匹配的进程
                    Process[] processes = Process.GetProcessesByName(taskNumber);

                    // 检查是否找到匹配的进程
                    if (processes.Length > 0)
                    {
                        // 获取CPU使用情况和内存使用情况
                        float cpuUsage = processes[0].TotalProcessorTime.Seconds; // CPU使用时间（秒）
                        long memoryUsage = processes[0].WorkingSet64 / (1024 * 1024); // 内存使用量（MB）

                        // 显示CPU和内存使用情况
                        MessageBox.Show($"任务号: {taskNumber}\nCPU使用情况: {cpuUsage} 秒\n内存使用情况: {memoryUsage} MB");
                    }
                    else
                    {
                        MessageBox.Show($"未找到匹配任务号: {taskNumber}");
                    }
                }
                catch (Exception ex)
                {
                    // 处理异常，例如无法访问任务的权限问题
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("请输入任务号");
            }

        }
    }
 }
