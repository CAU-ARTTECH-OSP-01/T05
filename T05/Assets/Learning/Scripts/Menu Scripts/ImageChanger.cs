using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageChanger : MonoBehaviour
{
    private Image image; //Image 컴포넌트를 담고 있을 image 변수 선언

    [SerializeField] //Sprite 배열을 Inspector 뷰에서 채워주기 위해 SerializedField 어트리뷰트를 적용
    // [SerializeField]는 선언하게 되면 Inspector에서는 보이지만, Public처럼 다른 클래스에서는 접근 할 수 없다.
    private Sprite[] sprites; //차례대로 변경될 Sprite들을 담고 있을 Sprite 배열 선언

    private int index; //지금 어떤 이미지를 보여주고 있는지를 정할 index 변수 선언

    void Start()
    {
        image = GetComponent<Image>(); // 게임 오브젝트에 부착되어 있는 Image 컴포넌트를 가져옴
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //image.sprite를 스페이스 키를 누를 때마다 sprites 배열의 index 번째 이미지로 바꾸도록 코드 작성
        {
            image.sprite = sprites[index]; //Image 컴포넌트의 Source Image는 스크립트에서 image.sprite로 접근하면 변경 할 수 있다.
            index++; //1씩 증가시켜 다음 스페이스 키에서 다음 이미지가 나오도록 함
            if(sprites.Length == index)
            {
                index = 0; // index 값이 sprites가 가진 이미지 수를 넘어가게 되면 오류가 발생하므로 방지를 위해 배열 길이와 index 값이 같아지면 0으로 바꿔준다.
            }
        }
        
    }
}
