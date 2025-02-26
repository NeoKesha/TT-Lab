using System;
using System.IO;
using System.IO.Packaging;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Caliburn.Micro;
using NAudio.Wave;
using TT_Lab.AssetData.Code;
using TT_Lab.Assets;
using TT_Lab.Attributes;
using TT_Lab.Audio;
using TT_Lab.Util;
using Twinsanity.Libraries;
using Action = System.Action;
using Package = System.IO.Packaging.Package;
using Timer = System.Timers.Timer;

namespace TT_Lab.ViewModels.Editors.Code;

public class SoundEffectViewModel : ResourceEditorViewModel
{
    private AudioPlayer _audioPlayer;
    private MemoryStream _soundStream;
    private Timer _progressUpdater;

    public SoundEffectViewModel()
    {
        _progressUpdater = new Timer();
        _progressUpdater.Interval = 15;
        _progressUpdater.AutoReset = true;
        _progressUpdater.Elapsed += (s, e) =>
        {
            NotifyOfPropertyChange(nameof(SoundProgress));
            NotifyOfPropertyChange(nameof(CurrentTime));
        };
    }

    protected override Task OnDeactivateAsync(bool close, CancellationToken cancellationToken)
    {
        if (close)
        {
            _audioPlayer.Dispose();
        }
        
        StopPlayback();
        
        return base.OnDeactivateAsync(close, cancellationToken);
    }

    public override void LoadData()
    {
        if (_audioPlayer != null)
        {
            _audioPlayer.Dispose();
        }
        
        var soundData = AssetManager.Get().GetAssetData<SoundEffectData>(EditableResource);
        _soundStream = soundData.GetSoundEffectStream();
        _audioPlayer = new AudioPlayer(_soundStream);
        _audioPlayer.OnPlaybackStopped += () =>
        {
            if (_audioPlayer.GetPlaybackState() == PlaybackState.Stopped)
            {
                SoundProgress = 0;
                _progressUpdater.Stop();
            }
            Log.WriteLine($"Sound playback stopped. Playback status: {_audioPlayer.GetPlaybackState()}");
        };
    }

    protected override void Save()
    {
        if (ReplacedAudioMark)
        {
            var sound = AssetManager.Get().GetAsset(EditableResource);
            sound.Serialize(true);
        }

        base.Save();
    }

    public void PlaySound()
    {
        if (_audioPlayer.IsPlaying)
        {
            StopPlayback();
        }

        _progressUpdater.Start();
        _audioPlayer.Play();
    }

    public void PauseSound()
    {
        _progressUpdater.Stop();
        _audioPlayer.Pause();
        NotifyOfPropertyChange(nameof(SoundProgress));
        NotifyOfPropertyChange(nameof(CurrentTime));
    }

    public void ReplaceSound()
    {
        var file = MiscUtils.GetFileFromDialogue("Wave File|*.wav");
        if (string.IsNullOrEmpty(file))
        {
            return;
        }
        
        using FileStream fs = new(file, FileMode.Open, FileAccess.Read);
        using BinaryReader reader = new(fs);
        Byte[] pcm = Array.Empty<byte>();
        short channels = 0;
        uint frequency = 0;
        RIFF.LoadRiff(reader, ref pcm, ref channels, ref frequency);
        if (channels != 1)
        {
            Log.WriteLine("ERROR: Stereo sound effects are not supported. Sound wasn't replaced.");
            return;
        }

        if (frequency > 48000)
        {
            Log.WriteLine("ERROR: Sounds over 48000 Hz are not supported. Sound wasn't replaced.");
            return;
        }
        
        fs.Flush();
        fs.Close();
        reader.Close();
        
        var soundData = AssetManager.Get().GetAssetData<SoundEffectData>(EditableResource);
        soundData.Load(file);
        LoadData();
        
        SoundProgress = 0;
        NotifyOfPropertyChange(nameof(TotalTimeLength));
        NotifyOfPropertyChange(nameof(ReplacedAudioMark));
    }

    public void ChangeTrackPosition(RoutedPropertyChangedEventArgs<double> e)
    {
        if (_audioPlayer.GetPlaybackState() == PlaybackState.Playing)
        {
            return;
        }
        
        _progressUpdater.Stop();
        _audioPlayer.Pause();
        _audioPlayer.SetPosition(e.NewValue);
        NotifyOfPropertyChange(nameof(CurrentTime));
    }

    public double SoundProgress
    {
        get => _audioPlayer.GetProgress;
        set
        {
            _audioPlayer.SetPosition(value);
            NotifyOfPropertyChange();
            NotifyOfPropertyChange(nameof(CurrentTime));
        }
    }

    [MarkDirty]
    public bool ReplacedAudioMark => IsDirty;

    public string CurrentTime => _audioPlayer.GetPosition.ToString(@"mm\:ss\.ff");
    public string TotalTimeLength => _audioPlayer.GetDuration.ToString(@"mm\:ss\.ff");

    private void StopPlayback()
    {
        _audioPlayer.Stop();
        SoundProgress = 0;
        _progressUpdater.Stop();
    }
}