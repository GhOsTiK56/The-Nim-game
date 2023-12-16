using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    private AudioSource audioSource;

    // Вызывается при запуске скрипта
    void Start()
    {
        // Получаем компонент AudioSource
        audioSource = GetComponent<AudioSource>();
        // Воспроизводим музыку
        PlayMusic();
    }

    // Метод для воспроизведения музыки
    void PlayMusic()
    {
        // Проверяем, не воспроизводится ли уже музыка
        if (!audioSource.isPlaying)
        {
            // Устанавливаем зацикленное воспроизведение
            audioSource.loop = true;
            // Запускаем воспроизведение музыки
            audioSource.Play();
        }
    }
}
