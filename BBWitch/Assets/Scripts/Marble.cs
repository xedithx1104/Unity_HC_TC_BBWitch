using UnityEngine;

public class Marble : MonoBehaviour
{
    public float attack;

    private void Awake()
    {
        // ª«²z.©¿²¤¶î¼h¸I¼²(A¹Ï¼h,B¹Ï¼h)©¿²¤ A B ¹Ï¼h¸I¼²
        Physics.IgnoreLayerCollision(6, 6);

    }
}
