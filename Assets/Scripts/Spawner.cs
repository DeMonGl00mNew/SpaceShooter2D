using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System;
using Random = UnityEngine.Random;
using UnityEngine.UI;

public class Spawner : MonoBehaviour
{
    // Статическая переменная для реализации паттерна Singleton
    static public Spawner Instance { get; private set; }
    public float RoundTime = 60; // Время раунда
    public int restTime = 20; // Время отдыха между волнами

    public TMP_Text graduatedLevelText; // Текст для отображения уровня
    public List<int[]> SidesFoWaves = new List<int[]>(); // Стороны для волн
    public List<int> PercentsTimingAttack = new List<int>(); // Проценты времени атаки
    public List<int> DifficultyWavesArray = new List<int>(); // Массив сложности волн
    private List<GameObject> BarAsteroidesSides = new List<GameObject>(); // Стороны астероидов
    static public int KolvoMeteoritov = 0; // Количество метеоритов
    static public Scene scene; // Текущая сцена
    static public int Friends = 0; // Количество друзей
    public GameObject[] BarMeteorites; // Массив баров метеоритов
    public Slider ProgressBar; // Полоса прогресса
    public Transform SpaceStationTransform; // Трансформатор космической станции
    public int wave = 1; // Текущая волна
    public float RangeForMeteorits = 0.4f; // Диапазон для метеоритов
    public int CountForBonuses = 3; // Количество бонусов
    public TMP_Text PrintWord; // Текст для отображения слова
    public GameObject Fireworks; // Префаб фейерверков
    public List<GameObject> bukvi; // Список букв
    public List<GameObject> MeteoriteTemplates; // Шаблоны метеоритов
    public List<GameObject> bonuses; // Список бонусов
    [HideInInspector] public string choiceWord; // Выбранное слово
    private List<Meteorit> FriendsOnScene = new List<Meteorit>(); // Друзья на сцене
    private List<string> words; // Список слов
    private int[] SidesForSpawn = new int[4]; // Стороны для спавна
    private List<string> alphabet = new List<string>() { "а", "б", "в", "г", "д", "е", "ё",
                                                         "ж", "з", "и", "й", "к", "л", "м",
                                                         "н", "о", "п", "р", "с", "т", "у",
                                                         "ф", "х", "ц", "ч", "ш", "щ", "ъ",
                                                         "ы", "ь", "э", "ю", "я"};

    private List<string> enemies; // Список врагов
    private int[] stages = new int[4] { 1, 1, 1, 2 }; // Этапы сложности
    private int[] percentMask = new int[4] { 0, 0, 0, 0 }; // Маска процентов

    private char VibrannayBukva; // Выбранная буква
    private float TimerForProgress = 0; // Таймер для прогресса
    private bool flagISSpawnFireworks = false; // Флаг для спавна фейерверков
    private int currentCountForBonuses = 0; // Текущая счетчик бонусов
    private GameObject currentBonus; // Текущий бонус
    private SceneColider SceneColider; // Коллайдер сцены
    private int indexPercent = 0; // Индекс процентов
    private bool inWave = false; // Флаг для волны
    private Coroutine EnemySpawnCouratine; // Корутин для спавна врагов
    private bool endingOfRoundOn = false; // Флаг окончания раунда
    private int SidesForSpawnCount = 1; // Количество сторон для спавна
    private static int graduatedLevel = 1; // Уровень

    // Метод для спавна бонуса
    public void SpawnBonus(Transform where)
    {
        currentCountForBonuses++;

        // Если бонусов меньше, чем нужно, выходим
        if (currentCountForBonuses < CountForBonuses)
            return;

        currentCountForBonuses = 0; // Сбрасываем счетчик бонусов

        // Уничтожаем текущий бонус, если он существует
        if (currentBonus != null)
            Destroy(currentBonus);

        // Выбираем случайный индекс бонуса и создаем его
        int index = Random.Range(0, bonuses.Count);
        currentBonus = Instantiate(bonuses[index], where.position, Quaternion.identity);
    }

    private void Awake()
    {
        // Проверяем, является ли текущий экземпляр единственным
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // Уничтожаем дубликат
        }

