// Класс для отображения изображения пальца
public class TutorialFireFinger : MonoBehaviour
{
    private Image image; // переменная для изображения
    
    void Start() // при старте
    {
        image = GetComponent<Image>(); // получаем компонент изображения
        Tutorial.StateTutorialEvent += FingerFireOn; // подписываемся на событие изменения состояния обучения
    }
    
    private void OnDestroy() // при уничтожении объекта
    {
        Tutorial.StateTutorialEvent -= FingerFireOn; // отписываемся от события
    }
    
    public void FingerFireOn() // метод для отображения пальца
    {
        //if (Tutorial.StateTutorial == 4) // если текущее состояние обучения равно 4
        //{
        //    GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(true); // активируем изображение
        //    image.color= new Color32(255, 255, 255, 255); // устанавливаем белый цвет для изображения
        //}
        
        //if (Tutorial.StateTutorial == 5) // если текущее состояние обучения равно 5
        //{
        //    GetComponentsInChildren<Image>(true)[1].gameObject.SetActive(false); // деактивируем изображение
        //}

    }
}