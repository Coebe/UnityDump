using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour {
    public PlayerControllerCS playerController;

    public void PlayerAttack() {
        //Debug.Log("Player attacking!!");
        playerController.DoAttack();
    }

    public void PlayerDamage() {
        transform.GetComponentInParent<EnemyController>().DamagePlayer();
    }

    public void PlayWalkSound() {
        LevelManager.instance.PlaySoundAtLoc(LevelManager.instance.audioList[0], LevelManager.instance.player.transform.position);
    }
}
