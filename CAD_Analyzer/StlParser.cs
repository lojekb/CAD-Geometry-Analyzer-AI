using CAD_Analyzer.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CAD_Analyzer
{
    public class StlParser
    {
        public List<Triangle> ParseAscii(string filePath)
        {
            var triangles = new List<Triangle>();
            var currentVertices = new List<Vector3>();
            Vector3 currentNormal = Vector3.Zero;

            foreach (var line in File.ReadLines(filePath))
            {
                string trimmed = line.Trim().ToLower();

                if (trimmed.StartsWith("facet normal"))
                {
                    currentNormal = ParseVector(trimmed.Replace("facet normal", ""));
                }
                else if (trimmed.StartsWith("vertex"))
                {
                    currentVertices.Add(ParseVector(trimmed.Replace("vertex", "")));
                }
                else if (trimmed.EndsWith("endfacet"))
                {
                    if (currentVertices.Count == 3)
                    {
                        triangles.Add(new Triangle(
                            currentVertices[0],
                            currentVertices[1],
                            currentVertices[2],
                            currentNormal));
                    }
                    currentVertices.Clear();
                }
            }
            return triangles;
        }
        private Vector3 ParseVector(string data)
        {
            var parts = data.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return new Vector3(
                float.Parse(parts[0], CultureInfo.InvariantCulture),
                float.Parse(parts[1], CultureInfo.InvariantCulture),
                float.Parse(parts[2], CultureInfo.InvariantCulture)
            );
        }
    }
}
