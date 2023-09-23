﻿using System;
using System.IO;
using Twinsanity.TwinsanityInterchange.Interfaces;
using static Twinsanity.TwinsanityInterchange.Common.Animation.Enums;

namespace Twinsanity.TwinsanityInterchange.Common.Animation
{
    public class JointSettings : ITwinSerializable
    {
        UInt16 flags;
        public Byte FacialShapesAmount { get; set; }
        public Boolean UnusedFlag { get; set; }
        public Boolean UseAdditionalRotation { get; set; }

        UInt16 transformationChoice;
        public TransformType TranslateX { get; set; }
        public TransformType TranslateY { get; set; }
        public TransformType TranslateZ { get; set; }
        public TransformType RotateX { get; set; }
        public TransformType RotateY { get; set; }
        public TransformType RotateZ { get; set; }
        public TransformType ScaleX { get; set; }
        public TransformType ScaleY { get; set; }
        public TransformType ScaleZ { get; set; }

        public UInt16 TransformationIndex { get; set; }
        public UInt16 AnimationTransformationIndex { get; set; }


        public Int32 GetLength()
        {
            return 4 * 2;
        }

        public void Read(BinaryReader reader, Int32 length)
        {
            flags = reader.ReadUInt16();
            {
                FacialShapesAmount = (Byte)(flags >> 0x8 & 0xF);
                UnusedFlag = (flags >> 0xD & 0x1) != 0;
                UseAdditionalRotation = (flags >> 0xC & 0x1) != 0;
            }
            transformationChoice = reader.ReadUInt16();
            {
                TranslateX = (TransformType)(transformationChoice & 0x1);
                TranslateY = (TransformType)(transformationChoice & 0x2);
                TranslateZ = (TransformType)(transformationChoice & 0x4);
                RotateX = (TransformType)(transformationChoice & 0x8);
                RotateY = (TransformType)(transformationChoice & 0x10);
                RotateZ = (TransformType)(transformationChoice & 0x20);
                ScaleX = (TransformType)(transformationChoice & 0x40);
                ScaleY = (TransformType)(transformationChoice & 0x80);
                ScaleZ = (TransformType)(transformationChoice & 0x100);
            }
            TransformationIndex = reader.ReadUInt16();
            AnimationTransformationIndex = reader.ReadUInt16();
        }

        public void Write(BinaryWriter writer)
        {
            UInt16 newFlags = (UInt16)(FacialShapesAmount << 0x8);
            var hasUnusedFlag = UnusedFlag ? 1 : 0;
            newFlags |= (UInt16)(hasUnusedFlag << 0xD);
            var hasUseAdditionalRotation = UseAdditionalRotation ? 1 : 0;
            newFlags |= (UInt16)(hasUseAdditionalRotation << 0xC);
            flags = newFlags;
            writer.Write(flags);

            UInt16 newTransformationChoice = (UInt16)TranslateX;
            newTransformationChoice |= (UInt16)((UInt16)TranslateY << 0x1);
            newTransformationChoice |= (UInt16)((UInt16)TranslateY << 0x2);
            newTransformationChoice |= (UInt16)((UInt16)TranslateZ << 0x3);
            newTransformationChoice |= (UInt16)((UInt16)RotateX << 0x4);
            newTransformationChoice |= (UInt16)((UInt16)RotateY << 0x5);
            newTransformationChoice |= (UInt16)((UInt16)RotateZ << 0x6);
            newTransformationChoice |= (UInt16)((UInt16)ScaleX << 0x7);
            newTransformationChoice |= (UInt16)((UInt16)ScaleY << 0x8);
            newTransformationChoice |= (UInt16)((UInt16)ScaleZ << 0x9);
            transformationChoice = newTransformationChoice;
            writer.Write(transformationChoice);

            writer.Write(TransformationIndex);
            writer.Write(AnimationTransformationIndex);
        }
    }
}
