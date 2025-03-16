using OxyPlot.Series;
using OxyPlot.Wpf;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Windows;

namespace Programowanie_V
{
    public partial class MainWindow : Window
    {
        private Przebiegi trojkat;

        public MainWindow()
        {
            InitializeComponent();
            trojkat = new Przebiegi();
        }

        private void btnWykresTrojkat_Click(object sender, RoutedEventArgs e)
        {
            // Generujemy przebieg trójkątny
            trojkat.Generuj_Trojkat(amplituda: 1.0, okres: 2.0, krok: 0.1, t_trwania: 10.0);

            // Tworzymy model wykresu
            var model = new PlotModel { Title = "Przebieg trójkątny" };

            // Dodajemy siatkę
            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.Gray,
                MinorGridlineColor = OxyColors.LightGray,
                Minimum = -1,
                Maximum = 1,
            });

            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.Gray,
                MinorGridlineColor = OxyColors.LightGray,
               // Minimum = 0,
               // Maximum = 10,
            });

            // Tworzymy serię i dodajemy punkty
            var seria = new LineSeries { Title = "Trojkat", Color = OxyColors.Blue };

            for (int i = 0; i < trojkat.xTrojkat.Count; i++)
            {
                seria.Points.Add(new DataPoint(trojkat.xTrojkat[i], trojkat.yTrojkat[i]));
            }

            // Dodajemy serię do modelu
            model.Series.Add(seria);
            RysujFFT(trojkat.yTrojkat);
            // Przypisujemy model do PlotView
            plotview.Model = model;
        }

        private void btn_Wykres_Sin_Click(object sender, RoutedEventArgs e)
        {
            // Generujemy przebieg sinusoidalny
            trojkat.Generuj_Sinus(amplituda: 1.0, czestotliwosc: 0.5, krok: 0.1, t_trwania: 10.0);

            // Tworzymy model wykresu
            var model = new PlotModel { Title = "Przebieg sinusoidalny" };

            // Dodajemy siatkę
            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.Gray,
                MinorGridlineColor = OxyColors.LightGray,
                //Minimum = -1,
                //Maximum = 1,
            });

            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.Gray,
                MinorGridlineColor = OxyColors.LightGray,
                //Minimum = 0,
                //Maximum = 10,
            });

            // Tworzymy serię i dodajemy punkty
            var seriaSin = new LineSeries { Title = "Sinus", Color = OxyColors.Green };

            for (int i = 0; i < trojkat.xSin.Count; i++)
            {
                seriaSin.Points.Add(new DataPoint(trojkat.xSin[i], trojkat.ySin[i]));
            }

            // Dodajemy serię do modelu
            model.Series.Add(seriaSin);

            // Przypisujemy model do PlotView
            plotview.Model = model;
            RysujFFT(trojkat.ySin);
        }

        private void btn_wykres_prostokat_Click(object sender, RoutedEventArgs e)
        {
            // Generujemy przebieg prostokątny
            trojkat.Generuj_Prostokat(amplituda: 1.0, czestotliwosc: 0.5, krok: 0.1, t_trwania: 10.0);

            // Tworzymy model wykresu
            var model = new PlotModel { Title = "Przebieg prostokątny" };

            // Dodajemy siatkę
            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Left,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.Gray,
                MinorGridlineColor = OxyColors.LightGray,
                Minimum = -1,
                Maximum = 1,
            });

            model.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = OxyPlot.Axes.AxisPosition.Bottom,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,
                MajorGridlineColor = OxyColors.Gray,
                MinorGridlineColor = OxyColors.LightGray,
                Minimum = 0,
                Maximum = 10,
            });

            // Tworzymy serię i dodajemy punkty
            var seriaProstokat = new LineSeries { Title = "Prostokat", Color = OxyColors.Red };

            for (int i = 0; i < trojkat.xProstokat.Count; i++)
            {
                seriaProstokat.Points.Add(new DataPoint(trojkat.xProstokat[i], trojkat.yProstokat[i]));
            }

            // Dodajemy serię do modelu
            model.Series.Add(seriaProstokat);

            RysujFFT(trojkat.yProstokat);
            // Przypisujemy model do PlotView
            plotview.Model = model;
        }


        private void RysujFFT(List<double> sygnal)
        {
            // Obliczamy FFT dla danego sygnału
            double czestotliwoscProbkowania = 1.0 / 0.1; // Zakładając, że krok to 0.1 (czas próbkowania)
            var wynikFFT = trojkat.ObliczFFT(sygnal, czestotliwoscProbkowania);

            // Tworzymy model wykresu FFT
            var modelFFT = new PlotModel { Title = "Transformata Fouriera" };
            var seriaFFT = new LineSeries { Title = "FFT", Color = OxyColors.Purple };

            for (int i = 0; i < wynikFFT.Item1.Count; i++)
            {
                seriaFFT.Points.Add(new DataPoint(wynikFFT.Item1[i], wynikFFT.Item2[i]));
            }

            modelFFT.Series.Add(seriaFFT);
            plotview_FFT.Model = modelFFT;
        }


    }
}
