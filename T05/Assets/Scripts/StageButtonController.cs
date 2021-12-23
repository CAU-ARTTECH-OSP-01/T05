using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonController : MonoBehaviour
{
    public Text txt_stageSelect; //승리 시 나오는 스테이지 이동 버튼의 Text

    public GameObject GetCardButton;
    public GameObject DeleteCardButton;

    private void Awake()
    {
        ButtonControl();
    }

    public void ButtonControl()
    {
        if (DataBase.Instance.stage > 4) //5 스테이지 이후에는 엔딩으로 이동
        {
            txt_stageSelect.text = "엔딩";
            GetCardButton.SetActive(false);
            DeleteCardButton.SetActive(false);
        }
    }

    public void onClick()
    {
        GameManager.Instance.player_Victory.SetActive(false);
        if (DataBase.Instance.stage > 4)
            SceneManager.LoadScene("EndingScene"); //5 스테이지 이후에는 엔딩으로 이동
        else
            SceneManager.LoadScene("StageSelectScene"); //이전에는 다음 스테이지 선택화면으로 이동
    }
}
