using Unity.VisualScripting;
using UnityEngine;

class sword_test : ItemFather
{

    protected override void Start()
    {
        base.Start();


        player = FindFirstObjectByType<Player>();

        discriprion = "�������.����������� ����.";
        parameters = "���� - 60\n���� +1";
        lore = "������ ���� ������� ������ ������������ �������� ���� ���. ��� ���?";
        _name = "sword_test";
        _price = (int)(60 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.damage;
    }
    public override void Affect()
    {
        base.Affect();
        player.damage_Up(1);
    }
}