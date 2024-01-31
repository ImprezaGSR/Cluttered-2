using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnAsteroids : MonoBehaviour
{
    public Transform spawnZone;
    public float respawnTime = 2.0f;
    public Vector2 zoneBounds;
    public float minScale = 0.5f;
    public float maxScale = 1.5f;
    private Rigidbody2D rb;
    public List<GameObject> Asteroids;

    // private float x;
    // private float y;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(asteroidWave());
        zoneBounds = new Vector2(spawnZone.transform.localScale.x, spawnZone.transform.localScale.y);
    }
    private void spawnAsteroid(){
        int randomItem = Random.Range(0, Asteroids.Count);
        GameObject a = Instantiate(Asteroids[randomItem]) as GameObject;
        float randomX = a.GetComponent<Rigidbody2D>().velocity.x;
        float randomY = a.GetComponent<Rigidbody2D>().velocity.y;
        
        // Debug.Log(randomX);
        // Debug.Log(randomY);
        a.transform.localScale *= Random.Range(minScale, maxScale);

        if(Mathf.Abs(randomX) >= Mathf.Abs(randomY) && randomX >= 0){
            a.transform.position = new Vector2(-zoneBounds.x * 0.8f, Random.Range(-zoneBounds.y * 0.5f, (zoneBounds.y + 10) * 0.5f));
        }else if (Mathf.Abs(randomX) >= Mathf.Abs(randomY) && randomX < 0){
            a.transform.position = new Vector2(zoneBounds.x * 0.8f, Random.Range(-zoneBounds.y * 0.5f, (zoneBounds.y + 10) * 0.5f));
        }else if(Mathf.Abs(randomX) < Mathf.Abs(randomY) && randomY >= 0){
            a.transform.position = new Vector2(Random.Range(-zoneBounds.x * 0.5f, (zoneBounds.x) * 0.5f), -zoneBounds.y * 0.8f);
        }else if(Mathf.Abs(randomX) < Mathf.Abs(randomY) && randomY < 0){
            a.transform.position = new Vector2(Random.Range(-zoneBounds.x * 0.5f, (zoneBounds.x) * 0.5f), zoneBounds.y * 0.8f);
        }else{
            Destroy(a);
        }
    }

    // private void ClampTransform()
    // {
    //     float x = Mathf.Clamp(rigidbody.velocity.x, -maxVelocity, maxVelocity);
    //     float y = Mathf.Clamp(rigidbody.velocity.y, -maxVelocity, maxVelocity);

    //     rigidbody.velocity = new Vector2(x,y);
    // }

    IEnumerator asteroidWave(){
        while(true){
            yield return new WaitForSeconds(respawnTime);
            spawnAsteroid();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        
        if (collision.CompareTag("Enemy")){
            Debug.Log("An asteroid is within collect zone!");
        }
    }
    
    // public float getX(){
    //     x = Random.Range(-1.0f, 1.0f);
    //     return x;
    // }
    // public float getY(){
    //     y = Random.Range(-1.0f, 1.0f);
    //     return y;
    // }
}
