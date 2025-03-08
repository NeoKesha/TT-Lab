using System;
using System.IO;
using System.Threading.Tasks;
using Caliburn.Micro;
using TT_Lab.AssetData;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Assets.Code;
using TT_Lab.Assets.Graphics;
using TT_Lab.Assets.Instance;
using TT_Lab.Models;
using TT_Lab.Util;
using Twinsanity.Libraries;
using Path = TT_Lab.Assets.Instance.Path;

namespace TT_Lab.ViewModels.ResourceTree;

// TODO: Currently we can't determine if the folder belongs to a chunk.
// TODO: Need to fix that for proper context menus
public class FolderElementViewModel : ResourceTreeElementViewModel
{
    public FolderElementViewModel(LabURI asset, ResourceTreeElementViewModel? parent = null) : base(asset, parent)
    {
    }

    public override void Init()
    {
        base.Init();
        
        BuildChildren((Folder)Asset);
    }

    protected override void CreateContextMenu()
    {
        RegisterMenuItem(new MenuItemSettings
        {
            Header = "Create Asset",
            Action = CreateItem
        });
        
        var mark = ((Folder)Asset).Mark;
        if (!mark.HasFlag(FolderMark.Locked))
        {
            base.CreateContextMenu();
        }
    }

    private void DefaultCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        createAssetViewModel.RegisterAssetToCreate<Folder>("Folder", asset =>
        {
            var folderData = asset.GetData<FolderData>();
            folderData.Parent = Asset.URI;

            return AssetCreationStatus.Success;
        });
    }

    private void ListNormalFolderCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        createAssetViewModel.RegisterAssetToCreate<SoundEffect>("Sound Effect", asset =>
        {
            var file = MiscUtils.GetFileFromDialogue("Wave File|*.wav");
            if (string.IsNullOrEmpty(file))
            {
                return AssetCreationStatus.Failed;
            }
        
            using FileStream fs = new(file, FileMode.Open, FileAccess.Read);
            using BinaryReader reader = new(fs);
            Byte[] pcm = Array.Empty<byte>();
            short channels = 0;
            uint frequency = 0;
            RIFF.LoadRiff(reader, ref pcm, ref channels, ref frequency);
            if (channels != 1)
            {
                Log.WriteLine("ERROR: Stereo sound effects are not supported. Sound wasn't added.");
                return AssetCreationStatus.Failed;
            }

            if (frequency > 48000)
            {
                Log.WriteLine("ERROR: Sounds over 48000 Hz are not supported. Sound wasn't added.");
                return AssetCreationStatus.Failed;
            }
        
            fs.Flush();
            fs.Close();
            reader.Close();

            var newSoundData = new SoundEffectData(file);
            asset.SetData(newSoundData);
            
            return AssetCreationStatus.Success;
        });
        createAssetViewModel.RegisterAssetToCreate<Texture>("Texture");
        createAssetViewModel.RegisterAssetToCreate<Skydome>("Skydome");
        createAssetViewModel.RegisterAssetToCreate<GameObject>("Game Object");
        createAssetViewModel.RegisterAssetToCreate<BehaviourStarter>("Behaviour");
    }

    private void ListInstanceCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        createAssetViewModel.RegisterAssetToCreate<AiPath>("AI Path");
        createAssetViewModel.RegisterAssetToCreate<AiPosition>("AI Position");
        createAssetViewModel.RegisterAssetToCreate<Camera>("Camera");
        createAssetViewModel.RegisterAssetToCreate<CollisionSurface>("Collision Surface");
        createAssetViewModel.RegisterAssetToCreate<InstanceTemplate>("Instance Template");
        createAssetViewModel.RegisterAssetToCreate<ObjectInstance>("Object Instance");
        createAssetViewModel.RegisterAssetToCreate<Path>("Path");
        createAssetViewModel.RegisterAssetToCreate<Position>("Position");
        createAssetViewModel.RegisterAssetToCreate<Trigger>("Trigger");
    }

    protected virtual void ListCreatableAssets(CreateAssetViewModel createAssetViewModel)
    {
        var mark = ((Folder)Asset).Mark;
        if (mark.HasFlag(FolderMark.ChunksOnly))
        {
            createAssetViewModel.RegisterAssetToCreate<ChunkFolder>("Chunk");
            return;
        }

        if (mark.HasFlag(FolderMark.InChunk))
        {
            ListInstanceCreatableAssets(createAssetViewModel);
            return;
        }
        
        if (mark.HasFlag(FolderMark.Normal))
        {
            ListNormalFolderCreatableAssets(createAssetViewModel);
        }
    }

    private void CreateItem()
    {
        var assetCreatorDialogue = IoC.Get<CreateAssetViewModel>();
        DefaultCreatableAssets(assetCreatorDialogue);
        ListCreatableAssets(assetCreatorDialogue);
        assetCreatorDialogue.AssignFolder(this);
        IoC.Get<IWindowManager>().ShowDialogAsync(assetCreatorDialogue);
    }
}