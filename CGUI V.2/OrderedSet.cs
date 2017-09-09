using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGUtilities
{
    public class SortedPoint : IComparable
    {
        public double Theta;
        public Point P;

        public SortedPoint(Point p, double theta)
        {
            P = p;
            Theta = theta;
            if (Theta < 0) Theta += 2 * Math.PI;
        }

        public int CompareTo(object j)
        {
            SortedPoint obj = (SortedPoint)j;
            if (obj.Theta > Theta) return -1;
            if (obj.Theta < Theta) return 1;
            if (obj.P.X > P.X) return -1;
            if (obj.P.X < P.X) return 1;
            if (obj.P.Y > P.Y) return -1;
            if (obj.P.Y < P.Y) return 1;
            return 0;
        }
    }
    public class OrderedSet<T> : SortedSet<T>
    {
        public T Pre(T p)
        {
            T pre = this.LastOrDefault(el => this.Comparer.Compare(el, p) < 0);
            return pre;
        }
        public T Next(T p)
        {
            T nxt = this.FirstOrDefault(el => this.Comparer.Compare(el, p) > 0);
            return nxt;
        }
        public T PreRotational(T p)
        {
            T pre = this.LastOrDefault(el => this.Comparer.Compare(el, p) < 0);
            if (pre == null)
                pre = this.Last();
            return pre;
        }
        public T NextRotational(T p)
        {
            T nxt = this.FirstOrDefault(el => this.Comparer.Compare(el, p) > 0);
            if (nxt == null)
                nxt = this.First();
            return nxt;
        }

        public void DeleteBetween(T pre, T nxt)
        {
            if (this.Comparer.Compare(pre,nxt) <= 0)
                this.RemoveWhere(el => this.Comparer.Compare(el, pre) > 0 && this.Comparer.Compare(el, nxt) < 0);
            else
                this.RemoveWhere(el => this.Comparer.Compare(el, pre) > 0 || this.Comparer.Compare(el, nxt) < 0);
        }
    }
}
