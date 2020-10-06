using System;
using System.Globalization;

namespace Lab2
{
    class Program
    {
        private static Func<double, double> func;
        private static double infelicity;
        static void Main(string[] args)
        {
            var a = ReadNumber("Введите левую границу");
            var b = ReadNumber("Введите правую граинцу");
            var c1 = ReadNumber("Введите первый коэффициент");
            var c2 = ReadNumber("Введите второй коэффциент");
            var c3 = ReadNumber("Введите третий коэффциент");
            var c4 = ReadNumber("Введите четвертый коэффциент");
            var c5 = ReadNumber("Введите пятый коэффициент");
            infelicity = ReadNumber("Введите точность");
            func = x => c1*Math.Pow(c2*x + c3, c4) - c5 / x;
            Func<double,double> rounder = x => Math.Round(x, (int)Math.Log10(Math.Round(1/infelicity)));
            Console.WriteLine("Метод деления отрезка пополам {0}", rounder(HalfCut(a, b)));
            Console.WriteLine("Метод Ньютона {0}", rounder(Newton(b)));
        }

        private static double ReadNumber(string s)
        {
            Console.WriteLine(s);
            double.TryParse(Console.ReadLine()?.Replace(',','.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var c);
            return c;
        }

        private static double HalfCut(double a, double b)
        {
            var x0 = (a + b) / 2;
            var fA = func(a);
            var fX0 = func(x0);
            var fB = func(b);
            if (Math.Abs(b - a) < infelicity) return x0;
            if (fA * fX0 < 0)
                return HalfCut(a, x0);
            if (fX0 * fB < 0)
                return HalfCut(x0, b);
            return x0;
        }
        public static double Newton(double currentValue)
        {
            var newValue = currentValue - func(currentValue) / Deriviate(currentValue);
            return Math.Abs(newValue - currentValue) < infelicity
                ? newValue
                : Newton(newValue);
        }

        private static double Deriviate(double x)
        {
            const double dx = 1E-12;
            return (func(x + dx) - func(x)) / dx;
        }
    }
}
