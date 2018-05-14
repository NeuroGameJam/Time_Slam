using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInicial : MonoBehaviour {
    
    public void LoadScene (int LevelNumber)
    {
        SceneManager.LoadScene(LevelNumber);
    }
}
