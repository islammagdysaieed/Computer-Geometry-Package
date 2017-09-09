using CGUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGAlgorithms.Algorithms.ConvexHull
{
    public class ExtremeSegments : Algorithm
    {
        public override void Run(List<Point> points, List<Line> lines, List<Polygon> polygons, ref List<Point> outPoints, ref List<Line> outLines, ref List<Polygon> outPolygons)
        {
            if (points.Count == 1)
            {
                outPoints.Add(points[0]);
                return;
            }
            for (int i = 0; i < points.Count-1; i++)
            {
                for (int j = i+1; j < points.Count; j++)
                {
                   // if (i == j) continue;
                    Line myline = new Line(points[i],points[j]);
                    bool left = false, right = false ,subSegment = false ;
                    for (int k = 0; k< points.Count; k++)
                    {
                        if (points[k] == myline.Start || points[k] == myline.End) continue;
                           
                        if (HelperMethods.CheckTurn(myline,points[k])== CGUtilities.Enums.TurnType.Left) 
                        {
                            left = true;
                        }
                        else if (HelperMethods.CheckTurn(myline, points[k]) == CGUtilities.Enums.TurnType.Right)
                        {
                            right = true;
                        }
                        else if (HelperMethods.CheckTurn(myline, points[k]) == CGUtilities.Enums.TurnType.Colinear)
                        {
                            double D1 = HelperMethods.EuclideanDistance(myline.Start, myline.End);
                            double D2 = HelperMethods.EuclideanDistance(myline.Start, points[k]);
                            double D3 = HelperMethods.EuclideanDistance(myline.End, points[k]);
                            double Dmax = Math.Max(D2, D3);
                            if (Dmax > D1)
                                subSegment = true;
                        }

                        if (left && right)
                            break;
                    }
                    if (((!left && right) || (left && !right) || (!left && !right))&& !subSegment)
                    {
                        bool startfound = false, endfound = false; 
                        for (int x = 0; x < outPoints.Count; x++)
                        {
                            if (outPoints[x] == myline.Start)
                                startfound = true;
                            if (outPoints[x] == myline.End)
                                endfound = true;
                        }
                        if (!startfound)
                            outPoints.Add(myline.Start);
                        if (!endfound)
                            outPoints.Add(myline.End);
                        outLines.Add(myline);
                    
                    }
                }
            }

        }

        

        public override string ToString()
        {
            return "Convex Hull - Extreme Segments";
        }
    }
}
