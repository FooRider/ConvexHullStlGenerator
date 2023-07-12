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
using System.CommandLine;
using System.CommandLine.Invocation;
using System.CommandLine.NamingConventionBinder;

namespace ConvexHullGenerator;

public class Permutahedron : HullGeneratorBase
{
    private readonly float _x;
    private readonly float _y;
    private readonly float _z;

    public Permutahedron(float x, float y, float z)
    {
        _x = x;
        _y = y;
        _z = z;
    }
    
    protected override IEnumerable<Vector3> GetPoints()
    {
        foreach (var vectorCoords in GetAllPermutations(new List<float>() { _x, _y, _z }))
            foreach (var sx in new[] {-1, 1})
                foreach (var sy in new[] {-1, 1})
                    foreach (var sz in new[] { -1, 1 })
                        yield return new Vector3(sx * vectorCoords[0], sy * vectorCoords[1], sz * vectorCoords[2]);
    }
    
    private IEnumerable<IList<T>> AllPermutations<T>(IList<T> items)
    {
        if (items.Count() == 1)
            yield return items;
        else
            foreach (var item in items)
                foreach (var permutation in AllPermutations(items.Where(i => !i.Equals(item)).ToList()))
                    yield return new[] { item }.Concat(permutation).ToList();
    }

    private IEnumerable<IList<T>> GetAllPermutations<T>(IList<T> data)
    {
        var indices = Enumerable.Range(0, data.Count).ToList();
        foreach (var permutation in AllPermutations(indices))
            yield return permutation.Select(i => data[i]).ToList();
    }

    public static void AddToRootCommand(Command rootCommand)
    {
        var command = new Command("permutahedron", "Generate permutahedron");
        command.AddArgument(new Argument<float>("x"));
        command.AddArgument(new Argument<float>("y"));
        command.AddArgument(new Argument<float>("z"));
        command.AddArgument(new Argument<FileInfo>("outputFile"));

        command.Handler = CommandHandler.Create<float, float, float, FileInfo>(HandleCommand);
        
        rootCommand.Add(command);
    }

    private static void HandleCommand(float x, float y, float z, FileInfo outputFile)
    {
        var permutahedron = new Permutahedron(x, y, z);
        permutahedron.Generate(outputFile);
    }
}