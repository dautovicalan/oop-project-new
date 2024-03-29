﻿using DataAccessLayer.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Utils
{
    public static class Utilities
    {
        public static List<Player> CalculatePlayerStatistics(List<Match> matches, List<Player> players, string fifaCode)
        {
            List<Player> result = new List<Player>(players);

            foreach (Match match in matches)
            {
                if (match.home_team.code == fifaCode)
                {
                    foreach (var item in match.home_team_events)
                    {
                        switch (item.type_of_event)
                        {
                            case "goal":
                                if (result.Find(x => x.name.Contains(item.player))?.name == item.player)
                                {
                                    result.Find(x => x.name.Contains(item.player)).GoalNumber++;
                                }
                                break;
                            case "goal-penalty":
                                if (result.Find(x => x.name.Contains(item.player))?.name == item.player)
                                {
                                    result.Find(x => x.name.Contains(item.player)).GoalNumber++;
                                }
                                break;
                            case "yellow-card":
                                if (result.Find(x => x.name.Contains(item.player))?.name == item.player)
                                {
                                    result.Find(x => x.name.Contains(item.player)).YellowCardNumber++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (match.away_team.code == fifaCode)
                {
                    foreach (var item in match.home_team_events)
                    {
                        switch (item.type_of_event)
                        {
                            case "goal":
                                if (result.Find(x => x.name.Contains(item.player))?.name == item.player)
                                {
                                    result.Find(x => x.name.Contains(item.player)).GoalNumber++;
                                }
                                break;
                            case "goal-penalty":
                                if (result.Find(x => x.name.Contains(item.player))?.name == item.player)
                                {
                                    result.Find(x => x.name.Contains(item.player)).GoalNumber++;
                                }
                                break;
                            case "yellow-card":
                                if (result.Find(x => x.name.Contains(item.player))?.name == item.player)
                                {
                                    result.Find(x => x.name.Contains(item.player)).YellowCardNumber++;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
            }

            result.Sort((x, y) => -x.GoalNumber.CompareTo(y.GoalNumber));
            return result;
        }
        
        public static List<Player> CalculatePlayerStatisticsForSingleMatch(List<Player> players, List<MatchEvents> events)
        {
            List<Player> result = players;

            foreach (MatchEvents matchEvents in events)
            {
                switch (matchEvents.type_of_event)
                {
                    case "goal":
                        if (players.Find(x => x.name.Contains(matchEvents.player))?.name == matchEvents.player)
                        {
                            result.Find(x => x.name.Contains(matchEvents.player)).GoalNumber++;
                        }
                        break;
                    case "goal-penalty":
                        if (players.Find(x => x.name.Contains(matchEvents.player))?.name == matchEvents.player)
                        {
                            result.Find(x => x.name.Contains(matchEvents.player)).GoalNumber++;
                        }
                        break;
                    case "yellow-card":
                        if (players.Find(x => x.name.Contains(matchEvents.player))?.name == matchEvents.player)
                        {
                            result.Find(x => x.name.Contains(matchEvents.player)).YellowCardNumber++;
                        }
                        break;
                    default:
                        break;
                }
            }

            return result;
        }

        public static string ErrorMessage()
        {
            return System.Threading.Thread.CurrentThread.CurrentCulture.Name == "en" ?
                "Please setup your config and favorite teams in settings" : "Molimo postavie konfiguraciju i favorit team u postavkama";
        }
    }
}
