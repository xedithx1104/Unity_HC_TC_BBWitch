using UnityEngine;

public class Marble : MonoBehaviour
{
    private void Awake()
    {
        Physics.IgnoreLayerCollision(6, 6);

    }
}
