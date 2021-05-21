using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


//This script controls all behaviors related to selecting car parts. It highlight the gameobject it is attached to when moused over
//and will zoom in on it when clicked on, as well as activate an associated text label. It also listens for a button press and calls
//the same methods as when the object is clicked on. This script requires references to a button object and a text object.
public class CarPartController : MonoBehaviour
{
    //Variable declarations
    Color highlightColor = Color.white;
    Color originalColor;
    MeshRenderer objectRenderer;
    private GameObject mainCamera;
    public Button buttonUI;
    //Must attach button to script in Unity editor
    public GameObject labelUI;
    private GameObject allLabels;
    private Light pointLight;
    
    void Start()
    {
        
        mainCamera = GameObject.Find("Camera");
        allLabels = GameObject.Find("Labels");

        //Check if the tag of the object is SubObject. We are enabling/disabling a point light instead of changing the color of the mesh render in that case
        //because SubObjects are parts of larger meshes.
        if(gameObject.CompareTag("SubObject"))
        {
            pointLight = gameObject.GetComponent<Light>();
        }
        else
        {
            objectRenderer = gameObject.GetComponent<MeshRenderer>();
            originalColor = objectRenderer.material.color;
        }

        

        //Event listener for button click
        buttonUI.onClick.AddListener(CameraGoTo);
        buttonUI.onClick.AddListener(SetLabelActive);
    }

    
    void Update()
    {
        
    }

    //Change object color to white when mouse hovers over
    private void OnMouseOver()
    {
        
        if(gameObject.CompareTag("SubObject"))
        {
            pointLight.enabled = true;
        }
        else
        {
            objectRenderer.material.color = highlightColor;
        }
        
    }

    //Change object color back to original when mouse leaves
    private void OnMouseExit()
    {
        if (gameObject.CompareTag("SubObject"))
        {
            pointLight.enabled = false;
        }
        else
        {
            objectRenderer.material.color = originalColor;
        }
        
        
    }

    //On mouse click change the camera position and rotation to focus on object. Also make the object label appear
    private void OnMouseDown()
    {
        CameraGoTo();
        SetLabelActive();
    }

    void CameraGoTo()
    {
        //If statement makes it so that camera is never positioned inside of the car
        if (gameObject.transform.position.x > 0)
        {
            mainCamera.transform.position = gameObject.transform.position + new Vector3(1, 1, 2);
        }
        else
        {
            mainCamera.transform.position = gameObject.transform.position + new Vector3(-1, 1, 2);
        }

        mainCamera.transform.LookAt(gameObject.transform.position);
    }

    void SetLabelActive()
    {
        //Make every label inactive first, then activate the label of the object selected
        for(int i = 0; i<allLabels.transform.childCount;i++)
        {
            allLabels.transform.GetChild(i).gameObject.SetActive(false);
        }
        labelUI.SetActive(true);
    }
}
