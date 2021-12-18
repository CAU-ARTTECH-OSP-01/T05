using System; //Serializable을 사용하기 위해 using 선언
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI 관련 사용을 하기 위해 using 선언

[Serializable] //Inspector 창에서 보여주기 위해 사용
public enum CardComponent //속성에는 여러 가지 종류가 있고, 그 종류가 한정되어 있음. 그걸 하나의 변수로 사용하기 위해 enum 이용.
                          //Inspector 창에서 드롭다운 메뉴로 쉽게 접근 가능.
{
    ATK, SHD, HEL, SEL //필요할 때마다 추가해서 사용
}

[Serializable]
public class ComponentStats //속성에 맞는 값을 연결하기 위한 클래스, component 종류와 그에 맞는 값을 가지고 있음.
{
    public CardComponent component; //enum으로 만들었던 CardComponent를 가져옴.
    public int value; //CardComponent에 해당하는 value값
}

[Serializable]
public class CardStats //카드 정보를 받아주는 클래스, 전체적인 데이터 값을 가지고 있음.
{
    public int index;
    public string name;
    public int count;
    public int rare;
    public Sprite image;
    public List<ComponentStats> components; //위의 ComponentStats class를 리스트로 선언
}

public class CardInfo : MonoBehaviour //카드에 컴포넌트로 부착해서 사용
{
    public CardStats stats; //위의 CardStats 클래스 선언, Serializable로 Inspector 창에서 변수 변경 가능
                            //CardStats의 변수에 접근할 때는 stats.index 등으로 접근 가능

    public List<Sprite> icons; //카드 속성에 사용될 아이콘 이미지 리스트

    //SetCardComponents 함수에 사용될 변수들
    public Text txtName; //UnityEngine.UI 참조를 통해 Text 사용 가능
                         //변수로 선언함으로써 Inspector 창의 컴포넌트에서 접근할 수 있다.
    public Text txtCount;
    public GameObject componentContainer; //주사위의 속성 text에 접근하기 위해 사용, '주사위 속성' 오브젝트를 불러오기 위해 사용한다.
    public List<GameObject> components; //주사위 속성 > 속성(0), 속성(1)...에 접근하기 위해 사용

    void Init() //시작할 때 초기화해주기 위한 함수
    {
        for(int i = 0; i < componentContainer.transform.childCount; i++) //transform.childCount를 통해 자식 오브젝트의 갯수를 구할 수 있다.
                                                                         //주사위 속성의 자식 오브젝트인 속성(0)부터 속성(6)까지, 6의 값을 갖게 된다.
        {
            components.Add(componentContainer.transform.GetChild(i).gameObject);
            //transform.GetChild(i)를 통해 i번째 자식 오브젝트를 찾을 수 있다.
            //gameObject 형식의 자식 오브젝트를 리스트에 추가한다.
            //위에서 선언해 준 components 리스트에 i번째 자식 오브젝트를 추가해준다.
        }
    }

    public void SetCardInfo(Dictionary<string, string> _data) //카드에 데이터를 넣어주기 위한 SetCardInfo 전역 함수 선언.
                                                              //인자로는 DataReader_TSV를 통해 만들어 준 Dictonary 형태로 받도록 한다.
                                                              //Data 파일의 값들을 각 위치에 맞게 정리해준다.

                                                              //_data[카드 번호][키 값]으로 사용
                                                              //카드 번호는 다른 script에서 선언하고, 뒤의 키 값을 여기에서 정리해준다.
    {
        Init(); //SetCardInfo 시작할 때 초기화해준다.
        stats.components.Clear(); //components 리스트 안의 값을 먼저 Clear를 통해 비워준다.
        
        stats.index = int.Parse(_data["Index"]); //_data[키 값]으로 값을 받아온다.
                                                 //index 변수는 int 형식으로, 받아오는 _data는 string 형식이기 때문에 앞에 int.Parse으로 변환해준다.
        stats.name = _data["Name"];
        stats.count = int.Parse(_data["Count"]);
        stats.rare = int.Parse(_data["Rare"]);

        for (int i = 0; i < 6; i++) //6개의 속성을 넣어주기 위해 for문 사용
        {
            CardComponent _component = CardComponent.ATK; //enum인 CardComponent를 사용하기 위해 지역 변수로 선언
                                                          //아무것도 없을 때의 기본 값 하나를 할당해준다.

            if (_data["Component_" + i] == "ATK") //data 파일의 string 값과 enum 값을 비교해서 _component에 넣어준다.
                _component = CardComponent.ATK; //비교해서 같으면 _component에서 CardComponent의 enum 값을 선택해준다.
            else if (_data["Component_" + i] == "SHD")
                _component = CardComponent.SHD;
            else if (_data["Component_" + i] == "HEL")
                _component = CardComponent.HEL;
            else if (_data["Component_" + i] == "SEL")
                _component = CardComponent.SEL;

            ComponentStats _stats = new ComponentStats() //리스트를 감싸줄 ComponentStats를 지역 변수로 선언
            {
                component = _component, //위에서 받아온 _component 값을 ComponentStats의 component에 넣어준다.
                value = int.Parse(_data["Value_" + i]) //value 값에 data 파일의 Value_i 값을 넣어준다.
            };

            stats.components.Add(_stats); //위에서 만들어 준 6개의 _stats를 리스트 안에 넣어주는 작업
        }

        SetCardComponents(); //카드의 정보가 다 들어오고 난 후, 값을 게임 화면에 보여주도록 한다.
    }

