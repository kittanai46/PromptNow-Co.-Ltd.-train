   M S B A M L   `   `   `  ����  Export Daily Log Report!    EventLogExporter.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	     � Title$  Event Log Exporter��5   	   $	��500��6$   $	��700��61   .��5      � .+�5   
   �� $��Select Log Type:��6   $
��Left=�6+   $��	10,10,0,0q�6F   $	��Top=�6Y   �� 5   
   -   5         ��Name$ LogTypeComboBox��5      $
��Left=�6,   $��	10,40,0,0q�6G   $	��Top=�6Z   $	��200��6r   .@�5      �� $��Application��6   �� 5	      $��System��6   �� 5
      $��Security��6   �� 5      $��Setup��6   �� 5      $��Forwarded Events��6   5      �� 5   
   $��Select Event Levels:��6   $
��Left=�6/   $��	10,80,0,0q�6J   $	��Top=�6]   �� 5   
    �� Orientation$ 
Horizontal=�6   $��
10,110,0,0q�6.   .+�5      �� -   5         ��Name$ CriticalCheckBox��5      $��Critical��61    d� 	IsChecked$
 TrueY�6D   $��0,0,10,0q�6U   �� 5      -   5        $ WarningCheckBox��5      $��Warning��60   $
 TrueY�6B   $��0,0,10,0q�6S   �� 5      -   5        $ ErrorCheckBox��5      $��Error��6.   $
 TrueY�6>   $��0,0,10,0q�6O   �� 5      -   5        $ InformationCheckBox��5      $��Information��64   $
 TrueY�6J   $��0,0,10,0q�6[   �� 5      -   5        $ VerboseCheckBox��5      $��Verbose��60   $ FalseY�6B   5      �� 5   
   $��Select Date Range:��6   $
��Left=�6-   $��
10,150,0,0q�6H   $	��Top=�6\   �� 5   
   $ 
Horizontal=�6   $��
10,180,0,0q�6.   .+�5      (  "System.Windows.Controls.DatePicker  -   5          Name$ StartDatePicker��5      $��0,0,10,0q�62     5      -   5        $ EndDatePicker��5      5      �� 5   
   -	   5        $��	Show Logs��5      $
��Left=�6%   $��
10,220,0,0q�6@   $	��Top=�6T   $	��100��6l   6x   �� 5   
   -
   5        $��Export Logs��5      $
��Left=�6'   $��120,220,0,0q�6B   $	��Top=�6W   $	��100��6o   6{   �� 5!   
   -   5         ��Name$ StatusTextBlock��5!      $
��Left=�6-   $��
10,260,0,0q�6H   $	��Top=�6\   $	��650��6t   $
��Wrap=�6�   &   System.Windows.Controls.DataGrid  5#   
   -   5          Name$ LogDataGrid��5#      $
��Left=�6(   $��
10,290,0,0q�6C   $	��Top=�6W   $	��150��6o   $	��650��6|      AutoGenerateColumns . 6�   5$      5%      