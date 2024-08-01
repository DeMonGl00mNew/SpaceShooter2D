using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Параметр скорости пули. Значение можно задавать в инспекторе.
    public float Speed = 1;

    private void Update()
    {
        // Перемещаем пулю вверх на основе заданной скорости и времени, прошедшего с последнего кадра.
        // Vector3.up указывает направление движения (вверх).
        transform.Translate(Vector3.up * Speed * Time.deltaTime);
    }

    // Этот метод вызывается, когда объект становится невидимым для камеры.
    void OnBecameInvisible()
    {
        // Уничтожаем объект пули, чтобы избежать утечек памяти и лишней нагрузки на систему.
        Destroy(gameObject);
    }
}