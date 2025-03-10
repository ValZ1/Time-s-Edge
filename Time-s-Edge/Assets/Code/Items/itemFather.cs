using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public enum Item_type //классификация необходима для подбора иконок + упрощает логистические приколы
{
    damage,  //предмет увеличивающий урон
    speed, //скорость передвежения
    atack_speed, //скорость атаки
    bullet_modificator, //всякие приколы в духе "с шансом n% выпускаете 2 снаряда вместо 1" и тд
    protection, //сопротивление урона и проч
    heal, //единоразовое лечение
    regeneration, //регенерация
    univertial, //предметы в духе (+1 урон +1 скорость атаки) и проч
    unique, //что то прям экстраординарное, требует координального изменения механики
    other //TODO  заполнить еще чем нить
}
abstract class ItemFather : MonoBehaviour
{
    public Player player;//у кого хп отнимать?
    public string _name; //имя предмета
    public int _price;  //цена предмета
    public Item_type _itemType; //тип предмета
    public double _difficulty_Price_Modificator = 1; //обсудить - нужно ли это вообще или нет/снять 1чку
    public double _PriceModificator = 1; //один и тот же предмет на 1 и на 10 этаже должны стоить по разному

    protected string discriprion = "Непонятно что";
    protected string parameters = "Непонятно что делает";
    protected string lore = "Никто не знает откуда";

    private bool _isPlayerInTrigger = false;
    //просто перечисление всего того, что может быть в описании предмета 
    /*
    //damage
    protected int _damage_Up;
    //
    protected int _speed_Up;
    //
    protected int _atack_Speed_Up;
    //
    protected int _momental_Heal;
    //
    protected int _regeneration_Up;
   */



    public TextMeshProUGUI Text;
    void Start()
    {
    }
    
    /// <summary>
    /// абстрактный метод, определяется для каждого объекта по своему. При применении вызывает желаемое воздействие на игрока
    /// </summary>
    public abstract void Affect();
    void Update()
    {
        if (_isPlayerInTrigger && player.get_CurHp() > _price && Input.GetKeyDown(KeyCode.E))
        {
            player.Buy(_price);
            Die();
            Affect();
        }
    }
    /// <summary>
    /// Уничтожает предмет
    /// </summary>
    private void Die(){ Destroy(gameObject); }

    /// <summary>
    /// Открывает текст, если игрок стоит в радиусе
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerStay2D(Collider2D collider)
    { 
        if (collider.gameObject.CompareTag("Player"))
        {
            Text.text = discriprion + "\n" + parameters + "\n" + lore;
            _isPlayerInTrigger = true;
        }
    }

    /// <summary>
    /// закрывает текст если игрок вышел из радиуса
    /// </summary>
    /// <param name="collider"></param>
    public void OnTriggerExit2D(Collider2D collider) 
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            Text.text = "";
            _isPlayerInTrigger = false;
        }      
    }

}

