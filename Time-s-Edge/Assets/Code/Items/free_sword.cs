using Unity.VisualScripting;
using UnityEngine;

class Free_sword : ItemFather
{

    protected override void Start()
    {
        base.Start();


        player = FindFirstObjectByType<Player>();

        discriprion = "�������.����������� ����.";
        parameters = "���� - 0(���������)\n���� +1";
        lore = "������ ���� ������� ������ ������������ �������� ���� ���. ��� ���?";
        _name = "Free_sword";
        _price = (int)(0 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.damage;
    }
    public override void Affect()
    {
        base.Affect();
        player.damage_Up(1);
    }
}