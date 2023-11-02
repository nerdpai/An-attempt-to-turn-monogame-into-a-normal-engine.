using System;
using System.Numerics;

namespace Engine.SceneElements.Components.Other;

/// <summary>
/// Structure <c>Size</c> represent a size of object
/// </summary>
public struct Size
    : IAdditionOperators<Size, Size, Size>,
        IMultiplyOperators<Size, float, Size>,
        IMultiplyOperators<Size, Vector2, Size>,
        ISubtractionOperators<Size, Size, Size>,
        ISubtractionOperators<Size, float, Size>,
        IDivisionOperators<Size, float, Size>,
        IEqualityOperators<Size, Size, bool>,
        IEquatable<Size>
{
    /// <summary>
    /// Constructor that sets the <c>height</c> and <c>width</c> as 0
    /// </summary>
    public Size()
    {
        Width = 0;
        Height = 0;
    }

    /// <value>
    /// Property <c>Height</c> represent a height
    /// </value>
    /// <remarks>
    /// should be >= 0
    /// </remarks>
    public float Height { readonly get; set; }

    /// <value>
    /// Property <c>Width</c> represent a width
    /// </value>
    /// <remarks>
    /// should be >= 0
    /// </remarks>
    public float Width { readonly get; set; }

    public static implicit operator Vector2(Size size) => new(size.Width, size.Height);

    public static Size operator /(Size left, Size right) =>
        new() { Width = left.Width / right.Width, Height = left.Height / right.Height };

    public static Size operator *(Size left, Size right) =>
        new() { Width = left.Width * right.Width, Height = left.Height * right.Height };

    public static Size operator +(Size left, Size right) =>
        new() { Width = left.Width + right.Width, Height = left.Height + right.Height };

    public static Size operator -(Size left, Size right) =>
        new() { Width = left.Width - right.Width, Height = left.Height - right.Height };

    public static Size operator *(Size left, float right) =>
        new() { Width = left.Width * right, Height = left.Height * right };

    public static Size operator *(Size left, Vector2 right) =>
        new() { Width = left.Width * right.X, Height = left.Height * right.Y };

    public static Size operator /(Size left, float right) =>
        new() { Width = left.Width / right, Height = left.Height / right };

    public static bool operator ==(Size left, Size right) =>
        left.Width == right.Width && left.Height == right.Height;

    public static bool operator !=(Size left, Size right) => !(left == right);

    public static Size operator -(Size left, float right) =>
        new() { Width = left.Width - right, Height = left.Height - right };

    public bool Equals(Size other) => this == other;
}
