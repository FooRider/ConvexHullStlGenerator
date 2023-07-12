/**
 * Copyright 2023 Peter Pokojný
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy
 * of this software and associated documentation files (the "Software"), to deal
 * in the Software without restriction, including without limitation the rights
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
 * copies of the Software, and to permit persons to whom the Software is
 * furnished to do so, subject to the following conditions:
 *
 * The above copyright notice and this permission notice shall be included in
 * all copies or substantial portions of the Software.
 *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
 * SOFTWARE.
 */

#nullable enable
using STLDotNet6.Formats.StereoLithography;

namespace ConvexHullGenerator;

public record Vector3(float x, float y, float z)
{
    public float GetSize() => (float)Math.Sqrt(x * x + y * y + z * z);
    public Vector3 GetNormalized() => this / GetSize();
    public Vector3 GetOpposite() => new Vector3(-x, -y, -z);
    public Vector3 normalized {get => GetNormalized();}
    public float magnitude {get => GetSize();}
    public static Vector3 zero { get => new Vector3(0, 0, 0); }

    public override string ToString() => $"({x}, {y}, {z})";

    public static Vector3 operator -(Vector3 a, Vector3 b) => new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);
    public static Vector3 operator +(Vector3 a, Vector3 b) => new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);
    public static Vector3 operator /(Vector3 a, float x) => new Vector3(a.x / x, a.y / x, a.z / x);
    public static Vector3 operator *(Vector3 a, float x) => new Vector3(a.x * x, a.y * x, a.z * x);
    public static Vector3 operator *(float x, Vector3 a) => new Vector3(a.x * x, a.y * x, a.z * x);
    public static Vector3 Cross(Vector3 a, Vector3 b) => new Vector3(a.y * b.z - a.z * b.y, a.z * b.x - a.x * b.z, a.x * b.y - a.y * b.x);
	
    public static explicit operator Vertex(Vector3 vector) => new Vertex(vector.x, vector.y, vector.z);
}