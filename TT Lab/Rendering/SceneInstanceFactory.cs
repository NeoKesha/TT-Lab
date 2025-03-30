using System;
using System.Collections.Generic;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Instance;
using TT_Lab.Rendering.Objects.SceneInstances;
using TT_Lab.ViewModels.ResourceTree;

namespace TT_Lab.Rendering;

public static class SceneInstanceFactory
{
    private static Dictionary<Type, Func<OgreWindow, EditingContext, AbstractAssetData, ResourceTreeElementViewModel, SceneInstance>> _sceneInstances = new();

    static SceneInstanceFactory()
    {
        _sceneInstances.Add(typeof(ObjectSceneInstance), CreateObjectInstance);
    }
    
    public static SceneInstance CreateSceneInstance(Type type, OgreWindow window, EditingContext editingContext, AbstractAssetData instanceData, ResourceTreeElementViewModel viewModel)
    {
        return _sceneInstances[type](window, editingContext, instanceData, viewModel);
    }

    public static SceneInstance CreateSceneInstance<T>(OgreWindow window, EditingContext editingContext, AbstractAssetData instanceData, ResourceTreeElementViewModel viewModel)
    {
        return _sceneInstances[typeof(T)](window, editingContext, instanceData, viewModel);
    }

    private static SceneInstance CreateObjectInstance(OgreWindow window, EditingContext editingContext,
        AbstractAssetData instanceData, ResourceTreeElementViewModel viewModel)
    {
        return new ObjectSceneInstance(window, editingContext, (ObjectInstanceData)instanceData, viewModel);
    }
}