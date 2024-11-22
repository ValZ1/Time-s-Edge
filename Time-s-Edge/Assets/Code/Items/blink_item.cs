using Unity.VisualScripting;
using UnityEngine;

class bleank_item : Item
{
    void Start()
    {
        _name = "Blink Dugger";
        _price = (int)(40 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.unique;
        _rb = GetComponent<Rigidbody2D>();
    }
    public override void Affect()
    {
        player.blink_Up();
        player.blink_range_Up(0.4f);
        player.blink_cooldown_reduction(0.1f);
    }

    // Update is called once per frame
 
}