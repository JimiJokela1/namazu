using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScreen : MonoBehaviour
{

    void Start()
    {

    }

    void Update()
    {

    }

    public void LoadMainScene(bool randomized)
    {
        GameManager.randomized = randomized;
        SceneManager.LoadScene("Main");
    }
}
