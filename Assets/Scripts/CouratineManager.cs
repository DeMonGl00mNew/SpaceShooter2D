// Класс для управления корутинами
public class CouratineManager : MonoBehaviour
{
    // Статическая переменная для доступа к экземпляру класса
    static public CouratineManager Instance { get; private set; }

    // Метод, вызывающийся при старте объекта
    void Awake()
    {
        // Если экземпляр класса еще не создан
        if (Instance == null)
        {
            // Создаем экземпляр и не уничтожаем его при смене сцены
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        // Если уже существует экземпляр класса
        else if (Instance != this)
        {
            // Уничтожаем текущий объект
            Destroy(gameObject);
        }

    }
}