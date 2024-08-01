// ����� ��� ��������� ������� ���������
public class TouchMeteorite : MonoBehaviour
{
    public Meteorit meteorit; // ������ �� ������ ���������
    bool _stateDontShoot = false; // ���������� ��� �������� ��������� "�� ��������"

    [HideInInspector] // ������ ���������� �� ����������
    public bool StateDontShootPropety // �������� ��� ������� � ���������� _stateDontShoot
    {
        get => _stateDontShoot; // ��������� ��������
        set // ��������� ��������
        {
            _stateDontShoot = value; // ��������� ��������
            meteorit.StateDontShoot = _stateDontShoot; // ������������ �������� ���������
        }
    }
}