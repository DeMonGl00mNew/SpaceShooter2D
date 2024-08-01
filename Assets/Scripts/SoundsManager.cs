// ���������� ������ � ����
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundsManager : MonoBehaviour
{
    public AudioSource AudioSourcePlay;
    public AudioMixerGroup Mixer;
    public AudioClip Right; // ���� ����������� ��������
    public AudioClip notRight; // ���� ������������� ��������
    public AudioClip End; // ���� ���������� ����
    public AudioClip Magnit; // ���� �������
    public Slider MusicSlider; // ������� ��� ���������� ���������� ������
    public Slider EffectSlider; // ������� ��� ���������� ���������� �������� ��������
    private float currentMusicVolume; // ������� ��������� ������
    private float currentEffectVolume; // ������� ��������� �������� ��������

    private void Start()
    {
        // ������������� ��������� �������� ��������� ��� ������ � ��������
        Mixer.audioMixer.SetFloat("MusicVolume", SetValue(MusicSlider.value,ref currentMusicVolume));
        Mixer.audioMixer.SetFloat("EffectVolume", SetValue(EffectSlider.value, ref currentEffectVolume));
    }

    // ��������� �������� ��������� � ���������� ����������
    private float SetValue(float value,ref float variable)
    {
        if(value!=-80)
            variable = value;
        return value;
    }

    // ��������������� ����� �� ������ ��������� �����
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

    // ��������� ��������� ������
    public void MusicChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("MusicVolume", SetValue(volume, ref currentMusicVolume));
    }

    // ��������� ��������� �������� ��������
    public void EffectChangeVolume(float volume)
    {
        Mixer.audioMixer.SetFloat("EffectVolume", SetValue(volume, ref currentEffectVolume));
    }

    // ���������� ����� ������
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

    // ���������� �������� ��������
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