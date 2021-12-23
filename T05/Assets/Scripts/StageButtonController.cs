using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButtonController : MonoBehaviour
{
    public Text txt_stageSelect; //�¸� �� ������ �������� �̵� ��ư�� Text

    public GameObject GetCardButton;
    public GameObject DeleteCardButton;

    private void Awake()
    {
        ButtonControl();
    }

    public void ButtonControl()
    {
        if (DataBase.Instance.stage > 4) //5 �������� ���Ŀ��� �������� �̵�
        {
            txt_stageSelect.text = "����";
            GetCardButton.SetActive(false);
            DeleteCardButton.SetActive(false);
        }
    }

    public void onClick()
    {
        GameManager.Instance.player_Victory.SetActive(false);
        if (DataBase.Instance.stage > 4)
            SceneManager.LoadScene("EndingScene"); //5 �������� ���Ŀ��� �������� �̵�
        else
            SceneManager.LoadScene("StageSelectScene"); //�������� ���� �������� ����ȭ������ �̵�
    }
}
