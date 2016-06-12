using UnityEngine;
using System.Collections;

public class DoorPlate : Interactable {

    bool move = false;

    int[] slots = new int[8];
    public int currentSlot = 0;

    Quaternion oldRot;
    Quaternion newRot;
    
    void Start()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = i * 45;
        }
        currentSlot = Random.Range(0, 8);
        transform.localRotation *= Quaternion.Euler(0, 0, slots[currentSlot]);
    }

    void Update()
    {
        transform.localRotation = Quaternion.Lerp(transform.localRotation, newRot, 0.3f);
    }

    public override void InteractTrigger()
    {
        base.InteractTrigger();
        currentSlot++;
        if (currentSlot > slots.Length-1)
            currentSlot = 0;
        newRot = Quaternion.Euler(90, 0, slots[currentSlot]);
    }
}
