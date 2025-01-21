using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumperAbility : MonoBehaviour
{
    public Character characterToJump;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(JumpAround());
    }

    // Update is called once per frame
    void Update()
    {
        characterToJump.Attack();
    }

    IEnumerator JumpAround()
    {
        while(true)
        {

            yield return new WaitForSeconds(3);
            transform.position = Vector3.one; //Set the position for what you want to be
            //characterToJump.Move();
            //if(distance < 0.1f) continue;
            yield return new WaitForSeconds(0.75f);
            transform.position = Vector3.one; //Set the position for what you want to be
            yield return new WaitForSeconds(0.75f);
        }
    }
}
