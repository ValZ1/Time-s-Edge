using Unity.VisualScripting;
using UnityEngine;

class Free_sword : ItemFather
{

    protected override void Start()
    {
        base.Start();


        player = FindFirstObjectByType<Player>();

        discriprion = "Молоток.Увеличивает урон.";
        parameters = "Цена - 0(бесплатно)\nУрон +1";
        lore = "Именно этим молотом Родион Раскольников совершил дабл кил. Или нет?";
        _name = "Free_sword";
        _price = (int)(0 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.damage;
    }
    public override void Affect()
    {
        base.Affect();
        player.damage_Up(1);
    }
}