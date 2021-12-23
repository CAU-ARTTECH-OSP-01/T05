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
    private static SoundManager instance; //�� ���� ������ �� �ϳ��� �����ϱ� ������, Singleton �������� ������ش�.
                                      //�ٸ� ������ ��ӽ��� ���� �ʴ��� ��𿡼��� ����� �� �ִ� ����̴�.
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

        DontDestroyOnLoad(gameObject); //DataBase�� ���� ������Ʈ�� Scene�� ����Ǵ��� ������� �ʰ� �Ѵ�.
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
