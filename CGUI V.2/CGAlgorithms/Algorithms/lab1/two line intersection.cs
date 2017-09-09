using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
namespace CGAlgorithms.Algorithms.lab1
{
    class two_line_intersection: Algorithm
    {
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            for (int i = 0; i < lines.Count; i++)
            {
                Line L1 = lines[i];
                for (int j = 0; j < lines.Count; j++)
                {
                   
                    Line L2 = lines[j];                    
                    double M1 = (L1.Start.Y - L1.End.Y) / (L1.Start.X - L1.End.X);
                    double M2 = (L2.Start.Y - L2.End.Y) / (L2.Start.X - L2.End.X);
                    double C1 = L1.Start.Y - L1.Start.X * M1;
                    double C2 = L2.Start.Y - L2.Start.X * M2;
                    if (M1 - M2 != 0)
                    {
                        double X = (C2 - C1) / (M1 - M2);
                        double Y = M1 * X + C1;

                        if (HelperMethods.CheckTurn(L1, L2.Start) != HelperMethods.CheckTurn(L1, L2.End) &&
                        HelperMethods.CheckTurn(L2, L1.Start) != HelperMethods.CheckTurn(L2, L1.End))
                            outPoints.Add(new Point(X, Y));
                    }
 
                }
            }
        }

        public override string ToString()
        {
            return " Two lines intersection";
        }

    }
}
