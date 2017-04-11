using System;
using Ensage;
using Ensage.Common;

namespace KarlAschnikov
{
    internal class Program
    {
        private static Entity _me;

        private static void Main(string[] args)
        {
            Events.OnLoad += Events_OnLoad;
            Events.OnClose += Events_OnClose;
        }

        // just memeing ^_^ 
        public static string[] Disrespect =
        {
            "What's the difference between {0} and eggs? Eggs get laid and {0} doesn't.",
            "{0}'s birth certificate is an apology letter from the condom factory.",
            "{0} must have been born on a highway because that's where most accidents happen.",
            "If I wanted to kill myself I'd climb your ego and jump to your skill {0}",
            "Roses are red violets are blue, God made me great, the opposite of you. ",
            "{0} you are so useless I would unplug your life support to charge my phone.",
            "You are a disgrace to your family {0}",
            "Somewhere out there is a tree, tirelessly producing oxygen so you can breathe. I think you owe it an apology {0}.",
            "You are so bad valve will bring back the unskilled report option {0}.",
            "My deepest condolences, {0}.",
            "Congratulations {0}, you have been recognized as one of the worst players in DotA 2. You're a downgrading force whose plays inspire even the worst teammates to even worse accomplishments.",
            "I don't know what techniques you are doing there {0} , but... keep doing them!",
            "If you don't stop using your abilities like a monkey {0}, this game ain't get better!",
            "How does it feel to be retarded {0} ?",
            "Is it just in DotA {0}, or are you acting like a handicapped fish everywhere?",
            "Because of players like you {0}, valve will change the surrender time to 10 minutes soon.",
            "HAHA for a second I thought you stopped trolling {0}",
            "We are currently experimenting with monkeys playing DotA in a team, we need one more player - are you interested {0} ?",
            "After this {0}, I will NEVER EVER call Kaceytron a troll again.",
            "You must have been hammering your head on the wall while playing DotA {0}",
            "Even with a steering wheel you can't play like that, tell me the trick {0}!",
            "What drug can cause those mental issues {0}?",
            "I had a dream how someone tried to play DotA by sitting with his booty on his keyboard, was that you {0} ?",
            "WP {0}, that was actually spastic enough to create a youtube video of it",
            "This is not DotA of Retards, you downloaded the wrong game {0}.",
            "I wonder how you haven't gotten hit by a car yet {0} with this decision making!",
            "What kind of complexes do you have {0} ?",
            "come on {0} atleast try",
            "you're boring me {0}",
            "you know {0}.. you're so bad that I'm gonna open a support ticket for you",
            "my god {0} are you boosted or smth ROFLMAO",
            "{0} reminds me of trick2g bronze subwars",
            "my god this {0} guy is such a god.. at being bad",
            "is {0} a bot guys?",
            "you remind me of intro bots {0}",
            "your stupidity knows no boundaries {0}",
            "wp {0}! (jk that was soo EZreal)",
            "thanks for the free LP {0}",
            "haha this {0} is so troll",
            "{0} is trolling no way someone can be this bad ROFL",
            "? {0} ???",
            "I feel so bad for owning {0}",
            "sorry {0} I know it's unfair for me to play against you...",
            "how much did the boost cost {0}",
            "I'm pretty sure that if monkeys would play DotA they'd do better than you {0}",
            "dude {0} I'm not even trying ROFL",
            "{0}.. you're such a fool man...",
            "add me after the game {0} I'll teach u how to play",
            "my god {0} just go afk.. you're dragging your team down...",
            "{0} the legend coming back once again with the gold for his daddy",
            "I'm going straight to the bank with this {0}",
            "ty {0} I really needed this gold",
            "Please don't report {0} it's not his fault he has to play against me..",
            "open mid?",
            "? Kappa?",
            "ff?",
            "surrender?",
            "{0} You're honestly trash",
            "Jaja {0}, try again",
            "{0} Jajajajajajajajajajajajajajajajajaja",
            "Thanks for the free gold {0}",
            "{0} Go and download scripts, you suck!",
            "{0} That was easy",
            "{0} are you okay?",
            "{0} It amazes me how someone can be so trash",
            "{0} You lost that fight harder than germany lost the war",
            "{0} Can you stop feeding?",
            "HAHA {0} that was a refreshing experience!",
            "LOL {0} no match for me!",
            "Fantastic performance right there {0}!",
            "Can't touch this {0}",
            "{0}, you have been reformed!",
            "Completely smashed there {0}",
            "haha pathetic {0}",
            "true display of skill {0}",
            "better luck next time {0}",
            "Nice try for a monkey {0}",
            "I see you've set aside this special time to humiliate yourself in public {0}",
            "Who lit the fuse on your tampon {0}?",
            "I like you {0}. You remind me of myself when I was young and stupid.",
            "{0}, I'll try being nicer if you'll try being more intelligent.",
            "{0}, if you have something to say raise your hand... then place it over your mouth. ",
            "Somewhere out there is a tree, tirelessly producing oxygen so you can breathe. I think you owe it an apology, {0}",
        };

        private static void Events_OnClose(object sender, EventArgs e)
        {
            Game.OnFireEvent -= Game_OnFireEvent;
        }

        private static void Events_OnLoad(object sender, EventArgs e)
        {
            _me = ObjectManager.LocalPlayer;

            Game.OnFireEvent += Game_OnFireEvent;
        }

        private static void Game_OnFireEvent(FireEventEventArgs args)
        {
            if (args.GameEvent.Name == "dota_player_kill")
            {
                var killer = (uint) args.GameEvent.GetInt("killer1_userid");
                var rekted = (uint) args.GameEvent.GetInt("victim_userid");

                if (ObjectManager.GetPlayerById(killer).Name == _me.Name)
                {
                    var disrespect = string.Format(Disrespect[new Random().Next(0, Disrespect.Length)],
                        ObjectManager.GetPlayerById(rekted).Name);

                    Game.ExecuteCommand($"say {disrespect}");
                }
            }
        }
    }
} 
