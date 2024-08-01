// Класс для создания взрыва
// Когда объект создается, через 1 секунду он будет уничтожен
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 1);
    }

}