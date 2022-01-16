using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ĤH��D�㱱�
/// </summary>
public class EnemyPropControl : MonoBehaviour
{
    private GameManager gm;

    [Header("�C�����ʪ��Z��")]
    public float moveDistance = 2;
    [Header("���ʪ��y�Щ��u")]
    public float moveUnderLine = -2;
    [Header("�u�]���W��")]
    public string nameMarble;
    [Header("��q")]
    public float hp = 100;
    [Header("�O�_������")]
    public bool hasUI;

    private float hpMax;
    private Image imgHp;
    private Text textHp;

    private void Awake()
    {
        hpMax = hp;

        if (hasUI)
        { 
            imgHp = transform.Find("�e�����").Find("���").GetComponent<Image>();
            textHp = transform.Find("�e�����").Find("��q").GetComponent<Text>();
            textHp.text = hp.ToString();
        }

        gm = FindObjectOfType<GameManager>();
        gm.onEnemyTurn.AddListener(Move);
    }
    
    /// <summary>
    /// ����
    /// </summary>
    private void Move()
    {
        transform.position += Vector3.forward * moveDistance;
        if (transform.position.z >= moveUnderLine) DestroyObject();
    }

    /// <summary>
    /// �R������
    /// </summary>
    private void DestroyObject()
    {
        Destroy(gameObject);
    }

    private void Hurt(float damage)
    {
        hp -= damage;
        if (hasUI)
        { 
            imgHp.fillAmount = hp / hpMax;
            textHp.text = hp.ToString();
        }
        if (hp <= 0) Dead();
    }

    private void Dead()
    {
        Destroy(gameObject);

        if(gameObject.name.Contains("�u�]"))
        {
            ControlSystem.maxMarbles++;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Contains(nameMarble))
        {
            Hurt(collision.gameObject.GetComponent<Marble>().attack);
        }
    }

}
