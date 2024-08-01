// Класс описывает поведение космической станции
using System.Collections;
using UnityEngine;
using TMPro;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using Random = UnityEngine.Random;
using DG.Tweening;

public class SpaceStation : MonoBehaviour
{
    static public event Action NeedNewWordEvent; // Событие для запроса нового слова
    public Spawner spawner; // Объект, отвечающий за спаун метеоритов
    public TMP_Text PrintWord; // Текстовое поле для отображения слова
    public TMP_Text LoseText; // Текст для показа проигрыша
    public Image HealingImage; // Изображение для визуализации лечения
    public Slider slider; // Полоса здоровья
    public int MaxHealth = 100; // Максимальное количество здоровья
    private int Health = 100; // Текущее количество здоровья
    private string Slovo; // Слово для угадывания
    private List<string> listForSlovo ; // Список букв в слове
    private List<int> IndexCount ; // Индексы угаданных букв
    private CircleCollider2D circleCollider2D; // Коллайдер космической станции
    private bool flagForVisualDestoyStation; // Флаг для визуализации уничтожения станции
    private SpriteRenderer spriteRenderer; // Компонент отображения спрайта
    private bool stationIsBusy = false; // Флаг занятости станции
    private Sequence mySequence; // Последовательность анимаций

    // Инициализация при старте игры
    private void Start()
    {
        mySequence = DOTween.Sequence();
        circleCollider2D = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        PrepareForColoring(); // Подготовка к подсветке букв
        if(Spawner.scene.name == "Game")
            Health = MaxHealth;
        else
        {
            Health = (int)(MaxHealth * 0.75f);
            slider.value = (float)Health / 100;
        }
    }

