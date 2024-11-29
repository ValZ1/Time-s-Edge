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
    public Rigidbody2D _rb ;

    public Collider2D collider_for_take;
    public Collider2D collider_for_info;


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
   
    protected string discriprion;
    protected string parameters;
    protected string lore;

    public TextMeshProUGUI Text;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    /// <summary>
    /// абстрактный метод, определяется для каждого объекта по своему. При применении вызывает желаемое воздействие на игрока
    /// </summary>
    public abstract void Affect();
    
    /// <summary>
    /// Уничтожает предмет
    /// </summary>
    private void Die(){ Destroy(gameObject); }

    /// <summary>
    /// Покупка
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collider_for_take)
    {
        if (collider_for_take.gameObject.CompareTag("Player"))
        {
            Player player = collider_for_take.gameObject.GetComponent<Player>();
            if (player.get_CurHp() > _price) { 
                player.TakeDamage(_price);
                Die();
                Affect();
            }
        }
    }
   
    /// <summary>
    /// Открывает текст, если игрок стоит в радиусе
    /// </summary>
    /// <param name="collider_for_info"></param>
    public void OnTriggerStay2D(Collider2D collider_for_info)
    { 
        if (collider_for_info.gameObject.CompareTag("Player")) 
            Text.text = discriprion + "\n" + parameters + "\n" + lore; 
    }

    /// <summary>
    /// закрывает текст если игрок вышел из радиуса
    /// </summary>
    /// <param name="collider_for_info"></param>
    public void OnTriggerExit2D(Collider2D collider_for_info) 
    {
        if (collider_for_info.gameObject.CompareTag("Player"))
            Text.text = "";           
    }

}

