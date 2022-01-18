using UnityEngine;

public class SoundManager : PersistentSingleton<SoundManager>
{
    [SerializeField] 
    private AudioSource _musicSource, _sfxSource;

    /// <summary>
    /// Play a sound from the SFX source.
    /// </summary>
    /// <param name="sfx">The clip to play.</param>
    public void PlayEffect(AudioClip sfx)
    {
        _sfxSource.PlayOneShot(sfx);
    }

    /// <summary>
    /// Play a sound from the music source.
    /// </summary>
    /// <param name="music">The clip to play</param>
    public void PlayMusic(AudioClip music)
    {
        _musicSource.clip = music;
        _musicSource.Play();
    }

    /// <summary>
    /// Pause the clip in the music source.
    /// </summary>
    public void PauseMusic()
    {
        if (_musicSource.clip != null)
        {
            _musicSource.Pause();
        } 
        else
        {
            Debug.LogWarning("No music is playing");
        }
    }
    
    /// <summary>
    /// Stop the clip in the music source.
    /// </summary>
    public void StopMusic()
    {
        if ( _musicSource.clip != null)
        {
            _musicSource.Stop();
        }
        else
        {
            Debug.LogWarning("No music is playing");
        }
    }

    /// <summary>
    /// Set the master volume (volume of all audio sources).
    /// </summary>
    /// <param name="volume">The new volume for all audio sources.</param>
    public void SetMasterVolume(float volume)
    {
        AudioListener.volume = volume;
    }

    /// <summary>
    /// Set the volume of the music source.
    /// </summary>
    /// <param name="volume">The new volume for the music source.</param>
    public void SetMusicVolume(float volume)
    {
        _musicSource.volume = volume;
    }

    /// <summary>
    /// Set the volume of the SFX source.
    /// </summary>
    /// <param name="volume">The new volume for the SFX source.</param>
    public void SetSfxVolume(float volume)
    {
        _sfxSource.volume = volume;
    }

    /// <summary>
    /// Toggle SFX sound on/off.
    /// </summary>
    public void ToggleSfx()
    {
        _sfxSource.mute = !_sfxSource.mute;
    }

    /// <summary>
    /// Toggle music sound on/off.
    /// </summary>
    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }
}
