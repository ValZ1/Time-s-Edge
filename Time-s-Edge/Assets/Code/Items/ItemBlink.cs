using Unity.VisualScripting;
using UnityEngine;

class ItemBlink : ItemFather
{
    protected override void Start()
    {
        base.Start();

        discriprion = "Позволяет совершать резкий рывок в сторону движения на клавишу Z. Перезарядка и дальность могут изменяться при наличии нескольких экземпляров.";
        parameters = "Цена - 40 \nбазовая перезарядка - 3 сек.\n уменьшение перезарядки - 10%\n дальность - 115\n увеличение дальности - 10\n ";
        lore = "Древний кинжал скованный из магических камней, кузнец, что сотворил его дал ему очень странное имя, которое никто уже и не вспомнит.";
        _name = "Blink Dugger";
        _price = (int)(40 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.unique;

    }
    public override void Affect()
    {
        base.Affect();
        player.blink_Up();
        player.blink_range_Up(1.0f);
        player.blink_cooldown_reduction(0.1f);
    }
}