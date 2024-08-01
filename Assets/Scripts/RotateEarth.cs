// Класс, отвечающий за вращение Земли
public class RotateEarth : MonoBehaviour
{
    // Скорость вращения
    public float speedRotate = 10f;

    // Обновление каждый кадр
    void Update()
    {
        // Вращаем объект вокруг оси Z с определенной скоростью
        transform.Rotate(0, 0, 10 * Time.deltaTime * speedRotate);
    }
}