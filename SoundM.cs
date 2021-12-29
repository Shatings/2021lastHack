using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundM : MonoBehaviour
{
    public AudioMixer mixer;
    public AudioSource adio;
    [SerializeField]
    private List<AudioClip> effectsound = new List<AudioClip>();
    [SerializeField]
    private List<AudioClip> background = new List<AudioClip>();
   

    public static SoundM instanse;
    private void Awake()
    {
        SceanMove();
        if (instanse == null)
        {
            instanse = this;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    public void BgSoundV(float val)
    {
        mixer.SetFloat("BGM", Mathf.Log10(val) * 30);

    }
    public void VFXV(float val)
    {
        mixer.SetFloat("Eff", Mathf.Log10(val) * 20);
    }
    public void Master(float val)
    {
        mixer.SetFloat("Master", Mathf.Log10(val) * 20);
    }
    private void BgmPlay(int i)
    {
        adio.outputAudioMixerGroup = mixer.FindMatchingGroups("BGM")[0];
        adio.clip = background[i];
        adio.loop = false;
        adio.Play();
    }
    public void SceanMove()
    {
        adio.Stop();
        StopCoroutine(Bgm());
        StartCoroutine(Bgm());
    }
    public void SoundEff(int i)
    {
       
        GameObject name = new GameObject(i + "Sound");

        AudioSource audiosouse = name.AddComponent<AudioSource>();
        audiosouse.outputAudioMixerGroup = mixer.FindMatchingGroups("Eff")[0];

        audiosouse.clip = effectsound[i];
        audiosouse.Play();

        Destroy(name, effectsound[i].length);
    }
    IEnumerator Bgm()
    {
        while (true)
        {
            if (!adio.isPlaying)
            {
               
                BgmPlay(Random.Range(0, background.Count));
            }
           
            yield return null;
        }
    }
   
}
