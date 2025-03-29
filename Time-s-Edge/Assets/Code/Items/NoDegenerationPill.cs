using Unity.VisualScripting;
using UnityEngine;

class NoDegenerationPill : ItemFather
{

    void Start()
    {
        discriprion = "Полностью исцеляет ваши травмы";
        parameters = " ";
        lore = "Но устроит ли вас это?"; //отсылка на студ. столовую, курочку не трогать!!!
        _name = "Pill";
        _price = 0;
        _itemType = Item_type.unique;
    }
    public override void Affect()
    {
        player._timeBurner = 0;
    }
}