using TMPro;
using UnityEngine;

public partial class GameManager : MonoBehaviour
{
    // Ссылка на TextMeshProUGUI для отображения сообщений о победе
    public TextMeshProUGUI victoryText;

    // Флаги для управления состоянием игры
    private bool playerTurn = true;
    private bool gameOver = false;
    private bool computerIsPlaying = false;
    private bool playerMadeMove = false;

    // Вызывается при запуске скрипта
    private void Start()
    {
        // Запуск корутины игрового цикла
        StartCoroutine(GameLoop());
    }

    // Проверка остались ли кубы на сцене
    private bool AreCubesRemaining()
    {
        return GameObject.FindWithTag("Cube") != null;
    }

    // Игровой цикл в виде корутины
    private System.Collections.IEnumerator GameLoop()
    {
        while (true)
        {
            yield return null;

            // Проверка завершения игры при отсутствии кубов
            if (!AreCubesRemaining() && !gameOver)
            {
                HandleGameEnd();
            }

            // Ход игрока
            if (playerTurn)
            {
                yield return StartCoroutine(PlayerTurn());
            }
            // Ход компьютера после того, как игрок сделал ход
            else if (playerMadeMove)
            {
                yield return StartCoroutine(ComputerTurn());
                playerMadeMove = false;
            }

            // Смена хода между игроком и компьютером
            playerTurn = !playerTurn;
        }
    }

    // Ход игрока в виде корутины
    private System.Collections.IEnumerator PlayerTurn()
    {
        Vector3 screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0);

        // Ожидание нажатия левой кнопки мыши, пока компьютер не делает свой ход
        while (!(Input.GetMouseButtonDown(0) && !computerIsPlaying))
            yield return null;

        Ray ray = Camera.main.ScreenPointToRay(screenCenter);
        RaycastHit hit;

        // Обработка клика на куб и его уничтожение
        if (Physics.Raycast(ray, out hit) && hit.collider.CompareTag("Cube"))
        {
            Destroy(hit.collider.gameObject);
            playerMadeMove = true;
        }
    }

    // Ход компьютера в виде корутины
    private System.Collections.IEnumerator ComputerTurn()
    {
        computerIsPlaying = true;

        // Ожидание некоторого времени перед ходом компьютера
        yield return new WaitForSeconds(2f);

        // Получение списка всех кубов на сцене
        GameObject[] cubes = GameObject.FindGameObjectsWithTag("Cube");

        if (cubes.Length > 0)
        {
            // Выбор случайного куба для уничтожения
            GameObject randomCube = cubes[Random.Range(0, cubes.Length)];
            Debug.Log($"Компьютер выбрал куб для уничтожения: {randomCube.name}");
            Destroy(randomCube);
        }

        computerIsPlaying = false;
    }

    // Отображение сообщения о победе
    private void DisplayVictoryMessage()
    {
        victoryText.gameObject.SetActive(true);
        victoryText.text = playerTurn ? "Компьютер победил!" : "Игрок победил!";
        StartCoroutine(HideVictoryMessageAfterDelay());
    }

    // Задержка перед скрытием сообщения о победе
    private System.Collections.IEnumerator HideVictoryMessageAfterDelay()
    {
        yield return new WaitForSeconds(3f);
        victoryText.gameObject.SetActive(false);
    }

    // Обработка завершения игры
    private void HandleGameEnd()
    {
        Debug.Log(playerTurn ? "Компьютер победил!" : "Игрок победил!");
        DisplayVictoryMessage();
        gameOver = true;
    }

    // Отображение метки в центре экрана
    private void OnGUI()
    {
        GUI.Label(new Rect(Screen.width / 2 - 6, Screen.height / 2 - 6, 12, 12), "*");
    }
}