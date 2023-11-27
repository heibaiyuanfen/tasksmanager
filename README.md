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

## WPF应用程序示例 - 显示文件和文件夹

### XAML布局

在XAML中，创建一个界面布局，包括一个按钮和一个ListBox用于显示文件和文件夹列表。

```xml
<Window x:Class="YourNamespace.MainWindow"
        xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        Title="显示文件和文件夹" Height="400" Width="600">
    <Grid>
        <!-- 按钮用于显示文件和文件夹 -->
        <Button Content="显示文件和文件夹" Click="ShowFilesAndDirectories_Click" Width="200" Height="30" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="20,10,0,0"/>
        
        <!-- ListBox用于显示文件和文件夹列表 -->
        <ListBox Name="taskListBox" HorizontalAlignment="Left" VerticalAlignment="Top" Width="500" Height="300" Margin="20,50,0,0">
            <ListBox.ItemTemplate>
                <DataTemplate>
                    <TextBlock Text="{Binding}" />
                </DataTemplate>
            </ListBox.ItemTemplate>
        </ListBox>
    </Grid>
</Window>

```
## c#代码
### 在C#代码中，实现按钮的点击事件处理程序，用于同时获取并显示D盘下的文件和文件夹列表。
``` c#code
using System;
using System.IO;
using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowFilesAndDirectories_Click(object sender, RoutedEventArgs e)
        {
            // 指定D盘的路径
            string dDrivePath = @"D:\";

            try
            {
                // 获取D盘下的所有文件和文件夹
                string[] items = Directory.GetFileSystemEntries(dDrivePath);

                // 清空ListBox以便显示新的文件和文件夹列表
                taskListBox.Items.Clear();

                // 将文件和文件夹名添加到ListBox中
                foreach (string item in items)
                {
                    taskListBox.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                // 处理异常，例如无法访问D盘或其他问题
                MessageBox.Show($"Error: {ex.Message}");
            }
        }
    }
}



```

## 更新了进入目标文件的功能

``` c#code


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



```
## 还要帮儿子解锁，真恶心



## 实现查找cpu和内存使用情况

``` c#code

<Grid>
    <!-- 按钮用于查找CPU和内存使用情况 -->
    <Button Content="查找CPU和内存使用情况" Click="ShowPerformance_Click" Width="250" Height="30" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="20,10,0,0"/>
    
    <!-- TextBox用于输入任务号 -->
    <TextBox Name="taskNumberTextBox" PlaceholderText="输入任务号" Width="150" Height="30" HorizontalAlignment="Left" VerticalAlignment="Top" Margin="280,10,0,0"/>

    <!-- ListBox用于显示文件夹和文件列表 -->
    <ListBox Name="folderListBox" HorizontalAlignment="Left" VerticalAlignment="Top" Width="500" Height="200" Margin="20,50,0,0">
        <ListBox.ItemTemplate>
            <DataTemplate>
                <TextBlock Text="{Binding}" />
            </DataTemplate>
        </ListBox.ItemTemplate>
    </ListBox>
    
    <!-- ListBox用于显示任务号和进程号 -->
    <ListBox Name="taskListBox" HorizontalAlignment="Right" VerticalAlignment="Top" Width="200" Height="200" Margin="0,50,20,0">
        <ListBox.ItemTemplate>
            <DataTemplate>
                <TextBlock Text="{Binding TaskNumber}" />
            </DataTemplate>
        </ListBox.ItemTemplate>
    </ListBox>
</Grid>


using System;
using System.Diagnostics;
using System.Windows;

namespace YourNamespace
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowPerformance_Click(object sender, RoutedEventArgs e)
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


```



