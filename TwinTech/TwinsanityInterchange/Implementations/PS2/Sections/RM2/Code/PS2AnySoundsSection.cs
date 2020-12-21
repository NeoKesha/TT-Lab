using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twinsanity.TwinsanityInterchange.Enumerations;
using Twinsanity.TwinsanityInterchange.Implementations.Base;
using Twinsanity.TwinsanityInterchange.Implementations.PS2.Items.RM2.Code;
using Twinsanity.TwinsanityInterchange.Interfaces;

namespace Twinsanity.TwinsanityInterchange.Implementations.PS2.Sections.RM2.Code
{
    public class PS2AnySoundsSection : BaseTwinSection
    {
        public PS2AnySoundsSection() : base()
        {
            defaultType = typeof(PS2AnySound);
        }
        public override ITwinItem GetItem(Int32 index)
        {
            if (index >= Items.Count) return null;
            ITwinItem twinItem = Items[index];
            LoadItem(twinItem);
            return twinItem;
        }

        public new T GetItem<T>(uint id) where T : ITwinItem
        {
            ITwinItem twinItem = Items.Where(item => item.GetID() == id).FirstOrDefault();
            LoadItem(twinItem);
            return (T)twinItem;
        }
        private void LoadItem(ITwinItem twinItem)
        {
            if (twinItem != null && !twinItem.GetIsLoaded() && isLazy)
            {
                foreach (PS2AnySound item in Items)
                {
                    if (!item.GetIsLoaded())
                    {
                        BinaryReader reader = new BinaryReader(root.GetStream());
                        reader.BaseStream.Position = item.GetOriginalOffset();
                        item.Read(reader, item.GetOriginalSize());
                        item.SetIsLoaded(true);
                    }
                }
                var offset = 0;
                foreach (PS2AnySound item in Items)
                {
                    Array.Copy(extraData, offset, item.Sound, 0, item.Sound.Length);
                    offset += item.Sound.Length;
                }
            }
        }
        public override void Read(BinaryReader reader, Int32 length)
        {
            base.Read(reader, length);
            var offset = 0;
            foreach (PS2AnySound item in Items)
            {
                if (!isLazy)
                {
                    Array.Copy(extraData, offset, item.Sound, 0, item.Sound.Length);
                    offset += item.Sound.Length;
                }
            }
        }

        protected override void PreprocessWrite()
        {
            using (var newExtraData = new MemoryStream())
            {
                var offset = 0;
                foreach (PS2AnySound item in Items)
                {
                    LoadItem(item);
                    newExtraData.Write(item.Sound, 0, item.Sound.Length);
                    item.offset = offset;
                    offset += item.Sound.Length;
                }
                extraData = newExtraData.ToArray();
            }
        }
    }
}
