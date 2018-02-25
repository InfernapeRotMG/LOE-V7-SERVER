﻿#region

using LoESoft.Core.config;
using LoESoft.GameServer.realm.entity.player;
using System;

#endregion

namespace LoESoft.GameServer.networking.messages.handlers.hack
{
    public class DexterityCheatHandler : ICheatHandler
    {
        protected Player player { get; set; }
        protected Item item { get; set; }
        protected bool isAbility { get; set; }
        protected double attackPeriod { get; set; }
        protected int attackAmount { get; set; }

        public DexterityCheatHandler() { }

        public void SetPlayer(Player player) => this.player = player;

        public void SetItem(Item item) => this.item = item;

        public void SetAbility(bool isAbility) => this.isAbility = isAbility;

        public void SetPeriod(float attackPeriod) => this.attackPeriod = (1 / Math.Round(attackPeriod, 4));

        public void SetAmount(int attackAmount) => this.attackAmount = attackAmount;

        private bool byPass
        { get { return player.AccountType == (int)AccountType.LOESOFT_ACCOUNT; } }

        CheatID ICheatHandler.ID
        { get { return CheatID.DEXTERITY; } }

        public void Handler()
        {
            if (item == player.Inventory[1] || item == player.Inventory[2] || item == player.Inventory[3])
                return;

            if (isAbility)
                return;

            if ((attackPeriod > ProcessAttackPeriod() || attackAmount != item.NumProjectiles) && !byPass)
            {
                Program.Manager.TryDisconnect(player.Client, Client.DisconnectReason.DEXTERITY_HACK_MOD);
                return;
            }
        }

        private double ProcessAttackPeriod() =>
            1 / Math.Round((1 / player.StatsManager.GetAttackFrequency())
            * (1 / item.RateOfFire), 4);
    }
}