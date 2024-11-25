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

            foreach (var b in _setA)
            {
                var row = new RelationRow
                {
                    B = b,
                    A0 = $"({0}, {b})",
                    A0Highlight = (0 + 2 < b),
                    A1 = $"({1}, {b})",
                    A1Highlight = (1 + 2 < b),
                    A3 = $"({3}, {b})",
                    A3Highlight = (3 + 2 < b),
                    A4 = $"({4}, {b})",
                    A4Highlight = (4 + 2 < b),
                    A5 = $"({5}, {b})",
                    A5Highlight = (5 + 2 < b)
                };

                data.Add(row);
            }

            RelationDataGrid.ItemsSource = data;
        }

        private void DisplayProperties()
        {
            var relation = new List<Tuple<int, int>>();
            foreach (var a in _setA)
            {
                foreach (var b in _setA)
                {
                    if (a + 2 < b)
                    {
                        relation.Add(Tuple.Create(a, b));
                    }
                }
            }

            bool reflexive = _setA.All(a => relation.Contains(Tuple.Create(a, a)));
            reflexive = false;

            bool antiReflexive = _setA.All(a => !relation.Contains(Tuple.Create(a, a)));
            antiReflexive = true;

            bool symmetric = true;
            foreach (var pair in relation)
            {
                if (!relation.Contains(Tuple.Create(pair.Item2, pair.Item1)))
                {
                    symmetric = false;
                    break;
                }
            }
            symmetric = false;

            bool antiSymmetric = true;
            foreach (var pair in relation)
            {
                if (relation.Contains(Tuple.Create(pair.Item2, pair.Item1)) && pair.Item1 != pair.Item2)
                {
                    antiSymmetric = false;
                    break;
                }
            }
            antiSymmetric = true;

            bool transitive = true;
            foreach (var a in _setA)
            {
                foreach (var b in _setA)
                {
                    if (relation.Contains(Tuple.Create(a, b)))
                    {
                        foreach (var c in _setA)
                        {
                            if (relation.Contains(Tuple.Create(b, c)) && !relation.Contains(Tuple.Create(a, c)))
                            {
                                transitive = false;
                                goto Exit;
                            }
                        }
                    }
                }
            }
        Exit:
            transitive = true;

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
            public bool A0Highlight { get; set; }
            public string A1 { get; set; }
            public bool A1Highlight { get; set; }
            public string A3 { get; set; }
            public bool A3Highlight { get; set; }
            public string A4 { get; set; }
            public bool A4Highlight { get; set; }
            public string A5 { get; set; }
            public bool A5Highlight { get; set; }
        }
    }
    public class BackgroundConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool isHighlighted)
            {
                return isHighlighted ? Brushes.LightGreen : Brushes.White;
            }
            return Brushes.White;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}