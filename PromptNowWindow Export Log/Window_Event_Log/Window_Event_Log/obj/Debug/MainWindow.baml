   M S B A M L   `   `   `  ����  Window_Event_Log    WpfStaticIPApp.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Windows Logs Viewer��5   	   $	��720��6%   $	��540��62   .��5      (� .��5   
   � $
��1440��6   $
��1080��6   ��5      �� 5	       �� ImageSource$4 .pack://application:,,,/Assets/WallpaperLog.png��6    j� Stretch$ UniformToFill=�6Z   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   $	��Top=�6M   $��	40,75,0,0q�6e   .+�5      �� -   5         ��Name$ DateText��5      $��20(�6.   $
��Bold"�6<   ���#CACACA$��Roboto)�6c   �� 5      $�� ��6   $��20(�6%   $��45��63   �� 5      -   5        $ TimeText��5      $��20(�6.   $��Medium"�6<   ���#CACACA$��	Noto Sans)�6e   $��0,-5,0,0q�6|   5      � 5      $��Center=�6   $��Center=�60   $��0,80,0,0q�6K   .+�5      �� $6��0pack://application:,,,/Assets/Background_Log.png��6   $	��960��5      $	��820��6$   �� 5      -   5        $	��410��5      $	��175��6%   $
��Left=�62   $	��Top=�6M   $��
50,160,0,0q�6e   $��0q�5   6   
������ .��5      �� $5��/pack://application:,,,/Assets/Application_B.png��6   $��Uniform=�6U   5      �� 5      -   5        $	��410��5      $	��175��6%   $��Right=�62   $	��Top=�6N   $��
0,160,50,0q�6f   $��0q�5   1   
������ .��5       �� $0��*pack://application:,,,/Assets/System_B.png��6   $��Uniform=�6P   5!      �� 5#      -   5        $	��410��5#      $	��175��6%   $
��Left=�62   $	��Top=�6M   $��50,360,50,0q�6e   $��0q�5$   3   
������ .��5%      �� $2��,pack://application:,,,/Assets/Security_B.png��6   $��Uniform=�6R   5&      �� 5(      -   5        $	��410��5(      $	��175��6%   $��Right=�62   $	��Top=�6N   $��
0,360,50,0q�6f   $��0q�5)   0   
������ .��5*      �� $/��)pack://application:,,,/Assets/Setup_B.png��6   $��Uniform=�6O   5+      �� 5-      -   5        $	��410��5-      $	��175��6%   $
��Left=�62   $	��Top=�6M   $��50,570,50,0q�6e   $��0q�5.   :   
������ .��5/      �� $:��4pack://application:,,,/Assets/Forwarded Events_B.png��6   $��Uniform=�6Z   50      �� 52      $	��410��6   $	��175��6%   $��Right=�62   $	��Top=�6N   $��
0,570,50,0q�6f   $��0q�53      
������ .��54      �� $.��(pack://application:,,,/Assets/Void_B.png��6   $��Uniform=�6N   55      57      �� 5:      -   5        $	��640��5:      $	��114��6!   $��Center=�6.   $��Bottom=�6K   $��0,0,0,50q�6f   $��0q�5;   .   
������ .��5<      �� $,��&pack://application:,,,/Assets/back.png��6   $��Uniform=�6H   5=      5>      5?      5@      