    // Обработка столкновения с метеоритом
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Meteorit meteorit))
        {
            if (!meteorit.Zagrugen && !stationIsBusy)
                return;

            meteorit.Zagrugen = false;
            meteorit.NaStancii = true;
            Scanner.isEmpty = true;
            stationIsBusy = true;
            meteorit.transform.parent = transform;
            StartCoroutine(replaceMeteorite(meteorit));
        }
    }

    // Сопрограмма для перемещения метеорита к станции
    IEnumerator replaceMeteorite(Meteorit meteorit)
    {
        while (meteorit != null && meteorit.transform.position != transform.position)
        {
            if (meteorit == null)
                break;
            mySequence.Append(meteorit.transform.DOLocalMove(transform.position, 2).SetSpeedBased().SetEase(Ease.Linear))
                      .Append(meteorit.transform.DOScale(new Vector3(0.5f, 0.5f, 0.5f), 2).SetDelay(0.5f));

            meteorit.GetComponent<SpriteRenderer>().sortingOrder = 3;
            DoorsAnimation.Instance.MoveAnimation();

            StartCoroutine(ActivationMeteorit(meteorit));
            yield return null;
        }
        stationIsBusy = false;
        yield return null;
    }

    // Проверка идентификации метеорита
    private bool IdentificationMeteorite(Meteorit _meteorite)
    {
        foreach (char item in spawner.choiceWord)
        {
            if (item.ToString() == _meteorite.name)
                return true;
        }
        return false;
    }

    // Сопрограмма для активации метеорита
    IEnumerator ActivationMeteorit(Meteorit meteorit)
    {
        if (meteorit != null)
        {
            bool isFiend = IdentificationMeteorite(meteorit);
            yield return new WaitForSeconds(2);
            if (meteorit != null && !isFiend)
            {
                Heartint(25);
                SingletoneResourses.Instance.SoundsManager.PlaySound(2);
                meteorit.WhenDestroy();
            }
            if (meteorit != null && isFiend)
            {
                if (Spawner.scene.name == "Tutorial" && Tutorial.StateTutorial == 5)
                    Tutorial.StateTutorial = 6;
                Score.StateScore += 1;
                Healing(25);
                if (spawner.choiceWord.Length > 0)
                {
                    string letter = spawner.choiceWord.Substring(spawner.choiceWord.IndexOf(meteorit.name), 1).ToUpper();
                    spawner.choiceWord = spawner.choiceWord.Remove(spawner.choiceWord.IndexOf(meteorit.name), 1);
                    if (listForSlovo.Contains(letter))
                    {
                        ColoringGreenLetter(PrintWord, listForSlovo.IndexOf(letter));
                        listForSlovo[listForSlovo.IndexOf(letter)] = " ";
                    }
                    
                    if (spawner.choiceWord.Length <= 0)
                    {
                        if (Tutorial.StateTutorial == 7)
                        {
                            Tutorial.StateTutorial = 8;
                            Debug.Break();
                        }
                        Score.StateScore += 1;
                        NeedNewWordEvent?.Invoke();
                        PrepareForColoring();
                        ColoringWhiteLetters(PrintWord);
                    }
                }
                Destroy(meteorit.gameObject);
            }
        }

        yield return null;
    }

    // Подготовка к подсветке букв
    private void PrepareForColoring()
    {
        listForSlovo = new List<string>();
        Slovo = spawner.choiceWord.ToUpper();
        PrintWord.text = Slovo;
        IndexCount = new List<int>();
        foreach (var item in Slovo)
        {
            listForSlovo.Add(item.ToString());
        }
    }

    // Подсветка белыми буквами
    void ColoringWhiteLetters(TMP_Text _text)
    {
        _text.ForceMeshUpdate(true);
        for (int i = 0; i < Slovo.Length; i++)
        {
            int meshIndex = _text.textInfo.characterInfo[i].materialReferenceIndex;
            int vertexIndex = _text.textInfo.characterInfo[i].vertexIndex;

            Color32[] vertexColors = _text.textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0] = Color.white;
            vertexColors[vertexIndex + 1] = Color.white;
            vertexColors[vertexIndex + 2] = Color.white;
            vertexColors[vertexIndex + 3] = Color.white;
        }

        _text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    // Подсветка зелеными буквами
    void ColoringGreenLetter(TMP_Text _text, int charIndex)
    {
        IndexCount.Add(charIndex);

        _text.ForceMeshUpdate(true);
        foreach (var index in IndexCount)
        {
            int meshIndex = _text.textInfo.characterInfo[index].materialReferenceIndex;
            int vertexIndex = _text.textInfo.characterInfo[index].vertexIndex;

            Color32[] vertexColors = _text.textInfo.meshInfo[meshIndex].colors32;
            vertexColors[vertexIndex + 0] = Color.green;
            vertexColors[vertexIndex + 1] = Color.green;
            vertexColors[vertexIndex + 2] = Color.green;
            vertexColors[vertexIndex + 3] = Color.green;
        }

        _text.UpdateVertexData(TMP_VertexDataUpdateFlags.Colors32);
    }

    // Уменьшение здоровья
    public void Heartint(int health)
    {
        Health -= health;
        Health = Health < 0 ? Health = 0 : Health;

        slider.value = Health / 100f;
        StartCoroutine(BlinkingHealthCouratine(false));
        if (Health <= 0)
        {
            LoseText.gameObject.SetActive(enabled);
            if (!flagForVisualDestoyStation)
                StartCoroutine(VisualDestoyStationCouratine());

            StartCoroutine(GameOver());
        }           
    }

    // Лечение станции
    public void Healing (int health)
    {
        StartCoroutine(BlinkingHealthCouratine(true));
        Health += health;
        Health = Health > MaxHealth ? Health = MaxHealth : Health;
        slider.value = Health / 100f;
    }

    // Сопрограмма для мигания цветом при лечении или уроне
    IEnumerator BlinkingHealthCouratine(bool heal)
    {
        if(heal)
            HealingImage.color = new Color32(22, 236, 149, 255);
        else
            HealingImage.color = new Color32(255, 57, 32, 255);
        
        for (int i = 0; i < 3; i++)
        {
            HealingImage.enabled = false;
            yield return new WaitForSeconds(0.5f);
            HealingImage.enabled = true;
            yield return new WaitForSeconds(0.5f);
        }
        HealingImage.color = new Color32(255, 255, 255, 255);
    }

    // Визуализация уничтожения станции
    private void VisualDestoyStation(int count)
    {
        for (int i = 0; i < count; i++)
        {
            float x = transform.position.x + Random.Range(-circleCollider2D.radius * 1.3f, circleCollider2D.radius * 1.3f);
            float y = transform.position.x + Random.Range(-circleCollider2D.radius * 1.3f, circleCollider2D.radius * 1.3f);
            Vector3 positionForSpawn = new Vector3(x, y, 0);
            Instantiate(SingletoneResourses.Instance.explosion, positionForSpawn, Quaternion.identity);
        }
    }

    // Сопрограмма для визуализации уничтожения станции
    IEnumerator VisualDestoyStationCouratine()
    {
        StartCoroutine(LerpInvisibleCouratine());
        flagForVisualDestoyStation = true;

        for (int i = 0; i < 10; i++)
        {
            VisualDestoyStation(1);
            yield return new WaitForSeconds(Random.Range(0.2f, 1f));
        }
        spriteRenderer.enabled = false;
        Destroy(circleCollider2D);
        flagForVisualDestoyStation = false;
    }

    // Сопрограмма для плавного исчезновения станции
    IEnumerator LerpInvisibleCouratine ()
    {
        while (spriteRenderer.color.a != 0)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                        Mathf.Lerp(spriteRenderer.color.a, 0.3f, Time.deltaTime * 0.5f));
            yield return null;
        }
    }

    // Сопрограмма для завершения игры
    IEnumerator GameOver()
    {
        yield return new WaitWhile(() => spriteRenderer.color.a == 0);
        yield return new WaitForSeconds(2);        
        if(Spawner.scene.name == "Game") 
            SceneManager.LoadScene("MainMenu");
        else
            SceneManager.LoadScene("Tutorial");
    }
}