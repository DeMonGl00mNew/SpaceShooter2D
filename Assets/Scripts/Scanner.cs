// Класс Scanner отвечает за сканер и загрузку метеоритов на борт

using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Scanner : MonoBehaviour
{
    static public bool isEmpty = true; // Переменная для проверки, пуст ли сканер
    public Image MagnitImage; // Ссылка на изображение магнита
    public Transform FirePlace; // Место для загрузки метеоритов
    public GameObject TriggeringGameObject; // Объект, который активировал сканер

    // При входе в триггер
    private void OnTriggerStay2D(Collider2D other)
    {
        TriggeringGameObject = other.gameObject;
        
        // Если объект - метеорит
        if (other.TryGetComponent<Meteorit>(out Meteorit meteorit))
        {
            // Если метеорит уже загружен или на станции, выходим из метода
            if (meteorit.Zagrugen == true || meteorit.NaStancii == true)
                return;
                
            MagnitImage.color = new Color32(255, 255, 255, 255); // Изменяем цвет изображения магнита
            
            // Если находимся в определённом уроке, изменяем его статус
            if (Tutorial.StateTutorial == 3)
                Tutorial.StateTutorial = 4;
        }
    }

    // При выходе из триггера
    private void OnTriggerExit2D(Collider2D collision)
    {
        TriggeringGameObject = null;
        MagnitImage.color = new Color32(94, 94, 94, 255); // Возвращаем цвет изображения магнита к обычному
    }

    // Метод для загрузки метеорита на борт
    public void ZagruzitNaBort()
    {
        if (TriggeringGameObject == null)
            return;

        // Если сканер пустой
        if (isEmpty == true)
        {
            // Пытаемся получить компонент Метеорит у объекта-триггера
            if (TriggeringGameObject.TryGetComponent(out Meteorit meteorit))
            {
                // Если метеорит уже загружен или на станции, выходим из метода
                if (meteorit.Zagrugen == true || meteorit.NaStancii == true)
                    return;
                    
                MagnitImage.color = new Color32(94, 94, 94, 255); // Возвращаем цвет изображения магнита к обычному
                SingletoneResourses.Instance.SoundsManager.PlaySound(4); // Проигрываем звук загрузки
                meteorit.Zagrugen = true; 
                meteorit.StateDontShoot = true;
                isEmpty = false;

                // Замораживаем движение метеорита и делаем его дочерним объектом сканера
                Rigidbody2D rb = meteorit.GetComponent<Rigidbody2D>();
                rb.velocity = Vector2.zero;
                rb.isKinematic = true;
                meteorit.transform.parent = transform;

                // Удаляем компонент для возвращения метеорита на сцену
                Destroy(TriggeringGameObject.GetComponent<ReturnToScene>());

                // Запускаем корутину для перемещения метеорита на место загрузки
                StartCoroutine(replaceMeteorite(meteorit));
            }
        }
    }

    // Корутина для перемещения метеорита на место загрузки
    IEnumerator replaceMeteorite(Meteorit meteorit)
    {
        // Проверяем условия для корутины
        if (meteorit == null || meteorit.NaStancii == true || !meteorit.Zagrugen)
            yield break;
            
        // Плавное перемещение метеорита
        meteorit.transform.DOLocalMove(FirePlace.localPosition, 2).SetSpeedBased().SetEase(Ease.Linear)
           .OnComplete(() => {
               // Если находимся в определённом уроке, изменяем его статус
               if (Tutorial.StateTutorial == 4)
                   Tutorial.StateTutorial = 5;
           }); 
           
        yield return null;
    }
}