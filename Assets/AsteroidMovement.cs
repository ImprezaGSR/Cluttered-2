using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidMovement : MonoBehaviour
{
    public ItemSpawnerScript itemSpawner;
    public spawnAsteroids spawnAsteroids;
    public float minSpeed = 5.0f;
    public float maxSpeed = 10.0f;
    public float Rotation = 10;
    public Transform spawnZone;
    private Rigidbody2D rigidbody;
    private Vector2 zoneBounds;
    private float x;
    private float y = 1;
    public ParticleSystem explosionEffect;
    public float fadeZone = 0.8f;

    // Start is called before the first frame update
    void Awake()
    {
        x = Random.Range(1.0f, -1.0f);
        rigidbody = this.GetComponent<Rigidbody2D>();
        y = Random.Range(-1.0f,1.0f);
        // rigidbody.velocity = new Vector2(Random.Range(-minSpeed,-maxSpeed), Random.Range(-5,5));
        if (x > 0){
            if (y > 0){
                y = 1 - x;
            }else{
                y = -1 + x;
            }  
        }else if(x < 0){
            if (y > 0){
                y = -1 - x;
            }else{
                y = 1 + x;
            }
        }
        rigidbody.velocity = new Vector2(x, y) * maxSpeed;
        // rigidbody.MovePosition(rigidbody.position + (direction * speed * Time.fixedDeltaTime));
        zoneBounds = new Vector2(spawnZone.transform.localScale.x, spawnZone.transform.localScale.y);
        // transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(-90,90));
        // float angle = Mathf.Atan2(1, 1) * Mathf.Rad2Deg;
        // transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
    void Start(){
        StartCoroutine(itemWave());
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < -zoneBounds.x * fadeZone || transform.position.x > zoneBounds.x * fadeZone || transform.position.y < -zoneBounds.y * fadeZone || transform.position.y > zoneBounds.y * fadeZone){
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        if (collision.CompareTag("Enemy")){
            itemSpawner.spawnObjectsByImpact(collision.transform.position);
            explosionEffect.gameObject.transform.localScale *= collision.transform.localScale.x;
            Instantiate(explosionEffect, collision.transform.position, collision.transform.rotation);
            explosionEffect.gameObject.transform.localScale /= collision.transform.localScale.x;
            Destroy(collision.gameObject);
            FindObjectOfType<AudioManager>().Play("Explosion");
        }
        
    }

    IEnumerator itemWave(){
        while(true){
            yield return new WaitForSeconds(1.0f);
            itemSpawner.spawnObjectsEach(gameObject.transform.position);
        }
    }


}
