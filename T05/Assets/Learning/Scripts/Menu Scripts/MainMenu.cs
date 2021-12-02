using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClickNewGame()
    {
        Debug.Log("새 게임");
    }
    public void OnClickLoad()
    {
        Debug.Log("불러오기");
    }
    public void OnClickOption()
    {
        Debug.Log("옵션");
    }
    public void OnClickQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
