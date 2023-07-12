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
namespace ConvexHullGenerator;

public class FaceOperations
{
    public static IEnumerable<(Vector3 a, Vector3 b, Vector3 c)> SplitTris(IList<Vector3> vectors, IList<int> tris)
    {
        using var enumerator = tris.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var a = vectors[enumerator.Current];
            enumerator.MoveNext();
            var b = vectors[enumerator.Current];
            enumerator.MoveNext();
            var c = vectors[enumerator.Current];
            yield return (a, b, c);
        }
    }

    public static Vector3 GetNormal(Vector3 a, Vector3 b, Vector3 c) => Vector3.Cross(b - a, c - a);

    public static Vector3 GetNormalFacingAwayFromCenter(Vector3 a, Vector3 b, Vector3 c, Vector3 center)
    {
        var adept1 = GetNormal(a, b, c);
        var adept2 = adept1.GetOpposite();

        var sum1 = (a - center + adept1).GetSize() + (b - center + adept1).GetSize() + (c - center + adept1).GetSize();
        var sum2 = (a - center + adept2).GetSize() + (b - center + adept2).GetSize() + (c - center + adept2).GetSize();

        if (sum1 > sum2)
            return adept1;
        return adept2;
    }
}