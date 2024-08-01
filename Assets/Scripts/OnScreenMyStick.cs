// Компонент для управления виртуальным джойстиком на экране
public class OnScreenMyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private float movementRangeClamp; // Значение для ограничения области движения

    protected override void OnEnable()
    {
        base.OnEnable();
        Tutorial.StateTutorialEvent += DefinitionMovementRange; // Подписываемся на событие изменения области движения
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Tutorial.StateTutorialEvent -= DefinitionMovementRange; // Отписываемся от события при отключении объекта
    }

    private Vector2 CurrentnewPos; // Текущая позиция джойстика

    // Обработчик нажатия на джойстик
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        // Получаем позицию по экрану и переводим в локальные координаты родительского объекта
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(),
                                                                eventData.position, eventData.pressEventCamera, out m_PointerDownPos);

        CurrentnewPos = m_PointerDownPos;

        CurrentnewPos = Vector2.ClampMagnitude(CurrentnewPos, movementRangeClamp);
        var newPos = new Vector2(CurrentnewPos.x / movementRange, CurrentnewPos.y / movementRange);
        ((RectTransform)transform).anchoredPosition = CurrentnewPos;
        SendValueToControl(newPos);
    }

    // Определение области движения в зависимости от уровня обучения
    public void DefinitionMovementRange()
    {
        if (Tutorial.StateTutorial < 3) movementRangeClamp = 50;
        else
            movementRangeClamp = 100;
    }

    // Обработчик перетаскивания джойстика
    public void OnDrag(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out var position);

        var delta = position;

        delta = Vector2.ClampMagnitude(delta, movementRangeClamp);
        ((RectTransform)transform).anchoredPosition = m_StartPos + (Vector3)delta;

        var newPos = new Vector2(delta.x / movementRange, delta.y / movementRange);
        SendValueToControl(newPos);
    }

    // Обработчик отпускания джойстика
    public void OnPointerUp(PointerEventData eventData)
    {
        ((RectTransform)transform).anchoredPosition = m_StartPos;
        SendValueToControl(Vector2.zero);
    }

    // Инициализация стартовой позиции
    private void Start()
    {
        m_StartPos = ((RectTransform)transform).anchoredPosition;
    }

    // Свойство для доступа к значению области движения
    public float movementRange
    {
        get => m_MovementRange;
        set { m_MovementRange = value; movementRangeClamp = value; }
    }

    [FormerlySerializedAs("movementRange")]
    [SerializeField]
    private float m_MovementRange = 50; // Значение области движения по умолчанию

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    private Vector3 m_StartPos; // Начальная позиция джойстика
    private Vector2 m_PointerDownPos; // Позиция нажатия на джойстик

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }
}