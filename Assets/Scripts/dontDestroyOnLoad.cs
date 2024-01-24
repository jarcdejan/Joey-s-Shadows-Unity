using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour
{
    public static DontDestroyOnLoad instance;
    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            // Check if there are other instances of SettingsMenu in the scene
            SettingsMenu[] existingInstances = FindObjectsOfType<SettingsMenu>();
            foreach (SettingsMenu existingInstance in existingInstances)
            {
                if (existingInstance != this)
                {
                    // Destroy the other instances
                    Destroy(existingInstance.gameObject);
                }
            };
        }
    }
}
