using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace Algos2Lab
{
    public partial class MainWindow : Window
    {
        private readonly List<int> _setA = new List<int> { 0, 1, 3, 4, 5 };

        public MainWindow()
        {
            InitializeComponent();
            DisplayRelation();
            DisplayProperties();
        }

        private void DisplayRelation()
        {
            var data = new ObservableCollection<RelationRow>();

            foreach (var row in _setA.Select(b => new RelationRow
                     {
                         B = b,
                         A0 = (0 + 2 < b) ? $"({0}, {b})" : "",
                         A1 = (1 + 2 < b) ? $"({1}, {b})" : "",
                         A3 = (3 + 2 < b) ? $"({3}, {b})" : "",
                         A4 = (4 + 2 < b) ? $"({4}, {b})" : "",
                         A5 = (5 + 2 < b) ? $"({5}, {b})" : ""
                     }))
            {
                data.Add(row);
            }

            RelationDataGrid.ItemsSource = data;
        }

        private void DisplayProperties()
        {
            var reflexive = true;
            var antiReflexive = true;
            var symmetric = true;
            var antiSymmetric = true;
            var transitive = true;

            foreach (var t in _setA)
            {
                foreach (var t1 in _setA)
                {
                    var relation = t + 2 < t1;

                    if (t == t1)
                    {
                        if (relation)
                        {
                            reflexive = false;
                        }
                    }
                    else
                    {
                        if (relation)
                        {
                            antiReflexive = false;
                        }

                        if (relation && !(t1 + 2 < t))
                        {
                            symmetric = false;
                        }

                        if (relation && (t1 + 2 < t) && (t != t1))
                        {
                            antiSymmetric = false;
                        }
                    }
                }
            }

            foreach (var relation1 in from t in _setA from t1 in _setA from t2 in _setA let relation1 = t + 2 < t1 let relation2 = t1 + 2 < t2 let relation3 = t + 2 < t2 where relation1 && relation2 && !relation3 select relation1)
            {
                transitive = false;
            }

            label1.Content = $"Рефлексивное: {reflexive}\n" +
                             $"Антирефлексивное: {antiReflexive}\n" +
                             $"Симметричное: {symmetric}\n" +
                             $"Антисимметричное: {antiSymmetric}\n" +
                             $"Транзитивное: {transitive}";
        }

        public class RelationRow
        {
            public int B { get; set; }
            public string A0 { get; set; }
            public string A1 { get; set; }
            public string A3 { get; set; }
            public string A4 { get; set; }
            public string A5 { get; set; }
        }
    }
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string content && !string.IsNullOrEmpty(content))
            {
                return Brushes.LightGreen;
            }

            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}