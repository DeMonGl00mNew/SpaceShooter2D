// Класс для управления метеоритами
// Содержит логику для движения, столкновений и уничтожения метеоритов

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class Meteorit : MonoBehaviour
{
    private Rigidbody2D rb; // Компонент Rigidbody2D для управления физикой объекта
    private int sideOfSpawn = 0; // Сторона спауна метеорита
    public GameObject EffectSelected; // Эффект при попадании по метеориту
    public Transform transformCenter; // Центр, относительно которого спаунится метеорит
    public string Letter { get { return letter; } set { letter = value; MountingLetter(); } } // Буква на метеорите
    [HideInInspector] public bool Zagrugen = false; // Флаг загрузки
    [HideInInspector] public bool NaStancii = false; // Флаг столкновения с космической станцией
    [HideInInspector] public bool WithEffect = false; // Флаг наличия эффекта
    public TMP_Text LetterText; // Текст буквы на метеорите
    private string letter; // Сама буква

    [HideInInspector] public event Action<Meteorit> StateDontShootEvent; // Событие при наличии состояния "не стрелять"
    private bool _stateDontShoot = false; // Состояние "не стрелять"

    // Метод для установки буквы на метеорите
    public void MountingLetter()
    {
        LetterText.text = letter.ToUpper(); // Установка текста на метеорите
        gameObject.name = letter; // Установка имени объекта как буквы
    }

    // Свойство для управления состоянием "не стрелять"
    public bool StateDontShoot
    {
        get { return _stateDontShoot; }
        set { _stateDontShoot = value; StateDontShootEvent?.Invoke(this); }
    }

    void Start()
    {
        _stateDontShoot = false; // Инициализация состояния "не стрелять"
        StateDontShootEvent += Shinning; // Подписка на событие
        Vector3 target;
        Vector2 sizeScreen = SceneColider.Instance.SizeScreen() * Spawner.Instance.RangeForMeteorits; // Получение размеров экрана
        target = transformCenter.position - transform.position; // Вычисление направления к центру

        rb = GetComponent<Rigidbody2D>(); // Получение компонента Rigidbody2D

        // Установка скорости метеорита в зависимости от состояния туториала или сцены
        if (Tutorial.StateTutorial >= 6 || Spawner.scene.name == "Game")
        {
            if (sideOfSpawn == 1)
                rb.velocity = new Vector2(target.x, target.y + Random.Range(-sizeScreen.y, sizeScreen.y)).normalized; // Спавн сверху
            if (sideOfSpawn == 2)
                rb.velocity = new Vector2(target.x + Random.Range(-sizeScreen.x, sizeScreen.x), target.y).normalized; // Спавн слева
        }

        // Установка угловой скорости для вращения метеорита
        if (Tutorial.StateTutorial == 0 || Tutorial.StateTutorial > 5) rb.angularVelocity = Random.Range(-30f, 30f);
    }

    private void OnDestroy()
    {
        Spawner.Instance.SpawnMeteoritTutorial(); // Спавн метеорита в туториале при уничтожении
        StateDontShootEvent -= Shinning; // Отписка от события
    }

    // Метод для установки стороны спауна метеорита
    public void SideForSpawn(int side, Transform where)
    {
        transformCenter = where; // Установка центра спауна
        sideOfSpawn = side; // Установка стороны спауна
    }

    // Метод для обработки столкновений
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Zagrugen || NaStancii) // Проверка флагов
            return;

        // Если столкновение с пулей
        if (other.TryGetComponent(out Bullet bullet))
        {
            if (Tutorial.StateTutorial == 6)
            {
                Tutorial.StateTutorial = 7; // Переход к следующему этапу туториала
            }
            Destroy(other.gameObject); // Уничтожение пули
            Spawner.Instance.SpawnBonus(transform); // Спавн бонуса
            WhenDestroy(); // Уничтожение метеорита
        }
        // Если столкновение с игроком
        else if (other.TryGetComponent(out Player player))
        {
            if (!player.neuyzvimost) // Проверка на неуязвимость игрока
            {
                player.NeuyzvimostFunc(); // Применение неуязвимости
            }
            WhenDestroy(); // Уничтожение метеорита
        }
        // Если столкновение с космической станцией
        else if (other.TryGetComponent(out SpaceStation spaceStation))
        {
            spaceStation.Heartint(25); // Уменьшение здоровья станции
            WhenDestroy(); // Уничтожение метеорита
        }
    }

    // Метод для создания эффекта при столкновении
    private void Shinning(Meteorit meteorit)
    {
        if (WithEffect) // Проверка наличия эффекта
            return;
        Instantiate(EffectSelected, transform); // Создание эффекта
        WithEffect = true; // Установка флага наличия эффекта
    }

    // Метод для уничтожения метеорита
    public void WhenDestroy()
    {
        Instantiate(SingletoneResourses.Instance.explosion, transform.position, Quaternion.identity); // Создание эффекта взрыва
        Destroy(gameObject); // Уничтожение метеорита
    }
}