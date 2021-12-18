using System; //Serializable�� ����ϱ� ���� using ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI ���� ����� �ϱ� ���� using ����

[Serializable] //Inspector â���� �����ֱ� ���� ���
public enum CardComponent //�Ӽ����� ���� ���� ������ �ְ�, �� ������ �����Ǿ� ����. �װ� �ϳ��� ������ ����ϱ� ���� enum �̿�.
                          //Inspector â���� ��Ӵٿ� �޴��� ���� ���� ����.
{
    ATK, SHD, HEL, SEL //�ʿ��� ������ �߰��ؼ� ���
}

[Serializable]
public class ComponentStats //�Ӽ��� �´� ���� �����ϱ� ���� Ŭ����, component ������ �׿� �´� ���� ������ ����.
{
    public CardComponent component; //enum���� ������� CardComponent�� ������.
    public int value; //CardComponent�� �ش��ϴ� value��
}

[Serializable]
public class CardStats //ī�� ������ �޾��ִ� Ŭ����, ��ü���� ������ ���� ������ ����.
{
    public int index;
    public string name;
    public int count;
    public int rare;
    public Sprite image;
    public List<ComponentStats> components; //���� ComponentStats class�� ����Ʈ�� ����
}

public class CardInfo : MonoBehaviour //ī�忡 ������Ʈ�� �����ؼ� ���
{
    public CardStats stats; //���� CardStats Ŭ���� ����, Serializable�� Inspector â���� ���� ���� ����
                            //CardStats�� ������ ������ ���� stats.index ������ ���� ����

    public List<Sprite> icons; //ī�� �Ӽ��� ���� ������ �̹��� ����Ʈ

    //SetCardComponents �Լ��� ���� ������
    public Text txtName; //UnityEngine.UI ������ ���� Text ��� ����
                         //������ ���������ν� Inspector â�� ������Ʈ���� ������ �� �ִ�.
    public Text txtCount;
    public GameObject componentContainer; //�ֻ����� �Ӽ� text�� �����ϱ� ���� ���, '�ֻ��� �Ӽ�' ������Ʈ�� �ҷ����� ���� ����Ѵ�.
    public List<GameObject> components; //�ֻ��� �Ӽ� > �Ӽ�(0), �Ӽ�(1)...�� �����ϱ� ���� ���

    void Init() //������ �� �ʱ�ȭ���ֱ� ���� �Լ�
    {
        for(int i = 0; i < componentContainer.transform.childCount; i++) //transform.childCount�� ���� �ڽ� ������Ʈ�� ������ ���� �� �ִ�.
                                                                         //�ֻ��� �Ӽ��� �ڽ� ������Ʈ�� �Ӽ�(0)���� �Ӽ�(6)����, 6�� ���� ���� �ȴ�.
        {
            components.Add(componentContainer.transform.GetChild(i).gameObject);
            //transform.GetChild(i)�� ���� i��° �ڽ� ������Ʈ�� ã�� �� �ִ�.
            //gameObject ������ �ڽ� ������Ʈ�� ����Ʈ�� �߰��Ѵ�.
            //������ ������ �� components ����Ʈ�� i��° �ڽ� ������Ʈ�� �߰����ش�.
        }
    }

    public void SetCardInfo(Dictionary<string, string> _data) //ī�忡 �����͸� �־��ֱ� ���� SetCardInfo ���� �Լ� ����.
                                                              //���ڷδ� DataReader_TSV�� ���� ����� �� Dictonary ���·� �޵��� �Ѵ�.
                                                              //Data ������ ������ �� ��ġ�� �°� �������ش�.

                                                              //_data[ī�� ��ȣ][Ű ��]���� ���
                                                              //ī�� ��ȣ�� �ٸ� script���� �����ϰ�, ���� Ű ���� ���⿡�� �������ش�.
    {
        Init(); //SetCardInfo ������ �� �ʱ�ȭ���ش�.
        stats.components.Clear(); //components ����Ʈ ���� ���� ���� Clear�� ���� ����ش�.
        
        stats.index = int.Parse(_data["Index"]); //_data[Ű ��]���� ���� �޾ƿ´�.
                                                 //index ������ int ��������, �޾ƿ��� _data�� string �����̱� ������ �տ� int.Parse���� ��ȯ���ش�.
        stats.name = _data["Name"];
        stats.count = int.Parse(_data["Count"]);
        stats.rare = int.Parse(_data["Rare"]);

        for (int i = 0; i < 6; i++) //6���� �Ӽ��� �־��ֱ� ���� for�� ���
        {
            CardComponent _component = CardComponent.ATK; //enum�� CardComponent�� ����ϱ� ���� ���� ������ ����
                                                          //�ƹ��͵� ���� ���� �⺻ �� �ϳ��� �Ҵ����ش�.

            if (_data["Component_" + i] == "ATK") //data ������ string ���� enum ���� ���ؼ� _component�� �־��ش�.
                _component = CardComponent.ATK; //���ؼ� ������ _component���� CardComponent�� enum ���� �������ش�.
            else if (_data["Component_" + i] == "SHD")
                _component = CardComponent.SHD;
            else if (_data["Component_" + i] == "HEL")
                _component = CardComponent.HEL;
            else if (_data["Component_" + i] == "SEL")
                _component = CardComponent.SEL;

            ComponentStats _stats = new ComponentStats() //����Ʈ�� ������ ComponentStats�� ���� ������ ����
            {
                component = _component, //������ �޾ƿ� _component ���� ComponentStats�� component�� �־��ش�.
                value = int.Parse(_data["Value_" + i]) //value ���� data ������ Value_i ���� �־��ش�.
            };

            stats.components.Add(_stats); //������ ����� �� 6���� _stats�� ����Ʈ �ȿ� �־��ִ� �۾�
        }

        SetCardComponents(); //ī���� ������ �� ������ �� ��, ���� ���� ȭ�鿡 �����ֵ��� �Ѵ�.
    }

