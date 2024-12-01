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

    protected string discriprion = "��������� ���";
    protected string parameters = "��������� ��� ������";
    protected string lore = "����� �� ����� ������";

    private bool _isPlayerInTrigger = false;
    //������ ������������ ����� ����, ��� ����� ���� � �������� �������� 
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
    /// ����������� �����, ������������ ��� ������� ������� �� ������. ��� ���������� �������� �������� ����������� �� ������
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
    /// ���������� �������
    /// </summary>
    private void Die(){ Destroy(gameObject); }

    /// <summary>
    /// ��������� �����, ���� ����� ����� � �������
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
    /// ��������� ����� ���� ����� ����� �� �������
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

