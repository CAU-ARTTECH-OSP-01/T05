using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void GoStartScene()
    {
        SceneManager.LoadScene("StartScene"); //StartScene���� �̵��Ѵ�.
    }

    public void GoSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene"); //StageSelectScene���� �̵��Ѵ�.
    }

    public void GoBattleScene()
    {
        SceneManager.LoadScene("BattleScene"); //BattleScene���� �̵��Ѵ�.
    }

    public void OnClickQuit() //���� ��ư
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
