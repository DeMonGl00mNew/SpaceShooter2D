// Класс для возвращения объекта на сцену при выходе за границы
using System.Collections;
using UnityEngine;

public class ReturnToScene : MonoBehaviour
{
    private bool flagReplace = true; // Флаг для контроля возможности замены объекта
    private Rigidbody2D rb; // Ссылка на компонент Rigidbody2D у объекта
    Coroutine rePlaceStop; // Ссылка на корутину для остановки

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); // Получаем компонент Rigidbody2D при старте
    }

    private void OnDestroy()
    {
        // При уничтожении объекта останавливаем корутину
        if (rePlaceStop != null && CouratineManager.Instance != null)
            CouratineManager.Instance.StopCoroutine(rePlaceStop);
    }

    // При выходе объекта за границы триггера
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out SceneColider sceneColider))
            if (flagReplace && gameObject.activeSelf && CouratineManager.Instance != null)
                rePlaceStop = CouratineManager.Instance.StartCoroutine(rePlace());
    }

    // Корутина для возвращения объекта на сцену
    IEnumerator rePlace()
    {
        flagReplace = false; // Отключаем возможность повторной замены

        // Перемещаем объект на противоположную сторону
        transform.position = transform.position * -1;
        yield return new WaitForSeconds(0.3f); // Ждем некоторое время

        // Увеличиваем скорость объекта, если он не является игроком и его скорость меньше 1
        if (!gameObject.CompareTag("Player"))
            if (rb.velocity.magnitude < 1)
                rb.velocity *= 3;

        flagReplace = true; // Включаем возможность повторной замены
    }
}