using Unity.VisualScripting;
using UnityEngine;

class ItemHeal : ItemFather
{

    protected override void Start()
    {
        base.Start();
        discriprion = "�����";
        parameters = "���� - 0 \n������� - 120";
        lore = "���������� '������� ������', �� ��� ��� ��� ���������?"; //������� �� ����. ��������, ������� �� �������!!!
        _name = "healka";
        _price = 0;
        _itemType = Item_type.damage;
    }
    public override void Affect()
    {
        player.Heal(120);
    }
}