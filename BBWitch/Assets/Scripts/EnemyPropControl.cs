using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// �ĤH��D�㱱�
/// </summary>
public class EnemyPropControl : MonoBehaviour
{
    private GameManager gm;

    [Header("�C�����ʪ��Z��")]
    public float moveDistance = 3.2f;
    [Header("���ʪ��y�Щ��u")]
    public float moveUnderLine = 0.5f;
    [Header("�u�]���W��")]
    public string nameMarble;
    [Header("��q")]
    public float hp = 100;
    [Header("�O�_������")]
    public bool hasUI;

    [SerializeField] private float hpMax;
    [SerializeField] private Image imgHp;
    [SerializeField] private Text textHp;


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
        transform.position += Vector3.right * moveDistance;
        gm.SwitchTurn(true);
        if (transform.position.x >= moveUnderLine)
        {
            FindObjectOfType<ControlSystem>().GameOver(false);
            DestroyObject();
        }
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

        if (!gameObject.name.Contains("�u�]"))
        {
            FindObjectOfType<ControlSystem>().AddScore();
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
