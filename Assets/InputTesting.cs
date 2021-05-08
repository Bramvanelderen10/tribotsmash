using System.Collections;
using System.Collections.Generic;
using Tribot;
using UnityEngine;

namespace Porcupine
{
	public class InputTesting : MonoBehaviour 
	{
		void Start () 
		{
            foreach (var name in Input.GetJoystickNames())
            {
                print(name);
            }
        }
		
		void Update ()
        {

            
            for (int i = 0; i < TribotInput.InputCount; i++)
            {
                if (TribotInput.GetButtonDown(TribotInput.InputButtons.A, i))
                {
                    print("A pressed on pad " + TribotInput.IntToIndex(i).ToString());
                }
                if (TribotInput.GetButtonDown(TribotInput.InputButtons.RT, i))
                {
                    print("RT pressed on pad " + TribotInput.IntToIndex(i).ToString());
                }
            }
		}
	}
}

