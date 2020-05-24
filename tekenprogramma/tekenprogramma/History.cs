﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tekenprogramma
{
    //History singleton
    class History
    {
        private static readonly History Instance = new History();
        public List<Snapshot> timeline;

        private History() { }

        public static History GetInstance()
        {
            if (Instance.timeline == null)
            {
                Instance.timeline = new List<Snapshot>();
            }
            return Instance;
        }
    }
}