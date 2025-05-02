using Unity.VisualScripting;
using UnityEngine;

class ItemBlink : ItemFather
{
    protected override void Start()
    {
        base.Start();

        discriprion = "��������� ��������� ������ ����� � ������� �������� �� ������� Z. ����������� � ��������� ����� ���������� ��� ������� ���������� �����������.";
        parameters = "���� - 40 \n������� ����������� - 3 ���.\n ���������� ����������� - 10%\n ��������� - 115\n ���������� ��������� - 10\n ";
        lore = "������� ������ ��������� �� ���������� ������, ������, ��� �������� ��� ��� ��� ����� �������� ���, ������� ����� ��� � �� ��������.";
        _name = "Blink Dugger";
        _price = (int)(40 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.unique;

    }
    public override void Affect()
    {
        base.Affect();
        player.blink_Up();
        player.blink_range_Up(1.0f);
        player.blink_cooldown_reduction(0.1f);
    }
}