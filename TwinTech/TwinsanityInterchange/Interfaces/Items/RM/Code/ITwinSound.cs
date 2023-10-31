using System;

namespace Twinsanity.TwinsanityInterchange.Interfaces.Items.RM.Code
{
    /// <summary>
    /// Generic Twinsanity's sound effect interface.
    /// However implementations are completely different for PS2 and XBOX.
    /// <para></para>
    /// PS2 implementation <seealso cref="Implementations.PS2.Items.RM2.Code.PS2AnySound"/>
    /// <para></para>
    /// XBox implementation <seealso cref="Implementations.Xbox.Items.RMX.Code.XboxAnySound"/>
    /// </summary>
    public interface ITwinSound : ITwinItem
    {
        /// <summary>
        /// Header value
        /// </summary>
        UInt32 Header { get; set; }
        /// <summary>
        /// Unknown flag
        /// </summary>
        Byte UnkFlag { get; set; }
        /// <summary>
        /// Frequence factor.
        /// </summary>
        Byte FreqFac { get; set; }
        /// <summary>
        /// Unknown param1
        /// </summary>
        UInt16 Param1 { get; set; }
        /// <summary>
        /// Unknown param2
        /// </summary>
        UInt16 Param2 { get; set; }
        /// <summary>
        /// Unknown param3
        /// </summary>
        UInt16 Param3 { get; set; }
        /// <summary>
        /// Unknown param4
        /// </summary>
        UInt16 Param4 { get; set; }
        /// <summary>
        /// Raw sound data
        /// </summary>
        Byte[] Sound { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Frequence of the sound</returns>
        UInt16 GetFreq();
        /// <summary>
        /// Converts raw sound data to its PCM format
        /// </summary>
        /// <returns></returns>
        Byte[] ToPCM();
    }
}
