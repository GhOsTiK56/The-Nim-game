using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayerMenu : MonoBehaviour
{
    // Статическая переменная для отслеживания состояния паузы
    public static bool isPaused = false;

    // Ссылка на объект интерфейса меню паузы
    public GameObject pauseMenuUI;

    // Вызывается при запуске сцены
    private void Start()
    {
        // Зафиксировать курсор в центре экрана
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Вызывается каждый кадр
    private void Update()
    {
        // Установка состояния блокировки курсора в зависимости от состояния паузы
        if (isPaused)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        // Обработка нажатия клавиши Esc
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // Если игра на паузе, продолжить; иначе поставить на паузу
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    // Метод для продолжения игры
    public void Resume()
    {
        Debug.Log("Продолжить");
        // Отключение интерфейса паузы
        pauseMenuUI.SetActive(false);
        // Восстановление времени в игре
        Time.timeScale = 1.0f;
        // Установка флага паузы в false
        isPaused = false;
    }

    // Метод для постановки игры на паузу
    public void Pause()
    {
        Debug.Log("Пауза");
        // Включение интерфейса паузы
        pauseMenuUI.SetActive(true);
        // Замедление времени в игре до нуля (пауза)
        Time.timeScale = 0f;
        // Установка флага паузы в true
        isPaused = true;
    }

    // Метод для обработки события кнопки продолжения
    public void ResumeButton()
    {
        // Вызов метода Resume для продолжения игры
        Resume();
    }

    // Метод для перезапуска уровня
    public void RestartLevel()
    {
        // Сброс флага паузы перед перезагрузкой сцены
        isPaused = false;
        // Загрузка текущей сцены для перезапуска уровня
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Вызов метода Resume для продолжения игры после перезагрузки уровня
        Resume();
    }

    // Метод для обработки события кнопки выхода из игры
    public void QuitButton()
    {
        // Выход из приложения
        Application.Quit();
    }

    // Метод для обработки события кнопки возврата в главное меню
    public void MenuButton()
    {
        // Загрузка сцены "Menu"
        SceneManager.LoadScene("Menu");
    }
}