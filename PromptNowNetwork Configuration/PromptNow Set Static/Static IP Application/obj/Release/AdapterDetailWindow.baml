   M S B A M L   `   `   `  ����   Static IP Application(    "WpfStaticIPApp.AdapterDetailWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$#  Network Adapter Configuration��$	��720��$	��540�����	#FFFFFFFF.��(� .��� $
��1440��$
��1080������  �� ImageSource$8 2pack://application:,,,/Assets/WallpaperAdapter.png�� j� Stretch$ UniformToFill=�.+���  �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�$��	40,75,0,0q�.+��� -    ��Name$ DateText��$��20(�$
��Bold"����#CACACA$��Roboto)��� $�� ��$��20(�$��45���� -   $ TimeText��$��20(�$��Medium"����#CACACA$��	Noto Sans)��� -   $ AdapterNameText��$��Adapter Name��$��Center=�$	��Top=�$��	0,275,0,0q�$��35(�$
��Bold"�
�������$��	Noto Sans)��� $9��3pack://application:,,,/Assets/Background_static.png��$	��960��$	��470��$��Center=�$	��Top=�$��	0,350,0,0q��� $��Hidden=�$��Disabled=� �� PanningMode$ VerticalOnly=�$	��960��$	��460��$��Center=�$	��Top=�$��	0,350,0,0q�.���� $ Vertical=�$��Center=�.+��� $1��+pack://application:,,,/Assets/IPAddress.png��$	��900��$	��180��$��Center=�$��Uniform=�$��0,40,0,0q��� -    ��Name$ IPAddressTextBox��$��	Noto Sans)�$��36(�$
��Bold"����#00654E
������ $��0q� �� TextAlignment$ Center=�$	��900��$��
0,-110,0,0q��� $2��,pack://application:,,,/Assets/SubnetMask.png��$	��900��$	��180��$��Center=�$��Uniform=�$��0,0,0,0q��� -   $ SubnetMaskTextBox��$��	Noto Sans)�$��36(�$
��Bold"����#00654E
������ $��0q�$ Center=�$	��900��$��
0,-110,0,0q��� $6��0pack://application:,,,/Assets/DEFAULTGATEWAY.png��$	��900��$	��180��$��Center=�$��Uniform=�$��0,0,0,0q��� -   $ GatewayTextBox��$��	Noto Sans)�$��36(�$
��Bold"����#00654E
������ $��0q�$ Center=�$	��900��$��
0,-110,0,0q��� $<��6pack://application:,,,/Assets/Preferred DNS Server.png��$	��900��$	��180��$��Center=�$��Uniform=�$��0,0,0,0q��� -   $ PreferredDNSTextBox��$��	Noto Sans)�$��36(�$
��Bold"����#00654E
������ $��0q�$ Center=�$	��900��$��
0,-110,0,0q��� $<��6pack://application:,,,/Assets/Alternate DNS server.png��$	��900��$	��180��$��Center=�$��Uniform=�$��0,0,0,0q��� -   $ AlternateDNSTextBox��$��	Noto Sans)�$��36(�$
��Bold"����#00654E
������ $��0q�$ Center=�$	��900��$��
0,-110,0,0q��� -	   $ 
StatusText��$��Center=�$	��Top=�$��	0,840,0,0q�$��	Noto Sans)�$��36(�$��SemiBold"��� $�	STATUS : ��
������� �� -
    ��Name$ StatusMessageRun��$�Loaded successfully��
��� � ��� $9��3pack://application:,,,/Assets/Background_button.png��$	��960��$	��286��$��Center=�$	��Top=�$��	0,900,0,0q��� -   	 ��Name$	 ButtonScrollViewer��$
��Auto=�$��Hidden=�$	��960��$	��230��$��Center=�$	��Top=�$��	0,940,0,0q�$ HorizontalOnly=�.���� -   
 ��Name$
 ButtonPanel��$ 
Horizontal=�$
��Left=�.+��� -   $	��380��$	��200��
������ $��0q�$
��10,0q�.���� $3��-pack://application:,,,/Assets/SAVE_button.png��$��Uniform=��� -   $	��380��$	��200��
������ $��0q�$
��10,0q�.���� $3��-pack://application:,,,/Assets/DHCP_Button.png��$��Uniform=��� -   $	��380��$	��200��
������ $��0q�$
��10,0q�.���� $4��.pack://application:,,,/Assets/CLEAR_Button.png��$��Uniform=��� -   $	��380��$	��200��
������ $��0q�$
��10,0q�.���� $6��0pack://application:,,,/Assets/REFRESH_Button.png��$��Uniform=�� -    �Name$ KeyboardGrid��$y�	Collapsed=�$	��960��$	��286��$��Center=�$	��Top=�$��	0,900,0,0q�.+�A�  A� Rows$ 1�� A� Columns$ 6��$��Center=�$��Center=�$��
0,-150,0,0q�.+��� -   $	��120��$��70��
������ $��0q�
 � Tag$ 1��$��-10,0q�.���� $)��#pack://application:,,,/Assets/1.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$ 2��$��-10,0q�.���� $)��#pack://application:,,,/Assets/2.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$ 3��$��-10,0q�.���� $)��#pack://application:,,,/Assets/3.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$ 4��$��-10,0q�.���� $)��#pack://application:,,,/Assets/4.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$��-10,0q�.���� $0��*pack://application:,,,/Assets/KEY_BACK.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$��-10,0q�.���� $1��+pack://application:,,,/Assets/KEY_CLOSE.png��$��Uniform=�A� $ 1��$ 6��$��Center=�$��Center=�$��0,10,0,0q�.+��� -   $	��120��$��70��
������ $��0q�$ 5��$��-10,0q�.���� $)��#pack://application:,,,/Assets/5.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$ 6��$��-10,0q�.���� $)��#pack://application:,,,/Assets/6.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$ 7��$��-10,0q�.���� $)��#pack://application:,,,/Assets/7.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$ 8��$��-10,0q�.���� $)��#pack://application:,,,/Assets/8.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$��-10,0q�.���� $1��+pack://application:,,,/Assets/KEY_CLEAR.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$��-10,0q�.���� $.��(pack://application:,,,/Assets/KEY_UP.png��$��Uniform=�A� $ 1��$ 6��$��Center=�$��Center=�$��	0,170,0,0q�.+��� -   $	��120��$��70��
������ $��0q�$ 9��$��-10,0q�.���� $)��#pack://application:,,,/Assets/9.png��$��Uniform=��� -   $	��120��$��70��
������ $��0q�$ 00��$��-10,0q�.���� $*��$pack://application:,,,/Assets/00.png��$��Uniform=��� -    $	��120��$��70��
������ $��0q�$ 0��$��-10,0q�.���� $)��#pack://application:,,,/Assets/0.png��$��Uniform=��� -!   $	��120��$��70��
������ $��0q�$ .��$��-10,0q�.���� $/��)pack://application:,,,/Assets/KEY_DOT.png��$��Uniform=��� -"   $	��120��$��70��
������ $��0q�$��-10,0q�.���� $3��-pack://application:,,,/Assets/KEY_REFRESH.png��$��Uniform=��� -#   $	��120��$��70��
������ $��0q�$��-10,0q�.���� $0��*pack://application:,,,/Assets/KEY_DOWN.png��$��Uniform=��� -$   $	��640��$	��114��$��Center=�$��Bottom=�$��0,0,0,50q�$��0q�
������ .���� $,��&pack://application:,,,/Assets/back.png��$��Uniform=