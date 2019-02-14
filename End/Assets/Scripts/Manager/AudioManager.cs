using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class AudioManager : MonoBehaviour
{

    #region 单例
    private static AudioManager _instance;
    static bool isHaveClone = false;

    public static AudioManager Instance
    {
        get { return _instance; }
    }
    #endregion

    private Dictionary<int, string> audioPathDict;       //存放音频的路径

    private AudioSource musicAudioSource;

    private List<AudioSource> unusedAudioSourceList;                  //存放未使用的音频

    private List<AudioSource> usedAudioSourceList;                  //存放已使用的音频

    private Dictionary<int, AudioClip> audioClipDict;          //缓存音频

    private float musicVolume = 1;    //背景音量
    private float soundVolume = 1;     //音效音量

    private int poolCount = 3;

    private string musicVolumePrefs = "MusicVolume";
    private string soundVolumePrefs = "SoundVolume";


    private void Awake()
    {
        if (!isHaveClone)
        {
            _instance = this;             //重新设置
            isHaveClone = true;
            DontDestroyOnLoad(this.gameObject);        //切场景不删除该空物体
        }
       
        audioPathDict = new Dictionary<int, string>();        //音频路径

        audioPathDict.Add(1, "AudioClip/attack1");
        audioPathDict.Add(2, "AudioClip/attack2");
        audioPathDict.Add(3, "AudioClip/attack3");
        audioPathDict.Add(4, "AudioClip/arrowskillbg");
        audioPathDict.Add(5, "AudioClip/jump");
        audioPathDict.Add(6, "AudioClip/down");
        audioPathDict.Add(7, "BGM/Menu");
        audioPathDict.Add(8, "BGM/Loading");
        audioPathDict.Add(9, "BGM/Game1");
        audioPathDict.Add(10, "AudioClip/dead");
        audioPathDict.Add(11, "BGM/Boss");


        musicAudioSource = gameObject.AddComponent<AudioSource>(); //添加AudioSource组件
        unusedAudioSourceList = new List<AudioSource>();
        usedAudioSourceList = new List<AudioSource>();
        audioClipDict = new Dictionary<int, AudioClip>();

    }


    private void Start()
    {
        //音量相关
        if (PlayerPrefs.HasKey(musicVolumePrefs))
        {
            musicVolume = PlayerPrefs.GetFloat(musicVolumePrefs);
        }

        if (PlayerPrefs.HasKey(soundVolumePrefs))
        {
            soundVolume = PlayerPrefs.GetFloat(soundVolumePrefs);
        }
    }



    /// <summary>
    /// 播放背景音乐
    /// </summary>
    /// <param name="id"></param>
    /// <param name="loop"></param>
    public void PlayBgMusic(int id, bool loop = true)    //默认循环播放
    {

        //声音小到大，大到小
        DOTween.To(() => musicAudioSource.volume, value => musicAudioSource.volume = value, 0, 0.5f).OnComplete(() =>
        {
            musicAudioSource.clip = GetAudioClip(id);
            musicAudioSource.clip.LoadAudioData();
            musicAudioSource.loop = loop;
            musicAudioSource.volume = musicVolume;
            musicAudioSource.Play();
            DOTween.To(() => musicAudioSource.volume, value => musicAudioSource.volume = value, musicVolume, 0.5f);
        });
    }




    public void PlaySound(int id)
    {
        if (unusedAudioSourceList.Count != 0)
        {
            AudioSource ads = ToUsed();
            ads.clip = GetAudioClip(id);
            ads.clip.LoadAudioData();
            ads.Play();
            StartCoroutine(WaitSoundEnd(ads));   //协程，在播放完执行
        }
        else
        {
            AddAudioSource();
            AudioSource ads = ToUsed();
            ads.clip = GetAudioClip(id);
            ads.clip.LoadAudioData();
            ads.volume = soundVolume;
            ads.loop = false;
            ads.Play();

            StartCoroutine(WaitSoundEnd(ads));
        }
    }


    /// <summary>
    /// 协程等待播放完
    /// </summary>
    /// <param name="ads"></param>
    /// <returns></returns>
    IEnumerator WaitSoundEnd(AudioSource ads)
    {
        yield return new WaitUntil(() => { return !ads.isPlaying; });
        ToUnused(ads);
    }



    /// <summary>
    /// 获取音频，并且缓存
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private AudioClip GetAudioClip(int id)
    {
        if (!audioClipDict.ContainsKey(id))
        {
            //该路径不存在音频就退出
            if (!audioPathDict.ContainsKey(id)) return null;

            AudioClip acp = Resources.Load(audioPathDict[id]) as AudioClip;
            audioClipDict.Add(id, acp);
        }
        return audioClipDict[id];
    }



    /// <summary>
    /// 添加音频组件
    /// </summary>
    /// <returns></returns>
    private AudioSource AddAudioSource()
    {
        if (unusedAudioSourceList.Count != 0)
        {
            return ToUsed();
        }
        else
        {
            AudioSource ads = gameObject.AddComponent<AudioSource>();
            unusedAudioSourceList.Add(ads);
            return ads;
        }
    }




    /// <summary>
    /// 将未使用的音频移至已使用的集合中
    /// </summary>
    /// <returns></returns>

    private AudioSource ToUsed()
    {
        AudioSource ads = unusedAudioSourceList[0];
        unusedAudioSourceList.RemoveAt(0);
        usedAudioSourceList.Add(ads);
        return ads;
    }




    /// <summary>
    /// 使用完的放到未使用里
    /// </summary>
    /// <param name="ads"></param>

    private void ToUnused(AudioSource ads)
    {
        usedAudioSourceList.Remove(ads);
        if (unusedAudioSourceList.Count >= poolCount)
        {
            Destroy(ads);
        }
        else
        {
            unusedAudioSourceList.Add(ads);
        }
    }




    /// <summary>
    /// 修改背景音乐大小
    /// </summary>
    /// <param name="vlm"></param>
    public void SetBgMusicVolume(float vlm)
    {
        musicVolume = vlm;
        musicAudioSource.volume = vlm;
        PlayerPrefs.SetFloat(musicVolumePrefs, vlm);
    }


    /// <summary>
    /// 修改音效音量大小
    /// </summary>
    /// <param name="vlm"></param>
    public void SetSoundVolume(float vlm)
    {
        //包括已使用和未使用列表
        soundVolume = vlm;
        for (int i = 0; i < unusedAudioSourceList.Count; i++)
        {
            unusedAudioSourceList[i].volume = vlm;
        }
        for (int j = 0; j < usedAudioSourceList.Count; j++)
        {
            usedAudioSourceList[j].volume = vlm;
        }
        PlayerPrefs.SetFloat(soundVolumePrefs, vlm);
    }
}