using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public enum Item_type //������������� ���������� ��� ������� ������ + �������� ������������� �������
{
    damage,  //������� ������������� ����
    speed, //�������� ������������
    atack_speed, //�������� �����
    bullet_modificator, //������ ������� � ���� "� ������ n% ���������� 2 ������� ������ 1" � ��
    protection, //������������� ����� � ����
    heal, //������������ �������
    regeneration, //�����������
    univertial, //�������� � ���� (+1 ���� +1 �������� �����) � ����
    unique, //��� �� ���� ����������������, ������� �������������� ��������� ��������
    other //TODO  ��������� ��� ��� ����
}
abstract class Item : MonoBehaviour
{
    public Player player;//� ���� �� ��������?
    public string _name; //��� ��������
    public int _price;  //���� ��������
    public Item_type _itemType; //��� ��������
    public double _difficulty_Price_Modificator = 1; //�������� - ����� �� ��� ������ ��� ���/����� 1���
    public double _PriceModificator = 1; //���� � ��� �� ������� �� 1 � �� 10 ����� ������ ������ �� �������
    public Rigidbody2D _rb ;

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
   
    


    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    
    /// <summary>
    /// ����������� �����, ������������ ��� ������� ������� �� ������
    /// </summary>
    public abstract void Affect();
    
    /// <summary>
    /// ���������� �������
    /// </summary>
    private void Die()
    {
        Destroy(gameObject);
    }

    /// <summary>
    /// �������
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player player = collision.gameObject.GetComponent<Player>();
            if (player.get_CurHp() > _price) { 
                player.TakeDamage(_price);
                Die();
                Affect();
            }
        }
    }
}

