// Класс для обработки касания метеорита
public class TouchMeteorite : MonoBehaviour
{
    public Meteorit meteorit; // Ссылка на скрипт метеорита
    bool _stateDontShoot = false; // Переменная для хранения состояния "не стрелять"

    [HideInInspector] // Скрыть переменную из инспектора
    public bool StateDontShootPropety // Свойство для доступа к переменной _stateDontShoot
    {
        get => _stateDontShoot; // Получение значения
        set // Установка значения
        {
            _stateDontShoot = value; // Установка значения
            meteorit.StateDontShoot = _stateDontShoot; // Присваивание значения метеориту
        }
    }
}