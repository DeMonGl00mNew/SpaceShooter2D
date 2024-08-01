// Класс Fireworks отвечает за воспроизведение звуковых эффектов при запуске фейерверка
// и контроль за количеством частиц в системе частиц

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireworks : MonoBehaviour
{
    AudioSource audioSource; // Компонент для воспроизведения звуков
    ParticleSystem _parentParticleSystem; // Ссылка на систему частиц, к которой привязан скрипт
    private int _currentNumberOfParticles = 0; // Текущее количество частиц в системе

    // Метод вызывается при старте сцены
    void Start()
    {
        _parentParticleSystem = GetComponent<ParticleSystem>(); // Получаем доступ к системе частиц
        audioSource = GetComponent<AudioSource>(); // Получаем доступ к компоненту AudioSource
    }

    // Метод вызывается на каждом кадре
    void Update()
    {
        // Вычисляем разницу между текущим и предыдущим количеством частиц
        var amount = Mathf.Abs(_currentNumberOfParticles - _parentParticleSystem.particleCount);
        
        // Если количество частиц увеличилось
        if (_parentParticleSystem.particleCount > _currentNumberOfParticles)
        {
            // Если звук уже воспроизводится, то ничего не делаем
            if (audioSource.isPlaying)
                return;
            // Иначе запускаем проигрывание звука
            else
                audioSource.Play();
        }   
    }
}