using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGame : MonoBehaviour {

    [SerializeField] Status status;

	private void OnFadeOutFinish()
    {
        status.RestartGame();
    }
}
