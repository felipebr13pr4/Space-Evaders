# Space Evaders

This repo is for learning. Comments are welcome. I will not be accepting pullings or modifications as that is not the purpose of this repo.

This will probably just be a Space Invaders clone but with some more things.

Develop branch will be where recent changes will be. And most of development.

If you clone this project, be aware that your editor game window resolution must correlate to the available resolutions so fonts won't break.
(1920x1080 (Most Ideal), 1440x810, 960x540, 480x270.)

Project created at "created_at": "2026-08-19T13:39:35Z", according to this repo's data from https://api.github.com/repos/felipebr13pr4/Space-Evaders

Link itch: (I'll add this when i have it which is after the project is entirely done)

# How to play

Didn't do anything yet.

# Development Notes

Fouth time using git and github.


Re-appearances from last project
- Empty for now.


I learned
- That unity packaging is really good.
- A little more about lists (they're interesting), Coroutines, Inherited static events, using the profiler, optimizing.
- Structs vs classes for small data.
- "is not" and "is" vs "!=" and "==".
- Get Component In Childrens doesn't get unactive children unless you pass true inside it, but it does grab a directly referenced children (Technically i learned this in the last project).
- Coroutines can freeze unity if caught in a infinite loop inside it without a yield return.
- Using % (remainder operator).

Learned but not implemented.
- Continue outer (C# lacks that so here its just a goto statement). I found it interesting.

Next time i should
- Empty for now.


Did I actually do it? (Did i do past "Next times i should")
---Broke and Out---
1 Document this section as i go, its easier than having to remember everything i did.
2 Step up git etiquette just like i did in this repo (Maybe trying to do branches for features? And maybe better naming).
3 Search more and not hesitate to ask things to AI.
4 Use AI as a learning tool more (it can be really useful for learning basic things which then you can fuse and build more complex things).
5 Debate and think more about the true weight of an addition, even if it seems small.
---Broke and Out---
---Did i?---
1 To be decided yet.
2 To be decided yet.
3 To be decided yet.
4 To be decided yet.
5 To be decided yet.
---Did i?---


General Thoughts
- Im starting to realize that there must be looots of situations where theres a better solution but i can't know that because i am not aware of that better solution, which also means i likely can't put it here on things i should learn or do next time as i just don't know about said thing.
- Claude's kinda good at catching bugs, I was having a really annoying one with the waves that I basically understood how to make it happen but not why it happened. The solution was kinda simple tbh. Just some troubles with coroutines still going.
- Once again Claude helped me, at optimizing when there are lots of enemies it was a bit laggy and he showed me to cache new waitforseconds, a small thing i forgot and a solution to just not using coroutine in the bullets movement. In general i learned to cache it. Anddd the Resource.Load was being used every time which was bad (that was what i forgot).
- I've created a file called TheGraveyard, i decided to keep old things there for documentations purpose, otherwise i would have just deleted the things as its saved in between these commits anyways. But this file can be used to show differences from developing and the final build.

# Project Plan

Made at the very start of the repo.

Time to make Space Invaders.

Space Invaders core consists of
- A player who can move left and right and shoot lasers up.
- Aliens that slowly come down and shoot down.
- Stationary barriers that block aliens shot that serves as covers.
- Kill all aliens.
- Less aliens = faster aliens.
- You touch aliens/alien shoots = you take damage.

Things for a polished experience
- 9:16 screen.
- All UI essentials, pause button, menu, configs window, main menu.

Ideas for fun?
- Maybe lean into evading? (because of the title name)
- Maybe a upgrade-type game? Where you do a run, eventually die but now you have some sort of money you can buy upgrades and advance more this time?
- Maybe a level game just like the last project?
- Maybe a bullet hell?
- Maybe unique patterns aliens can descend instead of the normal block grid?
- Maybe unique player bullets?
- Maybe other small fun mechanics?