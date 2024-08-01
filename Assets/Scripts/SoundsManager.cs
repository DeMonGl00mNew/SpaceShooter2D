// Управление звуком в игре
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public AudioSource AudioSourcePlay;
    public AudioMixerGroup Mixer;
    public AudioClip Right; // Звук правильного действия
    public AudioClip notRight; // Звук неправильного действия
    public AudioClip End; // Звук завершения игры
    public AudioClip Magnit; // Звук магнита
    public Slider MusicSlider; // Слайдер для управления громкостью музыки
    public Slider EffectSlider; // Слайдер для управления громкостью звуковых эффектов
    private float currentMusicVolume; // Текущая громкость музыки
    private float currentEffectVolume; // Текущая громкость звуковых эффектов

    private void Start()
    {
        // Устанавливаем начальные значения громкости для музыки и эффектов
        Mixer.audioMixer.SetFloat("MusicVolume", SetValue(MusicSlider.value,ref currentMusicVolume));
        Mixer.audioMixer.SetFloat("EffectVolume", SetValue(EffectSlider.value, ref currentEffectVolume));
    }

    // Установка значения громкости и обновление переменной
    private float SetValue(float value,ref float variable)
    {
        if(value!=-80)
            variable = value;
        return value;
    }

    // Воспроизведение звука по номеру звукового клипа
    public void PlaySound(int numberOfclip)
    {
        if (AudioSourcePlay.isPlaying)
            AudioSourcePlay.Stop();
        switch (numberOfclip)
        {
            case 1:
                AudioSourcePlay.clip = Right;
                break;
            case 2:
                AudioSourcePlay.clip = notRight;
                break;
            case 3:
                AudioSourcePlay.clip = End;
                break;
            case 4:
                AudioSourcePlay.clip = Magnit;
                break;
        }
        AudioSourcePlay.Play();
    }

    // Изменение громкости музыки
    public void MusicChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MusicVolume", SetValue(volume, ref currentMusicVolume));
    }

    // Изменение громкости звуковых эффектов
    public void EffectChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("EffectVolume", SetValue(volume, ref currentEffectVolume));
    }

    // Выключение звука музыки
    public void ToggleMusicOff(bool enabled)
    {
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("MusicVolume", -80);
            MusicSlider.value = -80;
        }
        else
        {
            Mixer.audioMixer.SetFloat("MusicVolume", currentMusicVolume);
            MusicSlider.value = currentMusicVolume;
        }
    }

    // Выключение звуковых эффектов
    public void ToggleEffectOff(bool enabled)
    {
        if (enabled)
        {
            Mixer.audioMixer.SetFloat("EffectVolume", -80);
            EffectSlider.value = -80;
        }
        else 
        {
            Mixer.audioMixer.SetFloat("EffectVolume", currentEffectVolume);
            EffectSlider.value = currentEffectVolume;
        } 
    }
}