using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGUtilities;
using  CGUtilities.DataStructures;
using CGUtilities.Data_Structure;
namespace CGAlgorithms.Algorithms.SegmentIntersection
{
   public class SweepLine:Algorithm
    {
       
        private static int CompareByLeastX(EventPoint ePoint1, EventPoint ePoint2)
        {
            int res = 0;
            if (ePoint1._point.X > ePoint2._point.X)
            { res = 1; }
            else if (ePoint1._point.X < ePoint2._point.X)
            { res = -1; }
            else if (ePoint1._point.X == ePoint2._point.X)
            {
                if (ePoint1._point.Y < ePoint2._point.Y) res = 1;
                else if (ePoint1._point.Y > ePoint2._point.Y) res = -1;
            }
            return res;
        }     
        OrderedSet<EventPoint> Q = new OrderedSet<CGUtilities.Data_Structure.EventPoint>(new Comparison<EventPoint>(CompareByLeastX));
        public static EventPoint currentEvent;
        static double Y_Interpolation(Line l, double x)
        {
            double M = (l.Start.Y - l.End.Y) / (l.Start.X - l.End.X);
            double C = l.Start.Y - l.Start.X * M;
            return (M * x) + C;
        }
        private static int CompareSegments(Line seg1, Line seg2)
        {
            double x = currentEvent._point.X;
            double seg1_y = Y_Interpolation(seg1, x);
            double seg2_y = Y_Interpolation(seg2, x);

            int res = 0;
            if (seg1_y > seg2_y)
            { res = 1; }
            else if (seg1_y < seg2_y)
            { res = -1; }
            else
            {
                double max_X = Math.Max(seg1.End.X ,seg2.End.X);
                double Yend1 = Y_Interpolation(seg1, max_X);
                double Yend2 = Y_Interpolation(seg2, max_X);
                if (Yend1 > Yend2)
                { res = 1; }
                else if (Yend1 < Yend2)
                { res = -1; }             

            }
            return res;
        }
        OrderedSet<Line> L = new OrderedSet<Line>(new Comparison<Line>(CompareSegments));
        List<Line> S;
        List<Point> intersections;
        public override void Run(List<CGUtilities.Point> points, List<CGUtilities.Line> lines, List<CGUtilities.Polygon> polygons, ref List<CGUtilities.Point> outPoints, ref List<CGUtilities.Line> outLines, ref List<CGUtilities.Polygon> outPolygons)
        {
            intersections = new List<Point>();
            S = lines;
            InitializeEvents();
            while (Q.Count != 0)
            {
                 currentEvent = Q[0];
                 HandleEvent();
              Q.RemoveFirst();
            }
            outPoints = new List<Point>(intersections);
        }
        void InitializeEvents()
        {
            for (int i = 0; i < S.Count; i++)
                {
                    if ( S[i].End.X < S[i].Start.X )
                    {
                       Point tmp = S[i].Start;
                        S[i].Start = S[i].End;
                        S[i].End = tmp;
                    }
                    Q.Add(new EventPoint(S[i].Start, CGUtilities.Enums.EventType.Start, S[i], S[i]));
                    Q.Add(new EventPoint(S[i].End, CGUtilities.Enums.EventType.End, S[i], S[i]));
                }
        }     
        void HandleEvent()
        {
            KeyValuePair<Line, Line> UpperAndLower = L.DirectUpperAndLower(currentEvent.seg1);
            
            if (currentEvent.eventType == Enums.EventType.Start)
            {
                Line newSegment =currentEvent.seg1;
                L.Add(newSegment);
                
                if (UpperAndLower.Key != null)
                {
                    Point intersectionPoint = CheckIntersection(newSegment, UpperAndLower.Key);
                    if (intersectionPoint != null && intersectionPoint.X > currentEvent._point.X)
                    {
                        EventPoint newEvent = new EventPoint(intersectionPoint, Enums.EventType.Intersection, UpperAndLower.Key, newSegment);
                        if (!Q.Contains(newEvent))
                            Q.Add(newEvent);
                    }
                }
                if (UpperAndLower.Value != null)
                {
                    Point intersectionPoint = CheckIntersection(newSegment, UpperAndLower.Value);
                    if (intersectionPoint != null && intersectionPoint.X > currentEvent._point.X)
                    {
                        EventPoint newEvent = new EventPoint(intersectionPoint, Enums.EventType.Intersection, newSegment, UpperAndLower.Value);
                        if (!Q.Contains(newEvent))
                            Q.Add(newEvent);
                    }
                }
               
            }
            else if (currentEvent.eventType == Enums.EventType.End)
            {
                if (UpperAndLower.Key != null && UpperAndLower.Value != null)
                {
                    Point intersectionPoint = CheckIntersection(UpperAndLower.Key,UpperAndLower.Value);
                    if (intersectionPoint != null && intersectionPoint.X > currentEvent._point.X)
                    {                       
                        EventPoint newEvent =new EventPoint(intersectionPoint, Enums.EventType.Intersection, UpperAndLower.Key, UpperAndLower.Value) ;
                        if (!Q.Contains(newEvent))
                            Q.Add(newEvent);
                    }
                }
                L.Remove(currentEvent.seg1);
            }
            else if (currentEvent.eventType == Enums.EventType.Intersection)
            {
                intersections.Add(currentEvent._point);
                Line S1below = L.DirectUpperAndLower(currentEvent.seg1).Key;
                Line S2upper = L.DirectUpperAndLower(currentEvent.seg2).Value;
                if (S1below != null )
                {
                    Point intersectionPoint = CheckIntersection(S1below, currentEvent.seg2);
                    if (intersectionPoint != null && intersectionPoint.X > currentEvent._point.X)
                    {                             
                        EventPoint newEvent = new EventPoint(intersectionPoint, Enums.EventType.Intersection, S1below, currentEvent.seg2);
                        if(!Q.Contains(newEvent))
                           Q.Add(newEvent);
                    }
                }
                if (S2upper != null)
                {
                    Point intersectionPoint = CheckIntersection(S2upper, currentEvent.seg1);
                    if (intersectionPoint != null && intersectionPoint.X > currentEvent._point.X)
                    {                                       
                          EventPoint newEvent =new EventPoint(intersectionPoint, Enums.EventType.Intersection,S2upper, currentEvent.seg1);
                          if (!Q.Contains(newEvent))
                              Q.Add(newEvent);            
                    }
                }
                L.Remove(currentEvent.seg1); L.Remove(currentEvent.seg2);
                //currentEvent._point.X++;
                L.Add(currentEvent.seg1); L.Add(currentEvent.seg2);
                //currentEvent._point.X--;
                // L.Remove(currentEvent.seg1);
                // L.Remove(currentEvent.seg2);
                // L.Add(new Line(currentEvent._point,currentEvent.seg1.End));
                // L.Add(new Line(currentEvent._point, currentEvent.seg2.End));
            }

        }
        Point CheckIntersection(Line L1,Line L2)
        {
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
                    return new Point(X, Y);
            }
            return null;
        }
        public override string ToString()
        {
            return "Sweep Line";
        }
    }
}
