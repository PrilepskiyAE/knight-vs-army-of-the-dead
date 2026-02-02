using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private GameObject _player;
    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnAnimationEvent()
    {
        _player.GetComponent<InfoPlayer>().Damage(10f);
    }

}
