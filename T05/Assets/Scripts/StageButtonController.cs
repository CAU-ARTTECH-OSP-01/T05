using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonController : MonoBehaviour
{
    public Text txt_stageSelect; //스테이지 이동 버튼의 Text

    public GameObject GetCardButton;
    public GameObject DeleteCardButton;

    private void Awake()
    {
        ButtonControl();
    }

    public void ButtonControl()
    {
        if (DataBase.Instance.stage > 4)
        {
            txt_stageSelect.text = "엔딩";
            GetCardButton.SetActive(false);
            DeleteCardButton.SetActive(false);
        }
    }

    public void onClick()
    {
        if (DataBase.Instance.stage > 4)
            SceneManager.LoadScene("EndingScene");
        else
            SceneManager.LoadScene("StageSelectScene");
    }
}
