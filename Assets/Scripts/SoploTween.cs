// ����� ��� ������� �������� � ������� ���������� DOTween
// ����������� ����������� ���������
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SoploTween : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Tweener tweener;
    public Ease ease; // �������� ��� ������� ���� ��������
    public Vector3 punch = new Vector3(0.1f, 0.1f, 0.1f); // �������� ��� ������� ����������� �������
    public float duration= 0.5f; // ����������������� ��������
    public int vibrato=1; // ���������� "��������"
    public float elasticity = 0.5f; // ������������ ��������

    // ������� ���������� ��� ������ �������
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer();
        spriteRenderer.DOColor(new Color(0.5424528f, 0.5992324f, 1, 1), 0.5f)
            .SetLoops(-1, LoopType.Yoyo) // ��������� ���������� ��������
            .SetEase(ease); // ��������� ���� ��������
        spriteRenderer.DOFade(0.5f, 0.5f).SetLoops(-1, LoopType.Yoyo); // �������� ��������� �������
        transform.DOPunchScale(punch, duration, vibrato, elasticity).SetLoops(-1, LoopType.Restart); // �������� ��������� ������� �������
    }

    // ������� ���������� ������ ����
    void Update()
    {
        
    }
}