    public void SetCardInfo(CardStats _stats) //위의 함수와 이름은 같으나, 인자를 CardStats _stats로 받는다.
    {
        Init();
        stats.components.Clear();

        stats = _stats; //받은 인자를 stats 변수에 넣어준다.

        SetCardComponents(); //카드의 정보를 게임 화면에 보여준다.
    }

    void SetCardComponents() //카드 오브젝트에 만들었던 값들을 넣어 보여주도록 하는 함수
    {
        txtName.text = stats.name; //Text의 text 컴포넌트에 접근해서 값을 넣어준다.
        txtCount.text = stats.count.ToString(); //text에는 string 값만 넣어줄 수 있고, count 값이 int 값이므로,
                                                //변환해주기 위해 ToString() 함수를 이용해준다.

        for(int i = 0; i < stats.components.Count; i++) //components 리스트의 갯수만큼 반복해준다.
        {
            Sprite _sprite = icons[0];
            if (stats.components[i].component == CardComponent.ATK) //components 리스트의 컴포넌트에 맞는 이미지를 넣어준다.
                _sprite = icons[0];
            else if (stats.components[i].component == CardComponent.SHD)
                _sprite = icons[1];
            else if (stats.components[i].component == CardComponent.HEL)
                _sprite = icons[2];
            else if (stats.components[i].component == CardComponent.SEL)
                _sprite = icons[3];

            components[i].transform.GetChild(0).GetComponent<Image>().sprite = _sprite; //넣어 준 이미지를 게임 화면에 보여준다.
            components[i].transform.GetChild(1).GetComponent<Text>().text = stats.components[i].value.ToString();
            //components 리스트의 i번째인 속성(i)에 들어있는 자식 오브젝트 중 2번째, 즉 txt 속성 값에 접근한다.
            //txt 속성 값에 접근한 후, Text 컴포넌트 안의 text에 값을 넣어 게임 화면에 보여줄 수 있도록 해준다.
            //stats의 components (이름 비슷하니 주의) 리스트의 i번째 개체의 value 값을 String으로 변환해서 넣어준다.
        }
    }

    public void CardUse() //카드가 사용되었을 때의 행동
    {
        stats.count--;

        //카드 사용
        int _rnd = UnityEngine.Random.Range(0, stats.components.Count); //컴포넌트 중 하나 랜덤으로 선택
        print("랜덤하게 뽑은 값 : " + _rnd + "\n랜덤하게 뽑은 컴포넌트 : " + stats.components[_rnd].component);

        switch (stats.components[_rnd].component) //고른 컴포넌트에 따라 행동하게 해준다.
        {
            case CardComponent.ATK:
                GameManager.Instance.Attack(stats.components[_rnd].value);
                break;
            case CardComponent.SHD:
                GameManager.Instance.Shield(stats.components[_rnd].value);
                break;
            case CardComponent.HEL:
                GameManager.Instance.Heal(stats.components[_rnd].value);
                break;
            case CardComponent.SEL:
                GameManager.Instance.Self(stats.components[_rnd].value);
                break;
        }

        SetCardComponents(); //count값이 변화한 내용을 카드에 적용

        if (stats.count <= 0)
        {
            print("카드 묘지로 이동"); //덱으로 이동하는 코드 참고해서 구현
            DeckManager.Instance.graveCardStats.Add(stats); //사용 횟수가 0이 되면 graveCardStats 리스트로 카드 이동
        }
        else
        {
            print("덱으로 이동");
            DeckManager.Instance.deckCardStats.Add(stats);
        }
        DeckManager.Instance.SetDeckCount(); //카드가 사용되었을 때 덱 버튼 텍스트 변경
        HandManager.Instance.WaitDeleteCard(); //카드가 사용되었을 때 위치 초기화

        Destroy(gameObject); //사용된 카드들은 게임 화면에서 제거한다. (덱으로 돌아가거나 묘지로 이동)
    }
}