    public void SetCardInfo(CardStats _stats) //���� �Լ��� �̸��� ������, ���ڸ� CardStats _stats�� �޴´�.
    {
        Init();
        stats.components.Clear();

        stats = _stats; //���� ���ڸ� stats ������ �־��ش�.

        SetCardComponents(); //ī���� ������ ���� ȭ�鿡 �����ش�.
    }

    void SetCardComponents() //ī�� ������Ʈ�� ������� ������ �־� �����ֵ��� �ϴ� �Լ�
    {
        txtName.text = stats.name; //Text�� text ������Ʈ�� �����ؼ� ���� �־��ش�.
        txtCount.text = stats.count.ToString(); //text���� string ���� �־��� �� �ְ�, count ���� int ���̹Ƿ�,
                                                //��ȯ���ֱ� ���� ToString() �Լ��� �̿����ش�.

        for(int i = 0; i < stats.components.Count; i++) //components ����Ʈ�� ������ŭ �ݺ����ش�.
        {
            Sprite _sprite = icons[0];
            if (stats.components[i].component == CardComponent.ATK) //components ����Ʈ�� ������Ʈ�� �´� �̹����� �־��ش�.
                _sprite = icons[0];
            else if (stats.components[i].component == CardComponent.SHD)
                _sprite = icons[1];
            else if (stats.components[i].component == CardComponent.HEL)
                _sprite = icons[2];
            else if (stats.components[i].component == CardComponent.SEL)
                _sprite = icons[3];

            components[i].transform.GetChild(0).GetComponent<Image>().sprite = _sprite; //�־� �� �̹����� ���� ȭ�鿡 �����ش�.
            components[i].transform.GetChild(1).GetComponent<Text>().text = stats.components[i].value.ToString();
            //components ����Ʈ�� i��°�� �Ӽ�(i)�� ����ִ� �ڽ� ������Ʈ �� 2��°, �� txt �Ӽ� ���� �����Ѵ�.
            //txt �Ӽ� ���� ������ ��, Text ������Ʈ ���� text�� ���� �־� ���� ȭ�鿡 ������ �� �ֵ��� ���ش�.
            //stats�� components (�̸� ����ϴ� ����) ����Ʈ�� i��° ��ü�� value ���� String���� ��ȯ�ؼ� �־��ش�.
        }
    }

    public void CardUse() //ī�尡 ���Ǿ��� ���� �ൿ
    {
        stats.count--;

        //ī�� ���
        int _rnd = UnityEngine.Random.Range(0, stats.components.Count); //������Ʈ �� �ϳ� �������� ����
        print("�����ϰ� ���� �� : " + _rnd + "\n�����ϰ� ���� ������Ʈ : " + stats.components[_rnd].component);

        switch (stats.components[_rnd].component) //�� ������Ʈ�� ���� �ൿ�ϰ� ���ش�.
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

        SetCardComponents(); //count���� ��ȭ�� ������ ī�忡 ����

        if (stats.count <= 0)
        {
            print("ī�� ������ �̵�"); //������ �̵��ϴ� �ڵ� �����ؼ� ����
            DeckManager.Instance.graveCardStats.Add(stats); //��� Ƚ���� 0�� �Ǹ� graveCardStats ����Ʈ�� ī�� �̵�
        }
        else
        {
            print("������ �̵�");
            DeckManager.Instance.deckCardStats.Add(stats);
        }
        DeckManager.Instance.SetDeckCount(); //ī�尡 ���Ǿ��� �� �� ��ư �ؽ�Ʈ ����
        HandManager.Instance.WaitDeleteCard(); //ī�尡 ���Ǿ��� �� ��ġ �ʱ�ȭ

        Destroy(gameObject); //���� ī����� ���� ȭ�鿡�� �����Ѵ�. (������ ���ư��ų� ������ �̵�)
    }
}
