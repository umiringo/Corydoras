using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GlobalDefines;

public class AudioPlayer : MonoBehaviour {
    public AudioClip bgm;
    public AudioClip rightSe;
    public AudioClip wrongSe;

    private bool isMute;
    private AudioSource audioSource;

    private void Awake()
    {
        isMute = PlayerPrefs.GetInt(PrefsKey.Mute, 0) > 0;
		audioSource = this.gameObject.AddComponent<AudioSource>();
		audioSource.loop = true;
		audioSource.volume = 0.5f;
        audioSource.clip = bgm;
    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool GetMute()
    {
        return isMute;
    }

    public void SetMute(bool mute)
    {
        isMute = mute;
        if(!isMute)
        {
            //audioSource.Play();
            PlayerPrefs.SetInt(PrefsKey.Mute, 0);
        }
        else
        {
            //audioSource.Stop();
			PlayerPrefs.SetInt(PrefsKey.Mute, 1);
        }
    }

    public void PlayeBGM()
    {
        if(!isMute)
        {
            audioSource.Play();
        }
    }

    public void StopBGM()
    {
        StartCoroutine(StopPlay());
    }

    IEnumerator StopPlay()
    {
        yield return new WaitForSeconds(0.1f);
        audioSource.Stop();
    }

    public void PlayRightSound()
    {
        if(!isMute)
        {
            audioSource.PlayOneShot(rightSe);
        }
    }

    public void PlayWrongSound()
    {
        if(!isMute)
        {
            audioSource.PlayOneShot(wrongSe);
        }
    }
}
