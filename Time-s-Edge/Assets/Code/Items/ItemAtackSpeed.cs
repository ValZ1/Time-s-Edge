using Unity.VisualScripting;
using UnityEngine;

class AtackSpeed : ItemFather
{

    protected override void Start()
    {
        base.Start();


        player = FindFirstObjectByType<Player>();

        discriprion = "Перчатка. Увеличивает скорострельность";
        parameters = "Цена - 40\nВыстрелов в секунду + 1";
        lore = "";
        _name = "perch";
        _price = (int)(40 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.atack_speed;
    }
    public override void Affect()
    {
        base.Affect();
        player._cooldownTime_redux(1.0f);
    }
}