﻿using System;
using UnityEngine;

namespace Lair
{
    public class BossMarkerController : MonoBehaviour
    {
        public LairManager lairManager;

        private void OnTriggerEnter(Collider other)
        {
            lairManager.StartCoroutine("BossBattle");
        }
    }
}