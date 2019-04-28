using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyPaperBack
{
    struct startParam
    {
        private static int n = 255;
        internal static int N { get => n; set => n = value; }

        private static int k = 0;
        internal static int K { get => k; set => k = value; }

        private static int t = (N - K) / 2;
        internal static int T { get => t; set => t = value; }
    }
}
