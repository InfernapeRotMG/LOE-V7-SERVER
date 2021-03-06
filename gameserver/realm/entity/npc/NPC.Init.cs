﻿#region

using LoESoft.GameServer.networking.outgoing;
using LoESoft.GameServer.realm.entity.player;
using System;
using System.Collections.Generic;

#endregion

namespace LoESoft.GameServer.realm.entity.npc
{
    /** NPC (LoESoft Games)
    * Author: Warlt
    */

    public abstract class NPC
    {
        protected List<string> _playersCache { get; set; }
        protected Entity _NPC { get; set; }
        protected string _NPCLeaveMessage { get; set; }
        protected int _NPCStars { get; set; }

        public void Config(
            Entity NPC,
            List<string> NPCLeaveMessages,
            bool randomNPCLeaveMessages
            )
        {
            _playersCache = new List<string>();
            _NPC = NPC;
            _NPCLeaveMessage = NPCLeaveMessages == null ? null : randomNPCLeaveMessages ? NPCLeaveMessages[new Random().Next(0, NPCLeaveMessages.Count)] : NPCLeaveMessages[0];
        }

        public void UpdateNPCStars(int NPCStars) => _NPCStars = NPCStars;

        public void UpdateNPC(Entity entity) => _NPC = entity;

        public void NoRepeat(Player player) => Callback(player, $"I'm already talking to you, {player.Name}...");

        public List<string> ReturnPlayersCache() => _playersCache;

        public void AddPlayer(Player player) => _playersCache.Add(player.Name);

        public void RemovePlayer(Player player) => _playersCache.Remove(player.Name);

        public abstract void Welcome(Player player);

        public abstract void Leave(Player player, bool polite);

        public abstract void Commands(Player player, string command);

        // send a private message to specific player (idea by Sebafra)
        protected void Callback(
            Player player,
            string message,
            bool isNPC = true
            )
        {
            if (player == null) return;
            TEXT _text = new TEXT
            {
                ObjectId = isNPC ? _NPC.Id : player.Id,
                BubbleTime = 10,
                Stars = isNPC ? _NPCStars : player.Stars,
                Name = isNPC ? _NPC.Name : player.Name,
                Admin = 0,
                Recipient = isNPC ? player.Name : _NPC.Name,
                Text = message.ToSafeText(),
                CleanText = ""
            };
            _text.NameColor = _text.TextColor = 0x123456;
            player.Client.SendMessage(_text);
        }
    }
}