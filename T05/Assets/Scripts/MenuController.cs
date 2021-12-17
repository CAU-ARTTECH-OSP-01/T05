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

    public void GoBattleScene(int _stage)
    {
        DataBase.Instance.stage = _stage; //DataBase에 있는 스테이지에 번호를 넣어주고, 그 번호를 각 BattleScene의 GameManager에 넘겨준다.
        SceneManager.LoadScene("BattleScene"); //BattleScene으로 이동한다.
    }
}
