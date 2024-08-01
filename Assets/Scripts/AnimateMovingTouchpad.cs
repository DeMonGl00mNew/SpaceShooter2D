// ����������� ���������� ��� ������ � ����������
// ����������� ����������� ����������
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��������� ����� ��� �������� �������� Touchpad
public class AnimateMovingTouchpad : MonoBehaviour
{
    Tween tweenRotate; // ��������� ���������� ��� �������� ��������

    // ������� ���������� ��� ������
    void Start()
    {
        // ������������� �� ������� ��������� ��������� Touchpad
        Player.StateTouchpadTutorialEvent += Rotate;
        
        // ������� � ����������� �������� ��������
        tweenRotate = transform.DORotate(new Vector3(0, 0, -1), 30).SetSpeedBased().SetEase(Ease.Linear).SetLoops(-1, LoopType.Incremental).Pause();
    }
    
    // ������� ���������� ��� ���������� �������
    private void OnDisable()
    {
        // ������������ �� ������� ��������� ��������� Touchpad
        Player.StateTouchpadTutorialEvent -= Rotate;
    }
    
    // ������� ��� �������� �������
    private void Rotate(float rotateValue)
    {
        // ���������, ���������� �� ��������
        if (tweenRotate == null)
            return;
        
        // �������� ��� ��������� �������� � ����������� �� �������� ��������
        if (rotateValue >= 0.8f)
            tweenRotate.Play();
        else
            tweenRotate.Pause();
    }
}