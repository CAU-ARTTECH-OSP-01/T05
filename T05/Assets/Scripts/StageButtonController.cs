using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonController : MonoBehaviour
{
    public Text txt_stageSelect; //�������� �̵� ��ư�� Text

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
            txt_stageSelect.text = "����";
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
