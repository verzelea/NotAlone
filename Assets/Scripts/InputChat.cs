using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using UnityEngine.Networking;

[RequireComponent(typeof(MotorChat))]
public class InputChat : NetworkBehaviour
{
    [SerializeField]
    private TMP_InputField inputField = null;

    private MotorChat motor;

    void Start()
    {
        motor = GetComponent<MotorChat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Submit") && !string.IsNullOrEmpty(inputField.text))
        {
            Send();
        }
    }

    [Client]
    public void Send()
    {
        Debug.Log("input");
        motor.SendChat("User1 : " + inputField.text + "\n");
        inputField.text = string.Empty;
    }
}