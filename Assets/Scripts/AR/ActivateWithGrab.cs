using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Qualcomm.Snapdragon.Spaces;

public class ActivateWithGrab : MonoBehaviour
{
    [SerializeField] GameObject mainGameObject;
    [SerializeField] GameObject leftObject;
    [SerializeField] GameObject rightObject;
    [SerializeField] bool allowOnlyOneHand;

    SpacesHandManager handManager;

    bool leftActive;
    bool rightActive;

    // Start is called before the first frame update
    void Start()
    {
        handManager = FindObjectOfType<SpacesHandManager>();
        handManager.handsChanged += OnHandChange;

        leftActive = false;
        rightActive = false;
    }

    void OnHandChange(SpacesHandsChangedEventArgs args)
    {
        bool isGrabbing = false;
        bool leftIsGrabbing = false;
        bool rightIsGrabbing = false;

        foreach (SpacesHand hand in args.updated) 
        {
            if(hand.CurrentGesture.Type == SpacesHand.GestureType.GRAB)
            {
                if(hand.IsLeft == true)
                {
                    leftIsGrabbing = true;
                    isGrabbing = true;
                }
                else if(hand.IsLeft == false)
                {
                    rightIsGrabbing = true;
                    isGrabbing = true;
                }
            }
            
        }

        if(allowOnlyOneHand)
        {
            if(leftActive && !leftIsGrabbing)
            {
                leftActive = false;
            }
            if(rightActive && !rightIsGrabbing)
            {
                rightActive = false;
            }

            if(leftIsGrabbing & !rightActive)
            {
                leftActive = true;
            }
            else if(rightIsGrabbing & !leftActive)
            {
                rightActive = true;
            }

            mainGameObject?.SetActive(isGrabbing);
            leftObject?.SetActive(leftActive);
            rightObject.SetActive(rightActive);
        }
        else if(isGrabbing)
        {
            leftObject?.SetActive(leftIsGrabbing);
            rightObject?.SetActive(rightIsGrabbing);

            leftActive = true;
            rightActive = true;
        }

    }
}
