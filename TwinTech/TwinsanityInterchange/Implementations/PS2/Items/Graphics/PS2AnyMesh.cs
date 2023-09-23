using System;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.Graphics
{
    public class PS2AnyMesh : PS2AnyRigidModel
    {
        public override String GetName()
        {
            return $"Mesh {id:X}";
        }
    }
}
