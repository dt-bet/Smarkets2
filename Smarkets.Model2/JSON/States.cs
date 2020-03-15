using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Smarkets.Model.JSON
{
    //Football
    //e.g https://api.smarkets.com/v3/events/785537/states/
    public class States
    {
        public Event_States[] event_states { get; set; }
    }

    public class Event_States
    {
        public object aggregate { get; set; }
        public bool hidden { get; set; }
        public string id { get; set; }
        public Info info { get; set; }
        public bool instant_match { get; set; }
        public object provider_metadata { get; set; }
        public object reversed { get; set; }
        public Scores scores { get; set; }
        public object show_flags { get; set; }
        public string state { get; set; }
        public object tennis_server { get; set; }
        public object visualization { get; set; }
    }

    public class Info
    {
        public string gsm_id { get; set; }
        public int team_a_id { get; set; }
        public string team_a_name { get; set; }
        public int team_b_id { get; set; }
        public string team_b_name { get; set; }
    }

    public class Scores
    {
        public int[] full { get; set; }

        public int[] half { get; set; }

        public Set[] sets { get; set; }
    }



    public class Set
    {
        public int score_a { get; set; }
        public int score_b { get; set; }
    }


    //Cricket
    //public class Rootobject
    //{
    //    public Event_States[] event_states { get; set; }
    //}

    //public class Event_States
    //{
    //    public object aggregate { get; set; }
    //    public bool hidden { get; set; }
    //    public string id { get; set; }
    //    public Info info { get; set; }
    //    public bool instant_match { get; set; }
    //    public Provider_Metadata provider_metadata { get; set; }
    //    public bool reversed { get; set; }
    //    public Scores scores { get; set; }
    //    public bool show_flags { get; set; }
    //    public string state { get; set; }
    //    public object tennis_server { get; set; }
    //    public object visualization { get; set; }
    //}

    //public class Info
    //{
    //    public string[] a_innings_score { get; set; }
    //    public string[] b_innings_score { get; set; }
    //    public string cricket_type { get; set; }
    //    public string gsm_competition_id { get; set; }
    //    public string gsm_id { get; set; }
    //    public int team_a_id { get; set; }
    //    public string team_a_name { get; set; }
    //    public int team_b_id { get; set; }
    //    public string team_b_name { get; set; }
    //}

    //public class Provider_Metadata
    //{
    //    public string gsm_competition_id { get; set; }
    //    public string gsm_competition_name { get; set; }
    //    public string gsm_id { get; set; }
    //    public string gsm_round_id { get; set; }
    //    public string gsm_round_name { get; set; }
    //    public int team_a_id { get; set; }
    //    public string team_a_name { get; set; }
    //    public int team_b_id { get; set; }
    //    public string team_b_name { get; set; }
    //}

    //public class Scores
    //{
    //    public string[] a_innings { get; set; }
    //    public string[] b_innings { get; set; }
    //    public int[] full { get; set; }
    //    public object winner { get; set; }
    //}

}
