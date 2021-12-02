using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UnityEngine.UI Using 선언

public class ImageFiller : MonoBehaviour
{
    private Image image; //image 변수 선언

    void Start()
    {
        image = GetComponent<Image>(); //Image 컴포넌트를 가져옴
    }

    float timer = 0f; //timer 변수 선언
    void Update() //시간에 따라서 Sin 그래프 형태로 1~0 사이를 왔다갔다 할 수 있도록 함
    {
        timer += Time.deltaTime;
        image.fillAmount = Mathf.Sin(timer) * 0.5f + 0.5f; //Image 타입으로 만들어진 image 변수에서 fillAmount로 접근하면 Image 컴포넌트의 Fill Amount 값을 조절할 수 있게 된다.
    }

}
