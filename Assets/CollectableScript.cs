using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CollectableScript : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public Items.ItemType CollectableType;
    private TextMeshPro textMeshPro;
    public bool moved = false;

    private void Awake(){
    }

    void Start(){
        rigidbody = GetComponent<Rigidbody2D>();
    }
    

    public void SetItem(Items item){
        if (item.amount > 1) {
            textMeshPro.SetText(item.amount.ToString());
        }else {
            textMeshPro.SetText("");
        }
    }
    
    private void OnTriggerStay2D(Collider2D collision){
        if (collision.CompareTag("Wall")){
            Destroy(gameObject);
        }
    }

    void FixedUpdate(){
        if (moved == true){
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            rigidbody.velocity = randomDir * 25.0f;
            moved = false;
        }
        rigidbody.drag = 5f;
    }
}