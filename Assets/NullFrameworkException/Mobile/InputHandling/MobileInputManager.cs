using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NullFrameworkException.Mobile.InputHandling
{    
    public class MobileInputManager : MonoSingleton<MobileInputManager>
    {
        private JoystickInputHandler joystick;
        public SwipeInputHandler swiping;
        
        // Start is called before the first frame update
        void Start()
        {
            RunnableUtils.Setup(ref joystick, gameObject, this);
            RunnableUtils.Setup(ref swiping, gameObject, true);
            //joystick = FindObjectOfType<JoystickInputHandler>();
            //joystick.Setup(this);
        }

        // Update is called once per frame
        void Update()
        {
            RunnableUtils.Run(ref joystick, gameObject, true);
            RunnableUtils.Run(ref swiping, gameObject, true);
            //joystick.Run();
        }
    }
}
