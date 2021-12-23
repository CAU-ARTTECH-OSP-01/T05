using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using DG.Tweening;

public class TextEffect : MonoBehaviour
{
    Text text;
    private void Start() //엔딩 화면에서의 환경 관련 글귀
    {
        text = GetComponent<Text>();
        StartCoroutine(Typing());
    }

    IEnumerator Typing()
    {
        yield return new WaitForSeconds(0.5f);
        //text.DOText("자연은 '공존'을 말해야 하는 대상이 아니다.\n살아남기 위해 반드시 살펴야 할 우리의 보금자리이다.\n\n두 번째 지구는 없다_타일러 라쉬", 15.0f);
    }

}
