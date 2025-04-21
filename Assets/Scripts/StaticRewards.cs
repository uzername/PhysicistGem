using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    /// <summary>
    /// small blurbs that are used as rewards lol
    /// </summary>
    internal class StaticRewards
    {
        public static List<Tuple<String, String>> researchQuotes = new List<Tuple<string, string>> 
        { 
            new Tuple<string, string>("Change is the end result of all true learning.", "Leo Buscaglia"),
            new Tuple<string, string>("Develop a passion for learning. If you do, you will never cease to grow.", "Anthony J. D'Angelo"),
            new Tuple<string, string>("Education is the movement from darkness to light","Allan Bloom"),
            new Tuple<string, string>("The best way to predict the future is to create it.", "Alan Kay"),
            new Tuple<string, string>("I have had my results for a long time: but I do not yet know how I am to arrive at them.", "Michael Faraday"),
            new Tuple<string, string>("No research without action, no action without research.", "Kurt Lewin"),
            new Tuple<string, string>("If we knew what we were doing, it wouldn't be called research, would it?", "Albert Einstein")
        };
        public static Tuple<string, string> GetRandomQuote()
        {
            return StaticRewards.researchQuotes[UnityEngine.Random.Range(0, StaticRewards.researchQuotes.Count)];
        }
    }
}
