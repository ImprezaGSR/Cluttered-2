using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawnerScript : MonoBehaviour
{
    public int numberSpawn = 20;
    public List<GameObject> spawnPool;
    public GameObject quad;
    // Start is called before the first frame update
    void Start()
    {
        spawnObjects();
        Debug.Log("Spawned!");
    }

    public void spawnObjects(){
        int randomItem;
        GameObject toSpawn;
        MeshCollider collider = quad.GetComponent<MeshCollider>();

        float screenx, screeny;

        Vector3 pos;

        for(int i = 0; i < numberSpawn; i++){
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];
            screenx = Random.Range(quad.transform.localScale.x*0.5f,quad.transform.localScale.x*-0.5f);
            screeny = Random.Range(quad.transform.localScale.y*0.5f,quad.transform.localScale.y*-0.5f);
            pos = new Vector3(screenx, screeny, 0.75f);

            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }

    public void spawnObjectsEach(Vector3 asteroidPos){
        int randomItem;
        int randomCounts = Random.Range(0,10);
        GameObject toSpawn;
        if(randomCounts == 0){ 
            Debug.Log("Spawned!");
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];
            Vector3 pos = new Vector3(asteroidPos.x, asteroidPos.y, 0.75f);
            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
        }
    }

    public void spawnObjectsByImpact(Vector3 asteroidPos){
        int randomItem;
        int randomNumbers = Random.Range(0, 3);
        Debug.Log("Crashed!");
        GameObject toSpawn;
        for(int i = 0; i < randomNumbers; i++){
            randomItem = Random.Range(0, spawnPool.Count);
            toSpawn = spawnPool[randomItem];
            Vector3 pos = new Vector3(asteroidPos.x, asteroidPos.y, 0.75f);
            Instantiate(toSpawn, pos, toSpawn.transform.rotation);
            toSpawn.GetComponent<CollectableScript>().moved = true;
        }
    }
}
