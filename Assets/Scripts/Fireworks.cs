// ����� Fireworks �������� �� ��������������� �������� �������� ��� ������� ����������
// � �������� �� ����������� ������ � ������� ������

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    AudioSource audioSource; // ��������� ��� ��������������� ������
    ParticleSystem _parentParticleSystem; // ������ �� ������� ������, � ������� �������� ������
    private int _currentNumberOfParticles = 0; // ������� ���������� ������ � �������

    // ����� ���������� ��� ������ �����
    void Start()
    {
        _parentParticleSystem = GetComponent<ParticleSystem>(); // �������� ������ � ������� ������
        audioSource = GetComponent<AudioSource>(); // �������� ������ � ���������� AudioSource
    }

    // ����� ���������� �� ������ �����
    void Update()
    {
        // ��������� ������� ����� ������� � ���������� ����������� ������
        var amount = Mathf.Abs(_currentNumberOfParticles - _parentParticleSystem.particleCount);
        
        // ���� ���������� ������ �����������
        if (_parentParticleSystem.particleCount > _currentNumberOfParticles)
        {
            // ���� ���� ��� ���������������, �� ������ �� ������
            if (audioSource.isPlaying)
                return;
            // ����� ��������� ������������ �����
            else
                audioSource.Play();
        }   
    }
}