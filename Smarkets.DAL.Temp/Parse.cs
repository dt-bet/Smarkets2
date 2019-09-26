using Smarkets.Entity.Temp;
using Smarkets.Model.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarkets.DAL.Temp
{
    public static class Parse
    {
        public static Result GetResult(Event_States eventStates,Dictionary<long,string> dictTeams,List<Team> teams)
        {

            if (eventStates.info == null)
            {
                return new Result
                {
                    Id = System.Convert.ToInt64(eventStates.id),
                    HasResult = false,
                    State = eventStates.state == "ended"
                };
            }
            else
            {
                int team1id = eventStates.info.team_a_id;
                int team2id = eventStates.info.team_b_id;
                string team1name = eventStates.info.team_a_name;
                string team2name = eventStates.info.team_b_name;

                if (!dictTeams.ContainsKey(team1id))
                    teams.Add(new Team { Id = team1id, Name = team1name });
                if (!dictTeams.ContainsKey(team2id))
                    teams.Add(new Team { Id = team2id, Name = team2name });

                if (eventStates.scores.full != null)
                {
                    Result result = new Result
                    {
                        Id = System.Convert.ToInt64(eventStates.id),
                        Team1Id = team1id,
                        Team2Id = team2id,
                        HasResult = true,
                        Team1FullTimeScore = (byte)eventStates.scores?.full?.First(),
                        Team2FullTimeScore = (byte)eventStates.scores?.full?.Last(),
                        State = eventStates.state == "ended"
                    };

                    if (eventStates.scores.half != null)
                    {
                        result.Team1HalfTimeScore = (byte)eventStates.scores?.half?.First();
                        result.Team2HalfTimeScore = (byte)eventStates.scores?.half?.Last();
                    }
                    return result;
                }
                else
                {
                    return new Result
                    {
                        Id = System.Convert.ToInt64(eventStates.id),
                        Team1Id = team1id,
                        Team2Id = team2id,
                        HasResult = false,
                        State = eventStates.state == "ended"
                    };
                }
            }
        }


    }
}
