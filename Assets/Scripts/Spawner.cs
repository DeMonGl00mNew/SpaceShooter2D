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
    // ����������� ���������� ��� ���������� �������� Singleton
    static public Spawner Instance { get; private set; }
    public float RoundTime = 60; // ����� ������
    public int restTime = 20; // ����� ������ ����� �������

    public TMP_Text graduatedLevelText; // ����� ��� ����������� ������
    public List<int[]> SidesFoWaves = new List<int[]>(); // ������� ��� ����
    public List<int> PercentsTimingAttack = new List<int>(); // �������� ������� �����
    public List<int> DifficultyWavesArray = new List<int>(); // ������ ��������� ����
    private List<GameObject> BarAsteroidesSides = new List<GameObject>(); // ������� ����������
    static public int KolvoMeteoritov = 0; // ���������� ����������
    static public Scene scene; // ������� �����
    static public int Friends = 0; // ���������� ������
    public GameObject[] BarMeteorites; // ������ ����� ����������
    public Slider ProgressBar; // ������ ���������
    public Transform SpaceStationTransform; // ������������� ����������� �������
    public int wave = 1; // ������� �����
    public float RangeForMeteorits = 0.4f; // �������� ��� ����������
    public int CountForBonuses = 3; // ���������� �������
    public TMP_Text PrintWord; // ����� ��� ����������� �����
    public GameObject Fireworks; // ������ �����������
    public List<GameObject> bukvi; // ������ ����
    public List<GameObject> MeteoriteTemplates; // ������� ����������
    public List<GameObject> bonuses; // ������ �������
    [HideInInspector] public string choiceWord; // ��������� �����
    private List<Meteorit> FriendsOnScene = new List<Meteorit>(); // ������ �� �����
    private List<string> words; // ������ ����
    private int[] SidesForSpawn = new int[4]; // ������� ��� ������
    private List<string> alphabet = new List<string>() { "�", "�", "�", "�", "�", "�", "�",
                                                         "�", "�", "�", "�", "�", "�", "�",
                                                         "�", "�", "�", "�", "�", "�", "�",
                                                         "�", "�", "�", "�", "�", "�", "�",
                                                         "�", "�", "�", "�", "�"};

    private List<string> enemies; // ������ ������
    private int[] stages = new int[4] { 1, 1, 1, 2 }; // ����� ���������
    private int[] percentMask = new int[4] { 0, 0, 0, 0 }; // ����� ���������

    private char VibrannayBukva; // ��������� �����
    private float TimerForProgress = 0; // ������ ��� ���������
    private bool flagISSpawnFireworks = false; // ���� ��� ������ �����������
    private int currentCountForBonuses = 0; // ������� ������� �������
    private GameObject currentBonus; // ������� �����
    private SceneColider SceneColider; // ��������� �����
    private int indexPercent = 0; // ������ ���������
    private bool inWave = false; // ���� ��� �����
    private Coroutine EnemySpawnCouratine; // ������� ��� ������ ������
    private bool endingOfRoundOn = false; // ���� ��������� ������
    private int SidesForSpawnCount = 1; // ���������� ������ ��� ������
    private static int graduatedLevel = 1; // �������

    // ����� ��� ������ ������
    public void SpawnBonus(Transform where)
    {
        currentCountForBonuses++;

        // ���� ������� ������, ��� �����, �������
        if (currentCountForBonuses < CountForBonuses)
            return;

        currentCountForBonuses = 0; // ���������� ������� �������

        // ���������� ������� �����, ���� �� ����������
        if (currentBonus != null)
            Destroy(currentBonus);

        // �������� ��������� ������ ������ � ������� ���
        int index = Random.Range(0, bonuses.Count);
        currentBonus = Instantiate(bonuses[index], where.position, Quaternion.identity);
    }

    private void Awake()
    {
        // ���������, �������� �� ������� ��������� ������������
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject); // ���������� ��������
        }

        SingletoneResourses.Instance.Reset(); // ���������� �������

        // ������������� ���� � ����������� �� �����
        if (scene.name == "Game")
        {
            words = new List<string>(){ "����", "����", "���", "����", "�������", "�������", "�����", "����",
                                                    "������", "������", "�����", "������", "�������", "�����", "������" ,
                                                    "����������", "������", "��", "�����", "����", "����", "����",
                                                    "�������", "���", "����", "����", "�����", "������", "������",
                                                    "����", "������", "�������", "������", "�������", "������", "����",
                                                    "����", "�����", "�����", "������", "�������", "������", "����"};
        }
        else if (scene.name == "Tutorial")
        {
            words = new List<string>() { "��" }; // ����� ��� ���������
        }
    }

    void Start()
    {
        SingletoneResourses.Instance.Reset(); // ���������� �������
        SceneColider = FindObjectOfType<SceneColider>(); // ������� ��������� �����
        NewWordSpawn(); // ����� ������ �����
        SpaceStation.NeedNewWordEvent += NewWordSpawn; // �������� �� ������� ������ �����

        // ������������� ��� ������� �����
        if (scene.name == "Game")
        {
            Tutorial.StateTutorial = 0; // ��������� ���������
            SetupPercentsTimingAttack(); // ��������� ��������� ������� �����
            SetupDifficultyWavesSides(); // ��������� ������ ����
            DifficultLevelSetup(); // ��������� ������ ���������
            InstantiateAsteroidsInBar(PercentsTimingAttack); // �������� ���������� � ����
            SplitMatchForStagesForSidesWaves(); // ���������� ������ �� ������
            MeteroiteBarArrowsManage(); // ���������� ��������� ����� ����������
        }
        // ������������� ��� ���������
        if (scene.name == "Tutorial")
        {
            Tutorial.StateTutorialEvent += SpawnMeteoritTutorial; // �������� �� ������� ���������
            Tutorial.StateTutorial = 1; // ��������� ��������� ���������
        }
    }

    private void StartLevel()
    {
        graduatedLevelText.text = $"������� {graduatedLevel}"; // ���������� ������ ������
        Invoke("SpawnRandomMeteorite", 2); // ������ ������ ���������� ����� 2 �������
    }

    private void SpawnRandomMeteorite()
    {
        graduatedLevelText.gameObject.SetActive(false); // ������� ������ ������
        StartCoroutine(spawnFriendsAlwaysCouratine(Random.Range(3, 7))); // ������ �������� ������ ������
        EnemySpawnCouratine = StartCoroutine(spawnEnemiesAlwaysCouratine(Random.Range(3, 7), Random.Range(1, 3))); // ������ �������� ������ ������
    }

    void Update()
    {
        TimerForProgress += Time.deltaTime; // ���������� ������� ���������
        ProgressBar.value = TimerForProgress * 100 / RoundTime; // ���������� �������� ������ ���������

        // �������� ���������� �������� ������� �����
        if ((indexPercent < PercentsTimingAttack.Count) && ProgressBar.value >= PercentsTimingAttack[indexPercent])
        {
            DifficultyWavesFunc(DifficultyWavesArray[indexPercent]); // ��������� ��������� ����
            indexPercent++; // ���������� ������� ���������

            // �������� ���������� ������
            if (ProgressBar.value == 100)
            {
                endingOfRoundOn = true; // ��������� ����� ��������� ������
                StopCoroutine(EnemySpawnCouratine); // ��������� �������� ������ ������
                SpaceStation.NeedNewWordEvent += TheEndRound; // �������� �� ������� ��������� ������
            }
        }
    }

    private void SplitMatchForStagesForSidesWaves()
    {
        int StageIndex = 0; // ������ �����
        int indexing;

        SidesForSpawnCount = 1; // ��������� ���������� ������ ��� ������

        // ���������� ������ �� ������
        for (int i = 0; i < PercentsTimingAttack.Count; i++)
        {
            indexing = (int)(StageIndex / 4f * 100); // ������ ��� ���������
            if (PercentsTimingAttack[i] > indexing)
            {
                SidesForSpawnCount = stages[StageIndex]; // ��������� ���������� ������ ��� ������
                StageIndex++; // ������� � ���������� �����
            }
            if (SidesForSpawnCount > 4) SidesForSpawnCount = 4; // ����������� ���������� ������
            SidesFoWaves.Add(CountSidesForSpawn(SidesForSpawnCount)); // ���������� ������ ��� ����
        }
    }

    public void DifficultLevelSetup()
    {
        // �������� �� ������� ������� ��������� ����
        if (DifficultyWavesArray.Count == 0)
            for (int i = 0; i < PercentsTimingAttack.Count; i++)
                DifficultyWavesArray.Add(0); // ������������� ������
        else
            for (int i = 0; i < DifficultyWavesArray.Count; i++)
            {
                DifficultyWavesArray[i] = 0; // ����� ��������
            }

        int indexDifficult = 0; // ������ ���������
        int index = 0; // ������ ��� ������
        for (int i = 1; i <= graduatedLevel; i++)
        {
            indexDifficult = DifficultyWavesArray.Count - (index) % DifficultyWavesArray.Count; // ���������� ������� ���������

            // ���������� ���������
            if (DifficultyWavesArray[indexDifficult - 1] < 3)
                DifficultyWavesArray[indexDifficult - 1]++;
            else
                DifficultyWavesArray[indexDifficult - 1] = 3; // ����������� ���������
            index++;
        }

        StartLevel(); // ������ ������
        graduatedLevel++; // ���������� ������
    }

    private void SetupDifficultyWaves()
    {
        int StageIndex = 0; // ������ �����
        int indexing;
        int probabilities; // �����������
        int difficultLevel = 0; // ������� ���������

        // ��������� ��������� ����
        for (int i = 0; i < PercentsTimingAttack.Count; i++)
        {
            probabilities = Random.Range(1, 101); // ��������� �����������
            indexing = (int)(StageIndex / 4f * 100); // ������ ��� ���������
            if (PercentsTimingAttack[i] >= indexing)
            {
                // ��������� ������ ��������� � ����������� �� �������
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
                StageIndex++; // ������� � ���������� �����
            }
            DifficultyWavesArray.Add(difficultLevel); // ���������� ������ ���������
        }
    }

    public void SetupDifficultyWavesSides()
    {
        int probabilities; // �����������
        for (int i = 0; i < 4; i++)
        {
            probabilities = Random.Range(1, 101); // ��������� �����������
            // ��������� ������ ���� � ����������� �� �������
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
                stages[i] = 4; // ����������� ���������� ������
        }
    }

    private void TheEndRound()
    {
        DestroyAllMeteorite(); // ����������� ���� ����������
        if (!flagISSpawnFireworks)
            StartCoroutine(SpawnFireworksCouratine()); // ������ �������� ������ �����������
        Invoke("Respawn", 4); // ������������ ����� ����� 4 �������
    }

    private void Respawn()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ������������ ������� �����
    }

    private void DifficultyWavesFunc(int DifficultyWave)
    {
        // ��������� ��������� ���� � ����������� �� ������ ���������
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
        int CountWaves = (int)(RoundTime) / restTime; // ���������� ����

        // ������������� ��������� ������� �����
        for (int i = 0; i < CountWaves; i++)
        {
            PercentsTimingAttack.Add(0);
        }

        // ��������� �������� ��������� ������� �����
        for (int i = 0; i < PercentsTimingAttack.Count; i++)
        {
            PercentsTimingAttack[i] = (int)((float)(i + 1) / CountWaves * 100);
        }
    }

    private void InstantiateAsteroidsInBar(List<int> _percentsTimingAttack)
    {
        RectTransform rectTransform = ProgressBar.GetComponent<RectTransform>(); // ��������� RectTransform ������ ���������
        int indexForDifficult = 0; // ������ ��� ���������

        // �������� ���������� � ����
        foreach (var percent in _percentsTimingAttack)
        {
            RectTransform _barMeteoriteRectTransform = Instantiate(BarMeteorites[DifficultyWavesArray[indexForDifficult++]], ProgressBar.transform).GetComponent<RectTransform>();
            BarAsteroidesSides.Add(_barMeteoriteRectTransform.gameObject); // ���������� � ������

            // ��������� ������� ���������
            _barMeteoriteRectTransform.anchorMin = new Vector2(0, 0.5f);
            _barMeteoriteRectTransform.anchorMax = new Vector2(0, 0.5f);
            _barMeteoriteRectTransform.localPosition = new Vector3(rectTransform.sizeDelta.x * percent / 100, 0, 0);
        }
    }

    private void MeteroiteBarArrowsManage()
    {
        BarAsteroidsTagSides[] arrows; // ������ �������
        for (int i = 0; i < BarAsteroidesSides.Count; i++)
        {
            arrows = BarAsteroidesSides[i].GetComponentsInChildren<BarAsteroidsTagSides>(true); // ��������� �������
            for (int j = 0; j < arrows.Length; j++)
            {
                // ��������� ������� � ����������� �� ������
                if (SidesFoWaves[i][j] == 1) arrows[j].gameObject.SetActive(true);
            }
        }
    }

    private void OnDestroy()
    {
        // ������� �� �������
        Tutorial.StateTutorialEvent -= SpawnMeteoritTutorial;
        SpaceStation.NeedNewWordEvent -= NewWordSpawn;
    }
    private void NewWordSpawn()
    {
        // ���������, ���������� �� �����
        if (endingOfRoundOn)
            return; // ���� ��, ������� �� ������

        // �������� ����� ����� ��� ������
        ChoiceWordForSpawn();
        // �������� ��������� �����
        PrintChoiceWord(choiceWord);
        // ���������� ������
        DetermineEnemies();
    }

    public void SpawnMeteoritTutorial()
    {
        // �������� ������ ����������, ����������� � �������� ��������
        Meteorit[] Enemies = GetComponentsInChildren<Meteorit>();

        // ���� ��������� ��������� 1, ������� �����
        if (Tutorial.StateTutorial == 1)
            spawnFriend();

        // ���� ��������� ��������� 6
        if (Tutorial.StateTutorial == 6)
        {
            // ���� ������ ���, ������� ������ �����
            if (Enemies.Length == 0)
                spawnEnemies(1);
        }

        // ���� ��������� ��������� 7
        if (Tutorial.StateTutorial == 7)
        {
            // ���� ������ ���, ������� ����� � ������ �����
            if (Enemies.Length == 0)
            {
                spawnFriend();
                spawnEnemies(1);
            }
        }
    }

    private void PrintChoiceWord(string slovo)
    {
        // �������� ��������� ����� � ������� ��������
        PrintWord.text = slovo.ToUpper();
    }

    private void ChoiceWordForSpawn()
    {
        // ���������, ���� �� ����� ��� ������
        if (words.Count > 0)
        {
            // �������� ��������� ������ �����
            int index = UnityEngine.Random.Range(0, words.Count);
            // ���������� ��������� �����
            choiceWord = words[index];
            // ������� ��������� ����� �� ������
            words.RemoveAt(index);
        }
    }

    private void DetermineEnemies()
    {
        // �������������� ������ ������ � ���������
        enemies = new List<string>(alphabet);
        // �������� �� ������ ����� � ��������� �����
        foreach (char bukva in choiceWord)
        {
            // ���� ����� ���� � ������ ������, ������� �
            if (enemies.Contains(bukva.ToString()))
                enemies.RemoveAt(enemies.IndexOf(bukva.ToString()));
        }
    }

    void spawnFriend(int[] sides)
    {
        // �������� ��������� ����� �� ���������� �����
        VibrannayBukva = choiceWord[Random.Range(0, choiceWord.Length)];
        // ������� �������� � ��������� ������
        spawnMeteorit(VibrannayBukva.ToString(), sides);
    }

    public void spawnEnemies(int kolvo, int[] sides)
    {
        // ������� ��������� ���������� ������
        for (int i = 0; i < kolvo; i++)
        {
            KolvoMeteoritov++; // ����������� ���������� ����������
                               // �������� ��������� ����� �� ������ ������
            string enemyLetter = enemies[Random.Range(0, enemies.Count)];
            // ������� �������� � ��������� ������
            spawnMeteorit(enemyLetter, sides);
        }
    }

    void spawnFriend()
    {
        // ���� ��������� ����� ������, ������� �� ������
        if (choiceWord == "")
            return;

        // �������� ��������� ����� �� ���������� �����
        VibrannayBukva = choiceWord[Random.Range(0, choiceWord.Length)];
        // ������� �������� � ��������� ������
        spawnMeteorit(VibrannayBukva.ToString());
    }

    public void spawnEnemies(int kolvo)
    {
        // ������� ��������� ���������� ������
        for (int i = 0; i < kolvo; i++)
        {
            KolvoMeteoritov++; // ����������� ���������� ����������
                               // �������� ��������� ����� �� ������ ������
            string enemyLetter = enemies[Random.Range(0, enemies.Count)];
            // ������� �������� � ��������� ������
            spawnMeteorit(enemyLetter);
        }
    }

    void spawnMeteorit(string letter, int[] sides)
    {
        // ������� ��������� ��������� �� �������
        Meteorit prefab = Instantiate(MeteoriteTemplates[Random.Range(0, MeteoriteTemplates.Count)],
                                                            transform.position, Quaternion.identity, transform).GetComponent<Meteorit>();
        // ������������� ������� ���������
        prefab.transform.position = offsetBySpawnForSide(prefab, sides);
        // ��������� �������� ��� �������� � �����
        StartCoroutine(ReturnToSceneCouratine(prefab.gameObject));
        // ������������� ����� ���������
        prefab.Letter = letter;
    }

    void spawnMeteorit(string letter)
    {
        // ������� ��������� ��������� �� �������
        Meteorit prefab = Instantiate(MeteoriteTemplates[Random.Range(0, MeteoriteTemplates.Count)],
                                          transform.position, Quaternion.identity, transform).GetComponent<Meteorit>();
        // ������������� ������� ���������
        prefab.transform.position = offsetForSpawn(prefab.GetComponent<Meteorit>());
        // ��������� �������� ��� �������� � �����
        StartCoroutine(ReturnToSceneCouratine(prefab.gameObject));

        // ���� ��������� � ��������� � ��������� ��������� 1, ��������� ���������
        if (scene.name == "Tutorial" && Tutorial.StateTutorial == 1)
        {
            prefab.gameObject.AddComponent<TutorialFirstMeteorite>();
        }

        // ������������� ����� ���������
        prefab.Letter = letter;
    }

    public Vector2 offsetForSpawn(Meteorit meteorit)
    {
        // �������� ������ ������
        Vector2 sizeScreen = SceneColider.SizeScreen() * 0.5f;
        int offsetXY = Random.Range(0, 2); // ��������� ����� ��� ��� ������

        // ������ ������ � ����������� �� ��������� ���������
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

        // ����� ������� ��� ������
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
        // �������� ������ ������
        Vector2 sizeScreen = SceneColider.SizeScreen() * 0.5f;
        // �������� ��������� ������� ��� ������
        int side = RandomFromListNumbers(numbers);

        // ������ ������ � ����������� �� ��������� �������
        if (side == 0) // ������ �������
        {
            meteorit.SideForSpawn(1, SpaceStationTransform);
            return new Vector2((sizeScreen.x + 2), Random.Range(-sizeScreen.y, sizeScreen.y));
        }

        if (side == 1) // ����� �������
        {
            meteorit.SideForSpawn(1, SpaceStationTransform);
            return new Vector2(-(sizeScreen.x + 2), Random.Range(-sizeScreen.y, sizeScreen.y));
        }
        if (side == 2) // ������� �������
        {
            meteorit.SideForSpawn(2, SpaceStationTransform);
            return new Vector2(Random.Range(-sizeScreen.x, sizeScreen.x), (sizeScreen.y + 2));
        }
        if (side == 3) // ������ �������
        {
            meteorit.SideForSpawn(2, SpaceStationTransform);
            return new Vector2(Random.Range(-sizeScreen.x, sizeScreen.x), -(sizeScreen.y + 2));
        }
        return Vector2.zero; // ���������� ������� ������, ���� ������ �� �������
    }

    private int[] CountSidesForSpawn(int count)
    {
        // ������� ������ ��� ������ ������
        int[] Array = new int[] { 0, 0, 0, 0 };
        List<int> ListNumbers = new List<int>() { 0, 1, 2, 3 };
        int ListNumbersCount = ListNumbers.Count;

        // ������� ��������� ������� �� ������
        for (int i = 0; i < ListNumbersCount - count; i++)
        {
            ListNumbers.RemoveAt(Random.Range(0, ListNumbers.Count));
        }

        // ��������� ������ ���������� ���������
        foreach (var index in ListNumbers)
        {
            Array[index] = 1;
        }

        return Array; // ���������� ������ ������
    }

    private int RandomFromListNumbers(int[] numbers)
    {
        // �������� ������ ������
        Vector2 sizeScreen = SceneColider.SizeScreen() * 0.5f;
        List<int> ListNumbers = new List<int>();

        // ��������� ������ ��������, ������� �� ����� ����
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] != 0)
            {
                ListNumbers.Add(i);
            }
        }
        // ���������� ��������� ����� �� ������
        return ListNumbers[Random.Range(0, ListNumbers.Count)];
    }

    private int RandomSigne()
    {
        // ���������� ��������� ���� (1 ��� -1)
        int offsetXY = Random.Range(0, 2);
        if (offsetXY == 0)
            return 1;
        else
            return -1;
    }

    float RandomExceptCenter(float diapason, float exceptCenter)
    {
        // ���������� ��������� ����� � �������� ���������
        float randomChislo = UnityEngine.Random.Range(-diapason, diapason);
        // ������� ����� �� ������
        if (randomChislo >= 0)
            randomChislo += exceptCenter;
        else
            randomChislo -= exceptCenter;
        return randomChislo; // ���������� ��������������� �����
    }

    public void SpawnFireworks(int count)
    {
        // ������� ���������� ���������� ����������
        for (int i = 0; i < count; i++)
        {
            float xSpawn = transform.position.x + RandomExceptCenter(3, 1);
            float ySpawn = transform.position.y + RandomExceptCenter(3, 1);

            // ������� ���������
            GameObject prefab = Instantiate(Fireworks, new Vector2(xSpawn, ySpawn), transform.rotation);
            // ������� ��������� ����� 3 �������
            Destroy(prefab, 3);
        }
    }

    public void DestroyAllMeteorite()
    {
        // �������� ��� ��������� � �������� ��������
        Meteorit[] meteorits = GetComponentsInChildren<Meteorit>();
        // ���� ���������� ���, ������� �� ������
        if (meteorits.Length == 0)
            return;

        // ���������� ������ ��������
        foreach (var meteorit in meteorits)
        {
            meteorit.WhenDestroy();
        }
    }

    IEnumerator spawnFriendsAlwaysCouratine(float seconds)
    {
        // ����������� ���� ��� ������ ������
        while (true)
        {
            spawnFriend(); // ������� �����
            yield return new WaitForSeconds(seconds); // ���� ��������� �����
        }
    }

    IEnumerator spawnEnemiesAlwaysCouratine(float seconds, int number)
    {
        // ����������� ���� ��� ������ ������
        while (true)
        {
            if (!inWave) // ���� �� � �����
                spawnEnemies(number); // ������� ������
            yield return new WaitForSeconds(seconds); // ���� ��������� �����
        }
    }

    IEnumerator spawnWawesEnemiesCouratine(float secBetweenWave, int numberMeteorite, int countWave, int[] sides)
    {
        inWave = true; // ������������� ����, ��� �� � �����
                       // ������� ��������� ���������� ���� ������
        for (int i = 0; i < countWave; i++)
        {
            spawnEnemies(numberMeteorite, sides); // ������� ������
            yield return new WaitForSeconds(secBetweenWave); // ���� ����� �������
        }
        inWave = false; // ���������� ����
    }

    IEnumerator ReturnToSceneCouratine(GameObject where)
    {
        yield return new WaitForSeconds(3); // ���� 3 �������
        if (where == null) yield break; // ���� ������ ���������, �������
        where.AddComponent<ReturnToScene>(); // ��������� ��������� ��� �������� � �����
    }

    IEnumerator SpawnFireworksCouratine()
    {
        flagISSpawnFireworks = true; // ������������� ���� ������ �����������
                                     // ������� ���������� � �����
        for (int i = 0; i < 8; i++)
        {
            SpawnFireworks(UnityEngine.Random.Range(1, 8)); // ������� ��������� ���������� �����������
            yield return new WaitForSeconds(UnityEngine.Random.Range(0.5f, 1.5f)); // ���� ��������� �����
        }
        flagISSpawnFireworks = false; // ���������� ����
    }
}