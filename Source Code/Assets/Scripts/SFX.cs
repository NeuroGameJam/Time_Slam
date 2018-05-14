using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SFX : MonoBehaviour
{
    public static SFX instance;

    private List<AudioSource> sources = new List<AudioSource>();
    public List<Clips> clips = new List<Clips>();

    private void Awake()
    {
        if (instance && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public void PlayInLoop(string name, float pitch = 1, float volume = 1)
    {

        AudioClip clip = clips.Find(x => x.name == name).clip;
        if (clip == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }

        AudioSource s = gameObject.AddComponent<AudioSource>();
        s.hideFlags = HideFlags.HideInInspector;
        sources.Add(s);
        s.playOnAwake = false;
        s.clip = clip;
        s.loop = true;
        s.time = 0f;
        s.pitch = pitch;
        s.volume = volume;
        s.Play();
    }

    public void PlayOneTimeClip(string name, float pitch = 1, float volume = 1)
    {

        AudioClip clip = clips.Find(x => x.name == name).clip;
        if (clip == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }

        AudioSource s = gameObject.AddComponent<AudioSource>();
        sources.Add(s);
        s.playOnAwake = false;
        s.clip = clip;
        s.loop = false;
        s.time = 0.1f;
        s.hideFlags = HideFlags.HideInInspector;
        s.pitch = pitch;
        s.volume = volume;
        s.Play();
        StartCoroutine(DestroySourceAfterEnd(s));
    }

    public void PlayOneTimeClipWithRandomPitch(string name, float min, float max, float volume = 1)
    {

        AudioClip clip = clips.Find(x => x.name == name).clip;
        if(clip == null)
        {
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }

        float pitch = Random.Range(min, max);
        AudioSource s = gameObject.AddComponent<AudioSource>();
        sources.Add(s);
        s.playOnAwake = false;
        s.clip = clip;
        s.loop = false;
        s.time = 0.1f;
        s.pitch = pitch;
        s.hideFlags = HideFlags.HideInInspector;
        s.volume = volume;
        s.Play();
        StartCoroutine(DestroySourceAfterEnd(s));
    }

    IEnumerator DestroySourceAfterEnd(AudioSource source)
    {
        yield return new WaitUntil(() => !source.isPlaying);
        sources.Remove(source);
        Destroy(source);
    }

}

[System.Serializable]
public class Clips
{
    public string name;
    public AudioClip clip;
}
