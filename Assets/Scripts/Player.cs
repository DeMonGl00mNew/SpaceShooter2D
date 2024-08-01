// ����� ������
public class Player : MonoBehaviour
{
    public static event Action<float> StateTouchpadTutorialEvent; // ������� ��������� touchpad ��� ��������
    public float RotateSpeed = 1; // �������� ��������
    public Transform FirePlace; // ����� ��������
    public GameObject BulletPrefab; // ������ ����
    public Scanner scanner; // ������
    public SpaceStation spaceStation; // ����������� �������
    public GameObject Nozzle; // �����
    public float Accelerate = 2f; // ���������
    public float MaxSpeed = 1; // ������������ ��������
    public float Speed = 5; // ��������

    [HideInInspector] public bool neuyzvimost = true; // ����������
    private Rigidbody2D rb; // rigidbody ������
    private SpriteRenderer spriteRenderer; // ������ ������
    private Vector3 StartPlayerPosition; // ��������� ������� ������
    private float VerticalAxis = 0; // ������������ ���
    private float HorizontalAxis = 0; // �������������� ���
    private bool isEngineOn = false; // ������� �� ���������
    private Animator animator; // ��������
    private bool dontStartNeuyzvimost = false; // �� �������� ����������
    private AudioSource audioSource; // �������� �����
    private Tweener fadeTween = null; // ������� ���������/������������

    private PlayerInputMap playerInputMap; // ����� ����� ������
    private Vector2 inputVector; // ������ �����
    private float inputVectorMagnitude = 0; // ��������� �����

    private void Awake()
    {
        playerInputMap = new PlayerInputMap(); // ������� ����� ����� ��� ������
        playerInputMap.Player.Move.started += StartMoveEffects; // ������ ������� ��������
        playerInputMap.Player.Move.canceled += EndMoveEffects; // ��������� ������� ��������
        playerInputMap.Player.Enable(); // �������� ����� ����� ������
    }

    void Start()
    {
        StartPlayerPosition = transform.position; // ��������� ��������� ������� ������
        rb = GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D
        spriteRenderer = GetComponent<SpriteRenderer(); // �������� ��������� SpriteRenderer

        audioSource = GetComponent<AudioSource(); // �������� ��������� AudioSource

        NeuyzvimostFunc(); // ����� ������� ����������
        animator = GetComponent<Animator>(); // �������� ��������� Animator
    }

    void Update()
    {
        inputVector = playerInputMap.Player.Move.ReadValue<Vector2>(); // �������� ���� ������
        inputVectorMagnitude = inputVector.magnitude; // ��������� ��������� �����

        StateTouchpadTutorialEvent?.Invoke(inputVectorMagnitude); // �������� ������� ��������� touchpad
    }

    private void OnDestroy()
    {
        playerInputMap.Player.Move.started -= StartMoveEffects; // �������� ������ �������� ��������
        playerInputMap.Player.Move.canceled -= EndMoveEffects; // �������� ���������� �������� ��������
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > MaxSpeed)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed); // ������������ ��������
        if (Tutorial.StateTutorial != 4)
        {
            rb.AddRelativeForce(new Vector2(0, MovePlayer()) * Speed); // �������� ������
        }
        else
            rb.velocity = new Vector2(0,0); // ������� ������

        if (isEngineOn)
            RotatePlayer(); // �������� ������
    }

    private void RotatePlayer()
    {
        Vector2 lookDir = inputVector; // ����������� ������
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // ����
        rb.rotation = Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * RotateSpeed); // �������� ������
    }

    private float MovePlayer()
    {
        if (inputVector.magnitude < 0.8f)
            return 0; // ������� 0, ���� ��������� ����� ������ 0.8
        return inputVector.magnitude; // ������� ��������� �����
    }

    private void StartMoveEffects(InputAction.CallbackContext context)
    {
        isEngineOn = true; // �������� ���������
        if (!audioSource.isPlaying)
            audioSource.Play(); // ����������� ����
        Nozzle.SetActive(true); // ���������� �����
    }

    private void EndMoveEffects(InputAction.CallbackContext context)
    {
        isEngineOn = false; // ��������� ���������
        audioSource.Stop(); // ������������� ����
        Nozzle.SetActive(false); // ������������ �����
    }

    public void NeuyzvimostFunc()
    {
        if (dontStartNeuyzvimost)
            spaceStation.Heartint(10); // ����������� �������� �������
        dontStartNeuyzvimost = true; // �� �������� ����������
        VibrositGruz(); // �������� ������� �������� �����

        transform.position = StartPlayerPosition; // ���������� ������ �� ��������� �������
        if (fadeTween == null)
            fadeTween = spriteRenderer.DOFade(0, 0.5f).SetLoops(6, LoopType.Yoyo).SetAutoKill(false); // ������� �������� �������� ���������/������������
        else if (!fadeTween.IsPlaying())
            fadeTween.Restart();
        StartCoroutine(NeuyzvimostCouratine()); // �������� �������� ����������
    }

    public void VibrositGruz()
    {
        if (Tutorial.StateTutorial < 6)
            return; // ���� ������� �������� ������ 6, �������

        Meteorit MeteoritInCargo = GetComponentInChildren<Meteorit>(); // �������� ���� � �����
        if (MeteoritInCargo != null)
        {
            MeteoritInCargo.transform.parent = null; // ������� ��������
            Rigidbody2D rb = MeteoritInCargo.GetComponent<Rigidbody2D>(); // �������� ��������� Rigidbody2D
            rb.isKinematic = false; // ������� ����������
            MeteoritInCargo.gameObject.AddComponent<ReturnToScene>(); // ��������� ��������� ����������� �� �����
            MeteoritInCargo.Zagrugen = false; // ������������� �������������� �� false
            MeteoritInCargo.StateDontShoot = false; // ������������� ������������� �������� �� false
            Scanner.isEmpty = true; // ������ ����
        }
    }

    private IEnumerator NeuyzvimostCouratine()
    {
        neuyzvimost = true; // ����� ������
        yield return new WaitForSeconds(3f); // ���� 3 �������
        neuyzvimost = false; // ����� ��������
    }

    public void Fire()
    {
        Instantiate(BulletPrefab, FirePlace.position, transform.rotation); // ��������
    }

    public void Magnit()
    {
        if (Scanner.isEmpty)
        {
            scanner.ZagruzitNaBort(); // ��������� �� ����
        }
        else
        {
            VibrositGruz(); // �������� �����
        }
    }
}
