﻿using UnityEngine;
using System.Collections;

public enum DogState {
    Idle = 0,
    Sit,
    Bark
}

public class PlayerController : MonoBehaviour, ITimerOnBeat {

    public LevelPlayer level;
    protected DogState state;
    protected DogSoundController soundController;
    Animator animator;
    protected float timeAtNewState;
    float stateDuration;

	void Start () {
	    state = DogState.Idle;
        animator = GetComponent<Animator>();
        soundController = GetComponent<DogSoundController>();
        stateDuration = 0.4f; // seconds
        timeAtNewState = 0.0f;
        StartExtra();

        Timer timer = GameObject.Find("MainController").GetComponent<Timer>();
        timer.AddSubscriber(this);
	}

    protected virtual void StartExtra() {
        // for definition of variables declared in inherited classes
    }
	
	void Update() {
        // is it time to idle again?! //
        animator.SetInteger("DogState", (int)state);

        if(state != DogState.Idle) {
            if(stateTimeOver()) {
                state = DogState.Idle;
            }
        }
	}

    public virtual void OnTimerBeat() {
    }

    bool stateTimeOver() {
        return Time.time - timeAtNewState > stateDuration;
    }
}
