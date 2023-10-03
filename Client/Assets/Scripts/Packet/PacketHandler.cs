using ServerCore;
using System.Collections.Generic;
using UnityEngine;

class PacketHandler
{
    public static void S_PlayerInfoHandler(PacketSession session, IPacket packet)
    {
        S_PlayerInfo playerInfoPacket = packet as S_PlayerInfo;
        ServerSession serverSession = session as ServerSession;

        PlayerInfo playerInfo = GameManager.PlayerInfo;
        if (playerInfo != null)
        {
            playerInfo.def = playerInfoPacket.def;
            playerInfo.evasion = playerInfoPacket.evasion;
            playerInfo.attack = playerInfoPacket.attack;
            playerInfo.gold = playerInfoPacket.gold;
            playerInfo.maxHealth = playerInfoPacket.health;
            playerInfo.speed = playerInfoPacket.speed;

            List<S_PlayerInfo.Item> items = playerInfoPacket.items;

            List<Item> inventory = new List<Item>();
            foreach (S_PlayerInfo.Item item in items)
            {
                Item invenItem = new Item();
                invenItem.id = item.id;
                invenItem.name = item.name;
                invenItem.price = item.price;
                invenItem.prefab = item.prefab;
                invenItem.itemType = (Define.ItemType)item.itemType;

                inventory.Add(invenItem);
            }
            playerInfo.inventory = inventory;
        }
    }
}
