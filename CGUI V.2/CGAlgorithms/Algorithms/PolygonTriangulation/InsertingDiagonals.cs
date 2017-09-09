using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms.PolygonTriangulation
{
    class InsertingDiagonals : Algorithm
    {
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
        List<Point> Ppoints = new List<Point>();
        for (int i = 0; i < polygons[0].lines.Count;i++)
        {
            Ppoints.Add(polygons[0].lines[i].Start);             
        }
        int index = getminXPointIndex(Ppoints);
           
        }

        public int getminXPointIndex(List<Point> points)
        {
            int index = -1 ;
            double x = 100000000000000000;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].X <= x)
                {
                   x = points[i].Y;
                   index = i;
                }
            } 
            return index;
        }
        //List<Line>Inserting_Diagonals (List<Point> P)
        //{
           
        //}
/*{
if (polygon.count > 3)
{
c = any convex point;
MaxDistantPoint = farthest point from line (c.prev, c.next) inside the triangle (cprev, c, cnext)
if MaxDistantPoint is null // no point exists in the triangle
outputDiagonals.Add( Line(c.prev, c.next) )
else
outputDiagonals.Add( Line(c, MaxDistantPoint) )
Polygon p1, p2;
p1 = subPolygon [ Polygon.Start -> Polygon.End) + Line (end, start)
p2 = subPolygon [ Polygon.End -> Polygon.Start) + Line (start, end)
Inserting Diagonals(p1);
Inserting Diagonals(p2);
}
}
   */     public override string ToString()
        {
            return "Inserting Diagonals";
        }
    }
}
