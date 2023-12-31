﻿using Assets.Game.Script.GameFlow;
using DG.Tweening;
using System.Collections;
using UnityEngine;

public class GameFlow1 : GameFlowSystem
{
    static bool _firstInit;

    public bool skip_上香;

    public float[] delays_上香;
    public ChatPrototype[] chats_上香;
    public GameObject grabMinion;
    public Transform grab位置1;
    public Transform grab位置2;
    void Start()
    {
        if (!_firstInit)
        {
            _firstInit = true;
            gameProcess.Init();
        }

        ToggleBossHpBar(false);
        ReviveSystem.instance.deathPhase = 0;

        if (skip_上香 || gameProcess.上香)
        {
            TogglePlayerControl(true);
            character.boy.gameObject.SetActive(false);
            return;
        }

        StartCoroutine(Cinematic_上香());
    }

    IEnumerator Cinematic_上香()
    {
        yield return null;
        ReviveSystem.instance.deathPhase = 0;
        TogglePlayerHpBar(false);
        ToggleBossHpBar(false);
        grabMinion.transform.position = grab位置1.position;
        TogglePlayerControl(false);

        character.boy.FlipLeft();
        character.girl.FlipLeft();

        yield return null;

        character.boy.SetAnimBool("pride", true);
        yield return new WaitForSeconds(delays_上香[0]);
        character.girl.SetMove(false, true);
        yield return new WaitForSeconds(delays_上香[1]);
        character.boy.SetMove(false, true);

        yield return new WaitForSeconds(delays_上香[2]);
        character.girl.SetMove(false, false);
        yield return new WaitForSeconds(delays_上香[3]);
        character.boy.SetMove(false, false);
        character.girl.FlipRight();
        yield return new WaitForSeconds(delays_上香[4]);
        ChatSystem.instance.ShowChat(chats_上香[0]);
        while (ChatSystem.instance.flag != "grab boy 1")
            yield return null;

        character.girl.FlipLeft();
        yield return new WaitForSeconds(delays_上香[5]);
        grabMinion.gameObject.SetActive(true);
        grabMinion.transform.DOMove(grab位置2.position, 2).SetEase(Ease.OutBack);

        yield return new WaitForSeconds(delays_上香[6]);
        ChatSystem.instance.ShowChat(chats_上香[1]);
        while (ChatSystem.instance.flag != "grab boy 2")
            yield return null;
        yield return null;
        character.boy.FlipRight();
        character.girl.FlipRight();
        yield return new WaitForSeconds(0.5f);
        character.girl.SetAnimTrigger("afraid");
        yield return new WaitForSeconds(delays_上香[7]);
        character.boy.FlipLeft();
        character.girl.SetAnimTrigger("afraid");

        yield return new WaitForSeconds(delays_上香[8]);

        ChatSystem.instance.ShowChat(chats_上香[2]);
        while (ChatSystem.instance.flag != "grab boy 3")
            yield return null;

        character.boy.SetMove(true, true);
        yield return new WaitForSeconds(delays_上香[9]);
        character.boy.SetMove(true, false);
        yield return new WaitForSeconds(0.7f);
        character.boy.FlipRight();
        yield return new WaitForSeconds(0.7f);
        character.boy.FlipLeft();
        yield return new WaitForSeconds(0.35f);
        character.boy.FlipRight();
        yield return new WaitForSeconds(0.35f);
        character.boy.FlipLeft();
        yield return new WaitForSeconds(0.35f);
        character.boy.FlipRight();
        yield return new WaitForSeconds(0.35f);
        character.boy.FlipLeft();
        yield return new WaitForSeconds(0.8f);
        character.boy.FlipRight();
        character.boy.SetAnimTrigger("sos");
        character.boy.SetAnimBool("pride", false);
        yield return new WaitForSeconds(delays_上香[10]);


        ChatSystem.instance.ShowChat(chats_上香[3]);
        while (ChatSystem.instance.flag != "grab boy 4")
            yield return null;
        yield return new WaitForSeconds(delays_上香[11]);
        character.girl.SetAnimTrigger("afraid");
        character.boy.SetMove(false, true);

        yield return new WaitForSeconds(delays_上香[12]);
        ChatSystem.instance.ShowChat(chats_上香[4]);
        while (ChatSystem.instance.flag != "grab boy 5")
            yield return null;
        grabMinion.transform.DOMove(character.boy.transform.position, 2.5f).SetEase(Ease.InOutCubic);
        character.boy.SetMove(false, false);


        yield return new WaitForSeconds(delays_上香[13]);
        grabMinion.GetComponentInChildren<Animator>().SetTrigger("melee");
        yield return new WaitForSeconds(0.35f);
        character.boy.transform.SetParent(grabMinion.transform);
        character.boy.GetComponent<Rigidbody2D>().isKinematic = true;
        grabMinion.transform.DOMove(grab位置1.position, 3.2f).SetEase(Ease.InCubic);

        yield return new WaitForSeconds(delays_上香[14]);
        character.girl.SetAnimTrigger("afraid");

        yield return new WaitForSeconds(delays_上香[15]);
        character.boy.SetAnimTrigger("sos");
        yield return new WaitForSeconds(delays_上香[16]);

        character.girl.SetAnimTrigger("afraid");
        yield return new WaitForSeconds(delays_上香[17]);
        ChatSystem.instance.ShowChat(chats_上香[5]);

        while (ChatSystem.instance.flag != "grab boy 6")
            yield return null;


        character.girl.SetMove(true, true);
        yield return new WaitForSeconds(delays_上香[18]);
        character.girl.SetMove(true, false);
        character.girl.SetAnimTrigger("jump");
        TogglePlayerControl(true);
        TogglePlayerHpBar(true);
        grabMinion.gameObject.SetActive(false);
        gameProcess.上香 = true;
    }

    public float[] delays_打输的Boss战;
    public ChatPrototype[] chats_打输的Boss战;
    public Transform boyTrunk;
    public Coroutine boySosCoroutine;

    public void PlayCinematic_打输的Boss战()
    {
        StartCoroutine(Cinematic_打输的Boss战());
    }

    IEnumerator boySos()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            character.boy.SetAnimTrigger("sos");
            yield return new WaitForSeconds(0.8f);
            character.boy.SetAnimTrigger("sos");
        }
    }

    IEnumerator Cinematic_打输的Boss战()
    {
        //对战
        //玩家被打败
        //boss喊话
        //逐渐黑屏
        //出现女主的话 分三段
        //逐渐亮屏幕 已经在洞穴里了
        TogglePlayerControl(true);

        character.boy.GetComponent<Rigidbody2D>().isKinematic = true;
        character.boy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        character.boy.transform.SetParent(boyTrunk);
        character.boy.transform.localPosition = Vector3.zero;
        character.boy.gameObject.SetActive(true);
        boySosCoroutine = StartCoroutine(boySos());
        character.girl.SetAnimTrigger("afraid");
        ReviveSystem.instance.deathPhase = 1;
        while (!character.girl.GetComponent<PlayerHealthBehaviour>().isDead)
            yield return null;

        Debug.Log("player died in 打输的Boss战");
        StopCoroutine(boySosCoroutine);
        yield return new WaitForSeconds(delays_打输的Boss战[0]);
        ChatSystem.instance.ShowChat(chats_打输的Boss战[0]);
        while (ChatSystem.instance.flag != "in cave")
            yield return null;
        gameProcess.推下山 = true;
    }

}