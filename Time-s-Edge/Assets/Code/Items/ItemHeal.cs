using Unity.VisualScripting;
using UnityEngine;

class ItemHeal : ItemFather
{

    protected override void Start()
    {
        base.Start();
        discriprion = "Лечит";
        parameters = "Цена - 0 \nЛечение - 120";
        lore = "Вкуснейшая 'Курочка острая', но как она тут оказалась?"; //отсылка на студ. столовую, курочку не трогать!!!
        _name = "healka";
        _price = 0;
        _itemType = Item_type.damage;
    }
    public override void Affect()
    {
        player.Heal(120);
    }
}