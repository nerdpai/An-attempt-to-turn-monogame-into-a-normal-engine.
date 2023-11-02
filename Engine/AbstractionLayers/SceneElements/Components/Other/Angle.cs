using System;
using System.Numerics;

namespace Engine.SceneElements.Components.Other;

/// <summary>
/// Structure <c>Angle</c> represent a angle in range [0 - 360) degree
/// </summary>
public struct Angle
    : IAdditionOperators<Angle, Angle, Angle>,
        IMultiplyOperators<Angle, float, Angle>,
        ISubtractionOperators<Angle, Angle, Angle>,
        IEqualityOperators<Angle, Angle, bool>,
        IEquatable<Angle>
{
    private float _angleInRadians;

    /// <summary>
    /// Constructor that sets the <c>angle</c> as 0
    /// </summary>
    public Angle() => _angleInRadians = 0;

    /// <value>
    /// Property <c>InRadians</c> represent a angle in range [0 - 2*PI) radians
    /// </value>
    /// <remarks>
    /// have overflow check, so:
    /// 3PI == PI,
    /// 2PI == 0
    /// </remarks>
    public float InRadians
    {
        readonly get => _angleInRadians;
        set
        {
            float temp = (float)(value % (2 * Math.PI));
            _angleInRadians = temp < 0 ? ((float)Math.PI * 2) + temp : temp;
        }
    }

    /// <value>
    /// Property <c>InDegree</c> represent a angle in range [0 - 360) degree
    /// </value>
    /// <remarks>
    /// have overflow check, so:
    /// 540 == 180,
    /// 360 == 0
    /// </remarks>
    public float InDegree
    {
        readonly get => (float)(180.0 / Math.PI) * _angleInRadians;
        set => InRadians = (float)(180.0 / Math.PI) * value;
    }

    public bool Equals(Angle other) => _angleInRadians == other._angleInRadians;

    public static Angle operator +(Angle left, Angle right) =>
        new() { InRadians = left.InRadians + right.InRadians };

    public static Angle operator -(Angle left, Angle right) =>
        new() { InRadians = left.InRadians - right.InRadians };

    public static Angle operator *(Angle left, float right) =>
        new() { InRadians = left.InRadians * right };

    public static bool operator ==(Angle left, Angle right) =>
        left._angleInRadians == right._angleInRadians;

    public static bool operator !=(Angle left, Angle right) =>
        left._angleInRadians != right._angleInRadians;
}
