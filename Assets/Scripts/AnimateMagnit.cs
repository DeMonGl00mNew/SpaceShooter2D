// ������ ���������
using UnityEngine;
using DG.Tweening;

// ���������� ������ AnimateMagnit, ������������ �� MonoBehaviour
public class AnimateMagnit : MonoBehaviour
{
    // ���������� ��� ��������� ��������
    public Ease ease; // ��� ��������
    public Vector3 punch = new Vector3(0.1f, 0.1f, 0.1f); // �������� �������
    public float duration = 0.5f; // ����������������� ��������
    public int vibrato = 1; // ��������
    public float elasticity = 0.5f; // ������������
    private Tweener tweener=null; // ������������ ������
    
    // ����� Start ���������� ��� ������� �����
    void Start()
    {
        // �������� �������� � ������� �� ����������
        tweener = transform.DOPunchScale(punch, duration, vibrato, elasticity).SetAutoKill(false).Pause();
        // ����� ������ Animate() ��� ������������ ��������
        Animate();
    }

    // ��������� ����� ��� ��������
    public void Animate()
    {
        // ��������, ��� �������� �� ������������� � ������ ������
        if (!tweener.IsPlaying())
            // ������������ �������� � ������������ �����������
            tweener.Play().Restart(true);
    }
}