        SingletoneResourses.Instance.Reset(); // Сбрасываем ресурсы

        // Инициализация слов в зависимости от сцены
        if (scene.name == "Game")
        {
            words = new List<string>(){ "мама", "папа", "дед", "баба", "конфета", "игрушка", "семья", "брат",
                                                    "сестра", "дружба", "добро", "вкусно", "спасибо", "кукла", "машина" ,
                                                    "пожалуйста", "хорошо", "ёж", "белка", "заяц", "лиса", "тигр",
                                                    "медведь", "лев", "слон", "волк", "кошка", "собака", "корова",
                                                    "мышь", "улыбка", "трактор", "огурец", "помидор", "яблоко", "енот",
                                                    "рыба", "акула", "жираф", "молоко", "шоколад", "солнце", "торт"};
        }
        else if (scene.name == "Tutorial")
        {
            words = new List<string>() { "ма" }; // Слова для туториала
        }
    }

    void Start()
    {
        SingletoneResourses.Instance.Reset(); // Сбрасываем ресурсы
        SceneColider = FindObjectOfType<SceneColider>(); // Находим коллайдер сцены
        NewWordSpawn(); // Спавн нового слова
        SpaceStation.NeedNewWordEvent += NewWordSpawn; // Подписка на событие нового слова

        // Инициализация для игровой сцены
        if (scene.name == "Game")
        {
            Tutorial.StateTutorial = 0; // Состояние туториала
            SetupPercentsTimingAttack(); // Настройка процентов времени атаки
            SetupDifficultyWavesSides(); // Настройка сторон волн
            DifficultLevelSetup(); // Настройка уровня сложности
            InstantiateAsteroidsInBar(PercentsTimingAttack); // Создание астероидов в баре
            SplitMatchForStagesForSidesWaves(); // Разделение матчей по этапам
            MeteroiteBarArrowsManage(); // Управление стрелками баров метеоритов
        }
        // Инициализация для туториала
        if (scene.name == "Tutorial")
        {
            Tutorial.StateTutorialEvent += SpawnMeteoritTutorial; // Подписка на событие туториала
            Tutorial.StateTutorial = 1; // Установка состояния туториала
        }
    }

    private void StartLevel()
    {
        graduatedLevelText.text = $"Уровень {graduatedLevel}"; // Обновление текста уровня
        Invoke("SpawnRandomMeteorite", 2); // Запуск спавна метеоритов через 2 секунды
    }

    private void SpawnRandomMeteorite()
    {
        graduatedLevelText.gameObject.SetActive(false); // Скрытие текста уровня
        StartCoroutine(spawnFriendsAlwaysCouratine(Random.Range(3, 7))); // Запуск корутина спавна друзей
        EnemySpawnCouratine = StartCoroutine(spawnEnemiesAlwaysCouratine(Random.Range(3, 7), Random.Range(1, 3))); // Запуск корутина спавна врагов
    }

    void Update()
    {
        TimerForProgress += Time.deltaTime; // Обновление таймера прогресса
        ProgressBar.value = TimerForProgress * 100 / RoundTime; // Обновление значения полосы прогресса

        // Проверка достижения процента времени атаки
        if ((indexPercent < PercentsTimingAttack.Count) && ProgressBar.value >= PercentsTimingAttack[indexPercent])
        {
            DifficultyWavesFunc(DifficultyWavesArray[indexPercent]); // Установка сложности волн
            indexPercent++; // Увеличение индекса процентов

            // Проверка завершения раунда
            if (ProgressBar.value == 100)
            {
                endingOfRoundOn = true; // Установка флага окончания раунда
                StopCoroutine(EnemySpawnCouratine); // Остановка корутина спавна врагов
                SpaceStation.NeedNewWordEvent += TheEndRound; // Подписка на событие окончания раунда
            }
        }
    }

    private void SplitMatchForStagesForSidesWaves()
    {
        int StageIndex = 0; // Индекс этапа
        int indexing;

        SidesForSpawnCount = 1; // Начальное количество сторон для спавна

        // Разделение матчей по этапам
        for (int i = 0; i < PercentsTimingAttack.Count; i++)
        {
            indexing = (int)(StageIndex / 4f * 100); // Индекс для процентов
            if (PercentsTimingAttack[i] > indexing)
            {
                SidesForSpawnCount = stages[StageIndex]; // Установка количества сторон для спавна
                StageIndex++; // Переход к следующему этапу
            }
            if (SidesForSpawnCount > 4) SidesForSpawnCount = 4; // Ограничение количества сторон
            SidesFoWaves.Add(CountSidesForSpawn(SidesForSpawnCount)); // Добавление сторон для волн
        }
    }

    public void DifficultLevelSetup()
    {
        // Проверка на пустоту массива сложности волн
        if (DifficultyWavesArray.Count == 0)
            for (int i = 0; i < PercentsTimingAttack.Count; i++)
                DifficultyWavesArray.Add(0); // Инициализация нулями
        else
            for (int i = 0; i < DifficultyWavesArray.Count; i++)
            {
                DifficultyWavesArray[i] = 0; // Сброс значений
            }

        int indexDifficult = 0; // Индекс сложности
        int index = 0; // Индекс для уровня
        for (int i = 1; i <= graduatedLevel; i++)
        {
            indexDifficult = DifficultyWavesArray.Count - (index) % DifficultyWavesArray.Count; // Вычисление индекса сложности

            // Увеличение сложности
            if (DifficultyWavesArray[indexDifficult - 1] < 3)
                DifficultyWavesArray[indexDifficult - 1]++;
            else
                DifficultyWavesArray[indexDifficult - 1] = 3; // Ограничение сложности
            index++;
        }

        StartLevel(); // Запуск уровня
        graduatedLevel++; // Увеличение уровня
    }

    private void SetupDifficultyWaves()
    {
        int StageIndex = 0; // Индекс этапа
        int indexing;
        int probabilities; // Вероятность
        int difficultLevel = 0; // Уровень сложности

        // Настройка сложности волн
        for (int i = 0; i < PercentsTimingAttack.Count; i++)
        {
            probabilities = Random.Range(1, 101); // Генерация вероятности
            indexing = (int)(StageIndex / 4f * 100); // Индекс для процентов
            if (PercentsTimingAttack[i] >= indexing)
            {
                // Установка уровня сложности в зависимости от индекса
                if (indexing == 0)
                {
                    if (10 >= probabilities)
                        difficultLevel = 1;
                }
                if (indexing == 25)
                {
                    if (50 >= probabilities)
                        difficultLevel = 1;
                }
                if (indexing == 50)
                {
                    if (50 >= probabilities)
                        difficultLevel = 1;
                    if (25 >= probabilities)
                        difficultLevel = 2;
                }
                if (indexing == 75)
                {
                    if (100 >= probabilities)
                        difficultLevel = 1;
                    if (50 >= probabilities)
                        difficultLevel = 2;
                }
                StageIndex++; // Переход к следующему этапу
            }
            DifficultyWavesArray.Add(difficultLevel); // Добавление уровня сложности
        }
    }

    public void SetupDifficultyWavesSides()
    {
        int probabilities; // Вероятность
        for (int i = 0; i < 4; i++)
        {
            probabilities = Random.Range(1, 101); // Генерация вероятности
            // Настройка сторон волн в зависимости от индекса
            if (i == 0)
            {
                if (percentMask[i] >= probabilities)
                    stages[i] += 1;
                percentMask[i] += 5;
                if (percentMask[i] > 15)
                    percentMask[i] = 15;
            }
            if (i == 1)
            {
                if (percentMask[i] >= probabilities)
                    stages[i] += 1;
                percentMask[i] += 5;
                if (percentMask[i] > 20)
                    percentMask[i] = 20;
            }
            if (i == 2)
            {
                if (percentMask[i] >= probabilities)
                    stages[i] += 1;
                percentMask[i] += 10;
                if (percentMask[i] > 40)
                    percentMask[i] = 40;
            }
            if (i == 3)
            {
                if (percentMask[i] >= probabilities)
                    stages[i] += 1;
                percentMask[i] += 10;
                if (percentMask[i] > 60)
                    percentMask[i] = 60;
            }

            if (stages[i] > 4)
                stages[i] = 4; // Ограничение количества этапов
        }
    }

    private void TheEndRound()
    {
        DestroyAllMeteorite(); // Уничтожение всех метеоритов
        if (!flagISSpawnFireworks)
            StartCoroutine(SpawnFireworksCouratine()); // Запуск корутина спавна фейерверков
        Invoke("Respawn", 4); // Перезагрузка сцены через 4 секунды
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Перезагрузка текущей сцены
    }

    private void DifficultyWavesFunc(int DifficultyWave)
    {
        // Установка сложности волн в зависимости от уровня сложности
        if (DifficultyWave == 0)
        {
            StartCoroutine(spawnWawesEnemiesCouratine(5, 1, 1, SidesFoWaves[indexPercent]));
        }
        if (DifficultyWave == 1)
        {
            StartCoroutine(spawnWawesEnemiesCouratine(3, 2, 2, SidesFoWaves[indexPercent]));
        }
        if (DifficultyWave == 2)
        {
            StartCoroutine(spawnWawesEnemiesCouratine(3, 3, 3, SidesFoWaves[indexPercent]));
        }
    }

    private void SetupPercentsTimingAttack()
    {
        int CountWaves = (int)(RoundTime) / restTime; // Количество волн

        // Инициализация процентов времени атаки
        for (int i = 0; i < CountWaves; i++)
        {
            PercentsTimingAttack.Add(0);
        }

        // Установка значений процентов времени атаки
        for (int i = 0; i < PercentsTimingAttack.Count; i++)
        {
            PercentsTimingAttack[i] = (int)((float)(i + 1) / CountWaves * 100);
        }
    }

    private void InstantiateAsteroidsInBar(List<int> _percentsTimingAttack)
    {
        RectTransform rectTransform = ProgressBar.GetComponent<RectTransform>(); // Получение RectTransform полосы прогресса
        int indexForDifficult = 0; // Индекс для сложности

        // Создание астероидов в баре
        foreach (var percent in _percentsTimingAttack)
        {
            RectTransform _barMeteoriteRectTransform = Instantiate(BarMeteorites[DifficultyWavesArray[indexForDifficult++]], ProgressBar.transform).GetComponent<RectTransform>();
            BarAsteroidesSides.Add(_barMeteoriteRectTransform.gameObject); // Добавление в список

            // Установка позиции астероида
            _barMeteoriteRectTransform.anchorMin = new Vector2(0, 0.5f);
            _barMeteoriteRectTransform.anchorMax = new Vector2(0, 0.5f);
            _barMeteoriteRectTransform.localPosition = new Vector3(rectTransform.sizeDelta.x * percent / 100, 0, 0);
        }
    }

    private void MeteroiteBarArrowsManage()
    {
        BarAsteroidsTagSides[] arrows; // Массив стрелок
        for (int i = 0; i < BarAsteroidesSides.Count; i++)
        {
            arrows = BarAsteroidesSides[i].GetComponentsInChildren<BarAsteroidsTagSides>(true); // Получение стрелок
            for (int j = 0; j < arrows.Length; j++)
            {
                // Активация стрелок в зависимости от сторон
                if (SidesFoWaves[i][j] == 1) arrows[j].gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        // Отписка от событий
        Tutorial.StateTutorialEvent -= SpawnMeteoritTutorial;
        SpaceStation.NeedNewWordEvent -= NewWordSpawn;
    }
    private void NewWordSpawn()
    {
        // Проверяем, закончился ли раунд
        if (endingOfRoundOn)
            return; // Если да, выходим из метода

        // Выбираем новое слово для спавна
        ChoiceWordForSpawn();
        // Печатаем выбранное слово
        PrintChoiceWord(choiceWord);
        // Определяем врагов
        DetermineEnemies();
    }

    public void SpawnMeteoritTutorial()
    {
        // Получаем массив метеоритов, находящихся в дочерних объектах
        Meteorit[] Enemies = GetComponentsInChildren<Meteorit>();

        // Если состояние туториала 1, спавним друга
        if (Tutorial.StateTutorial == 1)
            spawnFriend();

        // Если состояние туториала 6
        if (Tutorial.StateTutorial == 6)
        {
            // Если врагов нет, спавним одного врага
            if (Enemies.Length == 0)
                spawnEnemies(1);
        }

        // Если состояние туториала 7
        if (Tutorial.StateTutorial == 7)
        {
            // Если врагов нет, спавним друга и одного врага
            if (Enemies.Length == 0)
            {
                spawnFriend();
                spawnEnemies(1);
            }
        }
    }

    private void PrintChoiceWord(string slovo)
    {
        // Печатаем выбранное слово в верхнем регистре
        PrintWord.text = slovo.ToUpper();
    }

    private void ChoiceWordForSpawn()
    {
        // Проверяем, есть ли слова для спавна
        if (words.Count > 0)
        {
            // Выбираем случайный индекс слова
            int index = UnityEngine.Random.Range(0, words.Count);
            // Запоминаем выбранное слово
            choiceWord = words[index];
            // Удаляем выбранное слово из списка
            words.RemoveAt(index);
        }
    }

    private void DetermineEnemies()
    {
        // Инициализируем список врагов с алфавитом
        enemies = new List<string>(alphabet);
        // Проходим по каждой букве в выбранном слове
        foreach (char bukva in choiceWord)
        {
            // Если буква есть в списке врагов, удаляем её
            if (enemies.Contains(bukva.ToString()))
                enemies.RemoveAt(enemies.IndexOf(bukva.ToString()));
        }
    }

    void spawnFriend(int[] sides)
    {
        // Выбираем случайную букву из выбранного слова
        VibrannayBukva = choiceWord[Random.Range(0, choiceWord.Length)];
        // Спавним метеорит с выбранной буквой
        spawnMeteorit(VibrannayBukva.ToString(), sides);
    }

    public void spawnEnemies(int kolvo, int[] sides)
    {
        // Спавним указанное количество врагов
        for (int i = 0; i < kolvo; i++)
        {
            KolvoMeteoritov++; // Увеличиваем количество метеоритов
                               // Выбираем случайную букву из списка врагов
            string enemyLetter = enemies[Random.Range(0, enemies.Count)];
            // Спавним метеорит с выбранной буквой
            spawnMeteorit(enemyLetter, sides);
        }
    }

    void spawnFriend()
    {
        // Если выбранное слово пустое, выходим из метода
        if (choiceWord == "")
            return;

        // Выбираем случайную букву из выбранного слова
        VibrannayBukva = choiceWord[Random.Range(0, choiceWord.Length)];
        // Спавним метеорит с выбранной буквой
        spawnMeteorit(VibrannayBukva.ToString());
    }

    public void spawnEnemies(int kolvo)
    {
        // Спавним указанное количество врагов
        for (int i = 0; i < kolvo; i++)
        {
            KolvoMeteoritov++; // Увеличиваем количество метеоритов
                               // Выбираем случайную букву из списка врагов
            string enemyLetter = enemies[Random.Range(0, enemies.Count)];
            // Спавним метеорит с выбранной буквой
            spawnMeteorit(enemyLetter);
        }
    }

    void spawnMeteorit(string letter, int[] sides)
    {
        // Создаем экземпляр метеорита из шаблона
        Meteorit prefab = Instantiate(MeteoriteTemplates[Random.Range(0, MeteoriteTemplates.Count)],
                                                            transform.position, Quaternion.identity, transform).GetComponent<Meteorit>();
        // Устанавливаем позицию метеорита
        prefab.transform.position = offsetBySpawnForSide(prefab, sides);
        // Запускаем корутину для возврата в сцену
        StartCoroutine(ReturnToSceneCouratine(prefab.gameObject));
        // Устанавливаем букву метеорита
        prefab.Letter = letter;
    }

    void spawnMeteorit(string letter)
    {
        // Создаем экземпляр метеорита из шаблона
        Meteorit prefab = Instantiate(MeteoriteTemplates[Random.Range(0, MeteoriteTemplates.Count)],
                                          transform.position, Quaternion.identity, transform).GetComponent<Meteorit>();
        // Устанавливаем позицию метеорита
        prefab.transform.position = offsetForSpawn(prefab.GetComponent<Meteorit>());
        // Запускаем корутину для возврата в сцену
        StartCoroutine(ReturnToSceneCouratine(prefab.gameObject));

        // Если находимся в туториале и состояние туториала 1, добавляем компонент
        if (scene.name == "Tutorial" && Tutorial.StateTutorial == 1)
        {
            prefab.gameObject.AddComponent<TutorialFirstMeteorite>();
        }

        // Устанавливаем букву метеорита
        prefab.Letter = letter;
    }

    public Vector2 offsetForSpawn(Meteorit meteorit)
    {
        // Получаем размер экрана
        Vector2 sizeScreen = SceneColider.SizeScreen() * 0.5f;
        int offsetXY = Random.Range(0, 2); // Случайный выбор оси для спавна

        // Логика спавна в зависимости от состояния туториала
        if (scene.name == "Tutorial" && Tutorial.StateTutorial < 3)
        {
            meteorit.SideForSpawn(1, SpaceStationTransform);
            return new Vector2((sizeScreen.x + 2), 0);
        }
        if (scene.name == "Tutorial" && Tutorial.StateTutorial >= 6)
        {
            meteorit.SideForSpawn(1, SpaceStationTransform);
            return new Vector2((sizeScreen.x + 2) * RandomSigne(), Random.Range(-sizeScreen.y, sizeScreen.y));
        }

        // Выбор стороны для спавна
        if (offsetXY == 0)
        {
            meteorit.SideForSpawn(1, SpaceStationTransform);
            return new Vector2((sizeScreen.x + 2) * RandomSigne(), Random.Range(-sizeScreen.y, sizeScreen.y));
        }
        else
        {
            meteorit.SideForSpawn(2, SpaceStationTransform);
            return new Vector2(Random.Range(-sizeScreen.x, sizeScreen.x), (sizeScreen.y + 2) * RandomSigne());
        }
    }

    public Vector2 offsetBySpawnForSide(Meteorit meteorit, int[] numbers)
    {
        // Получаем размер экрана
        Vector2 sizeScreen = SceneColider.SizeScreen() * 0.5f;
        // Выбираем случайную сторону для спавна
        int side = RandomFromListNumbers(numbers);

        // Логика спавна в зависимости от выбранной стороны
        if (side == 0) // Правая сторона
        {
            meteorit.SideForSpawn(1, SpaceStationTransform);
            return new Vector2((sizeScreen.x + 2), Random.Range(-sizeScreen.y, sizeScreen.y));
        }

        if (side == 1) // Левая сторона
        {
            meteorit.SideForSpawn(1, SpaceStationTransform);
            return new Vector2(-(sizeScreen.x + 2), Random.Range(-sizeScreen.y, sizeScreen.y));
        }
        if (side == 2) // Верхняя сторона
        {
            meteorit.SideForSpawn(2, SpaceStationTransform);
            return new Vector2(Random.Range(-sizeScreen.x, sizeScreen.x), (sizeScreen.y + 2));
        }
        if (side == 3) // Нижняя сторона
        {
            meteorit.SideForSpawn(2, SpaceStationTransform);
            return new Vector2(Random.Range(-sizeScreen.x, sizeScreen.x), -(sizeScreen.y + 2));
        }
        return Vector2.zero; // Возвращаем нулевой вектор, если ничего не выбрано
    }

    private int[] CountSidesForSpawn(int count)
    {
        // Создаем массив для сторон спавна
        int[] Array = new int[] { 0, 0, 0, 0 };
        List<int> ListNumbers = new List<int>() { 0, 1, 2, 3 };
        int ListNumbersCount = ListNumbers.Count;

        // Удаляем случайные стороны из списка
        for (int i = 0; i < ListNumbersCount - count; i++)
        {
            ListNumbers.RemoveAt(Random.Range(0, ListNumbers.Count));
        }

        // Заполняем массив выбранными сторонами
        foreach (var index in ListNumbers)
        {
            Array[index] = 1;
        }

        return Array; // Возвращаем массив сторон
    }

    private int RandomFromListNumbers(int[] numbers)
    {
        // Получаем размер экрана
        Vector2 sizeScreen = SceneColider.SizeScreen() * 0.5f;
        List<int> ListNumbers = new List<int>();

        // Заполняем список номерами, которые не равны нулю
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] != 0)
            {
                ListNumbers.Add(i);
            }
        }
        // Возвращаем случайный номер из списка
        return ListNumbers[Random.Range(0, ListNumbers.Count)];
    }

    private int RandomSigne()
    {
        // Генерируем случайный знак (1 или -1)
        int offsetXY = Random.Range(0, 2);
        if (offsetXY == 0)
            return 1;
        else
            return -1;
    }

    float RandomExceptCenter(float diapason, float exceptCenter)
    {
        // Генерируем случайное число в заданном диапазоне
        float randomChislo = UnityEngine.Random.Range(-diapason, diapason);
        // Смещаем число от центра
        if (randomChislo >= 0)
            randomChislo += exceptCenter;
        else
            randomChislo -= exceptCenter;
        return randomChislo; // Возвращаем сгенерированное число
    }

    public void SpawnFireworks(int count)
    {
        // Спавним фейерверки указанного количества
        for (int i = 0; i < count; i++)
        {
            float xSpawn = transform.position.x + RandomExceptCenter(3, 1);
            float ySpawn = transform.position.y + RandomExceptCenter(3, 1);

            // Создаем фейерверк
            GameObject prefab = Instantiate(Fireworks, new Vector2(xSpawn, ySpawn), transform.rotation);
            // Удаляем фейерверк через 3 секунды
            Destroy(prefab, 3);
        }
    }

    public void DestroyAllMeteorite()
    {
        // Получаем все метеориты в дочерних объектах
        Meteorit[] meteorits = GetComponentsInChildren<Meteorit>();
        // Если метеоритов нет, выходим из метода
        if (meteorits.Length == 0)
            return;

        // Уничтожаем каждый метеорит
        foreach (var meteorit in meteorits)
        {
            meteorit.WhenDestroy();
        }
    }

    IEnumerator spawnFriendsAlwaysCouratine(float seconds)
    {
        // Бесконечный цикл для спавна друзей
        while (true)
        {
            spawnFriend(); // Спавним друга
            yield return new WaitForSeconds(seconds); // Ждем указанное время
        }
    }

    IEnumerator spawnEnemiesAlwaysCouratine(float seconds, int number)
    {
        // Бесконечный цикл для спавна врагов
        while (true)
        {
            if (!inWave) // Если не в волне
                spawnEnemies(number); // Спавним врагов
            yield return new WaitForSeconds(seconds); // Ждем указанное время
        }
    }

    IEnumerator spawnWawesEnemiesCouratine(float secBetweenWave, int numberMeteorite, int countWave, int[] sides)
    {
        inWave = true; // Устанавливаем флаг, что мы в волне
                       // Спавним указанное количество волн врагов
        for (int i = 0; i < countWave; i++)
        {
            spawnEnemies(numberMeteorite, sides); // Спавним врагов
            yield return new WaitForSeconds(secBetweenWave); // Ждем между волнами
        }
        inWave = false; // Сбрасываем флаг
    }

    IEnumerator ReturnToSceneCouratine(GameObject where)
    {
        yield return new WaitForSeconds(3); // Ждем 3 секунды
        if (where == null) yield break; // Если объект уничтожен, выходим
        where.AddComponent<ReturnToScene>(); // Добавляем компонент для возврата в сцену
    }

    IEnumerator SpawnFireworksCouratine()
    {
        flagISSpawnFireworks = true; // Устанавливаем флаг спавна фейерверков
                                     // Спавним фейерверки в цикле
        for (int i = 0; i < 8; i++)
        {
            SpawnFireworks(UnityEngine.Random.Range(1, 8)); // Спавним случайное количество фейерверков
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f)); // Ждем случайное время
        }
        flagISSpawnFireworks = false; // Сбрасываем флаг
    }
}