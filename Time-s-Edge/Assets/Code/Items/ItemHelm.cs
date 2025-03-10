using Unity.VisualScripting;
using UnityEngine;

class Healmet : ItemFather
{

    void Start()
    {
        discriprion = "����������� ������ �� �����.";
        parameters = "���� - 40\n������ + 1";
        lore = "����������� ����, �������� �������.";
        _name = "Healmet";
        _price = (int)(40 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.protection;
    }
    public override void Affect()
    {
        player.protection_Up(0.1); //���� ��� ���, �� ���������� ����� ��� �� ������������ ��� ����
    }
}