using Unity.VisualScripting;
using UnityEngine;

class NoDegenerationPill : ItemFather
{

    void Start()
    {
        discriprion = "��������� �������� ���� ������";
        parameters = " ";
        lore = "�� ������� �� ��� ���?"; //������� �� ����. ��������, ������� �� �������!!!
        _name = "Pill";
        _price = 0;
        _itemType = Item_type.unique;
    }
    public override void Affect()
    {
        player._timeBurner = 0;
    }
}