using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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
abstract class Item : MonoBehaviour
{
    public Player player;//у кого хп отнимать?
    public string _name; //имя предмета
    public int _price;  //цена предмета
    public Item_type _itemType; //тип предмета
    public double _difficulty_Price_Modificator = 1; //обсудить - нужно ли это вообще или нет/снять 1чку
    public double _PriceModificator = 1; //один и тот же предмет на 1 и на 10 этаже должны стоить по разному
    public Rigidbody2D _rb ;

    // Start is called before the first frame update
    
    void Start()
    {
        //_price = 3;
        _rb = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    
    void Update()
    {
        //Временно убрал, пока не требуется, вычисление дистанции до игрока
        //var distanceToPlayer = Vector2.Distance(_player.position, transform.position);
        //if (distanceToPlayer < DistanceChase)
        //{
        
        //}
    }
    public abstract void Affect();
    
    private void Die()
    {
        Destroy(gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.get_CurHp() > _price) { 
                player.TakeDamage(_price);
                Affect();
            }
            Die();
        }
    }
}

