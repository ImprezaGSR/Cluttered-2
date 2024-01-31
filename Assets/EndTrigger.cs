using UnityEngine;

public class EndTrigger : MonoBehaviour
{
    public GameObject backToShip;
    public GameManager gameManager;
    public GameObject touchButtons;
    void OnTriggerStay2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            backToShip.SetActive(true);
            touchButtons.transform.GetChild(0).gameObject.SetActive(false);
            touchButtons.transform.GetChild(3).gameObject.SetActive(true);
            if(collision.gameObject.GetComponent<PlayerScriptTouch>().pressed == true){
                collision.transform.parent.gameObject.transform.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                gameManager.CompleteLevel();
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision){
        if (collision.CompareTag("Player")){
            backToShip.SetActive(false);
            touchButtons.transform.GetChild(3).gameObject.SetActive(false);
            touchButtons.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}

