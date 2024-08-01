// ����������� ���������� DOTween ��� ������������� ��������
// ����������� ������������ ���� Unity
using DG.Tweening;
using UnityEngine;

// ���������� ����� AnimateCrossFire, ������� ��������� �� MonoBehaviour
public class AnimateCrossFire : MonoBehaviour 
{
    Tweener tweenRotate; // ��������� ���������� ��� �������� Tween-������� (�������� ��������)
    float rotAngle = 0; // ���������� ��� �������� ���� ��������, �������������� �����

    void Start() // �����, ���������� ��� ������ �������
    {
        rotAngle -= 90; // ��������� ���� �������� �� 90 �������� (��������� �������� ��� ��������)
    }

    public void Rotate() // ��������� ����� ��� ������ �������� �������
    {
        // ������� ������ � ������� DOTween
        // DORotate ��������� ������, ������������ �������� ��������� (� ������ ������ - rotAngle % 360)
        // 0.4f - ����������������� �������� � ��������
        // SetLoops(1, LoopType.Incremental) - ������������� ���������� ���������� (1) � ��� ���������� (���������������)
        transform.DORotate(new Vector3(0, 0, rotAngle % 360), 0.4f).SetLoops(1, LoopType.Incremental);

        rotAngle -= 90; // ��������� ���� �������� �� 90 �������� ��� ���������� ��������
    }
}