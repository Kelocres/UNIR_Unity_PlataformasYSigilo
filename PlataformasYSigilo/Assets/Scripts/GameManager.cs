using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    // Cuando el jugador muera, mostrar pantalla Game Over
    // y cargar el nivel en 2 segundos

    // Cuando el jugador llegue a la puerta de meta, mostrar pantalla Victory
    // y cargar el nivel en 2 segundos

    //private Player player;

    //Las pantallas de Game Over y Victory
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private GameObject pauseScreen;


    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }

        else
        {
            instance = this;
            //DontDestroyOnLoad(instance.gameObject);
        }

        //player = Find
    }

    public void PlayerIsDead()
    {
        StartCoroutine(WaitUntilLoadScene(gameOverScreen));
    }

    public void PlayerHasWon()
    {
        EnemyController[] allEnemies = FindObjectsOfType<EnemyController>();
        foreach (EnemyController enemy in allEnemies)
            enemy.Death();

        StartCoroutine(WaitUntilLoadScene(victoryScreen));
    }

    public void PauseGame()
    {
        pauseScreen.SetActive(true);
    }

    public void VolverAlMenuPausa()
    {
        SceneManager.LoadScene("StartScene");
    }

    IEnumerator WaitUntilLoadScene(GameObject screen)
    {
        if (screen != null)
        {
            screen.SetActive(true);
            
        }
        yield return new WaitForSeconds(2f);

        if (screen == gameOverScreen)
            SceneManager.LoadScene("ForestScene");
        else if (screen == victoryScreen)
            VolverAlMenuPausa();
    }
}
