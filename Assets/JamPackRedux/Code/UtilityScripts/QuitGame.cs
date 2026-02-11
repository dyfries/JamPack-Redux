using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    /* Quit
     * 
     * Call through Unity Event. Quits application.
     * 
     * Parameters: None
     * 
     * Return: None
     * 
     */
    public void Quit() {
        Application.Quit();
    }
}
