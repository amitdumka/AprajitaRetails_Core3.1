using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AprajitaRetails.Data;
using AprajitaRetails.Ops.Bot.Telegram;

namespace AprajitaRetails.Ops.Bot.TelgramService
{
    public interface IGiniService
    {
         Gini Gini { get; }
         bool IsRunning { get; set; }
    }
    public class GiniService : IGiniService
    {

        public GiniService()
        {
            if ( !IsRunning )
            {
                Gini.Start ();
                IsRunning = true;
            }
        }

        public void StartMe()
        {
            if ( !IsRunning )
            {
                Gini.Start ();
                IsRunning = true;
            }
        }
        public void StopMe()
        {
            Gini.Stop ();
            IsRunning = false;
        }
        public Gini Gini { get; }

        public bool IsRunning { get; set; }
    }
}
