using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjTypes // your custom enumeration
{
    battery, 
    pills, 
    paper,
    key,
    door,
    exitDoor
};

public class ObjData : MonoBehaviour
{
    public ObjTypes objType;
    public bool interactable;
}
