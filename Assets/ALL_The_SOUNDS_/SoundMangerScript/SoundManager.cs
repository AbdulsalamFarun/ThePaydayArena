using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

// --- Enums for Organization ---
public enum AudioType { SFX, Music }

// This custom class allows us to see and edit our sound properties in the Inspector.
[System.Serializable]
public class Sound
{
    public string name; // The name of the sound clip, used to call it.
    public AudioClip clip; // The actual audio file.
    public AudioType type; // Is this SFX or Music?

    [Range(0f, 1f)]
    public float volume = 0.7f; // The base volume of the sound.

    [Range(0.5f, 1.5f)]
    public float pitch = 1f; // The base pitch of the sound.

    // Add a slight random pitch variation to make SFX less repetitive.
    // X is the minimum, Y is the maximum. Set to (1, 1) for no variation.
    public Vector2 randomPitchRange = new Vector2(1f, 1f);

    public bool loop = false; // Whether the sound should loop (primarily for music).

    [HideInInspector] // We hide this because we will assign it in the SoundManager script.
    public AudioSource source;
}

/// <summary>
/// A professional, self-contained sound manager.
/// It is a singleton that persists across scenes and handles all audio playback,
/// including global volume controls, fading, and pitch variation without relying on an AudioMixer.
/// </summary>
public class SoundManager : MonoBehaviour
{
    // --- Singleton Instance ---
    public static SoundManager instance;

    // --- Global Volume Controls ---
    [Header("Global Volume Controls")]
    [Range(0f, 1f)]
    public float masterVolume = 1f;
    [Range(0f, 1f)]
    public float musicVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;

    // --- Sound Lists ---
    [Header("Audio Clips")]
    public Sound[] sounds; // A single list for all your audio clips.

    private Dictionary<string, Sound> soundMap = new Dictionary<string, Sound>();
    private Sound currentMusicTrack;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// It sets up the singleton pattern and initializes all sounds.
    /// </summary>
    void Awake()
    {
        // --- Singleton Pattern ---
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        // --- Initialize All Sounds ---
        InitializeSounds();
    }

    /// <summary>
    /// Initializes all sounds in the 'sounds' array by creating and configuring AudioSource components.
    /// </summary>
    private void InitializeSounds()
    {
        foreach (Sound s in sounds)
        {
            if (soundMap.ContainsKey(s.name))
            {
                Debug.LogWarning($"SoundManager: A sound with the name '{s.name}' already exists. Skipping.");
                continue;
            }

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.playOnAwake = false; // Important!
            s.source.loop = s.loop;

            // Set initial volume and pitch (will be adjusted by global controls when played)
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            soundMap[s.name] = s;
        }
    }

    /// <summary>
    /// Plays a sound effect by its name. Applies global volume and random pitch.
    /// </summary>
    /// <param name="soundName">The name of the sound to play.</param>
    public void Play(string soundName)
    {
        if (soundMap.TryGetValue(soundName, out Sound sound))
        {
            if (sound.type == AudioType.Music)
            {
                // If someone tries to play music with Play(), redirect to PlayMusic()
                PlayMusic(soundName, 0f);
                return;
            }

            // Apply global volume controls
            float finalVolume = sound.volume * sfxVolume * masterVolume;

            // Apply random pitch
            sound.source.pitch = sound.pitch * UnityEngine.Random.Range(sound.randomPitchRange.x, sound.randomPitchRange.y);

            // Use PlayOneShot for SFX to allow sounds to overlap
            sound.source.PlayOneShot(sound.clip, finalVolume);
        }
        else
        {
            Debug.LogWarning($"SoundManager: SFX '{soundName}' not found!");
        }
    }

    /// <summary>
    /// Plays a music track by name, with an optional fade-in time.
    /// </summary>
    /// <param name="musicName">The name of the music track to play.</param>
    /// <param name="fadeDuration">How long the fade-in should take in seconds. 0 for instant.</param>
    public void PlayMusic(string musicName, float fadeDuration = 1.0f)
    {
        if (soundMap.TryGetValue(musicName, out Sound music))
        {
            if (music.type != AudioType.Music)
            {
                Debug.LogWarning($"SoundManager: Tried to play SFX '{musicName}' as music. Use Play() instead.");
                return;
            }

            // Stop current music if a new track is being played
            if (currentMusicTrack != null && currentMusicTrack.name != musicName && currentMusicTrack.source.isPlaying)
            {
                StopMusic(0.5f); // Fade out the old music quickly
            }

            // Start playing the new music if it isn't already
            if (currentMusicTrack == null || currentMusicTrack.name != musicName || !currentMusicTrack.source.isPlaying)
            {
                currentMusicTrack = music;
                StartCoroutine(FadeIn(currentMusicTrack, fadeDuration));
            }
        }
        else
        {
            Debug.LogWarning($"SoundManager: Music track '{musicName}' not found!");
        }
    }

    /// <summary>
    /// Stops the currently playing music track with an optional fade-out.
    /// </summary>
    /// <param name="fadeDuration">How long the fade-out should take. 0 for instant.</param>
    public void StopMusic(float fadeDuration = 1.0f)
    {
        if (currentMusicTrack != null && currentMusicTrack.source.isPlaying)
        {
            StartCoroutine(FadeOut(currentMusicTrack, fadeDuration));
        }
    }

    // --- Coroutines for Fading ---
    private IEnumerator FadeIn(Sound sound, float duration)
    {
        float targetVolume = sound.volume * musicVolume * masterVolume;
        sound.source.volume = 0;
        sound.source.Play();

        while (sound.source.volume < targetVolume)
        {
            // Recalculate target volume each frame in case the user changes the global sliders
            targetVolume = sound.volume * musicVolume * masterVolume;
            sound.source.volume += targetVolume * Time.deltaTime / duration;
            yield return null;
        }
        sound.source.volume = targetVolume;
    }

    private IEnumerator FadeOut(Sound sound, float duration)
    {
        float startVolume = sound.source.volume;
        while (sound.source.volume > 0)
        {
            sound.source.volume -= startVolume * Time.deltaTime / duration;
            yield return null;
        }
        sound.source.Stop();
        sound.source.volume = startVolume; // Reset for next time
        if (sound == currentMusicTrack)
        {
            currentMusicTrack = null;
        }
    }
}
