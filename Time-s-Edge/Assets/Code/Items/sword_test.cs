using Unity.VisualScripting;
using UnityEngine;

class sword_test : ItemFather
{

    void Start()
    {
        discriprion = "Молоток.Увеличивает урон.";
        parameters = "Цена - 60\nУрон +1";
        lore = "Именно этим молотом Родион Раскольников совершил дабл кил. Или нет?";
        _name = "sword_test";
        _price = (int)(60 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.damage;
    }
    public override void Affect()
    {
        player.damage_Up(1);
    }
}