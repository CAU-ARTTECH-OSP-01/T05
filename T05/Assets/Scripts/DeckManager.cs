using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour //덱 버튼에 컴포넌트로 넣어준다.
{
    private static DeckManager instance; //이 게임 씬에서 단 하나만 존재하기 때문에, Singleton 패턴으로 만들어준다.
                                         //다른 곳에서 상속시켜 주지 않더라도 사용할 수 있는 방식이다.
    public static DeckManager Instance
    {
        get { return instance; } //값을 얻었을 때 instance로 반환
        set { instance = value; } //value 값을 얻었을 때 instance로 넣어준다.
    }

    private void Awake() //start보다 전에 실행되는 함수
    {
        instance = this;
    }

    public List<int> deckList; //가지고 있는 카드의 종류와 갯수를 리스트에 넣어준다.
    
}
