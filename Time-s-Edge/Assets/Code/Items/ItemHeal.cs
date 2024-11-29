using Unity.VisualScripting;
using UnityEngine;

class ItemHeal : ItemFather
{

    void Start()
    {
        discriprion = "Лечит";
        parameters = "Цена - 0 \nЛечение - 120";
        lore = "Вкуснейшая 'Курочка острая', но как она тут оказалась?";
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