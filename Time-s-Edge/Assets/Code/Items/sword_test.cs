using Unity.VisualScripting;
using UnityEngine;

class sword_test : ItemFather
{

    void Start()
    {
        discriprion = "�������.����������� ����.";
        parameters = "���� - 60\n���� +1";
        lore = "������ ���� ������� ������ ������������ �������� ���� ���. ��� ���?";
        _name = "sword_test";
        _price = (int)(60 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.damage;
    }
    public override void Affect()
    {
        player.damage_Up(1);
    }
}