using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class JarvisMarch : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            int index = -1 ;
            double y = 100000000000000000,x = 100000000000000000;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y <= y)
                {
                   y = points[i].Y;
                        index = i;
                }
            }       
           
            Point supportPoint = new Point(points[index].X + 10, points[index].Y);
            Point basePoint = points[index], fristPoint = points[index];        
            while (basePoint != fristPoint || outPoints.Count == 0)
            {
                outPoints.Add(basePoint);
                Point V1 = basePoint.Vector(supportPoint);
                double MinAngel = 10000000000000, MaxDistance = -1000000000000000000;
                index = -1;
                for (int j = 0; j < points.Count; j++)
                {
                    if (basePoint == points[j] || supportPoint== points[j]) continue;
                    Point V2 = basePoint.Vector(points[j]);
                    double cross = HelperMethods.CrossProduct(V1, V2);
                    double dot = HelperMethods.DotProduct(V1,V2);
                    double angel = Math.Atan2(cross, dot) * 180 / Math.PI;
                   if(angel < 0)
                        angel+=360;
                    if (angel < MinAngel)
                    {
                        MinAngel = angel;
                        index = j;
                        MaxDistance = HelperMethods.EuclideanDistance(basePoint, points[j]);
                    }
                    //else if (angel == MinAngel)
                    //{
                    //    double colinear_Distance = EuclideanDistance(basePoint, points[j]);
                    //    if (colinear_Distance > MaxDistance)
                    //    {
                    //        index = j;
                    //        MaxDistance = colinear_Distance;
                    //    }
                    //}
                }
                supportPoint = basePoint;
                if(index!=-1)
                basePoint = points[index];
            }
        }
        public override string ToString()
        {
            return "Convex Hull - Jarvis March";
        }
    }
}
