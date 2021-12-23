using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void GoStartScene()
    {
        SceneManager.LoadScene("StartScene"); //StartScene으로 이동한다.
    }

    public void GoSelectScene()
    {
        SceneManager.LoadScene("StageSelectScene"); //StageSelectScene으로 이동한다.
    }

    public void GoBattleScene()
    {
        SceneManager.LoadScene("BattleScene"); //BattleScene으로 이동한다.
    }

    public void OnClickQuit() //종료 버튼
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
