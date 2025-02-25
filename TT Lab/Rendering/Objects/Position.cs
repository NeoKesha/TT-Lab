﻿using GlmSharp;
using System;
using System.Diagnostics;
using org.ogre;
using TT_Lab.AssetData.Instance;
using TT_Lab.Assets;
using TT_Lab.ViewModels.Editors.Instance;

namespace TT_Lab.Rendering.Objects;

public class Position : ManualObject
{
    private int _layId;
    private PositionData _data;
    private Billboard _billboard;

    public Position(string name, SceneManager sceneManager, Billboard billboard, int layId, PositionData data) : base(name)
    {
        _layId = layId;
        _billboard = billboard;
        var position = new vec3(-data.Coords.X, data.Coords.Y, data.Coords.Z);
        var color = new vec4();
        System.Drawing.Color tmp = System.Drawing.Color.FromArgb(_layId * 255 / 7, 100, 200);
        color.x = tmp.R / 255.0f;
        color.y = tmp.G / 255.0f;
        color.z = tmp.B / 255.0f;
        color.w = tmp.A / 255.0f;
        _data = data;
        
        _billboard.setPosition(position.x, position.y, position.z);
        _billboard.setColour(new ColourValue(color.x, color.y, color.z));
    }
}