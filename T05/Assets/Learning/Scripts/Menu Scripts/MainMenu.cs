using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void OnClickNewGame()
    {
        Debug.Log("�� ����");
    }
    public void OnClickLoad()
    {
        Debug.Log("�ҷ�����");
    }
    public void OnClickOption()
    {
        Debug.Log("�ɼ�");
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
