using Unity.VisualScripting;
using UnityEngine;

class ItemBlink : ItemFather
{
    void Start()
    {
        discriprion = "��������� ��������� ������ ����� � ������� �������� �� ������� Z. ����������� � ��������� ����� ���������� ��� ������� ���������� �����������.";
        parameters = "���� - 40 \n������� ����������� - 3 ���.\n ���������� ����������� - 10%\n ��������� - 115\n ���������� ��������� - 10\n ";
        lore = "������� ������ ��������� �� ���������� ������, ������, ��� �������� ��� ��� ��� ����� �������� ���, ������� ����� ��� � �� ��������.";
    _name = "Blink Dugger";
        _price = (int)(40 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.unique;
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void Affect()
    {
        player.blink_Up();
        player.blink_range_Up(1.0f);
        player.blink_cooldown_reduction(0.1f);
    }

    // Update is called once per frame
 
}