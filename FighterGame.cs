/*
 * Serena Rojo
 * CIT 1713
 * C# Fighter Game
 * 12/16/2022
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class d20Roll
{
    Random rand = new Random();
    public int roll;
    public int getRoll()    // roll 1d20
    {
        roll = rand.Next(20) + 1;
        return roll;
    }
    // add getter and setter
    public int getDice()
    {
        return roll;
    }
    public void setDice(int dice)
    {
        this.roll = dice;
    }
} // end d20Roll class

public class damageRoll
{
    Random rand = new Random();
    public int roll;
    public int getRoll()    // roll 1d10 for standard damage
    {
        roll = rand.Next(10) + 1;
        return roll;
    }
    // add getter and setter
    public int getDice()
    {
        return roll;
    }
    public void setDice(int dice)
    {
        this.roll = dice;
    }
} // end damageRoll class

public class critDamageRoll
{
    Random rand = new Random();
    public int roll;
    public int getRoll()    // roll 1d10 for the damage and double it since the attack roll was a nat 20
    {
        roll = (rand.Next(10) + 1) * 2;
        return roll;
    }
    // add getter and setter
    public int getDice()
    {
        return roll;
    }
    public void setDice(int dice)
    {
        this.roll = dice;
    }
} // end critDamageRoll class

public class healthPotionRoll
{
    Random rand = new Random();
    public int roll;
    public int getRoll()    // roll 2d4 + 2 for health potion
    {
        roll = (rand.Next(4) + 1) + (rand.Next(4) + 1) + 2;
        return roll;
    }
    // add getter and setter
    public int getDice()
    {
        return roll;
    }
    public void setDice(int dice)
    {
        this.roll = dice;
    }
} // end healthPotionRoll class

class Program
{
    public static void menuArt()
    {
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("        _    .  ,   .           .                      ");
        Console.WriteLine("    *  /  _ *  /  _      _  *        *   / '__        *");
        Console.WriteLine("      /       /     ,   ((        .    _/  /     *'.   ");
        Console.WriteLine(" .   / /   / / :' __  _  `          _^/  ^/    `--.    ");
        Console.WriteLine("    /     /     _/   -'       *    /.' ^_    _   .'   *");
        Console.WriteLine("  /   .-   `.  /       /==~=-=~=-=-;.  _/   -. `_/     ");
        Console.WriteLine(" /  `-.__ ^   / .-'.--  =-=~_=-=~=^/  _ `--./ .-'  `-  ");
        Console.WriteLine("/        `.  / /       `.~-^=-=~=^=.-'      '-._ `._   ");
        Console.WriteLine("-------------------------------------------------------");
    } // end of menuArt

    public static void gameRules()
    {
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("                     How To Play                       ");
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("This game is based loosely around standard turn-based fighting games and the D&D ruleset.");
        Console.WriteLine("The game will start by rolling a d20 for both you and the enemy that you will be fighting \n to track the initiative order. The one with the higher number goes first.");
        Console.WriteLine("");
        Console.WriteLine("When you reach your turn, you will be able to choose from 3 different actions:");
        Console.WriteLine("1) Roll 1d20 to try and attack the enemy, if you roll a 10 or higher then you successfully \n landed the attack and will roll 1d10 for damage. If you rolled a nat 20 to hit, then you would roll 2d10 for damage.");
        Console.WriteLine("2) Drink a health potion - you would roll 2d4+2 for the amount of health healed");
        Console.WriteLine("The first side to get their opponent to 0 HP wins!");
        Console.WriteLine("");
        Console.WriteLine("Press 'Enter' to go back to the Main Menu");
        Console.ReadLine();
        Console.Clear();
        mainMenu();
    } // end of gameRules

    public static void mainMenu()
    {
        // variable to hold user input for main menu
        string mainMenuChoice;

        // display greeting and options to user
        Console.WriteLine("-------------------------------------------------------");
        Console.WriteLine("                       WELCOME                         ");
        Console.WriteLine("-------------------------------------------------------");
        menuArt();
        Console.WriteLine("1) Play");
        Console.WriteLine("2) How To Play");
        Console.WriteLine("3) Exit Game");

        // take the user's choice and take the appropriate action
        mainMenuChoice = Console.ReadLine().ToLower();
        Console.Clear();

        switch (mainMenuChoice)
        {
            case "1":   // starts the game
                playGame();
                break;
            case "2":   // takes the user to the how to play instructions
                gameRules();
                break;
            case "3":   // closes the program
                Environment.Exit(0);
                break;
            default:    // restarts the main menu
                Console.WriteLine("Invalid input. Please only enter '1' '2' or '3'.");
                Console.WriteLine("Press 'Enter' to restart.");
                Console.ReadLine();
                mainMenu();
                break;
        }
    } // end of mainMenu

    public static void playGame()
    {
        // player and enemy hit points
        int playerHP = 30;
        int enemyHP = 30;

        // player and enemy armor class
        int playerAC = 10;
        int enemyAC = 10;

        // placeholders for attack, damage, and health rolls as well as the enemyChoice, round, and runAway variables
        int playerAttack = 0;
        int enemyAttack = 0;
        int damageAmount = 0;
        int healAmount = 0;
        int enemyChoice = 0;
        int round = 1;
        bool runAway = false;

        // create instances for the Random, attackRoll, damageRoll, critDamageRoll,and healthPotionRoll classes
        Random random = new Random();
        d20Roll attack = new d20Roll();
        d20Roll initiative = new d20Roll();
        damageRoll damage = new damageRoll();
        critDamageRoll crit = new critDamageRoll();
        healthPotionRoll heal = new healthPotionRoll();

        // roll 1d20 for initiative before the battle begins. highest roll goes first in the turn order! if they get a tie, the player goes first by default
        int playerInitiative;
        int enemyInitiative;

        playerInitiative = initiative.getRoll();
        enemyInitiative = initiative.getRoll();

        Console.WriteLine("You rolled a " + playerInitiative + " and the enemy rolled a " + enemyInitiative + " for initiative.");

        // get turn order
        if (playerInitiative > enemyInitiative)
        {
            Console.WriteLine("You are first in the turn order!");

            // loop fight until either the player or the enemy dies
            while (playerHP > 0 && enemyHP > 0)
            {
                // top of the round divider
                Console.WriteLine("---------- ROUND #" + round + " ----------");

                // player turn first
                Console.WriteLine("-- Your Turn --");
                Console.WriteLine("Your HP = " + playerHP + " and Enemy's HP = " + enemyHP);
                Console.WriteLine("Enter '1' to attack or '2' to heal.");
                string playerChoice = Console.ReadLine().ToLower();

                switch (playerChoice)
                {
                    case "1":   // player chooses to attack
                        {
                            playerAttack = attack.getRoll();
                            if (playerAttack >= enemyAC && playerAttack != 20)  // attack hits, normal damage
                            {
                                damageAmount = damage.getRoll();
                                enemyHP -= damageAmount;
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit and deal " + damageAmount + " damage!");
                            }
                            else if (playerAttack >= enemyAC && playerAttack == 20) // natural 20, crit damage
                            {
                                damageAmount = crit.getRoll();
                                enemyHP -= damageAmount;
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit. That's a crit! The attack deals " + damageAmount + " damage!");
                            }
                            else if (playerAttack < enemyAC)    // attack misses
                            {
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit. That's a miss, no damage dealt. :(");
                            }
                            else
                            {
                                Console.WriteLine("Something went wrong... skipping turn.");
                            }
                            break;
                        }
                    case "2":   // player chooses to use a health potion
                        {
                            healAmount = heal.getRoll();
                            playerHP += healAmount;
                            Console.WriteLine("You restore " + healAmount + " hit points with a health potion!");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input. Please only enter '1' or '2' next time.");
                            Console.WriteLine("Skipping turn...");
                            break;
                        }
                } // end player turn

                // enemy turn second
                if (enemyHP > 0)
                {
                    Console.WriteLine("-- Enemy Turn --");
                    Console.WriteLine("Your HP = " + playerHP + " and Enemy's HP = " + enemyHP);
                    if (enemyHP <= 20)
                    {
                        enemyChoice = random.Next(0, 2); // randomly chooses 0 or 1 and completes the appropriate action, but only do this if the enemy's health is 20 or less
                    }
                    else
                    {
                        enemyChoice = 0;
                    }

                    switch (enemyChoice)
                    {
                        case 0: // the enemy chooses to attack
                            {
                                enemyAttack = attack.getRoll();
                                if (enemyAttack >= playerAC && enemyAttack != 20)   // attack hits, normal damage
                                {
                                    damageAmount = damage.getRoll();
                                    playerHP -= damageAmount;
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit and deals " + damageAmount + " damage!");
                                }
                                else if (enemyAttack >= playerAC && enemyAttack == 20)  // natural 20, crit damage
                                {
                                    damageAmount = crit.getRoll();
                                    playerHP -= damageAmount;
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit. That's a crit! The attack deals " + damageAmount + " damage!");
                                }
                                else if (enemyAttack < playerAC)    // attack misses
                                {
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit. That's a miss!");
                                }
                                else
                                {
                                    Console.WriteLine("Something went wrong... skipping turn.");
                                }
                                break;
                            }
                        case 1: // the enemy chooses to use a health potion
                            {
                                healAmount = heal.getRoll();
                                enemyHP += healAmount;
                                Console.WriteLine("Enemy restores " + healAmount + " hit points with a health potion!");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Something went wrong :(");
                                Console.WriteLine("Skipping turn...");
                                break;
                            }
                    }
                } // end enemy turn

                // keep track of round
                round++;

            } // end round loop

            // display game outcome
            if (playerHP > 0 && enemyHP <= 0)
            {
                Console.WriteLine("Congratulations, you win!");
            }
            else if (playerHP <= 0 && enemyHP > 0)
            {
                Console.WriteLine("You lose!");
            }
            else
            {
                Console.WriteLine("Sorry, something must have went wrong :(");
            }

            // return to main menu
            Console.WriteLine("Press 'Enter' when you are ready to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
            mainMenu();

        }// end player first turn loop

        else if (playerInitiative < enemyInitiative)
        {
            Console.WriteLine("The enemy is first in the turn order!");

            // loop fight until either the player or the enemy dies
            while (playerHP > 0 && enemyHP > 0)
            {
                // top of the round divider
                Console.WriteLine("---------- ROUND #" + round + " ----------");

                // enemy turn first
                if (enemyHP > 0)
                {
                    Console.WriteLine("-- Enemy Turn --");
                    Console.WriteLine("Your HP = " + playerHP + " and Enemy's HP = " + enemyHP);
                    if (enemyHP <= 20)
                    {
                        enemyChoice = random.Next(0, 2); // randomly chooses 0 or 1 and completes the appropriate action, but only do this if the enemy's health is 20 or less
                    }
                    else
                    {
                        enemyChoice = 0;
                    }

                    switch (enemyChoice)
                    {
                        case 0: // the enemy chooses to attack
                            {
                                enemyAttack = attack.getRoll();
                                if (enemyAttack >= playerAC && enemyAttack != 20)   // attack hits, normal damage
                                {
                                    damageAmount = damage.getRoll();
                                    playerHP -= damageAmount;
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit and deals " + damageAmount + " damage!");
                                }
                                else if (enemyAttack >= playerAC && enemyAttack == 20)  // natural 20, crit damage
                                {
                                    damageAmount = crit.getRoll();
                                    playerHP -= damageAmount;
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit. That's a crit! The attack deals " + damageAmount + " damage!");
                                }
                                else if (enemyAttack < playerAC)    // attack misses
                                {
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit. That's a miss!");
                                }
                                else
                                {
                                    Console.WriteLine("Something went wrong... skipping turn.");
                                }
                                break;
                            }
                        case 1: // the enemy chooses to use a health potion
                            {
                                healAmount = heal.getRoll();
                                enemyHP += healAmount;
                                Console.WriteLine("Enemy restores " + healAmount + " hit points with a health potion!");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Something went wrong :(");
                                Console.WriteLine("Skipping turn...");
                                break;
                            }
                    }
                } // end enemy turn

                // player turn second
                Console.WriteLine("-- Your Turn --");
                Console.WriteLine("Your HP = " + playerHP + " and Enemy's HP = " + enemyHP);
                Console.WriteLine("Enter '1' to attack or '2' to heal.");
                string playerChoice = Console.ReadLine().ToLower();

                switch (playerChoice)
                {
                    case "1":   // player chooses to attack
                        {
                            playerAttack = attack.getRoll();
                            if (playerAttack >= enemyAC && playerAttack != 20)  // attack hits, normal damage
                            {
                                damageAmount = damage.getRoll();
                                enemyHP -= damageAmount;
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit and deal " + damageAmount + " damage!");
                            }
                            else if (playerAttack >= enemyAC && playerAttack == 20) // natural 20, crit damage
                            {
                                damageAmount = crit.getRoll();
                                enemyHP -= damageAmount;
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit. That's a crit! The attack deals " + damageAmount + " damage!");
                            }
                            else if (playerAttack < enemyAC)    // attack misses
                            {
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit. That's a miss, no damage dealt. :(");
                            }
                            else
                            {
                                Console.WriteLine("Something went wrong... skipping turn.");
                            }
                            break;
                        }
                    case "2":   // player chooses to use a health potion
                        {
                            healAmount = heal.getRoll();
                            playerHP += healAmount;
                            Console.WriteLine("You restore " + healAmount + " hit points with a health potion!");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input. Please only enter '1' or '2' next time.");
                            Console.WriteLine("Skipping turn...");
                            break;
                        }
                } // end player turn

                // keep track of round
                round++;

            } // end round loop

            // display game outcome
            if (playerHP > 0 && enemyHP <= 0)
            {
                Console.WriteLine("Congratulations, you win!");
            }
            else if (playerHP <= 0 && enemyHP > 0)
            {
                Console.WriteLine("You lose!");
            }
            else
            {
                Console.WriteLine("Sorry, something must have went wrong :(");
            }

            // return to main menu
            Console.WriteLine("Press 'Enter' when you are ready to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
            mainMenu();

        } // end enemy first turn loop

        else
        {
            Console.WriteLine("It was a tie! By default, you are first in the turn order!");

            // loop fight until either the player or the enemy dies
            while (playerHP > 0 && enemyHP > 0)
            {
                // top of the round divider
                Console.WriteLine("---------- ROUND #" + round + " ----------");

                // player turn first
                Console.WriteLine("-- Your Turn --");
                Console.WriteLine("Your HP = " + playerHP + " and Enemy's HP = " + enemyHP);
                Console.WriteLine("Enter '1' to attack or '2' to heal.");
                string playerChoice = Console.ReadLine().ToLower();

                switch (playerChoice)
                {
                    case "1":   // player chooses to attack
                        {
                            playerAttack = attack.getRoll();
                            if (playerAttack >= enemyAC && playerAttack != 20)  // attack hits, normal damage
                            {
                                damageAmount = damage.getRoll();
                                enemyHP -= damageAmount;
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit and deal " + damageAmount + " damage!");
                            }
                            else if (playerAttack >= enemyAC && playerAttack == 20) // natural 20, crit damage
                            {
                                damageAmount = crit.getRoll();
                                enemyHP -= damageAmount;
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit. That's a crit! The attack deals " + damageAmount + " damage!");
                            }
                            else if (playerAttack < enemyAC)    // attack misses
                            {
                                Console.WriteLine("You attack the enemy with a " + playerAttack + " to hit. That's a miss, no damage dealt. :(");
                            }
                            else
                            {
                                Console.WriteLine("Something went wrong... skipping turn.");
                            }
                            break;
                        }
                    case "2":   // player chooses to use a health potion
                        {
                            healAmount = heal.getRoll();
                            playerHP += healAmount;
                            Console.WriteLine("You restore " + healAmount + " hit points with a health potion!");
                            break;
                        }
                    default:
                        {
                            Console.WriteLine("Invalid input. Please only enter '1' or '2' next time.");
                            Console.WriteLine("Skipping turn...");
                            break;
                        }
                } // end player turn

                // enemy turn second
                if (enemyHP > 0)
                {
                    Console.WriteLine("-- Enemy Turn --");
                    Console.WriteLine("Your HP = " + playerHP + " and Enemy's HP = " + enemyHP);
                    if (enemyHP <= 20)
                    {
                        enemyChoice = random.Next(0, 2); // randomly chooses 0 or 1 and completes the appropriate action, but only do this if the enemy's health is 20 or less
                    }
                    else
                    {
                        enemyChoice = 0;
                    }

                    switch (enemyChoice)
                    {
                        case 0: // the enemy chooses to attack
                            {
                                enemyAttack = attack.getRoll();
                                if (enemyAttack >= playerAC && enemyAttack != 20)   // attack hits, normal damage
                                {
                                    damageAmount = damage.getRoll();
                                    playerHP -= damageAmount;
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit and deals " + damageAmount + " damage!");
                                }
                                else if (enemyAttack >= playerAC && enemyAttack == 20)  // natural 20, crit damage
                                {
                                    damageAmount = crit.getRoll();
                                    playerHP -= damageAmount;
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit. That's a crit! The attack deals " + damageAmount + " damage!");
                                }
                                else if (enemyAttack < playerAC)    // attack misses
                                {
                                    Console.WriteLine("Enemy attacks you with a " + enemyAttack + " to hit. That's a miss!");
                                }
                                else
                                {
                                    Console.WriteLine("Something went wrong... skipping turn.");
                                }
                                break;
                            }
                        case 1: // the enemy chooses to use a health potion
                            {
                                healAmount = heal.getRoll();
                                enemyHP += healAmount;
                                Console.WriteLine("Enemy restores " + healAmount + " hit points with a health potion!");
                                break;
                            }
                        default:
                            {
                                Console.WriteLine("Something went wrong :(");
                                Console.WriteLine("Skipping turn...");
                                break;
                            }
                    }
                } // end enemy turn

                // keep track of round
                round++;

            } // end round loop

            // display game outcome
            if (playerHP > 0 && enemyHP <= 0)
            {
                Console.WriteLine("Congratulations, you win!");
            }
            else if (playerHP <= 0 && enemyHP > 0)
            {
                Console.WriteLine("You lose!");
            }
            else
            {
                Console.WriteLine("Sorry, something must have went wrong :(");
            }

            // return to main menu
            Console.WriteLine("Press 'Enter' when you are ready to return to the main menu.");
            Console.ReadLine();
            Console.Clear();
            mainMenu();

        } // end tie turn order loop
    } // end of playGame

    static void Main(string[] args)
    {

        mainMenu();
        playGame();

    } // end of Main()
} // end of Program class