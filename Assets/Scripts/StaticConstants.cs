using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    public class StaticConstants
    {
        public static int UsedSpheres = 0;
        public static int SegmentsHit = 0;
        public static int SegmentsLeft = 0;
        public static void ResetConstants()
        {
            UsedSpheres = 0;
            SegmentsHit = 0;
            SegmentsLeft = 0;
        }
    }
}
