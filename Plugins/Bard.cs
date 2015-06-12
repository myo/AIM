﻿using System;
using AIM.Util;
using LeagueSharp;
using LeagueSharp.Common;

namespace AIM.Plugins
{
    class Bard : PluginBase
    {
        public Bard()
        {
            Q = new Spell(SpellSlot.Q, 950);
            R = new Spell(SpellSlot.R, 3400);

            Q.SetSkillshot(0.25f, 60, 1600, false, SkillshotType.SkillshotLine);
            R.SetSkillshot(0.5f, 325, 2100, false, SkillshotType.SkillshotCircle);
        }

        public override void OnUpdate(EventArgs args)
        {
            var targetR = TargetSelector.GetTarget(2500, TargetSelector.DamageType.Magical);
            if (ComboMode)
            {
                if (Q.CastCheck(Target, "ComboQ"))
                {
                    Q.Cast(Target);
                }
            }

            if (HarassMode)
            {
                if (W.CastCheck(Target, "HarassQ"))
                {
                    Q.Cast(Target);
                }
            }
        }

        public override void OnPossibleToInterrupt(Obj_AI_Base unit, InterruptableSpell spell)
        {
            if (spell.DangerLevel < InterruptableDangerLevel.High || unit.IsAlly)
            {
                return;
            }

            if (R.CastCheck(unit, "Interrupt.R"))
            {
                R.Cast(unit);
            }
        }

        public override void ComboMenu(Menu config)
        {
            config.AddBool("ComboQ", "Use Q", true);
        }

        public override void HarassMenu(Menu config)
        {
            config.AddBool("HarassQ", "Use Q", true);
        }

        public override void InterruptMenu(Menu config)
        {
            config.AddBool("Interrupt.R", "Use R to Interrupt Spells", true);
        }
    }
}
