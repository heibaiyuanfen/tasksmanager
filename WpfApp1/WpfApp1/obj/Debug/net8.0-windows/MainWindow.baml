   M S B A M L   `   `   `  ����B  >WpfApp1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:WpfApp1WpfApp1  5   	       WpfApp1.MainWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:WpfApp1     5   	     � Title$  资源管理器��5   	   $	��450��6   $	��800��6$    � Icon$] WD:\Users\wufeifan\Documents\GitHub\tasksmanager\WpfApp1\WpfApp1\image\1701065843613.jpg��5	   	   .��5
      � ��5   
   �� 5       �� ImageSource$] WD:\Users\wufeifan\Documents\GitHub\tasksmanager\WpfApp1\WpfApp1\image\1701065843613.jpg��6   .+�5   
   �� -   5        $!��显示任务号和进程号��5      $
��Left=�6%   $��	20,26,0,0q�6@   $	��Top=�6S   6k   �� 5   
   -   5        $��taskListBox��5      $
��Left=�6%   $	��Top=�6@   $	��247��6X   $	��135��6d   $��	20,50,0,0q�6q   ��5      �� 5      $] WD:\Users\wufeifan\Documents\GitHub\tasksmanager\WpfApp1\WpfApp1\image\1701065843613.jpg��6   ��5      �� 5      .R�5      ��  �� Orientation$ 
Horizontal=�6!   .+�5      �� $��32��6    $��32��6+   $��0,0,5,0q�67   ��6H   �� *Icon+�� 5      $��Center=�6<   ��6$   �� *TaskID+6<   �� 5      $��Center=�6?   $��10,0,0,0q�6Z   ��6$   �� *	ProcessID+6Z   5       5!      5#      �� 5$   
   -   5        $��结束任务进程��5$      $
��Left=�6"   $��
20,198,0,0q�6=   $	��Top=�6Q   $��24��6i   $��86��6u   6�   �� 5%   
   -   5        $��searchTextBox��5%   b   $
��Left=�6   $��
147,27,0,0q�6-    �� TextWrapping$
 Wrap=�6A   $��输入任务号��6U   $	��Top=�6y   $	��120��6�   6�   �� 5&   
   -   5        $��显示文件��5&      $
��Left=�6    $��
348,25,0,0q�6;   $	��Top=�6O   6g   �� 5'   
   -   5        $��showfile��5'   &   $��
347,51,0,0q�6   $
��Left=�66   $	��Top=�6Q   $	��142��6i   $	��163��6v   ��5*      �� 5+      $] WD:\Users\wufeifan\Documents\GitHub\tasksmanager\WpfApp1\WpfApp1\image\1701065843613.jpg��6   ��5-      �� 5/      .R�50      �� $ 
Horizontal=�6!   .+�51      �� ��6$   �� 52      53      55      �� 56   
   -   5        $��进入目标文件��56      $
��Left=�6"   $��347,203,0,0q�6=   $	��Top=�6R   6j   �� 57   
   -   5        $��查看cpu使用情况��57      $
��Left=�6%   $��
595,36,0,0q�6@   $	��Top=�6T   6l   �� 58   
   -	   5        $��taskNumberTextBox��58   �   $
��Left=�6   $��
595,74,0,0q�6-   $
 Wrap=�6A   $��请输入任务号��6U   $	��Top=�6c   $	��120��6{   6�   �� 59   
   -
   5        $��processiconimage��59   q   $
��Left=�6   $	��100��6+   $��106,290,0,0q�68   $	��Top=�6M   $	��100��6e   6q   5<      5=      