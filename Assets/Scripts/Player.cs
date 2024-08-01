// класс игрока
public class Player : MonoBehaviour
{
    public static event Action<float> StateTouchpadTutorialEvent; // событие состояния touchpad для обучения
    public float RotateSpeed = 1; // скорость вращения
    public Transform FirePlace; // место стрельбы
    public GameObject BulletPrefab; // префаб пули
    public Scanner scanner; // сканер
    public SpaceStation spaceStation; // космическая станция
    public GameObject Nozzle; // сопло
    public float Accelerate = 2f; // ускорение
    public float MaxSpeed = 1; // максимальная скорость
    public float Speed = 5; // скорость

    [HideInInspector] public bool neuyzvimost = true; // уязвимость
    private Rigidbody2D rb; // rigidbody игрока
    private SpriteRenderer spriteRenderer; // спрайт игрока
    private Vector3 StartPlayerPosition; // начальная позиция игрока
    private float VerticalAxis = 0; // вертикальная ось
    private float HorizontalAxis = 0; // горизонтальная ось
    private bool isEngineOn = false; // включен ли двигатель
    private Animator animator; // аниматор
    private bool dontStartNeuyzvimost = false; // не начинать уязвимость
    private AudioSource audioSource; // источник звука
    private Tweener fadeTween = null; // плавное появление/исчезновение

    private PlayerInputMap playerInputMap; // карта ввода игрока
    private Vector2 inputVector; // вектор ввода
    private float inputVectorMagnitude = 0; // магнитуда ввода

    private void Awake()
    {
        playerInputMap = new PlayerInputMap(); // создаем карту ввода для игрока
        playerInputMap.Player.Move.started += StartMoveEffects; // начать эффекты движения
        playerInputMap.Player.Move.canceled += EndMoveEffects; // завершить эффекты движения
        playerInputMap.Player.Enable(); // включить карту ввода игрока
    }

    void Start()
    {
        StartPlayerPosition = transform.position; // сохраняем начальную позицию игрока
        rb = GetComponent<Rigidbody2D>(); // получаем компонент Rigidbody2D
        spriteRenderer = GetComponent<SpriteRenderer(); // получаем компонент SpriteRenderer

        audioSource = GetComponent<AudioSource(); // получаем компонент AudioSource

        NeuyzvimostFunc(); // вызов функции уязвимости
        animator = GetComponent<Animator>(); // получаем компонент Animator
    }

    void Update()
    {
        inputVector = playerInputMap.Player.Move.ReadValue<Vector2>(); // получаем ввод игрока
        inputVectorMagnitude = inputVector.magnitude; // вычисляем магнитуду ввода

        StateTouchpadTutorialEvent?.Invoke(inputVectorMagnitude); // вызываем событие состояния touchpad
    }

    private void OnDestroy()
    {
        playerInputMap.Player.Move.started -= StartMoveEffects; // отменяем начало эффектов движения
        playerInputMap.Player.Move.canceled -= EndMoveEffects; // отменяем завершение эффектов движения
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > MaxSpeed)
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, MaxSpeed); // ограничиваем скорость
        if (Tutorial.StateTutorial != 4)
        {
            rb.AddRelativeForce(new Vector2(0, MovePlayer()) * Speed); // движение игрока
        }
        else
            rb.velocity = new Vector2(0,0); // останов игрока

        if (isEngineOn)
            RotatePlayer(); // вращение игрока
    }

    private void RotatePlayer()
    {
        Vector2 lookDir = inputVector; // направление обзора
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg; // угол
        rb.rotation = Mathf.LerpAngle(rb.rotation, angle, Time.fixedDeltaTime * RotateSpeed); // вращение игрока
    }

    private float MovePlayer()
    {
        if (inputVector.magnitude < 0.8f)
            return 0; // возврат 0, если магнитуда ввода меньше 0.8
        return inputVector.magnitude; // возврат магнитуды ввода
    }

    private void StartMoveEffects(InputAction.CallbackContext context)
    {
        isEngineOn = true; // включаем двигатель
        if (!audioSource.isPlaying)
            audioSource.Play(); // проигрываем звук
        Nozzle.SetActive(true); // активируем сопло
    }

    private void EndMoveEffects(InputAction.CallbackContext context)
    {
        isEngineOn = false; // выключаем двигатель
        audioSource.Stop(); // останавливаем звук
        Nozzle.SetActive(false); // деактивируем сопло
    }

    public void NeuyzvimostFunc()
    {
        if (dontStartNeuyzvimost)
            spaceStation.Heartint(10); // увеличиваем здоровье станции
        dontStartNeuyzvimost = true; // не начинать уязвимость
        VibrositGruz(); // вызываем функцию вибрации груза

        transform.position = StartPlayerPosition; // возвращаем игрока на начальную позицию
        if (fadeTween == null)
            fadeTween = spriteRenderer.DOFade(0, 0.5f).SetLoops(6, LoopType.Yoyo).SetAutoKill(false); // создаем анимацию плавного появления/исчезновения
        else if (!fadeTween.IsPlaying())
            fadeTween.Restart();
        StartCoroutine(NeuyzvimostCouratine()); // начинаем корутину уязвимости
    }

    public void VibrositGruz()
    {
        if (Tutorial.StateTutorial < 6)
            return; // если уровень обучения меньше 6, выходим

        Meteorit MeteoritInCargo = GetComponentInChildren<Meteorit>(); // получаем груз в карго
        if (MeteoritInCargo != null)
        {
            MeteoritInCargo.transform.parent = null; // удаляем родителя
            Rigidbody2D rb = MeteoritInCargo.GetComponent<Rigidbody2D>(); // получаем компонент Rigidbody2D
            rb.isKinematic = false; // снимаем кинематику
            MeteoritInCargo.gameObject.AddComponent<ReturnToScene>(); // добавляем компонент возвращения на сцену
            MeteoritInCargo.Zagrugen = false; // устанавливаем взагруженность на false
            MeteoritInCargo.StateDontShoot = false; // устанавливаем невозможность стрельбы на false
            Scanner.isEmpty = true; // сканер пуст
        }
    }

    private IEnumerator NeuyzvimostCouratine()
    {
        neuyzvimost = true; // игрок уязвим
        yield return new WaitForSeconds(3f); // ждем 3 секунды
        neuyzvimost = false; // игрок неуязвим
    }

    public void Fire()
    {
        Instantiate(BulletPrefab, FirePlace.position, transform.rotation); // стрельба
    }

    public void Magnit()
    {
        if (Scanner.isEmpty)
        {
            scanner.ZagruzitNaBort(); // загрузить на борт
        }
        else
        {
            VibrositGruz(); // вибрация груза
        }
    }
}
