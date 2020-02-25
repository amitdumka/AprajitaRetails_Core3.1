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
        public Gini Gini { get; }
    }
    public class GiniService : IGiniService
    {
        
        public GiniService() => Gini.Start ();

        public void StartMe()
        {
            Gini.Start ();
        }
        public void StopMe()
        {
            Gini.Stop ();
        }
        public Gini Gini { get; }
    }
}
