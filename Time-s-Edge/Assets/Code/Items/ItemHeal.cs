using Unity.VisualScripting;
using UnityEngine;

class ItemHeal : ItemFather
{

    void Start()
    {
        discriprion = "�����";
        parameters = "���� - 0 \n������� - 120";
        lore = "���������� '������� ������', �� ��� ��� ��� ���������?";
        _name = "healka";
        _price = 0;
        _itemType = Item_type.damage;
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void Affect()
    {
        player.TakeDamage(-120);
    }
}