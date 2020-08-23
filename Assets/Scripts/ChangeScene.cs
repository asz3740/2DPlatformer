using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void moveLobby()
    {
        SceneManager.LoadScene("Lobby");
    }

    public void moveIngame()
    {
        SceneLoad.LoadScene("Ingame");
    }
}
