using Unity.VisualScripting;
using UnityEngine;

class ItemHeal : ItemFather
{

    void Start()
    {
        _name = "healka";
        _price = (int)(60 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.damage;
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void Affect()
    {
        player.TakeDamage(-120);
    }
}