// Класс для вращения объекта
public class EathRotate : MonoBehaviour
{
    public float speed = 1; // Скорость вращения

    // Обновление вызывается один раз за кадр
    void Update()
    {
        // Вращение объекта вокруг своей оси по осям x, y и z
        transform.Rotate(0, 0, speed * Time.deltaTime);
    }
}