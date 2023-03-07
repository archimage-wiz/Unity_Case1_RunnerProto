using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerComponent : MonoBehaviour
{
    public GameObject the_game;

    private void OnTriggerEnter(Collider other) {
        the_game.GetComponent<TheGame>().Damage(1);
    }


}
