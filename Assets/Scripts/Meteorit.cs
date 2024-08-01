// ����� ��� ���������� �����������
// �������� ������ ��� ��������, ������������ � ����������� ����������

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;
using TMPro;

public class Meteorit : MonoBehaviour
{
    private Rigidbody2D rb; // ��������� Rigidbody2D ��� ���������� ������� �������
    private int sideOfSpawn = 0; // ������� ������ ���������
    public GameObject EffectSelected; // ������ ��� ��������� �� ���������
    public Transform transformCenter; // �����, ������������ �������� ��������� ��������
    public string Letter { get { return letter; } set { letter = value; MountingLetter(); } } // ����� �� ���������
    [HideInInspector] public bool Zagrugen = false; // ���� ��������
    [HideInInspector] public bool NaStancii = false; // ���� ������������ � ����������� ��������
    [HideInInspector] public bool WithEffect = false; // ���� ������� �������
    public TMP_Text LetterText; // ����� ����� �� ���������
    private string letter; // ���� �����

    [HideInInspector] public event Action<Meteorit> StateDontShootEvent; // ������� ��� ������� ��������� "�� ��������"
    private bool _stateDontShoot = false; // ��������� "�� ��������"

    // ����� ��� ��������� ����� �� ���������
    public void MountingLetter()
    {
        LetterText.text = letter.ToUpper(); // ��������� ������ �� ���������
        gameObject.name = letter; // ��������� ����� ������� ��� �����
    }

    // �������� ��� ���������� ���������� "�� ��������"
    public bool StateDontShoot
    {
        get { return _stateDontShoot; }
        set { _stateDontShoot = value; StateDontShootEvent?.Invoke(this); }
    }

    void Start()
    {
        _stateDontShoot = false; // ������������� ��������� "�� ��������"
        StateDontShootEvent += Shinning; // �������� �� �������
        Vector3 target;
        Vector2 sizeScreen = SceneColider.Instance.SizeScreen() * Spawner.Instance.RangeForMeteorits; // ��������� �������� ������
        target = transformCenter.position - transform.position; // ���������� ����������� � ������

        rb = GetComponent<Rigidbody2D>(); // ��������� ���������� Rigidbody2D

        // ��������� �������� ��������� � ����������� �� ��������� ��������� ��� �����
        if (Tutorial.StateTutorial >= 6 || Spawner.scene.name == "Game")
        {
            if (sideOfSpawn == 1)
                rb.velocity = new Vector2(target.x, target.y + Random.Range(-sizeScreen.y, sizeScreen.y)).normalized; // ����� ������
            if (sideOfSpawn == 2)
                rb.velocity = new Vector2(target.x + Random.Range(-sizeScreen.x, sizeScreen.x), target.y).normalized; // ����� �����
        }

        // ��������� ������� �������� ��� �������� ���������
        if (Tutorial.StateTutorial == 0 || Tutorial.StateTutorial > 5) rb.angularVelocity = Random.Range(-30f, 30f);
    }

    private void OnDestroy()
    {
        Spawner.Instance.SpawnMeteoritTutorial(); // ����� ��������� � ��������� ��� �����������
        StateDontShootEvent -= Shinning; // ������� �� �������
    }

    // ����� ��� ��������� ������� ������ ���������
    public void SideForSpawn(int side, Transform where)
    {
        transformCenter = where; // ��������� ������ ������
        sideOfSpawn = side; // ��������� ������� ������
    }

    // ����� ��� ��������� ������������
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Zagrugen || NaStancii) // �������� ������
            return;

        // ���� ������������ � �����
        if (other.TryGetComponent(out Bullet bullet))
        {
            if (Tutorial.StateTutorial == 6)
            {
                Tutorial.StateTutorial = 7; // ������� � ���������� ����� ���������
            }
            Destroy(other.gameObject); // ����������� ����
            Spawner.Instance.SpawnBonus(transform); // ����� ������
            WhenDestroy(); // ����������� ���������
        }
        // ���� ������������ � �������
        else if (other.TryGetComponent(out Player player))
        {
            if (!player.neuyzvimost) // �������� �� ������������ ������
            {
                player.NeuyzvimostFunc(); // ���������� ������������
            }
            WhenDestroy(); // ����������� ���������
        }
        // ���� ������������ � ����������� ��������
        else if (other.TryGetComponent(out SpaceStation spaceStation))
        {
            spaceStation.Heartint(25); // ���������� �������� �������
            WhenDestroy(); // ����������� ���������
        }
    }

    // ����� ��� �������� ������� ��� ������������
    private void Shinning(Meteorit meteorit)
    {
        if (WithEffect) // �������� ������� �������
            return;
        Instantiate(EffectSelected, transform); // �������� �������
        WithEffect = true; // ��������� ����� ������� �������
    }

    // ����� ��� ����������� ���������
    public void WhenDestroy()
    {
        Instantiate(SingletoneResourses.Instance.explosion, transform.position, Quaternion.identity); // �������� ������� ������
        Destroy(gameObject); // ����������� ���������
    }
}