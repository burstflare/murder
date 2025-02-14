﻿using Murder.Components;
using Murder.Services;
using Bang;
using Bang.Entities;
using Bang.Systems;
using System.Collections.Immutable;
using Murder.Core.Sounds;

namespace Murder.Systems
{
    [Watch(typeof(MusicComponent))]
    internal class MusicSystem : IReactiveSystem
    {
        public void OnAdded(World world, ImmutableArray<Entity> entities)
        {
            StopAndPlay(entities);
        }

        public void OnModified(World world, ImmutableArray<Entity> entities)
        {
            StopAndPlay(entities);
        }

        public void OnRemoved(World world, ImmutableArray<Entity> entities)
        { }

        private void StopAndPlay(ImmutableArray<Entity> entities)
        {
            foreach (Entity e in entities)
            {
                _ = SoundServices.Play(e.GetMusic().Id, SoundProperties.Persist | SoundProperties.SkipIfAlreadyPlaying | SoundProperties.StopOtherMusic);
            }
        }
    }
}
