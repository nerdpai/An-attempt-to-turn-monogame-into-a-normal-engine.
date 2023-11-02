using System;
using System.Numerics;

namespace Engine.SceneElements.Components.Other;

public struct Vector3
    : IAdditionOperators<Vector3, Vector3, Vector3>,
        IMultiplyOperators<Vector3, float, Vector3>,
        ISubtractionOperators<Vector3, Vector3, Vector3>,
        IEqualityOperators<Vector3, Vector3, bool>,
        IEquatable<Vector3>,
        IDivisionOperators<Vector3, float, Vector3>
{
    public float X;
    public float Y;
    public float Z;

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public Vector3() => X = Y = Z = 0f;

    public Vector3(Vector2 v, float z)
    {
        X = v.X;
        Y = v.Y;
        Z = z;
    }

    public static implicit operator Microsoft.Xna.Framework.Vector3(Vector3 v) =>
        new(v.X, v.Y, v.Z);

    public static Vector3 operator +(Vector3 left, Vector3 right) =>
        new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    public static Vector3 operator *(Vector3 left, float right) =>
        new(left.X * right, left.Y * right, left.Z * right);

    public static Vector3 operator -(Vector3 left, Vector3 right) =>
        new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    public static explicit operator Vector2(Vector3 v) => new(v.X, v.Y);

    public static bool operator ==(Vector3 left, Vector3 right) =>
        left.X == right.X && left.Y == right.Y && left.Z == right.Z;

    public static bool operator !=(Vector3 left, Vector3 right) => !(left == right);

    public static Vector3 operator /(Vector3 left, float right) =>
        new(left.X / right, left.Y / right, left.Z / right);

    public bool Equals(Vector3 other) => this == other;
}
