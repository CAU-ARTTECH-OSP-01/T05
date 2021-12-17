using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSceneController : MonoBehaviour
{
    public void GoNextScene()
    {
        SceneManager.LoadScene("StageSelectScene"); //StageSelectScene으로 이동한다.
    }
}
