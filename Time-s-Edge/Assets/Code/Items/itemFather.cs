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
public abstract class ItemFather : MonoBehaviour
{
    [SerializeField] protected Player player;//� ���� �� ��������?
    private ItemInfoDisplay _display;

    public string _name; //��� ��������

    [SerializeField] protected int _price;  //���� ��������
    public Item_type _itemType; //��� ��������
    public double _difficulty_Price_Modificator = 1; //�������� - ����� �� ��� ������ ��� ���/����� 1���
    public double _PriceModificator = 1; //���� � ��� �� ������� �� 1 � �� 10 ����� ������ ������ �� �������

    public string discriprion = "��������� ���";
    public string parameters = "��������� ��� ������";
    public string lore = "����� �� ����� ������";

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
    protected virtual void Start()
    {
        player = FindFirstObjectByType<Player>();
        _display = player.GetComponent<ItemInfoDisplay>();
    }

    /// <summary>
    /// ����������� �����, ������������ ��� ������� ������� �� ������. ��� ���������� �������� �������� ����������� �� ������
    /// </summary>
    public abstract void Affect();
    void Update()
    {
        //Debug.Log(player.get_CurHp());
        if (_isPlayerInTrigger)
            if (player.get_CurHp() > _price ){
                if (Input.GetKeyDown(KeyCode.E)) { 
               
                {
                    player.Buy(_price);
                    Die();
                    Affect();
                    }
                }
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




    private void OnTriggerStay2D(Collider2D other)
    {
        
            if (player != null && player.get_CurHp() > _price && Input.GetKeyDown(KeyCode.E))
            {
                player.Buy(_price);
                Affect();
                ItemInfoDisplay.Instance.HideInfo();
                Destroy(gameObject);
            }
        
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
           
            _isPlayerInTrigger = true;
            _display.ShowItemInfo(this);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            _isPlayerInTrigger = false;
            _display.HideInfo();
        }
    }
}

