using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [System.Serializable]
    public struct AudioNode
    {
        public string name;
        public AudioClip sound;
    }

    public AudioNode[] audioNodes;

    private static float m_sfxVolume = 1f;

    private static SoundManager instance;
    private ArrayList soundSources;
    private static Hashtable audios;

    public static float sfxVolume
    {
        get { return m_sfxVolume; }

        set
        {
            m_sfxVolume = value;
            foreach (AudioSource src in instance.soundSources)
                src.volume = m_sfxVolume;
        }
    }

    public void PlaySoundUI(string name)
    {
        PlaySound((AudioClip)audios[name], false, 1f);
    }

    public static void PlaySound(string name, bool loop = false, float volume = 1f, float pitch = 1f)
    {
        PlaySound((AudioClip)audios[name], loop, volume, pitch);
    }

    public static void PlaySound(AudioClip sound, bool loop = false, float volume = 1f, float pitch = 1f)
    {
        foreach (AudioSource src in instance.soundSources)
        {
            if (!src.isPlaying)
            {
                src.loop = loop;
                src.volume = sfxVolume * volume;
                src.clip = sound;
                src.pitch = pitch;
                src.Play();
                return;
            }
        }

        AudioSource newSrc = CreateNewSource();
        newSrc.loop = loop;
        newSrc.volume = m_sfxVolume * volume;
        newSrc.PlayOneShot(sound);
    }

    public static void SetChannelVolume(string channel, float volume)
    {
        instance.transform.Find("Music/" + channel).
            GetComponent<AudioSource>().volume = volume;
    }

    private static AudioSource CreateNewSource()
    {
        GameObject temp = new GameObject();
        temp.transform.parent = instance.transform;
        temp.transform.localPosition = Vector3.zero;

        AudioSource src = temp.AddComponent<AudioSource>();
        instance.soundSources.Add(src);

        return src;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        GetAudioSources();
        FillAudioDictionary();

        sfxVolume = 1f;

        //DontDestroyOnLoad(gameObject);
    }

    private void GetAudioSources()
    {
        soundSources = new ArrayList();

        foreach (Transform child in transform)
        {
            AudioSource src = child.GetComponent<AudioSource>();
            soundSources.Add(src);
        }
    }

    private void FillAudioDictionary()
    {
        audios = new Hashtable();

        foreach (AudioNode node in audioNodes)
            audios.Add(node.name, node.sound);
    }
}