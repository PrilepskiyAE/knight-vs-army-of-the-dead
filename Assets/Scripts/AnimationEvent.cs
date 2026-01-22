using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    GameObject player; 
     private void Awake() {
         player = GameObject.FindGameObjectWithTag("Player");
    }

     public void OnAnimationEvent()
    {
       player.GetComponent<InfoPlayer>().Damage(10f);
    }

}
