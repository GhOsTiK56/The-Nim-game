using UnityEngine.SceneManagement;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Метод для обработки события начала игры
    public void PlayGame()
    {
        // Загрузка сцены "SampleScene"
        SceneManager.LoadScene("SampleScene");
    }

    // Метод для обработки события выхода из игры
    public void ExitGame()
    {
        // Вывод сообщения в консоль о закрытии игры
        Debug.Log("Игра закрылась");
        // Выход из приложения
        Application.Quit();
    }
}