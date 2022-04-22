using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;

/// <summary>
/// this class controlls the input of the controllers
/// </summary>
public class InputManager : MonoBehaviour
{
    // Nodes, to which the Controllers belong
    [SerializeField]
    private XRNode xrNodeLeft = XRNode.LeftHand;
    [SerializeField]
    private XRNode xrNodeRight = XRNode.RightHand;

    // Devicelist
    private List<InputDevice> devices = new List<InputDevice>();

    //Left and right controller
    private InputDevice controllerLeft;
    private InputDevice controllerRight;

    public GameObject leftHandModel;
    public GameObject rightHandModel;

    private Animator leftHandAnimator;
    private Animator rightHandAnimator;

    public bool lastTriggerClickLeft = false;
    public bool lastTriggerClickRight = false;

    public bool lastGraspClickLeft = false;
    public bool lastGraspClickRight = false;

    public bool lastStickClickLeft = false;
    public bool lastStickClickRight = false;

    public bool lastPrimaryClickLeft = false;
    public bool lastPrimaryClickRight = false;

    /// <summary>
    /// this method initialises the controllers, takes them from the devicelist and maps them to the respective device
    /// </summary>
    public void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNodeLeft, devices);
        controllerLeft = devices.FirstOrDefault();

        InputDevices.GetDevicesAtXRNode(xrNodeRight, devices);
        controllerRight = devices.FirstOrDefault();
    }

    /// <summary>
    /// checks if controllermapping is correct
    /// </summary>
    void OnEnable()
    {
        if (!controllerLeft.isValid || !controllerRight.isValid)
            GetDevice();
    }

    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        if (!controllerLeft.isValid || !controllerRight.isValid)
            GetDevice();
        // Gets a List of all features
        //List<InputFeatureUsage> features = new List<InputFeatureUsage>();
        //device.TryGetFeatureUsages(features);

        //prints all features and their types
        //foreach(var feature in features) {
        //    Debug.Log("Feature name:  " + feature.name + "     Feature Type: " + feature.type);
        //}
        controllerLeft.TryGetFeatureValue(CommonUsages.gripButton, out bool graspClickLeft);
        lastGraspClickLeft = graspClickLeft;

        controllerRight.TryGetFeatureValue(CommonUsages.gripButton, out bool graspClickRight);
        lastGraspClickRight = graspClickRight;

        controllerLeft.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerClickLeft);
        lastTriggerClickLeft = triggerClickLeft;

        controllerRight.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerClickRight);
        lastTriggerClickRight = triggerClickRight;

        controllerLeft.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryClickLeft);
        lastPrimaryClickLeft = primaryClickLeft;

        controllerRight.TryGetFeatureValue(CommonUsages.triggerButton, out bool primaryClickRight);
        lastPrimaryClickRight = primaryClickRight;
    }

    #region Trigger
    /// <summary>
    /// reads and returns the value of the left trigger (Indexfinger)
    /// </summary>
    /// <returns>percentage value of press state</returns>
    public float leftTriggerValue()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        return triggerValue;
    }

    /// <summary>
    /// reads and returns the value of the right trigger (Indexfinger)
    /// </summary>
    /// <returns>percentage value of press state</returns>
    public float rightTriggerValue()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
        return triggerValue;
    }

    /// <summary>
    /// reads and returns the value if the left trigger is fully clicked (Indexfinger)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool leftTriggerClick()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton);
        lastTriggerClickLeft = triggerButton;
        return triggerButton;
    }

    public bool leftTriggerClickOnce()
    {
        if (!lastTriggerClickLeft)
        {
            controllerLeft.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton);
            return triggerButton;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// reads and returns the value if the right trigger is fully clicked (Indexfinger)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool rightTriggerClick()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton);
        return triggerButton;
    }

    public bool rightTriggerClickOnce()
    {
        if (!lastTriggerClickRight)
        {
            controllerRight.TryGetFeatureValue(CommonUsages.triggerButton, out bool triggerButton);
            return triggerButton;
        }
        else
        {
            return false;
        }

    }

    #endregion

    #region Grip
    /// <summary>
    /// reads and returns the value if the left grip is fully clicked (Middlefinger)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool leftGripClick()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton);
        return gripButton;
    }

    public bool leftGripClickOnce()
    {
        if (!lastGraspClickLeft)
        {
            controllerLeft.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton);
            return gripButton;
        } else
        {
            return false;
        }
            
    }

    /// <summary>
    /// reads and returns the value if the right grip is fully clicked (Middlefinger)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool rightGripClick()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton);
        return gripButton;
    }

    public bool rightGripClickOnce()
    {
        if (!lastGraspClickRight)
        {
            controllerRight.TryGetFeatureValue(CommonUsages.gripButton, out bool gripButton);
            return gripButton;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// reads and returns the value of the left grip (Middlefinger)
    /// </summary>
    /// <returns>percentage value of press state</returns>
    public float leftGripValue()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        return gripValue;
    }

    /// <summary>
    /// reads and returns the value of the right grip (Middlefinger)
    /// </summary>
    /// <returns>percentage value of press state</returns>
    public float rightGripValue()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.grip, out float gripValue);
        return gripValue;
    }
    #endregion

    #region Stick

    /// <summary>
    /// reads and returns the position value of the left Joystick (Thumb)
    /// </summary>
    /// <returns>Vector2 of coordinates</returns>
    public Vector2 leftStickValue()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stickValue);
        return stickValue;
    }

    /// <summary>
    /// reads and returns the position value of the right Joystick (Thumb)
    /// </summary>
    /// <returns>Vector2 of coordinates</returns>
    public Vector2 rightStickValue()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.primary2DAxis, out Vector2 stickValue);
        return stickValue;
    }

    /// <summary>
    /// reads and returns the value if the left stick is clicked (Thumb)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool leftStickClick()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool stickClick);
        return stickClick;
    }

    public bool leftStickClickOnce()
    {
        if (!lastStickClickLeft)
        {
            controllerLeft.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool stickClick);
            return stickClick;
        }
        else
        {
            return false;
        }

    }

    /// <summary>
    /// reads and returns the value if the right stick is clicked (Thumb)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool rightStickClick()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool stickClick);
        return stickClick;
    }

    public bool rightStickClickOnce()
    {
        if (!lastStickClickRight)
        {
            controllerRight.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out bool stickClick);
            return stickClick;
        }
        else
        {
            return false;
        }

    }

    #endregion

    #region Touchpad

    /// <summary>
    /// reads and returns the position value of the left Touchpad (Thumb)
    /// </summary>
    /// <returns>Vector2 of coordinates</returns>
    public Vector2 leftTouchpadValue()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Vector2 padValue);
        return padValue;
    }

    /// <summary>
    /// reads and returns the position value of the right Touchpad (Thumb)
    /// </summary>
    /// <returns>Vector2 of coordinates</returns>
    public Vector2 rightTouchpadValue()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.secondary2DAxis, out Vector2 padValue);
        return padValue;
    }

    /// <summary>
    /// reads and returns the value if the left Touchpad is touched (Thumb)
    /// </summary>
    /// <returns>true if touched</returns>
    public bool leftTouchpadTouch()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch, out bool padTouch);
        return padTouch;
    }

    /// <summary>
    /// reads and returns the value if the right Touchpad is touched (Thumb)
    /// </summary>
    /// <returns>true if touched</returns>
    public bool rightTouchpadTouch()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.secondary2DAxisTouch, out bool padTouch);
        return padTouch;
    }

    /// <summary>
    /// reads and returns the value if the left Touchpad is clicked (Thumb)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool leftTouchpadClick()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool padClick);
        return padClick;
    }

    /// <summary>
    /// reads and returns the value if the right Touchpad is clicked (Thumb)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool rightTouchpadClick()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out bool padClick);
        return padClick;
    }

    #endregion

    #region Menu
    /// <summary>
    /// reads and returns the value if the left Menu Button is clicked (Thumb)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool leftMenu()
    {
        controllerLeft.TryGetFeatureValue(CommonUsages.menuButton, out bool menu);
        return menu;
    }

    /// <summary>
    /// reads and returns the value if the right Menu Button is clicked (Thumb)
    /// </summary>
    /// <returns>true if clicked</returns>
    public bool rightMenu()
    {
        controllerRight.TryGetFeatureValue(CommonUsages.menuButton, out bool menu);
        return menu;
    }

    public bool leftPrimaryButtonClickOnce()
    {
        if (!lastPrimaryClickLeft)
        {
            controllerLeft.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton);
            return primaryButton;
        }
        else
        {
            return false;
        }
    }

    public bool rightPrimaryButtonClickOnce()
    {
        if (!lastPrimaryClickRight)
        {
            controllerRight.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButton);
            return primaryButton;
        }
        else
        {
            return false;
        }
    }
    #endregion
}
