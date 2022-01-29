using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Audio;

namespace GameJaaj.Source.Data {
    public class SFX : ICollection<SoundEffect> {
        private readonly List<SoundEffect> _sounds = new List<SoundEffect>();
        private readonly Random random = new Random();

        public int Count => ((ICollection<SoundEffect>)_sounds).Count;

        public bool IsReadOnly => ((ICollection<SoundEffect>)_sounds).IsReadOnly;

        public SFX()
        {
        }

        public void Add(SoundEffect item)
        {
            ((ICollection<SoundEffect>)_sounds).Add(item);
        }

        public void Clear()
        {
            ((ICollection<SoundEffect>)_sounds).Clear();
        }

        public bool Contains(SoundEffect item)
        {
            return ((ICollection<SoundEffect>)_sounds).Contains(item);
        }

        public void CopyTo(SoundEffect[] array, int arrayIndex)
        {
            ((ICollection<SoundEffect>)_sounds).CopyTo(array, arrayIndex);
        }

        public IEnumerator<SoundEffect> GetEnumerator()
        {
            return ((IEnumerable<SoundEffect>)_sounds).GetEnumerator();
        }

        public bool Remove(SoundEffect item)
        {
            return ((ICollection<SoundEffect>)_sounds).Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)_sounds).GetEnumerator();
        }

        public void RandomPlay() {
            int randomInt = random.Next(_sounds.Count);

            _sounds[randomInt].Play();
        }
    }
}