   M S B A M L   `   `   `  ����   Window_Event_Log#    WpfStaticIPApp.LogLevelWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Log Level Selection��$	��720��$	��540��.��(� .��� $
��1440��$
��1080������  �� ImageSource$6 0pack://application:,,,/Assets/WallpaperLevel.png�� j� Stretch$ UniformToFill=�.+���  �� Orientation$ 
Horizontal=�$
��Left=�$	��Top=�$��	40,75,0,0q�.+��� -    ��Name$ DateText��$��20(�$
��Bold"����#CACACA$��Roboto)��� $�� ��$��20(�$��45���� -   $ TimeText��$��20(�$��Medium"����#CACACA$��	Noto Sans)�$��0,-5,0,0q��� -   $ LogTypeText��$��Selected Log: ��
�������$��Center=�$��	0,240,0,0q�$��45(�$
��Bold"�$��	Noto Sans)�� $��Center=�$	��Top=�$��	0,300,0,0q�.+��� $8��2pack://application:,,,/Assets/Background_Level.png��$	��960��$	��500���� -    ��Name$ CriticalBorder��
������ $��2q�$	��280��$	��200��$��	50,45,0,0q�$
��Left=�$	��Top=�._��� -   
������ $��0q�.���� $6��0pack://application:,,,/Assets/Event_Critical.png��$��Uniform=��� -   $ ErrorBorder��
������ $��2q�$	��280��$	��200��$��0,45,0,0q�$��Center=�$	��Top=�._��� -   
������ $��0q�.���� $3��-pack://application:,,,/Assets/Event_Error.png��$��Uniform=��� -   $ WarningBorder��
������ $��2q�$	��280��$	��200��$��	0,45,50,0q�$��Right=�$	��Top=�._��� -	   
������ $��0q�.���� $5��/pack://application:,,,/Assets/Event_Warning.png��$��Uniform=��� -
   $ InformationBorder��
������ $��2q�$	��280��$	��200��$��
50,250,0,0q�$
��Left=�$	��Top=�._��� -   
������ $��0q�.���� $9��3pack://application:,,,/Assets/Event_Information.png��$��Uniform=��� -   $ VerboseBorder��
������ $��2q�$	��280��$	��200��$��	0,250,0,0q�$��Center=�$	��Top=�._��� -   
������ $��0q�.���� $5��/pack://application:,,,/Assets/Event_Verbose.png��$��Uniform=��� -   $
 Void��
������ $��2q�$	��280��$	��200��$��	0,250,0,0q�$��Right=�$	��Top=�._��� $2��,pack://application:,,,/Assets/Event_Void.png��$��Uniform=�� $��Center=�$��Center=�$��	0,210,0,0q�.+��� $>��8pack://application:,,,/Assets/Background_PathDowload.png��$	��960��$	��102���� -   $ PathTextBlock��$*��$Export Path: PLEASE SELECT YOUR PATH��$��30(�$	��700��$��Center=�$��Center=�$��0,0,0,0q�
�������$
��Bold"�$��	Noto Sans)�$
��Wrap=�$��CharacterEllipsis=��� $ 
Horizontal=�$��Center=�$��Center=�$��	0,170,0,0q�.+��� $��STATUS: ��$��40(�$��SemiBold"�
�������$��Roboto)��� -   $ StatusTextBlock��$��PENDING��$��40(�$��SemiBold"����#FF8800$��Roboto)��� -   $	��150��$��50��$��Right=�$��Center=�$��0,0,60,0q�
������ $��0q�.���� $0��*pack://application:,,,/Assets/Browse_B.png��$��Uniform=�� $��Center=�$��Center=�$��	0,720,0,0q�.+��� $4��.pack://application:,,,/Assets/Background_B.png��$	��960��$	��320���� -   $	��400��$	��230��
������ $��0q�$��	0,0,450,0q�.���� $.��(pack://application:,,,/Assets/Save_B.png��$��Uniform=��� -   $	��400��$	��230��
������ $��0q�$��	450,0,0,0q�.���� $0��*pack://application:,,,/Assets/Export_B.png��$��Uniform=��� -   $	��640��$	��114��$��Center=�$��Bottom=�$��0,0,0,50q�$��0q�
������ .���� $,��&pack://application:,,,/Assets/back.png��$��Uniform=