using System;
using System.Collections.Generic;
using System.Linq;
using Assets.HeroEditor4D.Common.CommonScripts;
using HeroEditor4D.Common;
using HeroEditor4D.Common.Enums;
using UnityEngine;

namespace Assets.HeroEditor4D.Common.CharacterScripts
{
	/// <summary>
	/// Controls 4 characters (for each direction).
	/// </summary>
	public class Character4D : Character4DBase
    {
		public SpriteCollection SpriteCollection => Parts[0].SpriteCollection;
        public AnimationManager AnimationManager;
        public LayerManager LayerManager;

        public void OnValidate()
		{
			Parts = new List<CharacterBase> { Left, Right };
			Parts.ForEach(i => i.BodyRenderers.ForEach(j => j.color = BodyColor));
			Parts.ForEach(i => i.EarsRenderers.ForEach(j => j.color = BodyColor));
		}

        public void Start()
        {
            var stateHandler = Animator.GetBehaviours<StateHandler>().SingleOrDefault(i => i.Name == "Death");

            if (stateHandler)
            {
                stateHandler.StateExit.AddListener(() => SetExpression("Default"));
            }

            Animator.keepAnimatorControllerStateOnDisable = true;
        }

		public override void Initialize()
		{
			Parts.ForEach(i => i.Initialize());
		}

		public override void CopyFrom(Character4DBase character)
		{
			for (var i = 0; i < Parts.Count; i++)
			{
				Parts[i].CopyFrom(character.Parts[i]);
			}
		}

		public override string ToJson()
		{
		    return Front.ToJson();
		}

		public override void FromJson(string json, bool silent)
		{
		    Parts.ForEach(i => i.LoadFromJson(json, silent));
		}

        public Vector2 Direction { get; private set; }

		public void SetDirection(Vector2 direction)
		{
            if (Direction == direction) return;

			Direction = direction;

            if (Direction == Vector2.zero)
            {
                Parts.ForEach(i => i.SetActive(true));
                Shadows.ForEach(i => i.SetActive(true));
                Parts[0].transform.localPosition = Shadows[2].transform.localPosition = new Vector3(-1.5f, 0);
                Parts[1].transform.localPosition = Shadows[3].transform.localPosition = new Vector3(1.5f, 0);

                return;
            }

			Parts.ForEach(i => i.transform.localPosition = Vector3.zero);
			Shadows.ForEach(i => i.transform.localPosition = Vector3.zero);

			int index;

			if (direction == Vector2.left)
			{
				index = 0;
			}
			else if (direction == Vector2.right)
			{
				index = 1;
			}
			else
			{
				throw new NotSupportedException();
			}

			for (var i = 0; i < Parts.Count; i++)
			{
                Parts[i].SetActive(i == index);
				Shadows[i].SetActive(i == index);
			}
		}

    }
}