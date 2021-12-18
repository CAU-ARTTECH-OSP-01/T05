using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageSelectController : MonoBehaviour
{
    public int stage; //스테이지 번호
    public GameObject selectBG; //StageSelectScene 배경을 생성하도록 한다.

    private void Start()
    {
        stage = DataBase.Instance.stage; //DataBase의 스테이지 번호를 시작하면서 설정해준다.
        if (stage > 4)
            stage = 0;
        StageSpawner();
    }

    public void StageSpawner()
    {
        selectBG.GetComponent<SpriteRenderer>().sprite = DataBase.Instance.stageSelectSceneList[stage];
    }
}
