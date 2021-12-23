using System; //Serializable�� ����ϱ� ���� using ����
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //UI ���� ����� �ϱ� ���� using ����

[Serializable] //Inspector â���� �����ֱ� ���� ���
public enum __CardComponent //�Ӽ����� ���� ���� ������ �ְ�, �� ������ �����Ǿ� ����. �װ� �ϳ��� ������ ����ϱ� ���� enum �̿�.
                            //Inspector â���� ��Ӵٿ� �޴��� ���� ���� ����.
{
    ATK, SHD, HEL, SEL //�ʿ��� ������ �߰��ؼ� ���
}

[Serializable]
public class __ComponentStats //�Ӽ��� �´� ���� �����ϱ� ���� Ŭ����, component ������ �׿� �´� ���� ������ ����.
{
    public __CardComponent __component; //enum���� ������� CardComponent�� ������.
    public int __value; //CardComponent�� �ش��ϴ� value��
}

[Serializable]
public class __CardStats //ī�� ������ �޾��ִ� Ŭ����, ��ü���� ������ ���� ������ ����.
{
    public int __index;
    public string __name;
    public int __count;
    public int __rare;
    public Sprite __image;
    public List<__ComponentStats> __components; //���� ComponentStats class�� ����Ʈ�� ����
}

public class Popup_CardInfo : MonoBehaviour //ī�忡 ������Ʈ�� �����ؼ� ���
{
    public __CardStats __stats; //���� CardStats Ŭ���� ����, Serializable�� Inspector â���� ���� ���� ����
                            //CardStats�� ������ ������ ���� stats.index ������ ���� ����

    public List<Sprite> __icons; //ī�� �Ӽ��� ���� ������ �̹��� ����Ʈ

    //SetCardComponents �Լ��� ���� ������
    public Text __txtName; //UnityEngine.UI ������ ���� Text ��� ����
                         //������ ���������ν� Inspector â�� ������Ʈ���� ������ �� �ִ�.
    public Text __txtCount;
    public Image __cardImage;
    public GameObject __componentContainer; //�ֻ����� �Ӽ� text�� �����ϱ� ���� ���, '�ֻ��� �Ӽ�' ������Ʈ�� �ҷ����� ���� ����Ѵ�.
    public List<GameObject> __components; //�ֻ��� �Ӽ� > �Ӽ�(0), �Ӽ�(1)...�� �����ϱ� ���� ���


    void Init() //������ �� �ʱ�ȭ���ֱ� ���� �Լ�
    {
        for (int i = 0; i < __componentContainer.transform.childCount; i++) //transform.childCount�� ���� �ڽ� ������Ʈ�� ������ ���� �� �ִ�.
                                                                          //�ֻ��� �Ӽ��� �ڽ� ������Ʈ�� �Ӽ�(0)���� �Ӽ�(6)����, 6�� ���� ���� �ȴ�.
        {
            __components.Add(__componentContainer.transform.GetChild(i).gameObject);
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
        __stats.__components.Clear(); //components ����Ʈ ���� ���� ���� Clear�� ���� ����ش�.

        __stats.__index = int.Parse(_data["Index"]); //_data[Ű ��]���� ���� �޾ƿ´�.
                                                 //index ������ int ��������, �޾ƿ��� _data�� string �����̱� ������ �տ� int.Parse���� ��ȯ���ش�.
        __stats.__name = _data["Name"];
        __stats.__count = int.Parse(_data["Count"]);
        __stats.__rare = int.Parse(_data["Rare"]);

        for (int i = 0; i < 6; i++) //6���� �Ӽ��� �־��ֱ� ���� for�� ���
        {
            __CardComponent _component = __CardComponent.ATK; //enum�� CardComponent�� ����ϱ� ���� ���� ������ ����
                                                          //�ƹ��͵� ���� ���� �⺻ �� �ϳ��� �Ҵ����ش�.

            if (_data["Component_" + i] == "ATK") //data ������ string ���� enum ���� ���ؼ� _component�� �־��ش�.
                _component = __CardComponent.ATK; //���ؼ� ������ _component���� CardComponent�� enum ���� �������ش�.
            else if (_data["Component_" + i] == "SHD")
                _component = __CardComponent.SHD;
            else if (_data["Component_" + i] == "HEL")
                _component = __CardComponent.HEL;
            else if (_data["Component_" + i] == "SEL")
                _component = __CardComponent.SEL;

            __ComponentStats _stats = new __ComponentStats() //����Ʈ�� ������ ComponentStats�� ���� ������ ����
            {
                __component = _component, //������ �޾ƿ� _component ���� ComponentStats�� component�� �־��ش�.
                __value = int.Parse(_data["Value_" + i]) //value ���� data ������ Value_i ���� �־��ش�.
            };

            __stats.__components.Add(_stats); //������ ����� �� 6���� _stats�� ����Ʈ �ȿ� �־��ִ� �۾�
        }

        SetCardComponents(); //ī���� ������ �� ������ �� ��, ���� ���� ȭ�鿡 �����ֵ��� �Ѵ�.
    }

    public void SetCardInfo(__CardStats _stats) //���� �Լ��� �̸��� ������, ���ڸ� CardStats _stats�� �޴´�.
    {
        Init();
        __stats.__components.Clear();

        __stats = _stats; //���� ���ڸ� stats ������ �־��ش�.

        SetCardComponents(); //ī���� ������ ���� ȭ�鿡 �����ش�.
    }

    void SetCardComponents() //ī�� ������Ʈ�� ������� ������ �־� �����ֵ��� �ϴ� �Լ�
    {
        __txtName.text = __stats.__name; //Text�� text ������Ʈ�� �����ؼ� ���� �־��ش�.
        __txtCount.text = __stats.__count.ToString(); //text���� string ���� �־��� �� �ְ�, count ���� int ���̹Ƿ�,
                                                      //��ȯ���ֱ� ���� ToString() �Լ��� �̿����ش�.

        __cardImage.sprite = __stats.__image;

        for (int i = 0; i < __stats.__components.Count; i++) //components ����Ʈ�� ������ŭ �ݺ����ش�.
        {
            Sprite _sprite = __icons[0];
            if (__stats.__components[i].__component == __CardComponent.ATK) //components ����Ʈ�� ������Ʈ�� �´� �̹����� �־��ش�.
                _sprite = __icons[0];
            else if (__stats.__components[i].__component == __CardComponent.SHD)
                _sprite = __icons[1];
            else if (__stats.__components[i].__component == __CardComponent.HEL)
                _sprite = __icons[2];
            else if (__stats.__components[i].__component == __CardComponent.SEL)
                _sprite = __icons[3];

            __components[i].transform.GetChild(0).GetComponent<Image>().sprite = _sprite; //�־� �� �̹����� ���� ȭ�鿡 �����ش�.
            __components[i].transform.GetChild(1).GetComponent<Text>().text = __stats.__components[i].__value.ToString();
            //components ����Ʈ�� i��°�� �Ӽ�(i)�� ����ִ� �ڽ� ������Ʈ �� 2��°, �� txt �Ӽ� ���� �����Ѵ�.
            //txt �Ӽ� ���� ������ ��, Text ������Ʈ ���� text�� ���� �־� ���� ȭ�鿡 ������ �� �ֵ��� ���ش�.
            //stats�� components (�̸� ����ϴ� ����) ����Ʈ�� i��° ��ü�� value ���� String���� ��ȯ�ؼ� �־��ش�.
        }
    }
}