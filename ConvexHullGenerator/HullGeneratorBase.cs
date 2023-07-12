﻿/**
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

public abstract class HullGeneratorBase
{
    protected abstract IEnumerable<Vector3> GetPoints();

    protected void Generate(FileInfo outputFile)
    {
        var stlDoc = new STLDocument();
        var points = GetPoints().ToList();

        var sum = Vector3.zero;
        foreach (var point in points)
            sum += point;
        var center = sum / points.Count;
        
        var convexHullCalculator = new ConvexHullCalculator();
        var vertices = new List<Vector3>();
        var tris = new List<int>();
        var normals = new List<Vector3>();
        convexHullCalculator.GenerateHull(points, false, ref vertices, ref tris, ref normals);
        var almostFaces = FaceOperations.SplitTris(vertices, tris);

        stlDoc.AppendFacets(almostFaces.Select(f => 
            new Facet(
                Normal.FromVertex((Vertex)FaceOperations.GetNormalFacingAwayFromCenter(f.a, f.b, f.c, center)),
                new[] { (Vertex)f.a, (Vertex)f.b, (Vertex)f.c },
                attributeByteCount: 0)));
        
        stlDoc.SaveAsText(outputFile.FullName);
    }
}