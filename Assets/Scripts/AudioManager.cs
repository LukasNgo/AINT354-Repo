using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    private bool _heartBeat1;
    private bool _heartBeat2;
    private bool _running1;
    private bool _running2;


    void Awake () {
		foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
            s.source.spatialBlend = s.spatialBlend;
        }
	}

    private void Start()
    {
        Play("AmbienceNormal");
        Play("Breathing");
        StartCoroutine(Whispers());
    }

    private IEnumerator Whispers()
    {
        yield return new WaitForSeconds(3f);
        Play("Whisper1");
        yield return new WaitForSeconds(30f);
        Play("Whisper2");
        yield return new WaitForSeconds(60f);
        Play("Whisper1");
        yield return new WaitForSeconds(60f);
        Play("Whisper2");
    }

    private void Update()
    {
        if (FindObjectOfType<Player>().GetHealth() < 100 && !_heartBeat1)
        {
            FindObjectOfType<AudioManager>().Play("HeartBeatFast");
            FindObjectOfType<AudioManager>().Stop("HeartBeatNormal");
            _heartBeat1 = true;
            _heartBeat2 = false;
        }

        if (FindObjectOfType<Player>().GetHealth() == 100 && !_heartBeat2)
        {
            FindObjectOfType<AudioManager>().Play("HeartBeatNormal");
            FindObjectOfType<AudioManager>().Stop("HeartBeatFast");
            _heartBeat1 = false;
            _heartBeat2 = true;
        }

        if (FindObjectOfType<PlayerMovement>().isPlayerRunning() && !_running1)
        {
            FindObjectOfType<AudioManager>().Play("PlayerFootstepsHigh");
            _running1 = true;
            _running2 = false;
        }

        if (!FindObjectOfType<PlayerMovement>().isPlayerRunning() && !_running2)
        {
            FindObjectOfType<AudioManager>().Stop("PlayerFootstepsHigh");
            _running1 = false;
            _running2 = true;
        }
    }

    public void Play (string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void SetVolume(string name, float value)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        s.source.volume = value;
    }
}
