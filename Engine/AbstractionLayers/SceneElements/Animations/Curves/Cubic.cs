using System;

namespace Engine.SceneElements.Animations.Curves;

public class Cubic : Curve
{
    private readonly double _a;
    private readonly double _b;
    private readonly double _c;
    private readonly double _d;

    private const double _cubicErrorBound = 0.001;

    public Cubic(double a, double b, double c, double d)
    {
        _a = a;
        _b = b;
        _c = c;
        _d = d;
    }

    private static double EvaluateCubic(double a, double b, double m)
    {
        //kidnaped from flutter
        // csharpier-ignore
        return 3 * a * (1 - m) * (1 - m) * m +
               3 * b * (1 - m) *           m * m +
                                           m * m * m;
    }

    protected override float EvaluateValueAtTime(double time)
    {
        //also kidnaped from flutter
        double start = 0.0;
        double end = 1.0;
        while (true)
        {
            double midpoint = (start + end) / 2;
            double estimate = EvaluateCubic(_a, _c, midpoint);
            if (Math.Abs(time - estimate) < _cubicErrorBound)
            {
                return (float)EvaluateCubic(_b, _d, midpoint);
            }
            if (estimate < time)
            {
                start = midpoint;
            }
            else
            {
                end = midpoint;
            }
        }
    }
}
