using UnityEngine;

public class Marble : MonoBehaviour
{
    public float attack;

    private void Awake()
    {
        // ���z.������h�I��(A�ϼh,B�ϼh)���� A B �ϼh�I��
        Physics.IgnoreLayerCollision(6, 6);

    }
}
