# tasksmanager
 帮儿子搞一个任务管理器，我可太tmd贴心了

## WPF应用程序示例 - 显示和结束任务/进程，搜索任务号

### XAML布局

在XAML中，创建界面布局，包括一个按钮、一个搜索框、和一个ListBox用于显示任务号和进程号。

```xml
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="任务号显示" Height="300" Width="400">
    <Grid>
        <!-- 显示任务号和进程号的ListBox -->
        <ListBox Name="taskListBox" HorizontalAlignment="Left" VerticalAlignment="Top" Width="300" Height="200" Margin="20,50,0,0">
            <ListBox.ItemTemplate>
                <DataTemplate>
                    <StackPanel Orientation="Horizontal">
                        <TextBlock Text="任务号: " />
                        <TextBlock Text="{Binding TaskNumber}" />
                        <TextBlock Text="  进程号: " />
                        <TextBlock Text="{Binding ProcessId}" />
                    </StackPanel>
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>

        <!-- 搜索框 -->
        <TextBox Name="searchTextBox" PlaceholderText="输入任务号或进程号进行搜索" Width="200" HorizontalAlignment="Left" Margin="20,10,0,0"/>

        <!-- 显示任务号和结束任务/进程的按钮 -->
        <Button Content="显示任务号" Click="ShowTaskNumbers_Click" Width="100" Height="30" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="20,260,0,0"/>
        <Button Content="结束任务和进程" Click="EndTask_Click" Width="150" Height="30" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="130,260,0,0"/>
    </Grid>
</Window>

```

##  c#代码
### 在C#代码中，处理按钮点击事件、获取任务号和进程号、结束任务和进程以及搜索任务号。
```code 
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        private List<string> taskNumbers = new List<string>();

        public MainWindow()
        {
            InitializeComponent();
            searchTextBox.TextChanged += SearchTextBox_TextChanged;
        }

        private void ShowTaskNumbers_Click(object sender, RoutedEventArgs e)
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
                        TaskInfo taskInfo = new TaskInfo
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

        private void EndTask_Click(object sender, RoutedEventArgs e)
        {
            // 检查是否选择了一个任务
            if (taskListBox.SelectedItem != null)
            {
                TaskInfo selectedTask = (TaskInfo)taskListBox.SelectedItem;
                
                try
                {
                    // 根据任务号结束任务和进程
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
                        TaskInfo taskInfo = new TaskInfo
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

        // 其他代码，包括获取任务号的方法和TaskInfo数据模型的定义
        // ...
    }

    public class TaskInfo
    {
        public string TaskNumber { get; set; }
        public int ProcessId { get; set; }
    }
}

```

