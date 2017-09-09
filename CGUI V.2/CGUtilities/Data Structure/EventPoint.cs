using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGUtilities.Data_Structure
{
  public  class EventPoint
    {
      public Point _point;
      public CGUtilities.Enums.EventType eventType;
     public Line seg1, seg2;

     public EventPoint(Point _point, CGUtilities.Enums.EventType eventType, Line seg1 ,Line seg2)
     {
         this._point = _point;
         this.eventType = eventType;
         this.seg1 = seg1;
         this.seg2 = seg2;

     }

  }
}
