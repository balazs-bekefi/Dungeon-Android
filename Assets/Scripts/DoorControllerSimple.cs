using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorControllerSimple : MonoBehaviour
{
    [SerializeField] private DoorTrigger doorTrigger;
    public List<Sprite> doorSkins;
    public List<Sprite> pressureplateSkin;
    public SpriteRenderer spriteRenderer;
    public List <GameObject> doorBlock;


    private void Update()
    {
        if (doorTrigger.door == true)
        {
            doorTrigger.spriteRenderer.sprite = pressureplateSkin[1];
            doorBlock[0].SetActive(false);
            doorBlock[1].SetActive(false);
            doorBlock[2].SetActive(false);
            doorBlock[3].SetActive(false);
            spriteRenderer.sprite = doorSkins[1];
        }
    }
}
