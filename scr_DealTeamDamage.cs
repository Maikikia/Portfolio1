using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class scr_DealTeamDamage : MonoBehaviour
{
    // Used to determine the direction to damage the player
    public enum Direction { North = 0, East, South, West, All, Unidentified };

    // Used to keep track of the players positions
    public GameObject[] players;

    // Used to keep track of the players direction
    scr_Player_TileBasedMovement playerDirection;

    int randomPlayer;
    int playersComparedDirection;
    int enemiesComparedDirection;

    // Use this for initialization
    void Start()
    {
        playerDirection = gameObject.GetComponent<scr_Player_TileBasedMovement>();
    }

    //Ai reference
    //(damage that will be dealt, direction enemy is facing, should damage be dealt to both, type of damage enemy is doing (Fire, Shadow, ect...))
    public int TakeDamage(float dmg, Direction dir, bool dealDmgToBoth = false, scr_AIReferences.DamageTypes type = scr_AIReferences.DamageTypes.PHYSICAL)
    {
        if (dir != Direction.Unidentified)
        {
            //If the damage is not dealt to all team members
            if (dir != Direction.All)
            {
                randomPlayer = Random.Range(0, 2);
                playersComparedDirection = ((int)playerDirection.playerDirection) % 2;
                enemiesComparedDirection = ((int)dir) % 2;

                //If the Players compared direction is equal to the enemies compared direction 
                //determine if the front row gets damage or the back row does;
                if (playersComparedDirection == enemiesComparedDirection)
                {
                    //If the players direction is equal to the enemy direction then
                    //deal damage to sub 2 or sub 3;
                    if (((int)playerDirection.playerDirection) == (int)dir)
                    {
                        // Deal player damage to sub 3 or sub 2, or both
                        if (!dealDmgToBoth)
                        {
                            if (randomPlayer == 0)
                            {
                                players[2].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 2;
                            }
                            else
                            {
                                players[3].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 3;
                            }
                        }
                        else
                        {
                            players[2].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            players[3].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            return -1;
                        }
                    }
                    else
                    {
                        // Deal damage to sub 0 or sub 1, or both
                        if (!dealDmgToBoth)
                        {
                            if (randomPlayer == 0)
                            {
                                players[0].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 0;
                            }
                            else
                            {
                                players[1].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 1;
                            }
                        }
                        else
                        {
                            players[0].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            players[1].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            return -1;
                        }
                    }
                }
                else
                {
                    enemiesComparedDirection = ((int)dir + 1) % 4;
                    if (((int)playerDirection.playerDirection) == enemiesComparedDirection)
                    {
                        // Deal damage 0 or 3 or both
                        if (!dealDmgToBoth)
                        {
                            if (randomPlayer == 0)
                            {
                                players[1].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 1;
                            }
                            else
                            {
                                players[2].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 2;
                            }
                        }
                        else
                        {
                            players[1].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            players[2].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            return -1;
                        }
                    }
                    else
                    {
                        // Deal damage sub 1 or 2 or both
                        if (!dealDmgToBoth)
                        {
                            if (randomPlayer == 0)
                            {
                                players[0].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 0;
                            }
                            else
                            {
                                players[3].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                                return 3;
                            }

                        }
                        else
                        {
                            players[0].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            players[3].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                            return -1;
                        }
                    }
                }
            }
            //If damage is dealt to all team members
            else
            {
                for (int i = 0; i < players.Length; i++)
                {
                    //If that team member is dead then continue to the next
                    if (players[i].GetComponent<Scr_PlayerAttributes>().dead)
                        continue;
                    //Deal damage to each team member
                    players[i].GetComponent<Scr_PlayerAttributes>().DamagePlayer(dmg, type);
                }
            }
        }
        return -1;
    }
}

