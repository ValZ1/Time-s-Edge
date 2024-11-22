using Unity.VisualScripting;
using UnityEngine;

class sword_test : Item
{

    void Start()
    {
        _name = "sword_test";
        _price = (int)(60 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.damage;
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void Affect()
    {
        player.damage_Up(1);
    }
}