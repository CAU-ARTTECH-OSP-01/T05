using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonTester : MonoBehaviour
{
    Button button; //Button 변수 선언

    public void OnclickButton()
    {
        Debug.Log("Button Click!");
    }

    void Start()
    {
        button = GetComponent<Button>(); //GetComponent로 게임 오브젝트에 부착된 Button 컴포넌트를 가져온다.
        button.onClick.AddListener(OnclickButton); // AddListener에 함수를 넣어 호출되도록 한다.
    }

    void Update()
    {
        
    }
}
