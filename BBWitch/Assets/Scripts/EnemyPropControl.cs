using UnityEngine;

public class EnemyPropControl : MonoBehaviour
{
    public GameManager gm;
    [Header("每次移動的距離")]
    public float moveDistance = 2;
    [Header("移動的座標底線")]
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
