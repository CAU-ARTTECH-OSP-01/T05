using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hellmade.Sound;

[System.Serializable]
public class SoundStats
{
    public string name;
    public AudioSource audio;
}

[System.Serializable]
public struct EazySoundControls
{
    public AudioClip audioclip;
    public Audio audio;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance; //이 게임 씬에서 단 하나만 존재하기 때문에, Singleton 패턴으로 만들어준다.
                                      //다른 곳에서 상속시켜 주지 않더라도 어디에서나 사용할 수 있는 방식이다.
    public static SoundManager Instance
    {
        get { return instance; }
        set { instance = value; }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject); //DataBase를 가진 오브젝트를 Scene이 변경되더라도 사라지지 않게 한다.
    }

    public EazySoundControls[] AudioControls;
    public List<SoundStats> Music;
    public List<SoundStats> SFX;


    public static void PlaySound(int _index)
    {
        EazySoundControls audioControl = instance.AudioControls[_index];
        int audioID = EazySoundManager.PlaySound(audioControl.audioclip, 0.3f, true, null);

        instance.AudioControls[_index].audio = EazySoundManager.GetAudio(audioID);
    }

    public void Stop(string audioControlIDStr)
    {
        int audioControlID = int.Parse(audioControlIDStr);
        EazySoundControls audioControl = AudioControls[audioControlID];

        audioControl.audio.Stop();
    }

    /*public static void PlaySFX(string _name)
    {
        for (int i = 0; i < instance.SFX.Count; i++)
        {
            if (_name == instance.SFX[i].name)
                instance.SFX[i].audio.Play();
        }
    }

    public static void StopSFX()
    {
        for (int i = 0; i < instance.SFX.Count; i++)
        {
            instance.SFX[i].audio.Stop();
        }
    }*/
}
