using Unity.VisualScripting;
using UnityEngine;

class Healmet : ItemFather
{

    void Start()
    {
        discriprion = "Увеличивает защиту от урона.";
        parameters = "Цена - 40\nЗащита + 1";
        lore = "Простенький шлем, выглядит надежно.";
        _name = "Healmet";
        _price = (int)(40 * _difficulty_Price_Modificator * _PriceModificator);
        _itemType = Item_type.protection;
    }
    public override void Affect()
    {
        player.protection_Up(0.1); //пока что так, но необходимо будет как то пересмотреть эту тему
    }
}