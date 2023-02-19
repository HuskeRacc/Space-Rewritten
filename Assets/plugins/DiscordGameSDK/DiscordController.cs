using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Discord;
using System;

public class DiscordController : MonoBehaviour
{
    public Discord.Discord discord;
    [SerializeField] ActivityManager activityManager;
    string state;
    readonly string developerModestr = "Developing";
    readonly string playerMode = "Surviving";
    

    void Start()
    {
        if(Application.isEditor)
        {
            state = developerModestr;
        }
        else
        {
            state = playerMode;
        }

        discord = new Discord.Discord(1076723320545878127, (System.UInt64)Discord.CreateFlags.Default);
        activityManager = discord.GetActivityManager();
        SetActivity();
    }

    void SetActivity()
    {
        var activity = new Discord.Activity
        {
            Name = "Huske's Space-Game",
            State = state,
            Details = "Chewing MRE's",
            Timestamps =
            {
                Start = 0,
            },
            Assets =
            {
                LargeImage = "gameicon",
                LargeText = "Hi :)",
            }
        };
        activityManager.UpdateActivity(activity, (res) =>
        {
            if (res == Discord.Result.Ok)
            {
                Debug.Log("Discord Running.");
            }
            else
            {
                Debug.LogError("Discord Failed");
            }
        });
    }

    // Update is called once per frame
    void Update()
    {
        discord.RunCallbacks();
    }
}
