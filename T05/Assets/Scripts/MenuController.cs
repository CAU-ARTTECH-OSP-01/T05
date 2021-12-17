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

    public void GoBattleScene(int _stage)
    {
        DataBase.Instance.stage = _stage; //DataBase�� �ִ� ���������� ��ȣ�� �־��ְ�, �� ��ȣ�� �� BattleScene�� GameManager�� �Ѱ��ش�.
        SceneManager.LoadScene("BattleScene"); //BattleScene���� �̵��Ѵ�.
    }
}
