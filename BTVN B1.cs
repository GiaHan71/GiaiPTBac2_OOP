// File: Program.cs
// Console app C#: Quadratic solver with inheritance from LinearEquation

using System;

class LinearEquation
{
    // biểu diễn Ax + B = 0
    protected double A;
    protected double B;

    public LinearEquation(double a = 0.0, double b = 0.0)
    {
        A = a; B = b;
    }

    public void SetCoefficients(double a, double b) { A = a; B = b; }

    public virtual string Solve()
    {
        const double eps = 1e-12;
        if (Math.Abs(A) < eps)
        {
            if (Math.Abs(B) < eps) return "Vô số nghiệm (mọi x đều thỏa).";
            else return "Vô nghiệm.";
        }
        double x = -B / A;
        return $"Nghiệm: x = {x}";
    }
}

class QuadraticEquation : LinearEquation
{
    // ax^2 + bx + c = 0
    private double a, b, c;

    public QuadraticEquation(double aa = 0.0, double bb = 0.0, double cc = 0.0)
    {
        a = aa; b = bb; c = cc;
    }

    public void SetCoefficients(double aa, double bb, double cc)
    {
        a = aa; b = bb; c = cc;
    }

    public override string Solve()
    {
        const double eps = 1e-12;
        if (Math.Abs(a) < eps)
        {
            // hạ bậc: bx + c = 0
            var lin = new LinearEquation(b, c);
            return "Đã hạ bậc vì a = 0. " + lin.Solve();
        }

        double D = b * b - 4.0 * a * c;
        if (D > eps)
        {
            double sqrtD = Math.Sqrt(D);
            double x1 = (-b + sqrtD) / (2.0 * a);
            double x2 = (-b - sqrtD) / (2.0 * a);
            return $"D = {D}. Hai nghiem phan biet: x1 = {x1}, x2 = {x2}";
        }
        else if (Math.Abs(D) <= eps)
        {
            double x = -b / (2.0 * a);
            return $"D = {D}. Nghiệm kép: x = {x}";
        }
        else
        {
            double real = -b / (2.0 * a);
            double imag = Math.Sqrt(-D) / (2.0 * a);
            return $"D = {D}. Hai nghiệm phức: x1 = {real} + {imag}i, x2 = {real} - {imag}i";
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Giai phuong trinh bac 2 ax^2 + bx + c = 0 (C# OOP)");
        Console.Write("Nhap a b c (cach nhau bang dau cach): ");
        string line = Console.ReadLine();
        string[] parts = line?.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

        if (parts == null || parts.Length < 3
            || !double.TryParse(parts[0], out double a)
            || !double.TryParse(parts[1], out double b)
            || !double.TryParse(parts[2], out double c))
        {
            Console.WriteLine("Dữ liệu không hợp lệ. Kết thúc chương trình.");
            return;
        }

        var eq = new QuadraticEquation(a, b, c);
        Console.WriteLine(eq.Solve());
    }
}
