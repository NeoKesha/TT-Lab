using System;
using System.Collections.Generic;
using System.Diagnostics;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Graphics;
using TT_Lab.Services;
using TT_Lab.Services.Implementations;

namespace TT_Lab.ServiceProviders;

public static class TwinIdGeneratorServiceProvider
{
    private static Dictionary<Type, ITwinIdGeneratorService> _idGeneratorServices = new();
    
    static TwinIdGeneratorServiceProvider()
    {
        RegisterGeneratorService<Animation>();
        RegisterGeneratorService<BehaviourCommandsSequence>();
        RegisterGeneratorService<BehaviourStarter>(new TwinIdGeneratorServiceBehaviour());
        RegisterGeneratorService<GameObject>();
        RegisterGeneratorService<OGI>();
        RegisterGeneratorService<SoundEffect>();
        RegisterGeneratorService<SoundEffectEN>(GetGenerator<SoundEffect>());
        RegisterGeneratorService<SoundEffectFR>(GetGenerator<SoundEffect>());
        RegisterGeneratorService<SoundEffectGR>(GetGenerator<SoundEffect>());
        RegisterGeneratorService<SoundEffectIT>(GetGenerator<SoundEffect>());
        RegisterGeneratorService<SoundEffectJP>(GetGenerator<SoundEffect>());
        RegisterGeneratorService<SoundEffectSP>(GetGenerator<SoundEffect>());
        RegisterGeneratorService<BlendSkin>();
        RegisterGeneratorService<LodModel>();
        RegisterGeneratorService<Material>();
        RegisterGeneratorService<Mesh>();
        RegisterGeneratorService<RigidModel>();
        RegisterGeneratorService<Skin>();
        RegisterGeneratorService<Skydome>();
        RegisterGeneratorService<Texture>();
        RegisterGeneratorService<Folder>(new TwinIdGeneratorServiceFolder());
        RegisterGeneratorService<Package>(GetGenerator<Folder>());
    }

    public static void RegisterGeneratorService<T>() where T : IAsset
    {
        _idGeneratorServices.Add(typeof(T), new TwinIdGeneratorService<T>());
    }
    
    public static void RegisterGeneratorService<T>(ITwinIdGeneratorService gen) where T : IAsset
    {
        _idGeneratorServices.Add(typeof(T), gen);
    }

    public static ITwinIdGeneratorService GetGenerator<T>() where T : IAsset
    {
        return _idGeneratorServices[typeof(T)];
    }

    public static ITwinIdGeneratorService GetGenerator(Type type)
    {
        Debug.Assert(type.IsAssignableTo(typeof(IAsset)), $"{type} does not implement IAsset");
        return _idGeneratorServices[type];
    }
}