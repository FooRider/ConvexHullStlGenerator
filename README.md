# ConvexHullStlGenerator
A small tool that is able to generate a convex hull from arbitrary set of points and save result as a STL file.
(Currently only permutahedron is supported)

## Usage
`ConvexHullGenerator permutahedron 1 2 3 permutahedron123.stl`

## Used libraries

This program uses:
* Quickhull algorithm implementation from https://github.com/OskarSigvardsson/unity-quickhull
* STL file reader/writer library from https://www.nuget.org/packages/STLDotNet6.Formats.StereoLithography