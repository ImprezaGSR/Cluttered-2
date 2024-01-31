using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetherScript : MonoBehaviour
{
    public Point[] points;
    public Stick[] sticks;
    public int numIterations = 1;
    void Simulate(){
        foreach(Point p in points){
            if(!p.locked) {
                Vector2 positionBeforeUpdate = p.position;
                p.position += p.position - p.prevPosition;
                p.position += Vector2.down * Physics2D.gravity * Time.deltaTime * Time.deltaTime;
                p.prevPosition = positionBeforeUpdate;
            }
        }

        for(int i = 0; i < numIterations; i++){
            foreach (Stick stick in sticks) {
                Vector2 stickCentre = (stick.pointA.position + stick.pointB.position) / 2;
                Vector2 stickDir = (stick.pointA.position - stick.pointB.position).normalized;
                if(!stick.pointA.locked)
                    stick.pointA.position = stickCentre + stickDir * stick.length / 2;
                if(!stick.pointB.locked)
                    stick.pointB.position = stickCentre + stickDir * stick.length / 2;
            }
        }
        
    }
}
