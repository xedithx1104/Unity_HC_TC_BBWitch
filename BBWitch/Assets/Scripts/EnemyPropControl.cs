using UnityEngine;

public class EnemyPropControl : MonoBehaviour
{
    public GameManager gm;
    [Header("�C�����ʪ��Z��")]
    public float moveDistance = 2;
    [Header("���ʪ��y�Щ��u")]
    public float moveUnderLine = -2;

    private void Move()
    {
        transform.position += Vector3.forward * moveDistance;
        if (transform.position.z <= moveUnderLine) DestroyObject();
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
