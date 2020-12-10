using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Common
{
    public class BDRecord
    {
        public BDRecord(BHRecord head, Byte[] data)
        {
            Header = head;
            Data = data;
        }
        public BHRecord Header { get; }
        public Byte[] Data { get; set; }
    }
}
