using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager2 : MonoBehaviour
{
    public static AudioManager2 Instence {get; private set;}
    [Header("Все AudioSuurce")]
    [SerializeField] private AudioSource MusicSource;
    [SerializeField] private AudioSource SfxSource;

    [Header("Все звуки")]
    [SerializeField] private AudioClip VictoriClip;
    [SerializeField] private AudioClip DefeatClip;
    [SerializeField] private AudioClip ErrorClip;
    [SerializeField] private AudioClip ClickClip;

    private float MusicVol;
    private float SfxVol;

    private void Awake()
    {
        if (Instence == null)
        {
            Instence=this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        AudioVolumeGer();
    }

    public void VictorClipPlay()
    {
        if (VictoriClip != null)
        {
            SfxSource.PlayOneShot(VictoriClip, SfxVol);
        }
    }

    public void DefeatClipPlay()
    {
        if (DefeatClip != null)
        {
            SfxSource.PlayOneShot(DefeatClip, SfxVol);
        }
    }

    public void ErrorClipPlay()
    {
        if (ErrorClip != null)
        {
            SfxSource.PlayOneShot(ErrorClip, SfxVol);
        }
    }

    public void ClickClipPlay()
    {
        if (ClickClip != null)
        {
            SfxSource.PlayOneShot(ClickClip, SfxVol);
        }
    }

    public void MusicVolSet(float vol)
    {
        if (vol <= 1)
        {
            MusicVol=vol;
            MusicSource.volume=vol;
            PlayerPrefs.SetFloat("MusicVol",vol);
        }
    }
    public void SfxVolSet(float vol)
    {
        if (vol <= 1)
        {
            SfxVol=vol;
            SfxSource.volume=vol;
            PlayerPrefs.SetFloat("SfxVol",vol);
        }
    }

    public void AudioVolumeGer()
    {
        MusicVol= PlayerPrefs.GetFloat("MusicVol");
        SfxVol= PlayerPrefs.GetFloat("SfxVol");

        MusicSource.volume=MusicVol;
        SfxSource.volume=SfxVol;
    }

}
