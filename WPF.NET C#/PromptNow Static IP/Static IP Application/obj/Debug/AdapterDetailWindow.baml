   M S B A M L   `   `   `  ����P  LStatic IP Application, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:WpfStaticIPApp.DialogsWpfStaticIPApp.Dialogs  5   	   (    "WpfStaticIPApp.AdapterDetailWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   8dialogs$clr-namespace:WpfStaticIPApp.Dialogs     5   	     � Title$#  Network Adapter Configuration��5   	   $	��720��6/   $	��540��6<   ���	#FFFFFFFF.��5      (� .��5   
   � $
��1440��6   $
��1080��6   ��5
      �� 5       �� ImageSource$8 2pack://application:,,,/Assets/WallpaperAdapter.png��6    j� Stretch$ UniformToFill=�6^   .+�5      ��  �� Orientation$ 
Horizontal=�6   $
��Left=�62   $	��Top=�6M   $��	40,75,0,0q�6e   .+�5      �� -   5         ��Name$ DateText��5      $��20(�6.   $
��Bold"�6<   ���#CACACA$��Roboto)�6c   �� 5      $�� ��6   $��20(�6%   $��45��63   �� 5      -   5        $ TimeText��5      $��20(�6.   $��Medium"�6<   ���#CACACA$��	Noto Sans)�6e   5      �� 5      -   5        $ AdapterNameText��5      $��Adapter Name��61   $��Center=�6E   $	��Top=�5      $��	0,275,0,0q�60   $��35(�6C   $
��Bold"�6Q   
�������$��	Noto Sans)�5   +   �� 5      $9��3pack://application:,,,/Assets/Background_static.png��6   $	��960��6Q   $	��470��6]   $��Center=�5      $	��Top=�61   $��	0,350,0,0q�6I   �� 5      $��Hidden=�6   $��Disabled=�5        �� PanningMode$ VerticalOnly=�5!      $	��960��5"      $	��460��6'   $��Center=�5#      $	��Top=�68   $��	0,350,0,0q�6P   .��5$      �� $ Vertical=�6   $��Center=�64   .+�5&      �� $1��+pack://application:,,,/Assets/IPAddress.png��6   $	��900��6Q   $	��180��6]   $��Center=�5'      $��Uniform=�69   $��0,40,0,0q�6K   �� 5(      -   5         ��Name$ IPAddressTextBox��5(      $��Center=�68   $��	Noto Sans)�5)      $��36(�65   $
��Bold"�6C   ���#00654E
������ $��0q�5*   7    �� TextAlignment$ Center=�6K   $	��900��5+      $��
0,-110,0,0q�6*   �� 5.      $2��,pack://application:,,,/Assets/SubnetMask.png��6   $	��900��6R   $	��180��6^   $��Center=�5/      $��Uniform=�69   $��0,0,0,0q�6K   �� 50      -   5        $ SubnetMaskTextBox��50      $��Center=�69   $��	Noto Sans)�51      $��36(�65   $
��Bold"�6C   ���#00654E
������ $��0q�52   7   $ Center=�6K   $	��900��53      $��
0,-110,0,0q�6*   �� 56      $6��0pack://application:,,,/Assets/DEFAULTGATEWAY.png��6   $	��900��6V   $	��180��6b   $��Center=�57      $��Uniform=�69   $��0,0,0,0q�6K   �� 58      -   5        $ GatewayTextBox��58      $��Center=�66   $��	Noto Sans)�59      $��36(�65   $
��Bold"�6C   ���#00654E
������ $��0q�5:   7   $ Center=�6K   $	��900��5;      $��
0,-110,0,0q�6*   �� 5>      $<��6pack://application:,,,/Assets/Preferred DNS Server.png��6   $	��900��6\   $	��180��6h   $��Center=�5?      $��Uniform=�69   $��0,0,0,0q�6K   �� 5@      -   5        $ PreferredDNSTextBox��5@      $��Center=�6;   $��	Noto Sans)�5A      $��36(�65   $
��Bold"�6C   ���#00654E
������ $��0q�5B   7   $ Center=�6K   $	��900��5C      $��
0,-110,0,0q�6*   �� 5F      $<��6pack://application:,,,/Assets/Alternate DNS server.png��6   $	��900��6\   $	��180��6h   $��Center=�5G      $��Uniform=�69   $��0,0,0,0q�6K   �� 5H      -   5        $ AlternateDNSTextBox��5H      $��Center=�6;   $��	Noto Sans)�5I      $��36(�65   $
��Bold"�6C   ���#00654E
������ $��0q�5J   7   $ Center=�6K   $	��900��5K      $��
0,-110,0,0q�6*   5L      5M      �� 5P      -	   5        $ 
StatusText��5P      $��Center=�6,   $	��Top=�6I   $��	0,840,0,0q�5Q      $��	Noto Sans)�6+   $��36(�6B   $��SemiBold"�6P   �� 5R      $�	STATUS : ��6   
�������6'    �� 5S      -
   5         ��Name$ StatusMessageRun��5S      $�Loaded successfully��60   
��� � �6K   5T      �� 5W      $9��3pack://application:,,,/Assets/Background_button.png��6   $	��960��6Q   $	��286��6]   $��Center=�5X      $	��Top=�61   $��	0,900,0,0q�6I   �� 5[      -   5        	 ��Name$	 ButtonScrollViewer��5[      $
��Auto=�5\      $��Hidden=�6@   $	��960��5]      $	��230��6'   $��Center=�64   $	��Top=�6Q   $��	0,940,0,0q�5^      $ HorizontalOnly=�6.   .��5_      �� -   5        
 ��Name$
 ButtonPanel��5_      $ 
Horizontal=�62   $
��Left=�6K   .+�5a      �� -   5        $	��380��5a      $	��200��6)   
������ $��0q�6O   $
��10,0q�6c   .��5c      �� $3��-pack://application:,,,/Assets/SAVE_button.png��6    $��Uniform=�6W   5d      �� 5f      -   5        $	��380��5f      $	��200��6)   
������ $��0q�6O   $
��10,0q�6c   .��5h      �� $3��-pack://application:,,,/Assets/DHCP_Button.png��6    $��Uniform=�6W   5i      �� 5k      -   5        $	��380��5k      $	��200��6)   
������ $��0q�6O   $
��10,0q�6c   .��5m      �� $4��.pack://application:,,,/Assets/CLEAR_Button.png��6    $��Uniform=�6X   5n      �� 5p      -   5        $	��380��5p      $	��200��6)   
������ $��0q�6O   $
��10,0q�6c   .��5r      �� $6��0pack://application:,,,/Assets/REFRESH_Button.png��6    $��Uniform=�6Z   5s      5t      5u      �� 5y      -   5        $	��640��5y      $	��114��6!   $��Center=�6.   $��Bottom=�6K   $��0,0,0,50q�6f   $��0q�5z   .   
������ .��5{      �� $,��&pack://application:,,,/Assets/back.png��6   $��Uniform=�6H   5|      5}      5~      5      