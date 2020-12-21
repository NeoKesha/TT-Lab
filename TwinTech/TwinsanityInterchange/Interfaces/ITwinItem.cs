using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Twinsanity.TwinsanityInterchange.Interfaces
{
    public interface ITwinItem : ITwinSerializable
    {
        /**
         * Get item's ID
         */
        UInt32 GetID();
        /**
         * Set item's ID
         */
        void SetID(UInt32 id);
        /**
         * Get item's name
         */
        String GetName();

        bool GetIsLoaded();
        void SetIsLazy(bool isLazy);
        ITwinItem GetRoot();
        void SetRoot(ITwinItem root);
        MemoryStream GetStream();
        void SetStream(MemoryStream stream);
    }
}
