using UnityEngine;
using System.Collections;

public class GameInitialiser : MonoBehaviour
{
	void Start ()
	{
		ProgressionSave.currentlevel = -1;
		FinalCase.nbobjectif = 0;
	}
}
