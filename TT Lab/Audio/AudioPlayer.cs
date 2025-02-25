using System;
using System.IO;
using NAudio.Wave;

namespace TT_Lab.Audio;

public class AudioPlayer : IDisposable
{
    private readonly WaveOutEvent _waveOut;
    private readonly WaveStream _waveStream;
    private bool _isDisposed = false;

    public event Action OnPlaybackStopped;

    public AudioPlayer(MemoryStream audioStream)
    {
        _waveStream = new WaveFileReader(audioStream);
        _waveOut = new WaveOutEvent();
        _waveOut.Init(_waveStream);
        _waveOut.PlaybackStopped += WaveOutOnPlaybackStopped;
    }

    private void WaveOutOnPlaybackStopped(object? sender, StoppedEventArgs e)
    {
        if (_isDisposed)
        {
            return;
        }
        
        OnPlaybackStopped?.Invoke();
    }

    public void Play()
    {
        if (_isDisposed)
        {
            return;
        }
        
        _waveOut.Play();
    }

    public void Pause()
    {
        if (_isDisposed)
        {
            return;
        }
        
        _waveOut.Pause();
    }

    public void Stop()
    {
        if (_isDisposed)
        {
            return;
        }
        
        _waveOut.Stop();
    }

    public void SetPosition(double position)
    {
        if (_isDisposed)
        {
            return;
        }
        
        _waveStream.CurrentTime = GetDuration * position;
    }

    public PlaybackState GetPlaybackState() => _waveOut.PlaybackState;
    public bool IsPlaying => _waveOut.PlaybackState == PlaybackState.Playing;
    public double GetProgress => GetPosition / GetDuration;
    public TimeSpan GetPosition => _waveStream.CurrentTime;
    public TimeSpan GetDuration => _waveStream.TotalTime;

    public void Dispose()
    {
        _isDisposed = true;
        _waveOut.Dispose();
        _waveStream.Dispose();
    }
}