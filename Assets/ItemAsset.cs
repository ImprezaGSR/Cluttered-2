using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemAsset : MonoBehaviour
{
    // Start is called before the first frame update
    public static ItemAsset Instance {get; private set;}

    // Update is called once per frame

    private void Awake() {
        Instance = this;
    }

    public Sprite Cube;
    public Sprite Sphere;
    public Sprite Capsule;

    public Sprite Asteroid;
    public Sprite SolarPanel;
    public Sprite Pipe;
    public Sprite CopperPipe;
    public Sprite Cog;
    public Sprite CopperCog;
    public Sprite Plane;
    public Sprite CopperPlane;

}
