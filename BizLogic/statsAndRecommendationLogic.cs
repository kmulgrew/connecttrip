﻿using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BizLogic
{
    public static class statsAndRecommendationLogic
    {
        public static double overallPercentage(this Person user)
        {
            
            return percentage(user.LevelOneWins + user.LevelTwoWins + user.LevelThreeWins, user.LevelOneLose + user.LevelTwoLose + user.LevelThreeLose);
        }

        public static double levelOnePercentage(this Person user)
        {
           
            return percentage(user.LevelOneWins, user.LevelOneLose);
        }

        public static double levelTwoPercentage(this Person user)
        {
            
            return percentage(user.LevelTwoWins, user.LevelTwoLose);

        }

        public static double levelThreePercentage(this Person user)
        {
            return percentage(user.LevelThreeWins, user.LevelThreeLose);
        }

        public static double didNotAnwserPercentage(this Person user)
        {
           
            return percentage(user.DidNotAnswer, user.Answered);
        }

        public static double levelOneMathCorrectPerecentage(this Person user)
        {
            
            return percentage(user.levelOneAnsweredCorrectly, user.levelOneAnsweredIncorrectly);
        }

        public static double levelTwoMathCorrectPerecentage(this Person user)
        {
            return percentage(user.levelTwoAnsweredCorrectly, user.levelTwoAnsweredIncorrectly);
        }

        public static double levelThreeMathCorrectPerecentage(this Person user)
        {
            return percentage(user.levelThreeAnsweredCorrectly, user.levelThreeAnsweredIncorrectly);
        }
        public static double overallMathPercentage(this Person user)
        {
            return percentage(user.levelOneAnsweredCorrectly + user.levelTwoAnsweredCorrectly + user.levelThreeAnsweredCorrectly, user.levelOneAnsweredIncorrectly + user.levelTwoAnsweredIncorrectly + user.levelThreeAnsweredIncorrectly);
        }

        public static double numGames(this Person user)
        {
          //  return user.Answered + user.DidNotAnswer;
            return user.LevelOneWins + user.LevelTwoWins + user.LevelThreeWins + user.LevelOneLose + user.LevelTwoLose + user.LevelThreeLose;
        }

        public static double numWins(this Person user)
        {
            return user.LevelOneWins + user.LevelTwoWins + user.LevelThreeWins;
        }

        public static double numLose(this Person user)
        {
            return user.LevelOneLose + user.LevelTwoLose + user.LevelThreeLose;
        }
        public static double percentage(double one, double two)
        {
            if (one + two == 0) return 0;
            return (one / (one + two)) * 100;
        }

        public static IEnumerable<startGamePlayer> findMatch(this Person user, IEnumerable<startGamePlayer> pool, Entities2 Context)
        {

            double level1Percentage = levelOnePercentage(user);
            double level2Percentage = levelTwoPercentage(user);
            double level3Percentage = levelThreePercentage(user);
            
            

            
                var possUsers = (from u in pool select u);
                if (level1Percentage >= level2Percentage && level1Percentage >= level3Percentage)
                {

                possUsers = (from u in pool where Math.Abs(percentage(user.LevelOneWins, user.LevelOneLose) - percentage(Context.getPersonById(u.player1Id).LevelOneWins, Context.getPersonById(u.player1Id).LevelOneLose)) <= 30 && Math.Abs(overallPercentage(user) - overallPercentage(Context.getPersonById(u.player1Id))) <= 50 select u);
                }
                else if (level2Percentage >= level1Percentage && level2Percentage >= level3Percentage)
                {
                    possUsers = (from u in pool where Math.Abs(percentage(user.LevelTwoWins, user.LevelTwoLose) - percentage(Context.getPersonById(u.player1Id).LevelTwoWins, Context.getPersonById(u.player1Id).LevelTwoLose)) <= 30 && Math.Abs(overallPercentage(user) - overallPercentage(Context.getPersonById(u.player1Id))) <= 50 select u);
                }
                else
                {
                    possUsers = (from u in pool where Math.Abs(percentage(user.LevelThreeWins, user.LevelThreeLose) - percentage(Context.getPersonById(u.player1Id).LevelThreeWins, Context.getPersonById(u.player1Id).LevelTwoLose)) <= 30 && Math.Abs(overallPercentage(user) - overallPercentage(user)) <= 50 select u);
                }

            /*Random rand = new Random();
            int toSkip = rand.Next(0, possUsers.Count());

            var chosenUser = possUsers.Skip(toSkip).Take(1).FirstOrDefault();
            while (chosenUser == user)
            {
                toSkip = rand.Next(0, Context.Persons.Count());

                chosenUser = Context.Persons.Skip(toSkip).Take(1).FirstOrDefault();
            }

            if (chosenUser == null)
            {
                chosenUser = user;
                while (chosenUser == user)
                {
                    int toSkip2 = rand.Next(0, Context.Persons.Count());
                    chosenUser = Context.Persons.Skip(toSkip2).Take(1).FirstOrDefault();
                }
            }
            return chosenUser;*/

            if (possUsers.FirstOrDefault() == null)
            {
                return null;
            }
            else
            {
                return possUsers;
            }
        }

        public static string GameCompliment(this Person user)
        {
            double wins = overallPercentage(user);
            string compliment = null;
            if(wins >=75.00)
            {
                compliment = "You are doing great! Kepp it up!";
            }
            else if(wins >=50.00 && wins <75.00)
            {
                compliment = "You are doing good!";
            }
            else if(wins >=24.00 && wins <50.00)
            {
                compliment = "You can do better!";
            }
            else
            {
                compliment = "You need to put more effort!";
            }
            return compliment;

        }

        public static string MathCompliment(this Person user)
        {
            /* double lost = didNotAnwserPercentage(user);
            
             string compliment = null;
             if (lost >= 75.00)
             {
                 compliment = "You need to put more effort!";

             }
             else if (lost >= 50.00 && lost < 75.00)
             {
                 compliment = "You can do better!";

             }
             else if (lost >= 24.00 && lost < 50.00)
             {
                 compliment = "You are doing good!";
             }
             else
             {
                 compliment = "You are doing great! Kepp it up!";
             }
             return compliment;*/

            double correctAnswers = overallMathPercentage(user);
            string compliment = null;
            if (correctAnswers >= 75.00)
            {
                compliment = "You are doing great! Kepp it up!";
            }
            else if (correctAnswers >= 50.00 && correctAnswers < 75.00)
            {
                compliment = "You are doing good!";
            }
            else if (correctAnswers >= 24.00 && correctAnswers < 50.00)
            {
                compliment = "You can do better!";
            }
            else
            {
                compliment = "You need to put more effort!";
            }
            return compliment;


        }
    }
}

