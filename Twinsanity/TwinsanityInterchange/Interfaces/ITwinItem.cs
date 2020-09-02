using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinItem : ITwinSerializeable
    {
        /**
         * Get item's ID
         */
        UInt32 GetID();
        /**
         * Set item's ID
         */
        void SetID(UInt32 id);
    }
}
