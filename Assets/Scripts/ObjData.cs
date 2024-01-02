using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ObjTypes // your custom enumeration
{
    battery, 
    pills, 
    paper
};

public class ObjData : MonoBehaviour
{
    public ObjTypes objType;
}
