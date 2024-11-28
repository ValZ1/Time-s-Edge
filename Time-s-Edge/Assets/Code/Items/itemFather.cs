using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using Unity.VisualScripting;
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
abstract class ItemFather : MonoBehaviour
{
    public Player player;//� ���� �� ��������?
    public string _name; //��� ��������
    public int _price;  //���� ��������
    public Item_type _itemType; //��� ��������
    public double _difficulty_Price_Modificator = 1; //�������� - ����� �� ��� ������ ��� ���/����� 1���
    public double _PriceModificator = 1; //���� � ��� �� ������� �� 1 � �� 10 ����� ������ ������ �� �������
    public Rigidbody2D _rb ;

    public Collider2D collider1;
    public Collider2D collider2;

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
    private void OnCollisionEnter2D(Collider2D collider1)
    {
        if (collider1.gameObject.CompareTag("Player"))
        {
            Player player = collider1.gameObject.GetComponent<Player>();
            if (player.get_CurHp() > _price) { 
                player.TakeDamage(_price);
                Die();
                Affect();
            }
        }
    }
   

    public void OnTriggerStay2D(Collider2D collider2)
    {
        Text.text = discriprion + "\n" + parameters + "\n" + lore;
    }
    public void OnTriggerExit2D(Collider2D collider2)
    {
        Text.text = "";
    }

}

