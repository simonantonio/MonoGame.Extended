﻿using System;
using MonoGame.Extended.Animations.SpriteSheets;
using MonoGame.Extended.Sprites;

namespace MonoGame.Extended.Animations
{
    public class AnimatedSprite : Sprite, IUpdate
    {
        private readonly SpriteSheetAnimationFactory _animationFactory;
        private SpriteSheetAnimation _currentAnimation;

        public AnimatedSprite(SpriteSheetAnimationFactory animationFactory, string playAnimation = null)
            : base(animationFactory.Frames[0])
        {
            _animationFactory = animationFactory;

            if (playAnimation != null)
                Play(playAnimation);
        }

        public SpriteSheetAnimation Play(string name, Action onCompleted = null)
        {
            if (_currentAnimation == null || _currentAnimation.IsComplete || _currentAnimation.Name != name)
            {
                _currentAnimation = _animationFactory.Create(name);

                if(_currentAnimation != null)
                    _currentAnimation.OnCompleted = onCompleted;
            }

            return _currentAnimation;
        }

        public void Update(float elapsedSeconds)
        {
            if (_currentAnimation != null && !_currentAnimation.IsComplete)
            {
                _currentAnimation.Update(elapsedSeconds);
                TextureRegion = _currentAnimation.CurrentFrame;
            }
        }

    }
}