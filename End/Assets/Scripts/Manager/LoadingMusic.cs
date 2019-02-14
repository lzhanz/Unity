using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadingMusic : MonoBehaviour
{

    public int num;
    // Use this for initialization
    void Start()
    {
        choose(num);

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void choose(int id)
    {
        switch (id)
        {
            case 1:
                AudioManager.Instance.SetBgMusicVolume(0.2f);
                AudioManager.Instance.PlayBgMusic(7, true);
                break;
            case 2:
                AudioManager.Instance.SetSoundVolume(0.3f);
                AudioManager.Instance.PlayBgMusic(8, true);
                break;
            case 3:
                AudioManager.Instance.SetBgMusicVolume(0.2f);
                AudioManager.Instance.PlayBgMusic(9, true);
                break;
            case 4:
                AudioManager.Instance.SetBgMusicVolume(0.2f);
                AudioManager.Instance.PlayBgMusic(11, true);
                break;

        }
    }
}
