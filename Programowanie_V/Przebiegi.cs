using System;
using System.Collections.Generic;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;



namespace Programowanie_V
{
    internal class Przebiegi
    {
        public List<double> xTrojkat { get; set; }
        public List<double> yTrojkat { get; set; }
        public List<double> xSin { get; set; }
        public List<double> ySin { get; set; }
        public List<double> xProstokat { get; set; }
        public List<double> yProstokat { get; set; }

        public Przebiegi()
        {
            xTrojkat = new List<double>();
            yTrojkat = new List<double>();

            xSin = new List<double>();
            ySin = new List<double>();

            xProstokat = new List<double>();
            yProstokat = new List<double>();
        }

        public void Generuj_Trojkat(double amplituda, double okres, double krok, double t_trwania)
        {
            xTrojkat.Clear();
            yTrojkat.Clear();

            for (double t = 0; t <= t_trwania; t += krok)
            {
                double y = amplituda * (4 * Math.Abs((t / okres) % 1 - 0.5) - 1);
                xTrojkat.Add(t);
                yTrojkat.Add(y);
            }
        }

        public void Generuj_Sinus(double amplituda, double czestotliwosc, double krok, double t_trwania)
        {
            xSin.Clear();
            ySin.Clear();

            for (double t = 0; t <= t_trwania; t += krok)
            {
                double y = amplituda * Math.Sin(2 * Math.PI * czestotliwosc * t);
                xSin.Add(t);
                ySin.Add(y);
            }
        }

        public void Generuj_Prostokat(double amplituda, double czestotliwosc, double krok, double t_trwania)
        {
            xProstokat.Clear();
            yProstokat.Clear();

            for (double t = 0; t <= t_trwania; t += krok)
            {
                double y = amplituda * Math.Sign(Math.Sin(2 * Math.PI * czestotliwosc * t));
                xProstokat.Add(t);
                yProstokat.Add(y);
            }
        }

        public (List<double>, List<double>) ObliczFFT(List<double> sygnal, double czestotliwoscProbkowania)
        {
            int N = sygnal.Count;
            Complex[] daneFFT = new Complex[N];
            for (int i = 0; i < N; i++)
            {
                daneFFT[i] = new Complex(sygnal[i], 0);
            }

            Fourier.Forward(daneFFT, FourierOptions.Matlab);

            List<double> amplitudy = new List<double>();
            List<double> czestotliwosci = new List<double>();

            for (int i = 0; i < N / 2; i++) // Analizujemy tylko pierwszą połowę widma
            {
                double freq = (double)i * czestotliwoscProbkowania / N;
                double amp = daneFFT[i].Magnitude;

                czestotliwosci.Add(freq);
                amplitudy.Add(amp);
            }

            return (czestotliwosci, amplitudy);
        }
    }
}
