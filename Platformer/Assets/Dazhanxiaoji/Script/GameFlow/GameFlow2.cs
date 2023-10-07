using Assets.Game.Script.GameFlow;
using com;
using System.Collections;
using UnityEngine;

public class GameFlow2 : GameFlowSystem
{
    void Start()
    {
        ToggleBossHpBar(false);
        ReviveSystem.instance.deathPhase = 0;

        if (gameProcess.出山洞)
        {

        }
        else
        {
            StartCoroutine(Cinematic_山洞());
        }

    }
    IEnumerator PlayerBecomeWarrior()
    {
        TogglePlayerControl(false);
        yield return new WaitForSeconds(1);

        SoundSystem.instance.Play("change");
        psChange.Play(true);
        ChatSystem.instance.ShowChat(chats_山洞中[0]);
        PlayerBehaviour.instance.ToggleWarrierState(true);

        while (ChatSystem.instance.flag != "change done")
            yield return null;

        character.girl.SetAnimTrigger("attack");
        yield return new WaitForSeconds(0.5f);
        TogglePlayerControl(true);

        ReviveSystem.instance.deathPhase = 2;
    }


    public float[] delays_山洞中;
    public ChatPrototype[] chats_山洞中;
    public ParticleSystem psChange;
    IEnumerator Cinematic_山洞()
    {
        yield return null;
    }

    IEnumerator Cinematic_决战()
    {
        yield return null;
    }

    IEnumerator Cinematic_结局()
    {
        yield return null;
    }
}