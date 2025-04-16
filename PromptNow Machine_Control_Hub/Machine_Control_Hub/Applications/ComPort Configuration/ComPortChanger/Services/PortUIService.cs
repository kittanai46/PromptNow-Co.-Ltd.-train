using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Data;
using ComPortChanger.Models;

namespace ComPortChanger.Services
{
    public class PortUIService : IPortUIService
    {
        public void CreatePortBox(PortInfo port, StackPanel container)
        {
            Grid portBox = new Grid
            {
                Height = 180,
                Margin = new Thickness(0, 0, 0, -15) 
            };
            portBox.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/ComPortChanger;component/Assets/PortBox.png")),
                Stretch = Stretch.Fill
            };

            Grid contentGrid = new Grid
            {
                Margin = new Thickness(70, 0, 80, 0)
            };

            contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

            TextBlock portName = new TextBlock
            {
                Text = port.Name,
                FontSize = 27,
                FontFamily = new FontFamily("Noto Sans"),
                FontWeight = FontWeights.Bold,
                Foreground = (Brush)new BrushConverter().ConvertFrom("#00654E"),
                VerticalAlignment = VerticalAlignment.Center,
                TextTrimming = TextTrimming.CharacterEllipsis,
                MaxWidth = 500  
            };
            Grid.SetColumn(portName, 0);
            contentGrid.Children.Add(portName);
            ComboBox comboBox = new ComboBox
            {
                Width = 120,
                Height = 50,
                VerticalAlignment = VerticalAlignment.Center,
                FontSize = 18,
                FontFamily = new FontFamily("Roboto"),
                ItemsSource = port.AvailableComNumbers
            };
            comboBox.SelectedItem = port.SelectedComNumber;
            comboBox.ItemTemplate = new DataTemplate();
            var factory = new FrameworkElementFactory(typeof(TextBlock));
            factory.SetBinding(TextBlock.TextProperty, new Binding
            {
                StringFormat = "COM{0}"
            });
            comboBox.ItemTemplate.VisualTree = factory;
            comboBox.SelectionChanged += (sender, e) =>
            {
                port.SelectedComNumber = (int)comboBox.SelectedItem;
            };

            Grid.SetColumn(comboBox, 1);
            comboBox.Margin = new Thickness(0, 0, 0, 0);
            contentGrid.Children.Add(comboBox);

            portBox.Children.Add(contentGrid);
            container.Children.Add(portBox);
        }
        public void SetFrameBackground(Grid frameContainer)
        {
            frameContainer.Background = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri("pack://application:,,,/ComPortChanger;component/Assets/Frame.png")),
                Stretch = Stretch.Fill
            };
        }
    }
}