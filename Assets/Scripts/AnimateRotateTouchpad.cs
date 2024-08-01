// ����� ��� �������� �������� �������� ���������� (touchpad)
public class AnimateRotateTouchpad : MonoBehaviour
{
    // ���������� ��� �������� Tween �������� ��������
    Tween tweenRotate;

    // Start ���������� ����� ������ ������
    void Start()
    {
        // �������� �� �������, ������� ����� �������� ����� Rotate ��� ��������� ��������� touchpad
        Player.StateTouchpadTutorialEvent += Rotate;

        // ������������� �������� ��������: �������� ������ ��� Z �� 1 ������ �� 30 ������
        tweenRotate = transform.DORotate(new Vector3(0, 0, 1), 30)
            .SetSpeedBased() // ������������� �������� �������� � ����������� �� �������
            .SetEase(Ease.Linear) // ������������� �������� ������������
            .SetLoops(-1, LoopType.Incremental) // ����������� �������� � ��������������� �����������
            .Pause(); // ������ �������� �� �����, ����� ��� �� ���������� �����
    }

    // OnDisable ���������� ��� ���������� �������
    private void OnDisable()
    {
        // ������� �� �������, ����� �������� ������ ������
        Player.StateTouchpadTutorialEvent -= Rotate;
    }

    // ����� ��� ��������� �������� � ����������� �� �������� rotateValue
    private void Rotate(float rotateValue)
    {
        // ���������, ���������� �� tweenRotate
        if (tweenRotate == null)
            return;

        // ���� �������� rotateValue � �������� �� 0 �� 0.8, ��������� ��������
        if (rotateValue < 0.8f && rotateValue > 0)
            tweenRotate.Play();
        else
            // � ��������� ������ ������ �������� �� �����
            tweenRotate.Pause();
    }
}