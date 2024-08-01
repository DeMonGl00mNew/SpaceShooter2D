// ��������� ��� ���������� ����������� ���������� �� ������
public class OnScreenMyStick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    private float movementRangeClamp; // �������� ��� ����������� ������� ��������

    protected override void OnEnable()
    {
        base.OnEnable();
        Tutorial.StateTutorialEvent += DefinitionMovementRange; // ������������� �� ������� ��������� ������� ��������
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        Tutorial.StateTutorialEvent -= DefinitionMovementRange; // ������������ �� ������� ��� ���������� �������
    }

    private Vector2 CurrentnewPos; // ������� ������� ���������

    // ���������� ������� �� ��������
    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData == null)
            throw new System.ArgumentNullException(nameof(eventData));

        // �������� ������� �� ������ � ��������� � ��������� ���������� ������������� �������
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform.parent.GetComponentInParent<RectTransform>(),
                                                                eventData.position, eventData.pressEventCamera, out m_PointerDownPos);

        CurrentnewPos = m_PointerDownPos;

        CurrentnewPos = Vector2.ClampMagnitude(CurrentnewPos, movementRangeClamp);
        var newPos = new Vector2(CurrentnewPos.x / movementRange, CurrentnewPos.y / movementRange);
        ((RectTransform)transform).anchoredPosition = CurrentnewPos;
        SendValueToControl(newPos);
    }

    // ����������� ������� �������� � ����������� �� ������ ��������
    public void DefinitionMovementRange()
    {
        if (Tutorial.StateTutorial < 3) movementRangeClamp = 50;
        else
            movementRangeClamp = 100;
    }

    // ���������� �������������� ���������
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

    // ���������� ���������� ���������
    public void OnPointerUp(PointerEventData eventData)
    {
        ((RectTransform)transform).anchoredPosition = m_StartPos;
        SendValueToControl(Vector2.zero);
    }

    // ������������� ��������� �������
    private void Start()
    {
        m_StartPos = ((RectTransform)transform).anchoredPosition;
    }

    // �������� ��� ������� � �������� ������� ��������
    public float movementRange
    {
        get => m_MovementRange;
        set { m_MovementRange = value; movementRangeClamp = value; }
    }

    [FormerlySerializedAs("movementRange")]
    [SerializeField]
    private float m_MovementRange = 50; // �������� ������� �������� �� ���������

    [InputControl(layout = "Vector2")]
    [SerializeField]
    private string m_ControlPath;

    private Vector3 m_StartPos; // ��������� ������� ���������
    private Vector2 m_PointerDownPos; // ������� ������� �� ��������

    protected override string controlPathInternal
    {
        get => m_ControlPath;
        set => m_ControlPath = value;
    }
}