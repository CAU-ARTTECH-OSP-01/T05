using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    public int stage; //�������� ��ȣ
    public GameObject selectBG; //StageSelectScene ����� �����ϵ��� �Ѵ�.

    private void Start()
    {
        stage = DataBase.Instance.stage; //DataBase�� �������� ��ȣ�� �����ϸ鼭 �������ش�.
        if (stage > 4)
            stage = 0;
        StageSpawner();
    }

    public void StageSpawner()
    {
        selectBG.GetComponent<SpriteRenderer>().sprite = DataBase.Instance.stageSelectSceneList[stage];
    }
}
