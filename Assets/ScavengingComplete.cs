using UnityEngine;
using UnityEngine.SceneManagement;

public class ScavengingComplete : MonoBehaviour
{
    public void LoadInsideShip(){
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
