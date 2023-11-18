using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager instance;
    public List<AudioClip> clips;
    private Dictionary<string, AudioSource> audioSources;

    void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        
        audioSources = new Dictionary<string, AudioSource>();

        
        foreach (AudioClip clip in clips)
        {
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.clip = clip;
            audioSources.Add(clip.name, audioSource);
        }
    }

    public void PlaySound(string soundName, float volume, bool loop)
    {
        if (audioSources.ContainsKey(soundName))
        {
            AudioSource audioSource = audioSources[soundName];
            audioSource.volume = volume;
            audioSource.loop = loop;
            audioSource.Play();
        }
        else
        {
            Debug.Log("Sound not found: " + soundName);
        }
    }

    public void StopSound(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            AudioSource audioSource = audioSources[soundName];
            audioSource.Stop();
        }
        else
        {
            Debug.Log("Sound not found: " + soundName);
        }
    }
    public bool IsPlaying(string soundName)
    {
        if (audioSources.ContainsKey(soundName))
        {
            return audioSources[soundName].isPlaying;
        }
        return false;
    }

}




public class MyHashMap<K, V>
{
    private LinkedList<KeyValuePair<K, V>>[] data;
    private int size;

    public MyHashMap(int size)
    {
        this.size = size;
        data = new LinkedList<KeyValuePair<K, V>>[size];
    }

    private int GetHash(K key)
    {
        return key.GetHashCode() % size;
    }

    public void Put(K key, V value)
    {
        int hash = GetHash(key);
        if (data[hash] == null)
            data[hash] = new LinkedList<KeyValuePair<K, V>>();

        foreach (var pair in data[hash])
        {
            if (pair.Key.Equals(key))
            {
                data[hash].Remove(pair);
                break;
            }
        }
        data[hash].AddLast(new KeyValuePair<K, V>(key, value));
    }

    public V Get(K key)
    {
        int hash = GetHash(key);
        if (data[hash] != null)
        {
            foreach (var pair in data[hash])
            {
                if (pair.Key.Equals(key))
                    return pair.Value;
            }
        }
        return default(V);
    }

    public bool ContainsKey(K key)
    {
        int hash = GetHash(key);
        if (data[hash] != null)
        {
            foreach (var pair in data[hash])
            {
                if (pair.Key.Equals(key))
                    return true;
            }
        }
        return false;
    }

    public void Remove(K key)
    {
        int hash = GetHash(key);
        if (data[hash] != null)
        {
            foreach (var pair in data[hash])
            {
                if (pair.Key.Equals(key))
                {
                    data[hash].Remove(pair);
                    break;
                }
            }
        }
    }
}
