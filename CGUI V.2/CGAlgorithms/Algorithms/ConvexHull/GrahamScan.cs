using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class GrahamScan : Algorithm
    {
        private Point basePoint;
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            List<Point> grahamPoints = new List<Point>(points);
            List<Point> ConvexPoints = new List<Point>();
            int baseIndex = getminYPointIndex(grahamPoints);
            basePoint = grahamPoints[baseIndex];
            grahamPoints.RemoveAt(baseIndex);       
            ConvexPoints.Add(basePoint);
            if (grahamPoints.Count == 0) {outPoints.Add(basePoint);return;}
            if (grahamPoints.Count == 1)
            {
                outPoints.Add(basePoint);
                if(grahamPoints[0]!=basePoint)
                  outPoints.Add(grahamPoints[0]);
                return; 
            }
            grahamPoints.Sort(CompareByAngle);
            ConvexPoints.Add(grahamPoints[0]);
            grahamPoints.Remove(grahamPoints[0]);
            
            while(grahamPoints.Count > 0 )
            {
                if (ConvexPoints.Count == 1)
                {
                   // if(!Co//////////////////////nvexPoints.Co////////////ntains(grahamPoints[0]))
                    ConvexPoints.Add(grahamPoints[0]);
                    grahamPoints.Remove(grahamPoints[0]);
                    continue;
                }
                Point Ptop = ConvexPoints[ConvexPoints.Count - 1];
                Point Pprev = ConvexPoints[ConvexPoints.Count - 2];
              
                Point Pi = grahamPoints[0];               
                if (HelperMethods.CheckTurn(new Line(Pprev, Ptop), Pi) == Enums.TurnType.Left)
                {
                    ConvexPoints.Add(Pi);
                    grahamPoints.Remove(Pi);
                }
                else
                    ConvexPoints.RemoveAt(ConvexPoints.Count - 1);
            }
            outPoints = new List<Point>(ConvexPoints);

        }
        public int getminYPointIndex(List<Point> points)
        {
            int index = -1;
            double y = 100000000000000000;
            for (int i = 0; i < points.Count; i++)
            {
                if (points[i].Y <= y)
                {
                    y = points[i].Y;
                    index = i;
                }
            }
            return index;
        }
        public double CalculateAngel(Point vec1, Point vec2)
        {
            double cross = HelperMethods.CrossProduct(vec1, vec2);
            double dot = HelperMethods.DotProduct(vec1, vec2);
            double seta = Math.Atan2(cross, dot) * 180 / Math.PI;
            if (seta < 0)
                seta += 360;
            return seta;
        }
        private int CompareByAngle(Point point1  , Point point2) 
        {
            Point supportPoint = new Point(basePoint.X + 7, basePoint.Y);
            Point supportVector =  basePoint.Vector(supportPoint);
            Point V1 = basePoint.Vector(point1);
            Point V2 = basePoint.Vector(point2);
            double seta1 = CalculateAngel(supportVector,V1);
            double seta2 = CalculateAngel(supportVector, V2);
            int res = 0;
            if (seta1 > seta2)
            {     res =  1;}
            else if (seta1 < seta2)
            {   res =  -1;}
            else if (seta1 == seta2)
            {
                double D1 = HelperMethods.EuclideanDistance(basePoint, point1);
                double D2 = HelperMethods.EuclideanDistance(basePoint, point2);
                if (D1 >= D2) res = 1;
                else  res = -1;
            }
            return res;
        }
        public override string ToString()
        {
            return "Convex Hull - Graham Scan";
        }
    }
}
