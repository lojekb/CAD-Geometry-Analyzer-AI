using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace CAD_Analyzer.Models
{
    public record Triangle(
        Vector3 V1,
        Vector3 V2,
        Vector3 V3,
        Vector3 Normal
    );
    public record GeometryStats(
        int TriangleCount,
        Vector3 MinBounds,
        Vector3 MaxBounds,
        float TotalSurfaceArea
    );
}
