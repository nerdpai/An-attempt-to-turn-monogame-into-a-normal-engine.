using System;
using System.Numerics;

namespace Engine.SceneElements.Components.Other;

public struct Vector2
    : IAdditionOperators<Vector2, Vector2, Vector2>,
        IMultiplyOperators<Vector2, float, Vector2>,
        IMultiplyOperators<Vector2, Vector2, Vector2>,
        ISubtractionOperators<Vector2, Vector2, Vector2>,
        IDivisionOperators<Vector2, Vector2, Vector2>,
        IDivisionOperators<Vector2, float, Vector2>,
        IEqualityOperators<Vector2, Vector2, bool>,
        IEquatable<Vector2>
{
    public float X;
    public float Y;

    public Vector2(float x, float y)
    {
        X = x;
        Y = y;
    }

    public Vector2() => X = Y = 0f;

    public static explicit operator Vector3(Vector2 v) => new(v.X, v.Y, 0);

    public static implicit operator Microsoft.Xna.Framework.Vector2(Vector2 v) => new(v.X, v.Y);

    public static Vector2 operator +(Vector2 left, Vector2 right) =>
        new(left.X + right.X, left.Y + right.Y);

    public static Vector2 operator *(Vector2 left, float right) =>
        new(left.X * right, left.Y * right);

    public static Vector2 operator -(Vector2 left, Vector2 right) =>
        new(left.X - right.X, left.Y - right.Y);

    public static Vector2 operator /(Vector2 left, Vector2 right) =>
        new(left.X / right.X, left.Y / right.Y);

    public static Vector2 operator *(Vector2 left, Vector2 right) =>
        new(left.X * right.X, left.Y * right.Y);

    public static bool operator ==(Vector2 left, Vector2 right) =>
        left.X == right.X && left.Y == right.Y;

    public static bool operator !=(Vector2 left, Vector2 right) => !(left == right);

    public static Vector2 operator /(Vector2 left, float right) =>
        new(left.X / right, left.Y / right);

    public static Angle AngleBetween(Vector2 first, Vector2 second)
    {
        double ab = first.X * second.X + first.Y * second.Y;
        double aMod = Math.Sqrt(Math.Pow(first.X, 2) + Math.Pow(first.Y, 2));
        double bMod = Math.Sqrt(Math.Pow(second.X, 2) + Math.Pow(second.Y, 2));
        return new Angle() { InRadians = (float)Math.Acos(ab / (aMod * bMod)) };
    }

    public bool Equals(Vector2 other) => this == other;
}
