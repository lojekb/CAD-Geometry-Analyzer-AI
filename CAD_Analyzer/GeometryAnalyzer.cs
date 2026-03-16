using CAD_Analyzer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CAD_Analyzer
{
    public class GeometryAnalyzer
    {
        public GeometryStats CalculateStats(List<Triangle> triangles)
        {
            if (triangles == null || triangles.Count == 0)
            {
                return new GeometryStats(0, Vector3.Zero, Vector3.Zero, 0);
            }

            int triangleCount = triangles.Count;
            float totalArea = 0;
            Vector3 minBounds = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
            Vector3 maxBounds = new Vector3(float.MinValue, float.MinValue, float.MinValue);
            foreach (var triangle in triangles)
            {
                var edge1 = triangle.V2 - triangle.V1;
                var edge2 = triangle.V3 - triangle.V1;
                var crossProduct = Vector3.Cross(edge1, edge2);
                float area = crossProduct.Length() / 2;
                totalArea += area;
                minBounds = Vector3.Min(minBounds, Vector3.Min(triangle.V1, Vector3.Min(triangle.V2, triangle.V3)));
                maxBounds = Vector3.Max(maxBounds, Vector3.Max(triangle.V1, Vector3.Max(triangle.V2, triangle.V3)));
            }
            return new GeometryStats(triangleCount, minBounds, maxBounds, totalArea);
        }
    }
}
